#!/bin/bash

# =====================[ REPORT LEGEND ]=====================
# TIMING & ROWS
#  cold_ms    — "cold" run: elapsed time of the first EXPLAIN (ANALYZE) in this session
#               after DISCARD PLANS/SEQUENCES. Milliseconds.
#  hot_ms     — "hot" run: elapsed time of the second run of the same query.
#               Usually ≤ cold_ms thanks to warmed caches.
#  rows_plan  — actual row count at the root plan node (.Plan."Actual Rows")
#               taken from the EXPLAIN JSON (hot run).
#  rows_real  — COUNT(*) for the same predicate. Should match rows_plan unless
#               there’s a LIMIT or extra filtering elsewhere.
#
# BUFFERS (sum over all plan nodes, hot run; 1 block = 8 KB)
#  shared_hit     — blocks served from PostgreSQL shared buffer cache (no disk I/O).
#  shared_read    — blocks read from disk (lower is better).
#  temp_read      — blocks read from temporary files (hash/sort spills).
#  temp_written   — blocks written to temporary files (spills).
#  Convert: MB = blocks * 8 / 1024;  GB = MB / 1024.
#
# DATA COMPARISON (between two snapshots, here: WDN ↔ WDS)
#  wdn_rows / wds_rows — set sizes (number of unique row hashes).
#  intersection        — |WDN ∩ WDS|, rows present in both sets.
#  wdn-wds             — |WDN \ WDS|, rows only in WDN.
#  wds-wdn             — |WDS \ WDN|, rows only in WDS.
#  jaccard             — Jaccard index = |A ∩ B| / |A ∪ B|
#                        = intersection / (wdn_rows + wds_rows − intersection).
#                        Range [0..1]. 1.0 = sets are identical; closer to 0 = large differences.
#
# ===========================================================
set -euo pipefail

SCRIPT_NAME="$(basename "$0")"
SCRIPT_VERSION="v1.0 (built on 2025-09-25)"

echo "== ${SCRIPT_NAME} ${SCRIPT_VERSION} =="

if [[ "${1:-}" == "--version" || "${1:-}" == "-V" ]]; then
  echo "${SCRIPT_NAME} v${SCRIPT_VERSION}"
  exit 0
fi

DIR="/home/mikhailchm/septim-query-bench/report"
cd "$DIR"

need() { for f in "$@"; do [[ -f "$f" ]] || { echo "Нет файла: $f" >&2; exit 1; }; done; }

need \
  wd_explain_cold.json  wd_explain_hot.json  wd_count.txt  wd_snapshot.csv \
  wdn_explain_cold.json wdn_explain_hot.json wdn_count.txt wdn_snapshot.csv \
  wds_explain_cold.json wds_explain_hot.json wds_count.txt wds_snapshot.csv

