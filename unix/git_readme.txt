1. Atom. File - Settings - Install
	установить platformio-ide-terminal
    установить emmet 
               atom-live-server (CTRL+SHIFT+P  программа  server запуск сервера на порту или CTRL+ALT+8(на порту 8000), остановить сервер CTRL+ALT+Q)
               
2. Установить Git - система управления версиями проектов
		в терминале 	git --version
						git config --global user.name mchmir
						git config --global user.email mchmir@mail.ru
3. git init 
		в проекте скрытая папка .git папка для работы git внутри нашего проекта
4. добавление файлов внутрь локального репозитория
	git add .      весь проект, все папки
	git add css/  вся папка
	git add index.html один файл
	git *.html  все файлы во всех подпапках и папках проекта
	git !*.html все кроме
	git !css/style.css все файлы кроме 
	git !index.html все файлы кроме этого файла
	
5. git status  статус файлов (после add файл становится в стадию ожидания

6. git rm --cached index.html  удалить из стадии ожидания
7. необходимо файлы из стадии ожидания закомитеть - добавить в локальное хранилище
	git commit -m "Комментарии к файлам которые добавили"
	
После этого при изменении файлов по git status будут указаны файлы которые были модифицированы.
И файлы снова необходимо git add и после их закомитеть git commit -m "...."

git log   все изменения в файлах

файл .gitignore (без название только расширение) в папке
	css/
	js/*.js
	index.html
	*.txt
	
8. git checkout 4f4f288 (id комита из команды git log --oneline) и можем видеть прям в файле чтобыло изменено - !не сохранять 
   git checkout master возрат к последней 

9. git revert id-комита   отмена комита и мы попадаем в редактор VIM  :q выход  
10 git reset id-комита  далить все комиты до указанного из списка НО ФАЙЛЫ НЕ МЕНЯЮТСЯ! - практически не используется см.ниже
11 git reset id-комита --hard  жесткое удаление комита - с полным изменением файлов

ВЕТКА  - видится только локально (остальные пользователи не видят) основная ветка MASTER
после создания проекта свою ветку можно слить с MASTER 
12. git branch forum   создать ветку forum
13. git checkout forum  - перейти в ветку
14. git checkout -b admin создать ветку и перейти в нее
15. git branch -a просмотреть все ветки
16. git checkout master
    git merge forum     перейти к ветке мастер и соеденить ветку с мастером.


GIT HUB - облако, хранилище
1. Регистрируемся.
2. Создаем репозиторий  NEW

…or create a new repository on the command line
//echo "# mssql_ggs" >> README.md
//git init
//git add README.md
//git commit -m "first commit"
//git branch -M main
git remote add origin https://github.com/mchmir/mssql_ggs.git  подключаемся к репозиторию
git remote    должно выйти origion
git push -u origin main    добавляет файлы из локального репозитория MAIN в удаленный репозиторий
 git push -u origin master 
 
 вводим юзер и пароль  и все
 в git hub можно просмотреть историю, комиты, ветки  и прочее
 создать в проекте файл README.MD  markdown syntaksys   ---   markdownguide.org
 
 # Заголовок репозитория
   текс с описание **текст жирный**
   - Список 1
   - Список 2
   - Список 3
   
и добавить снова файлы  git add .
						git commit -m ""   добавили в локальное хранилище
						git push -u origin master   и в удаленное

КЛОНИРОВАНИЕ репозитория себе.
1. кнопка Clone or Download - zip файл загрузить себе
2. способ 2  с git-hub  нажимаем Clone with HTTP и копируем адресную строку
cd ../
git clone https://github.com/mchmir/mssql_ggs.git 
 и данные будут скачены в папку в которой мы были в терминале
 
3. git pull   добаляет все что является новым из скаченного репозитория. 


********************************************************************
1. Atom. File - Settings - Install
	установить go-plus (в пакетах можно вылючить/включить)
			   autocomplete-go


