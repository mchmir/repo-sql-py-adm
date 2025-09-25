#!/bin/bash

# ==============================================================================
# EN: WHAT THIS SCRIPT DOES AND WHY
# ------------------------------------------------------------------------------
# Purpose
# - For three databases (wd_926_db / wdn_926_db / wds_926_db) it captures query
#   performance and produces data snapshots for:
#       SELECT * FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V
#       WHERE DATUMCAS >= DATE '2025-05-01';
#
# Outputs (written to $OUTDIR):
# - ${tag}_explain_cold.json — "cold" plan via EXPLAIN (ANALYZE, BUFFERS, JSON)
# - ${tag}_explain_hot.json  — "hot" plan (second run of the same query)
# - ${tag}_count.txt         — COUNT(*) of the result set (sanity check)
# - ${tag}_snapshot.csv      — data snapshot with two columns:
#       row_hash  — SHA-256 hash of row_to_json(row), for cross-DB comparisons
#       payload   — the exact row JSON (for inspecting differences)
#
# Supplying the query
# - By default, the script uses its built-in query.
# - Or pass the SQL inline:
#     ./run_bench.sh "SELECT * FROM schema.view WHERE col >= DATE '2025-05-01';"
# - Or from a file:
#     ./run_bench.sh -f /path/to/query.sql
# The script removes the trailing semicolon for subquery contexts (COUNT/snapshot)
# and appends it where a full statement is required (EXPLAIN).
#
# Outputs (written to $OUTDIR; FIXED NAMES — previous runs are overwritten):
# - wd_explain_cold.json   wdn_explain_cold.json   wds_explain_cold.json
# - wd_explain_hot.json    wdn_explain_hot.json    wds_explain_hot.json
# - wd_count.txt           wdn_count.txt           wds_count.txt
# - wd_snapshot.csv        wdn_snapshot.csv        wds_snapshot.csv
#
# File contents
# - *_explain_cold.json / *_hot.json — clean EXPLAIN JSON (psql settings:
#   \timing off, \pset tuples_only on, \pset footer off, \pset format unaligned).
# - *_count.txt — COUNT(*) of the result set.
# - *_snapshot.csv — two columns:
#       row_hash  — SHA-256 of row_to_json(row) for set comparison;
#       payload   — the exact row as JSON (for detailed diff).
#
# Why cold vs hot
# - We issue DISCARD PLANS/SEQUENCES and run the query twice.
#   The first warms up caches & the plan; the second shows “hot” numbers.
#
# Requirements
# - Server must have the pgcrypto extension (digest()).
# - Role must be allowed to run: admin_core.X_SESSIONPREPARE(),
#   X_SESSIONLOGIN(...), EXPLAIN, and SELECT on referenced objects.
#
# Important notes
# - \copy is a psql meta-command (do NOT end the line with a semicolon).
# - SQL is injected as text via psql variables; only pass trusted statements.
# - Cold/Hot refer to PostgreSQL shared buffers; OS cache isn’t cleared,
#   which is adequate for comparing versions/plans.
#
# Where to get the final summary
# - Use analyzer (e.g., septim-bench-report.sh) to parse JSON/COUNT/CSV and print
#   cold_ms, hot_ms, rows_plan, rows_real, buffers, and data comparisons.
# ==============================================================================

set -euo pipefail

SCRIPT_NAME="$(basename "$0")"
SCRIPT_VERSION="v2 (built on 2025-09-25)"

echo "== ${SCRIPT_NAME} ${SCRIPT_VERSION} =="

if [[ "${1:-}" == "--version" || "${1:-}" == "-V" ]]; then
  echo "${SCRIPT_NAME} v${SCRIPT_VERSION}"
  exit 0
fi

OUTDIR="/home/mikhailchm/septim-query-bench/report"
mkdir -p "$OUTDIR"

# --- argument parsing: query by string or from file (-f) ---
QUERY_RAW=""
if [[ $# -eq 0 ]]; then
  # default: request
  QUERY_RAW="SELECT * FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V WHERE DATUMCAS >= DATE '2025-05-01';"
elif [[ "$1" == "-f" ]]; then
  [[ -f "${2:-}" ]] || { echo "No such file: $2" >&2; exit 1; }
  QUERY_RAW="$(cat "$2")"
else
  # everything that was transferred — we consider SQL (with spaces)
  QUERY_RAW="$*"
fi

# normalization: remove the final ';' for subqueries and add it where needed in its entirety
QUERY_NOSEMI="$(printf '%s' "$QUERY_RAW" | sed -e 's/[[:space:]]\+$//' -e 's/;[[:space:]]*$//')"
QUERY_SEMI="${QUERY_NOSEMI};"

# list of databases and their short tags for files (file names — WITHOUT tags)
declare -A DBS=(
  [wd]="wd_926_db"
  [wdn]="wdn_926_db"
  [wds]="wds_926_db"
)

for tag in wd wdn wds; do
  db="${DBS[$tag]}"
  f_explain_cold="$OUTDIR/${tag}_explain_cold.json"
  f_explain_hot="$OUTDIR/${tag}_explain_hot.json"
  f_count="$OUTDIR/${tag}_count.txt"
  f_snapshot="$OUTDIR/${tag}_snapshot.csv"

  echo "== $db ($tag) =="

  # -X to avoid picking up .psqlrc
  psql -X "dbname=$db" \
       -v ON_ERROR_STOP=1 \
       -v q_semi="$QUERY_SEMI" \
       -v q_nosemi="$QUERY_NOSEMI" <<SQL
\timing off
\pset pager off
\pset footer off
\pset tuples_only on
\x off
\pset format unaligned

create extension if not exists pgcrypto;

select admin_core.X_SESSIONPREPARE();
select X_SESSIONLOGIN('admin', 'aswpha', 'asw', a_BEGINSESSION := true);

DISCARD PLANS;
DISCARD SEQUENCES;

\o $f_explain_cold
EXPLAIN (ANALYZE, BUFFERS, FORMAT JSON)
:q_semi
\o

\o $f_explain_hot
EXPLAIN (ANALYZE, BUFFERS, FORMAT JSON)
:q_semi
\o

\o $f_count
SELECT COUNT(*) AS rows_cnt
FROM (
  :q_nosemi
) AS t;
\o

\pset tuples_only off
\pset footer on
\timing on

-- snapshot: hash + payload (will overwrite the file)
COPY (
  SELECT
    encode(digest(row_to_json(t)::text,'sha256'),'hex') AS row_hash,
    row_to_json(t)                                      AS payload
  FROM (
    :q_nosemi
  ) AS t
) TO STDOUT WITH (FORMAT CSV, HEADER true)\g $f_snapshot
SQL

done

echo "Done. Files in $OUTDIR:"
ls -lh "$OUTDIR"
