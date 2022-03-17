:: robocopy.exe "D:\BackUp" "\\Gg-serv1\f$\GG_APP\BD_GEFEST\" *.zip /MAXAGE:1

::xcopy /s /m D:\BackUp\*.zip \\Gg-serv1\f$\GG_APP\BD_GEFEST

echo off
setlocal ENABLEDELAYEDEXPANSION
set s=D:\BackUp\
set d=\\Gg-serv1\f$\GG_APP\BD_GEFEST
set ext=zip

set dd=%date%
:: либо - set dd=%date% - если нужна текущая дата
for /r "%s%" %%x in (*.%ext%) do (
    for /f %%y in ('dir /o:-d /t:c "%%~x" ^| find "%%~nxx"') do (
        set t=%%y
        set t=!t:~-4!!t:~3,2!!t:~0,2!
        if !t! geq %dd:~-4%%dd:~3,2%%dd:~0,2% (
            xcopy "%%~x" "%d%\%%~px" /i /y
        )
    )
)