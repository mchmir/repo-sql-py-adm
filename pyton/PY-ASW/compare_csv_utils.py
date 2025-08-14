import pandas as pd

def are_equal(val1, val2, treat_nan_as_zero=False):
    """
    Compares two values, optionally treating NaN as zero.

    Parameters:
        val1, val2: The values to compare (can be of any type, usually numbers or strings).
        treat_nan_as_zero (bool): If True, NaN is considered equal to zero (0 or 0.0).

    Returns:
        bool: True if the values are considered equal according to the rules, False otherwise.

    Examples:
        are_equal(1, 1) -> True
        are_equal(float('nan'), 0, treat_nan_as_zero=True) -> True
        are_equal(float('nan'), 0) -> False
    """
    try:
        v1 = float(val1)
    except (TypeError, ValueError):
        v1 = val1
    try:
        v2 = float(val2)
    except (TypeError, ValueError):
        v2 = val2

    if treat_nan_as_zero:
        if (pd.isna(v1) and (v2 == 0 or v2 == 0.0)) or (pd.isna(v2) and (v1 == 0 or v1 == 0.0)):
            return True
        if pd.isna(v1) and pd.isna(v2):
            return True
        if v1 == v2:
            return True
        if (v1 == 0 and v2 == 0.0) or (v1 == 0.0 and v2 == 0):
            return True
    else:
        if pd.isna(v1) and pd.isna(v2):
            return True
        if v1 == v2:
            return True
    return False


def find_differences(df1, df2, are_equal_func, treat_nan_as_zero=False):
    """
    Compares two DataFrames by rows and columns.
    Returns a list of differences with details by rows and columns.
    """
    differences = []
    for idx in range(len(df1)):
        row1 = df1.iloc[idx]
        row2 = df2.iloc[idx]
        unequal_cols = [
            col for col in df1.columns
            if not are_equal_func(row1[col], row2[col], treat_nan_as_zero=treat_nan_as_zero)
        ]
        if unequal_cols:
            differences.append({
                'row': idx,
                'columns': unequal_cols,
                'wd_926_db': row1[unequal_cols].to_dict(),
                'wdn_926_db': row2[unequal_cols].to_dict(),
                'full_row_wd_926_db': row1.to_dict(),
                'full_row_wdn_926_db': row2.to_dict()
            })
    return differences
