import shutil
from os import listdir
from os.path import isdir, join
from datetime import timedelta, datetime
import os.path
 
# Директория с бэкапами
directory = r'//192.168.0.26/backup/1C-backup/'
 
# Время хранения бэкапов в днях
days = timedelta(days=60)
 
# Получаем содержимое директории
only_directory = [f for f in listdir(directory) if isdir(join(directory, f))]
 
# Пробегаемся в цикле по директории
for folder in only_directory:
    p = [x for x in listdir(directory + folder)]
    for path in p:
        # Определяем возраст папки
        how_long_ago_creation_date = datetime.now()-datetime.fromtimestamp(os.path.getctime(directory+folder+'/'+path))
        print(path+" created "+str(how_long_ago_creation_date)+" ago")
         
            # Если дата создания папки больше days то удаляем
            if (how_long_ago_creation_date>days):
                print("Delete folder: "+directory+folder+'/'+path)
             
            # Исключение так как не всегда удается удалить, папка/файл может быть чемто занята
            try:
                shutil.rmtree(os.path.join(os.path.abspath(os.path.dirname(__file__)), directory+folder+'/'+path))
            except OSError:
                print('Не удалось удалить папку')