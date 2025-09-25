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
# Why we record both cold and hot
# - We run DISCARD PLANS/SEQUENCES and execute the query twice. The first run
#   warms up caches and plan; the second (“hot”) reflects warmed performance.
#
# Why EXPLAIN is saved as "clean" JSON
# - psql settings (\timing off, \pset tuples_only on, \pset footer off) ensure
#   no extra lines like "Time: …" or "(1 row)" pollute the JSON files.
#
# Why the snapshot (row_hash, payload)
# - row_hash enables quick set comparisons across databases independent of row
#   order. payload preserves exact JSON for manual diff/drill-down.
#
# Requirements/assumptions
# - pgcrypto must be available on the server (digest()).
# - The role must be allowed to call:
#     admin_core.X_SESSIONPREPARE(), X_SESSIONLOGIN(...),
#     EXPLAIN, and SELECT on the referenced objects.
# - $OUTDIR must exist and be writable.
#
# Important notes
# - \copy is a psql meta-command (do NOT end the line with a semicolon).
# - The output file path is expanded by the shell (heredoc is unquoted here).
# - Cold/Hot refer to PostgreSQL shared buffers; OS cache is not explicitly dropped,
#   which is fine for comparative benchmarking of plans/versions.
#
# How to adapt
# - Change the date predicate / view name in both EXPLAIN queries and the \copy.
# - Remove/replace X_SESSION... if you authenticate differently.
# - Adjust OUTDIR and the database map (tag→dbname) at the top.
# ==============================================================================

set -euo pipefail

OUTDIR="/home/mikhailchm/1115"
mkdir -p "$OUTDIR"

# список баз и их короткие теги для файлов
declare -A DBS=(
  [wd]="wd_926_db"
  [wdn]="wdn_926_db"
  [wds]="wds_926_db"
)

for tag in "${!DBS[@]}"; do
  db="${DBS[$tag]}"
  f_explain_cold="$OUTDIR/${tag}_explain_cold.json"
  f_explain_hot="$OUTDIR/${tag}_explain_hot.json"
  f_count="$OUTDIR/${tag}_count.txt"
  f_snapshot="$OUTDIR/${tag}_snapshot.csv"

  echo "== $db ($tag) =="

  # -X чтобы не подхватывать .psqlrc (и избежать стартовых ошибок)
  psql -X "dbname=$db" -v ON_ERROR_STOP=1 <<SQL
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
SELECT *
FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V
WHERE DATUMCAS >= DATE '2025-05-01';
\o

\o $f_explain_hot
EXPLAIN (ANALYZE, BUFFERS, FORMAT JSON)
SELECT *
FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V
WHERE DATUMCAS >= DATE '2025-05-01';
\o

\o $f_count
SELECT COUNT(*) AS rows_cnt
FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V
WHERE DATUMCAS >= DATE '2025-05-01';
\o

\pset tuples_only off
\pset footer on
\timing on

create extension if not exists pgcrypto;

\set snapfile '$f_snapshot'
\copy (SELECT encode(digest(row_to_json(t)::text,'sha256'),'hex') AS row_hash, row_to_json(t) AS payload FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V t WHERE datumcas >= DATE '2025-05-01') TO $f_snapshot CSV HEADER
SQL

done

echo "Готово. Файлы в $OUTDIR:"
ls -lh "$OUTDIR"
