@ECHO OFF
:start
ping -n 10 mail.ru>nul 
goto answer%ERRORLEVEL%
:answer0
goto exit
:answer1
netsh interface set interface "InetGNS" disable
netsh interface set interface "InetGNS" enable
set dt=%date:~7,2%-%date:~4,2%-%date:~10,4%_%time:~,8%
%dt% >> C:\Scripts\log.txt
"Reload InetGNS interface" >> C:\Scripts\log.txt
:exit
exit