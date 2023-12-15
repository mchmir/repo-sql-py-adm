@echo off
chcp 1251 > nul

:: Вывод заголовочного комментария
echo ==================================================================================
echo  Скрипт для копирования архива операционной системы
echo  Автор: Mikhail Chmir
echo  Создан: 13.12.2023
echo  Описание: Этот скрипт копирует определенные папки в указанные места назначения.
echo            Требует подтверждения перед запуском и ввода специального кода доступа.
echo ==================================================================================


:: Получаем дату и время в формате YYYYMMDD_HHMMSS
SET timestamp=%date:~-4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%

:: Устанавливаем имя лог-файла с датой и временем
SET logFile=F:\archives101\copy_logSYS_%timestamp%.txt

:: Запрос на подтверждение запуска
echo Do you really want to start the copy process? (Y/N)
set /p confirm=
if /I "%confirm%" neq "Y" goto end

:: Проверка готовности архивного диска
echo Is the archive disk ready? (Y/N)
set /p diskReady=
if /I "%diskReady%" neq "Y" goto end

:: Запрос кода доступа
echo Enter access code:
set /p accessCode=
if "%accessCode%" neq "3010796" goto end

echo Starting copy process at %date% %time%
echo Starting copy process at %date% %time% > %logFile%

:: 1. Копирование папки F:\WindowsImageBackup на диск H. Копирование системы каждую пятницу в 23:00
echo [%date% %time%] Copying F:\WindowsImageBackup to H:\...
echo [%date% %time%] Copying F:\WindowsImageBackup to H:\... >> %logFile%

REM Robocopy по умолчанию заменяет файлы в пункте назначения, если они старше файлов в источнике.
REM В команде Robocopy "%source%" "%destination%" /E /R:%retryCount% /W:%waitTime% /NP /LOG+:"%logFile%":
REM
REM /E указывает Robocopy копировать все подкаталоги (включая пустые).
REM /R:%retryCount% и /W:%waitTime% задают количество повторных попыток и время ожидания между попытками в случае ошибок.
REM /NP предотвращает вывод информации о прогрессе копирования.
REM /LOG+:"%logFile%" указывает Robocopy добавлять информацию о процессе копирования в указанный лог-файл.
REM При копировании файлов Robocopy сравнивает файлы в источнике и назначении.
REM Если файл в назначении отсутствует или старше, чем файл в источнике, Robocopy заменяет его. Это поведение обеспечивает,
REM что в папке назначения всегда будут самые последние версии файлов из папки источника.
REM
REM Чтобы Robocopy не заменял файлы, которые уже существуют в пункте назначения, независимо от их даты,
REM можно использовать дополнительные ключи команды. Например:
REM
REM /XO (Exclude Older) - исключить старые файлы, не копировать файлы, которые старше файлов в пункте назначения.
REM /XC (Exclude Changed) - исключить измененные файлы, не копировать файлы, если они изменены.
REM /XN (Exclude Newer) - исключить новые файлы, не копировать файлы, если они новее файлов в пункте назначения.
REM setlocal...endlocal  область видимости переменных

setlocal
::Robocopy по умолчанию выводит информацию о процессе копирования на экран (в командную строку) во время выполнения
set "source=F:\WindowsImageBackup"
set "destination=H:\WindowsImageBackup"
set "retryCount=3"
set "waitTime=20"

:copyloop
Robocopy "%source%" "%destination%" /E /R:%retryCount% /W:%waitTime% /NP /LOG+:"%logFile%"
if %ERRORLEVEL% geq 8 (
    echo An error occurred while copying, please try again...
    goto copyloop
)
echo Copy WindowsImageBackup completed successfully.
endlocal


echo Copy process completed at %date% %time%
echo Copy process completed at %date% %time% >> %logFile%

xcopy /E /Y /I %logFile% "H:\"

echo All operations completed, see the log file[%logFile%] for details.

:end
echo Process terminated. Close the window to exit.
pause > nul