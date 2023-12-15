@echo off
chcp 1251 > nul

:: Получаем дату и время в формате YYYYMMDD_HHMMSS
SET timestamp=%date:~-4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%

:: Устанавливаем имя лог-файла с датой и временем
SET logFile=F:\archives101\copy_log_%timestamp%.txt

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
xcopy /E /Y /I "F:\WindowsImageBackup" "H:\WindowsImageBackup" >> %logFile%

:: 2. Копирование папок из E:\VBOX и D:\vbox в H:\VBOX
echo [%date% %time%] Copying E:\VBOX\WIN10ASW, E:\VBOX\WIN10GGS, and D:\vbox to H:\VBOX...
echo [%date% %time%] Copying E:\VBOX\WIN10ASW, E:\VBOX\WIN10GGS, and D:\vbox to H:\VBOX... >> %logFile%
xcopy /E /Y /I "E:\VBOX\WIN10ASW" "H:\VBOX\WIN10ASW" >> %logFile%
xcopy /E /Y /I "E:\VBOX\WIN10GGS" "H:\VBOX\WIN10GGS" >> %logFile%
xcopy /E /Y /I "D:\vbox" "H:\VBOX\vbox" >> %logFile%

:: 3. Копирование папки D:\YandexDisk\YandexDisk\data engineering в H:\WORK
echo [%date% %time%] Copying D:\YandexDisk\YandexDisk\data engineering to H:\WORK...
echo [%date% %time%] Copying D:\YandexDisk\YandexDisk\data engineering to H:\WORK... >> %logFile%
xcopy /E /Y /I "D:\YandexDisk\YandexDisk\data engineering" "H:\WORK\data engineering" >> %logFile%

:: 4. Копирование папки C:\_git в H:\WORK
echo [%date% %time%] Copying C:\_git to H:\WORK...
echo [%date% %time%] Copying C:\_git to H:\WORK... >> %logFile%
xcopy /E /Y /I "C:\_git" "H:\WORK\_git" >> %logFile%

echo Copy process completed at %date% %time%
echo Copy process completed at %date% %time% >> %logFile%

xcopy /E /Y /I %logFile% "H:\"

echo All operations completed, see the log file[%logFile%] for details.

:end
echo Process terminated. Close the window to exit.
pause > nul