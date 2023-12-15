@echo off
chcp 1251 > nul

:: ����� ������������� �����������
echo ==================================================================================
echo  ������ ��� ����������� ������ ����������� ������
echo  �����: Mikhail Chmir
echo  ������: 13.12.2023
echo  ��������: ���� ������ �������� ������������ ����� � ��������� ����� ����������.
echo            ������� ������������� ����� �������� � ����� ������������ ���� �������.
echo ==================================================================================


:: �������� ���� � ����� � ������� YYYYMMDD_HHMMSS
SET timestamp=%date:~-4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%

:: ������������� ��� ���-����� � ����� � ��������
SET logFile=F:\archives101\copy_logPROG_%timestamp%.txt

:: ������ �� ������������� �������
echo Do you really want to start the copy process? (Y/N)
set /p confirm=
if /I "%confirm%" neq "Y" goto end

:: �������� ���������� ��������� �����
echo Is the archive disk ready? (Y/N)
set /p diskReady=
if /I "%diskReady%" neq "Y" goto end

:: ������ ���� �������
echo Enter access code:
set /p accessCode=
if "%accessCode%" neq "3010796" goto end

echo Starting copy process at %date% %time%
echo Starting copy process at %date% %time% > %logFile%

:: 1. ����������� ����� D:\YandexDisk\YandexDisk\data engineering � H:\WORK
echo [%date% %time%] Copying D:\YandexDisk\YandexDisk\data engineering to H:\WORK...
echo [%date% %time%] Copying D:\YandexDisk\YandexDisk\data engineering to H:\WORK... >> %logFile%

REM Robocopy �� ��������� �������� ����� � ������ ����������, ���� ��� ������ ������ � ���������.
REM � ������� Robocopy "%source%" "%destination%" /E /R:%retryCount% /W:%waitTime% /NP /LOG+:"%logFile%":
REM
REM /E ��������� Robocopy ���������� ��� ����������� (������� ������).
REM /R:%retryCount% � /W:%waitTime% ������ ���������� ��������� ������� � ����� �������� ����� ��������� � ������ ������.
REM /NP ������������� ����� ���������� � ��������� �����������.
REM /LOG+:"%logFile%" ��������� Robocopy ��������� ���������� � �������� ����������� � ��������� ���-����.
REM ��� ����������� ������ Robocopy ���������� ����� � ��������� � ����������.
REM ���� ���� � ���������� ����������� ��� ������, ��� ���� � ���������, Robocopy �������� ���. ��� ��������� ������������,
REM ��� � ����� ���������� ������ ����� ����� ��������� ������ ������ �� ����� ���������.
REM
REM ����� Robocopy �� ������� �����, ������� ��� ���������� � ������ ����������, ���������� �� �� ����,
REM ����� ������������ �������������� ����� �������. ��������:
REM
REM /XO (Exclude Older) - ��������� ������ �����, �� ���������� �����, ������� ������ ������ � ������ ����������.
REM /XC (Exclude Changed) - ��������� ���������� �����, �� ���������� �����, ���� ��� ��������.
REM /XN (Exclude Newer) - ��������� ����� �����, �� ���������� �����, ���� ��� ����� ������ � ������ ����������.
REM setlocal...endlocal  ������� ��������� ����������

setlocal
::Robocopy �� ��������� ������� ���������� � �������� ����������� �� ����� (� ��������� ������) �� ����� ����������
set "source=D:\YandexDisk\YandexDisk\data engineering"
set "destination=H:\WORK"
set "retryCount=3"
set "waitTime=20"

:copyloop_three
Robocopy "%source%" "%destination%" /E /R:%retryCount% /W:%waitTime% /LOG+:"%logFile%"
if %ERRORLEVEL% geq 8 (
    echo An error occurred while copying, please try again...
    goto copyloop_three
)
echo Copy YandexDisk completed successfully.
endlocal

:: 2. ����������� ����� C:\_git � H:\WORK
echo [%date% %time%] Copying C:\_git to H:\WORK...
echo [%date% %time%] Copying C:\_git to H:\WORK... >> %logFile%

setlocal
::Robocopy �� ��������� ������� ���������� � �������� ����������� �� ����� (� ��������� ������) �� ����� ����������
set "source=C:\_git"
set "destination=H:\WORK"
set "retryCount=3"
set "waitTime=20"

:copyloop_three
Robocopy "%source%" "%destination%" /E /R:%retryCount% /W:%waitTime% /NP /LOG+:"%logFile%"
if %ERRORLEVEL% geq 8 (
    echo An error occurred while copying, please try again...
    goto copyloop_three
)
echo Copy GIT completed successfully.
endlocal


echo Copy process completed at %date% %time%
echo Copy process completed at %date% %time% >> %logFile%

xcopy /E /Y /I %logFile% "H:\"

echo All operations completed, see the log file[%logFile%] for details.

:end
echo Process terminated. Close the window to exit.
pause > nul