# Открытие исходного файла для чтения
with open("input_file.txt", "r") as infile:
    lines = infile.readlines()

# Открытие нового файла для записи
with open("output_file.txt", "w") as outfile:
    for line in lines:
        # Преобразование каждой строки
        if "INSERT INTO Balance" in line:
            # Извлечение значений из строки
            parts = line.split("VALUES ")[1].strip("();\n").split(", ")
            id_gru = parts[0]
            id_product = parts[1].split(",")[0]
            amount = parts[1].split(",")[1]
            '''
            Тест переменных
            print(parts)
            print(f'id_gru = {id_gru}')
            print(f'id_product = {id_product}')
            print(f'amount = {amount}')
            # date_balance = parts[3].strip("'")
            '''
            # Создание новой строки
            # new_date = datebalance[:8] + str(int(datebalance[8:10]) - 1).zfill(2) + "'"
            new_date = '2021-10-31'
            new_line = f"update Balance set AMOUNT = {amount} where IDGRU = {id_gru} and IDPRODUCT = {id_product} and DATEBALANCE = '{new_date}';\n"

            # Запись новой строки в файл
            outfile.write(new_line)
