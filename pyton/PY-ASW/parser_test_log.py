"""
Данный Скрипт создавался при помощи Gemeni, необходимо доработать
git diff --shortstat
 191 files changed, 1164 insertions(+), 190 deletions(-)

Скрипт созданный при помощи GPT - parser-test-log_gpt.py  боле чище и точнее
git diff --shortstat
 192 files changed, 1050 insertions(+), 191 deletions(-)
"""


import os
import re


def process_log_files():
    """
    Обрабатывает лог-файлы, находит ошибки и вставляет текст
    в исходные файлы согласно правилам, избегая дубликатов.
    """
    log_folder = r'D:\Temp\test LOG'
    base_source_path = r'C:\septim.git\Septim4.limited\Septim\PgSQL\Proc'
    text_to_insert = 'testprocex FUNCTXCACHE_Clear () \\'

    # === ИЗМЕНЕНИЕ 1: Создаем множество для отслеживания уже обработанных мест ===
    processed_insertions = set()

    if not os.path.isdir(log_folder):
        print(f"Ошибка: Папка с логами '{log_folder}' не найдена.")
        return

    for log_filename in os.listdir(log_folder):
        if log_filename.endswith(".log"):
            log_filepath = os.path.join(log_folder, log_filename)
            try:
                with open(log_filepath, 'r', encoding='utf-8', errors='ignore') as f:
                    lines = f.readlines()
            except IOError as e:
                print(f"Не удалось прочитать файл: {log_filepath}. Ошибка: {e}")
                continue

            for i, line in enumerate(lines):
                if "does not contain user right check." in line:
                    if i + 2 < len(lines):
                        file_line = lines[i + 1].strip()
                        line_num_line = lines[i + 2].strip()

                        source_file_match = re.search(r'FILE:(.+)', file_line)
                        line_num_match = re.search(r'LINE:(\d+)', line_num_line)

                        if source_file_match and line_num_match:
                            original_filename = source_file_match.group(1).strip()
                            line_number = int(line_num_match.group(1))

                            prefix = "_PgSQL_Proc_"
                            if log_filename.startswith(prefix):
                                temp_name = log_filename[len(prefix):]
                                last_underscore_pos = temp_name.rfind('_')
                                if last_underscore_pos != -1:
                                    subdirectory = temp_name[:last_underscore_pos]
                                    source_file_path = os.path.join(base_source_path, subdirectory, original_filename)

                                    # === ИЗМЕНЕНИЕ 2: Передаем множество в функцию ===
                                    insert_text_into_file(source_file_path, line_number, text_to_insert,
                                                          processed_insertions)
                                else:
                                    print(f"ПРЕДУПРЕЖДЕНИЕ: Не удалось извлечь имя подпапки из {log_filename}")
                            else:
                                print(f"ПРЕДУПРЕЖДЕНИЕ: Нестандартное имя файла лога {log_filename}")


def insert_text_into_file(filepath, line_num, text, processed_locations_set):
    """
    Вставляет текст, избегая повторных вставок в одно и то же место.
    """
    if not os.path.exists(filepath):
        print(f"ПРЕДУПРЕЖДЕНИЕ: Исходный файл не найден: {filepath}")
        return

    try:
        with open(filepath, 'r+', encoding='utf-8-sig', errors='ignore') as f:
            lines = f.readlines()

            start_index = line_num - 1
            if not (0 <= start_index < len(lines)):
                print(f"❌ Ошибка: Исходный номер строки {line_num} выходит за пределы файла {filepath}")
                return

            insertion_index = -1
            for i in range(start_index, -1, -1):
                if lines[i].strip().lower().startswith('test'):
                    insertion_index = i
                    break

            if insertion_index != -1:
                # === ИЗМЕНЕНИЕ 3: Проверяем, не было ли уже вставки в это место ===
                insertion_key = (filepath, insertion_index)
                if insertion_key in processed_locations_set:
                    print(f"INFO: Пропуск дублирующей вставки в файл: {filepath} на строку: {insertion_index + 1}")
                    return

                original_line = lines[insertion_index]
                lines[insertion_index] = text + '\n' + original_line

                f.seek(0)
                f.writelines(lines)
                f.truncate()

                # Запоминаем, что мы успешно выполнили вставку
                processed_locations_set.add(insertion_key)

                actual_line_num = insertion_index + 1
                print(
                    f"✅ Успешно вставлен текст в файл: {filepath} на строку: {actual_line_num} (поиск от строки {line_num})")
            else:
                print(
                    f"⚠️ ПРЕДУПРЕЖДЕНИЕ: В файле {filepath} не найдена строка, начинающаяся с 'test', при поиске вверх от строки {line_num}.")

    except IOError as e:
        print(f"❌ Не удалось записать в файл: {filepath}. Ошибка: {e}")
    except Exception as e:
        print(f"❌ Произошла непредвиденная ошибка при работе с файлом {filepath}: {e}")


if __name__ == "__main__":
    process_log_files()