# --- Get a CLEAN JSON array from the tabular EXPLAIN output of psql ---
clean_explain_json() {  # $1 = path to *_explain_*.json
  awk '
    BEGIN{start=0; depth=0}
    {
      line=$0
      sub(/[[:space:]]+\+$/,"", line)                    # remove the tail " +"
      if (!start) {                                       # waiting for the first line с '['
        if (line ~ /^\[/) { start=1 }
        else { next }
      }
      if (line ~ /^Time: / || line ~ /^Timing is /) next  # выбрасываем таймерные строки
      print line
      # balance the brackets so that they end exactly on the closing “]” of the top level
      tmp=line; nopen=gsub(/\[/,"",tmp)
      tmp=line; nclose=gsub(/\]/,"",tmp)
      depth += (nopen - nclose)
      if (depth==0) exit
    }
  ' "$1"
}

# --- Calculate metrics from EXPLAIN JSON (stdin = plain JSON) ---
explain_stats() {
  jq -r '
    def buf:
      (.[0].Plan
       | [.. | objects
           | select(has("Shared Hit Blocks")
                 or has("Shared Read Blocks")
                 or has("Temp Read Blocks")
                 or has("Temp Written Blocks"))]
       | reduce .[] as $n (
           {"shared_hit":0,"shared_read":0,"temp_read":0,"temp_written":0};
           .shared_hit   += ($n["Shared Hit Blocks"]   // 0) |
           .shared_read  += ($n["Shared Read Blocks"]  // 0) |
           .temp_read    += ($n["Temp Read Blocks"]    // 0) |
           .temp_written += ($n["Temp Written Blocks"] // 0)
         )
      );
    {
      time_ms:     (.[0]["Execution Time"] // 0),
      planning_ms: (.[0]["Planning Time"] // 0),
      actual_rows: (.[0].Plan["Actual Rows"] // 0),
      buffers:     (buf)
    }'
}

# --- Reliably retrieve COUNT(*) (the first row containing only a number) ---
count_value() { awk '/^[[:space:]]*[0-9]+[[:space:]]*$/{print $1; exit}' "$1"; }

# --- Hashes of strings from CSV snapshots ---
load_hashes() { tail -n +2 "$1" | cut -d',' -f1; }

# === Metrics for three databases ===
wd_cold=$(clean_explain_json wd_explain_cold.json | explain_stats 2>/dev/null || echo '{"time_ms":0,"actual_rows":0,"buffers":{"shared_hit":0,"shared_read":0,"temp_read":0,"temp_written":0}}')
wd_hot=$( clean_explain_json wd_explain_hot.json  | explain_stats 2>/dev/null || echo '{"time_ms":0,"actual_rows":0,"buffers":{"shared_hit":0,"shared_read":0,"temp_read":0,"temp_written":0}}')
wdn_cold=$(clean_explain_json wdn_explain_cold.json | explain_stats 2>/dev/null || echo '{"time_ms":0,"actual_rows":0,"buffers":{"shared_hit":0,"shared_read":0,"temp_read":0,"temp_written":0}}')
wdn_hot=$( clean_explain_json wdn_explain_hot.json  | explain_stats 2>/dev/null || echo '{"time_ms":0,"actual_rows":0,"buffers":{"shared_hit":0,"shared_read":0,"temp_read":0,"temp_written":0}}')
wds_cold=$(clean_explain_json wds_explain_cold.json | explain_stats 2>/dev/null || echo '{"time_ms":0,"actual_rows":0,"buffers":{"shared_hit":0,"shared_read":0,"temp_read":0,"temp_written":0}}')
wds_hot=$( clean_explain_json wds_explain_hot.json  | explain_stats 2>/dev/null || echo '{"time_ms":0,"actual_rows":0,"buffers":{"shared_hit":0,"shared_read":0,"temp_read":0,"temp_written":0}}')

wd_cold_ms=$(echo "$wd_cold"   | jq -r '.time_ms');  wd_hot_ms=$(echo "$wd_hot"   | jq -r '.time_ms')
wdn_cold_ms=$(echo "$wdn_cold" | jq -r '.time_ms');  wdn_hot_ms=$(echo "$wdn_hot" | jq -r '.time_ms')
wds_cold_ms=$(echo "$wds_cold" | jq -r '.time_ms');  wds_hot_ms=$(echo "$wds_hot" | jq -r '.time_ms')

wd_rows_plan=$(echo "$wd_hot"   | jq -r '.actual_rows')
wdn_rows_plan=$(echo "$wdn_hot" | jq -r '.actual_rows')
wds_rows_plan=$(echo "$wds_hot" | jq -r '.actual_rows')

wd_rows_real=$(count_value wd_count.txt)
wdn_rows_real=$(count_value wdn_count.txt)
wds_rows_real=$(count_value wds_count.txt)

wd_hot_buf=$( echo "$wd_hot"  | jq -c '.buffers')
wdn_hot_buf=$(echo "$wdn_hot" | jq -c '.buffers')
wds_hot_buf=$(echo "$wds_hot" | jq -c '.buffers')

# === Data comparison: only wdn ↔ wds ===
LC_ALL=C sort -u <(load_hashes wdn_snapshot.csv) > wdn.set
LC_ALL=C sort -u <(load_hashes wds_snapshot.csv) > wds.set

inter() { LC_ALL=C comm -12 "$1" "$2" | wc -l; }
diffa() { LC_ALL=C comm -23 "$1" "$2" | wc -l; }
diffb() { LC_ALL=C comm -13 "$1" "$2" | wc -l; }

wdn_n=$(wc -l < wdn.set)
wds_n=$(wc -l < wds.set)

wdn_wds_inter=$(inter wdn.set wds.set)
wdn_minus_wds=$(diffa wdn.set wds.set)
wds_minus_wdn=$(diffb wdn.set wds.set)

if (( wdn_n + wds_n - wdn_wds_inter == 0 )); then
  jaccard=1
else
  jaccard=$(awk -v i="$wdn_wds_inter" -v a="$wdn_n" -v b="$wds_n" 'BEGIN{printf "%.6f\n", i/(a+b-i)}')
fi

# --- Examples of differences (up to 50) ---
awk -F',' 'NR>1{h=$1; rest=substr($0,index($0,",")+1); print h "\t" rest}' wdn_snapshot.csv > wdn_pairs.tsv
awk -F',' 'NR>1{h=$1; rest=substr($0,index($0,",")+1); print h "\t" rest}' wds_snapshot.csv > wds_pairs.tsv
head -n 50 <(LC_ALL=C comm -23 wdn.set wds.set | sed "s/$/\t/") > wdn_only.pat
head -n 50 <(LC_ALL=C comm -13 wdn.set wds.set | sed "s/$/\t/") > wds_only.pat
grep -F -f wdn_only.pat wdn_pairs.tsv | sed 's/\t/,/' > wdn_minus_wds_sample.csv || true
grep -F -f wds_only.pat wds_pairs.tsv | sed 's/\t/,/' > wds_minus_wdn_sample.csv || true

# --- Summary ---
echo "=== TIME AND ROWS (ms/rows) ==="
printf "%-5s %12s %12s %12s %12s\n" "DB" "cold_ms" "hot_ms" "rows_plan" "rows_real"
printf "%-5s %12.3f %12.3f %12d %12d\n" "wd"  "$wd_cold_ms"  "$wd_hot_ms"  "$wd_rows_plan"  "$wd_rows_real"
printf "%-5s %12.3f %12.3f %12d %12d\n" "wdn" "$wdn_cold_ms" "$wdn_hot_ms" "$wdn_rows_plan" "$wdn_rows_real"
printf "%-5s %12.3f %12.3f %12d %12d\n" "wds" "$wds_cold_ms" "$wds_hot_ms" "$wds_rows_plan" "$wds_rows_real"

echo
echo "=== HOT BUFFERS (planned amount) ==="
printf "%-5s %14s %14s %14s %14s\n" "DB" "shared_hit" "shared_read" "temp_read" "temp_written"
printf "%-5s %14d %14d %14d %14d\n" "wd"  \
  "$(echo "$wd_hot_buf"  | jq -r '.shared_hit')" \
  "$(echo "$wd_hot_buf"  | jq -r '.shared_read')" \
  "$(echo "$wd_hot_buf"  | jq -r '.temp_read')"  \
  "$(echo "$wd_hot_buf"  | jq -r '.temp_written')"
printf "%-5s %14d %14d %14d %14d\n" "wdn" \
  "$(echo "$wdn_hot_buf" | jq -r '.shared_hit')" \
  "$(echo "$wdn_hot_buf" | jq -r '.shared_read')" \
  "$(echo "$wdn_hot_buf" | jq -r '.temp_read')"  \
  "$(echo "$wdn_hot_buf" | jq -r '.temp_written')"
printf "%-5s %14d %14d %14d %14d\n" "wds" \
  "$(echo "$wds_hot_buf" | jq -r '.shared_hit')" \
  "$(echo "$wds_hot_buf" | jq -r '.shared_read')" \
  "$(echo "$wds_hot_buf" | jq -r '.temp_read')"  \
  "$(echo "$wds_hot_buf" | jq -r '.temp_written')"

echo
echo "=== DATA COMPARISON: wdn ↔ wds ==="
printf "wdn_rows=%d, wds_rows=%d, intersection=%d, wdn-wds=%d, wds-wdn=%d, jaccard=%s\n" \
  "$wdn_n" "$wds_n" "$wdn_wds_inter" "$wdn_minus_wds" "$wds_minus_wdn" "$jaccard"
echo "Examples of discrepancies: wdn_minus_wds_sample.csv, wds_minus_wdn_sample.csv (up to 50 lines each)."

# --- JSON report (reliable build) ---
safe_json() { jq -c '.' <<<"$1" 2>/dev/null || echo '{"shared_hit":0,"shared_read":0,"temp_read":0,"temp_written":0}'; }
wd_buf_json=$(safe_json "$wd_hot_buf");  wdn_buf_json=$(safe_json "$wdn_hot_buf");  wds_buf_json=$(safe_json "$wds_hot_buf")

jq -n \
  --argjson wd   "{\"cold_ms\":$wd_cold_ms,\"hot_ms\":$wd_hot_ms,\"rows_plan\":$wd_rows_plan,\"rows_real\":$wd_rows_real,\"hot_buffers\":$wd_buf_json}" \
  --argjson wdn  "{\"cold_ms\":$wdn_cold_ms,\"hot_ms\":$wdn_hot_ms,\"rows_plan\":$wdn_rows_plan,\"rows_real\":$wdn_rows_real,\"hot_buffers\":$wdn_buf_json}" \
  --argjson wds  "{\"cold_ms\":$wds_cold_ms,\"hot_ms\":$wds_hot_ms,\"rows_plan\":$wds_rows_plan,\"rows_real\":$wds_rows_real,\"hot_buffers\":$wds_buf_json}" \
  --argjson cmp  "{\"wdn_rows\":$wdn_n,\"wds_rows\":$wds_n,\"intersection\":$wdn_wds_inter,\"wdn_minus_wds\":$wdn_minus_wds,\"wds_minus_wdn\":$wds_minus_wdn,\"jaccard\":$jaccard}" \
  '$ARGS.named' \
  > report_summary.json

echo "Done: report_summary.json, *.set, *_sample.csv в $DIR"


# === Snapshot then archive the whole /home/mikhailchm/septim-query-bench/report ===
LABEL="${1:-report}"
TS="$(date +'%Y%m%d_%H%M%S')"
SRC="/home/mikhailchm/septim-query-bench/report"
OUTDIR="/home/mikhailchm/septim-query-bench"
OUTBASE="${OUTDIR}/${LABEL}_${TS}"

[[ -d "$SRC" ]] || { echo "Folder not found: $SRC" >&2; exit 1; }

# 1) Create a stable snapshot in a temp dir (handles live changes)
SNAPROOT="$(mktemp -d)"
trap 'rm -rf "$SNAPROOT"' EXIT
SNAP="${SNAPROOT}/report"
mkdir -p "$SNAP"

if command -v rsync >/dev/null 2>&1; then
  # rsync keeps perms/times/hardlinks and can do --delete to keep consistency
  rsync -a --delete "${SRC}/" "${SNAP}/"
else
  # fallback without rsync
  cp -a "${SRC}/." "${SNAP}/"
fi

# 2) Pack the snapshot (prefer tar; fallback to cpio+gzip)
if command -v tar >/dev/null 2>&1; then
  # write archive next to OUTDIR (not inside SRC), with relative paths
  tar -C "$SNAPROOT" -czf "${OUTBASE}.tar.gz" "report"
  echo "Archive created: ${OUTBASE}.tar.gz"
else
  ( cd "$SNAPROOT" \
    && find "report" -print0 | cpio --null -ov -H newc | gzip > "${OUTBASE}.cpio.gz" )
  echo "Archive created: ${OUTBASE}.cpio.gz"
fi
