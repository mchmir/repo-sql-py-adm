import pandas as pd
from compare_csv_utils import are_equal, find_differences


TREAT_NAN_AS_ZERO = True  # <-- This is where I switch the comparison mode!

file1 = 'wd_926_db.csv'
file2 = 'wdn_926_db.csv'

# Pandas will read the entire file before defining the type low_memory=False
df1 = pd.read_csv(file1, low_memory=False)
df2 = pd.read_csv(file2, low_memory=False)

# df1 = pd.read_csv(file1)
# df2 = pd.read_csv(file2)

if list(df1.columns) != list(df2.columns):
    print('⚠️ Difference in the names or order of the columns!')
    print('wd_926_db.csv:', list(df1.columns))
    print('wdn_926_db.csv:', list(df2.columns))
    exit(1)

df1_sorted = df1.sort_values(by=list(df1.columns)).reset_index(drop=True)
df2_sorted = df2.sort_values(by=list(df2.columns)).reset_index(drop=True)

if len(df1_sorted) != len(df2_sorted):
    print(f'❌ The number of rows differs: {len(df1_sorted)} vs {len(df2_sorted)}')
    only_in_1 = pd.concat([df1_sorted, df2_sorted]).drop_duplicates(keep=False)
    only_in_2 = pd.concat([df2_sorted, df1_sorted]).drop_duplicates(keep=False)
    if not only_in_1.empty:
        print('\n⚠️ Rows only in wd_926_db.csv (not in the second file):')
        print(only_in_1)
    if not only_in_2.empty:
        print('\n⚠️ Rows only in wdn_926_db.csv (not in the first file):')
        print(only_in_2)
    print('\n-------\nComparison by rows is not performed due to the different number of rows!')
    exit(1)

differences = find_differences(
    df1_sorted,
    df2_sorted,
    are_equal_func=are_equal,
    treat_nan_as_zero=TREAT_NAN_AS_ZERO
)

if not differences:
    if TREAT_NAN_AS_ZERO:
        print('✅ The tables are identical given nan == 0 (or 0.0).')
    else:
        print('✅ The tables are identical (comparison is strict, nan ≠ 0).')
else:
    print(f'❌ Detected {len(differences)} differences at matching string positions:\n')
    for diff in differences:
        print(f'-------\nRow #{diff["row"] + 1}:')
        for col in diff['columns']:
            print(f'  Column: {col}')
            print(f'    wd_926_db:    {diff["wd_926_db"][col]!r}')
            print(f'    wdn_926_db:   {diff["wdn_926_db"][col]!r}')
        print('  >>> Full row from wd_926_db:')
        print('     ', diff["full_row_wd_926_db"])
        print('  >>> Full row from wdn_926_db:')
        print('     ', diff["full_row_wdn_926_db"])
    print('\n-------\nDone!')
