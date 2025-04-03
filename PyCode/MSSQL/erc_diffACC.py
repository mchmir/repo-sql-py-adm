def extract_accounts(filepath):
    accounts = set()
    with open(filepath, encoding='utf-8') as f:
        next(f)  # Пропустить заголовок
        for line in f:
            if line.strip():  # Игнорируем пустые строки
                account = line.split('|', 1)[0][:7]
                accounts.add(account)
    return accounts

file1 = r"D:\2025\FAILEIRC_GORGAZ_032025_56786.csv"
file2 = r"D:\2025\EIRC_GORGAZ_032025_56781.csv"

accounts_file1 = extract_accounts(file1)
accounts_file2 = extract_accounts(file2)

missing_accounts = accounts_file1 - accounts_file2

print("Лицевые счета, отсутствующие во втором файле:")
for acc in sorted(missing_accounts):
    print(acc)
