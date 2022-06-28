Option Explicit

Public objFSO
Public objArchiveFolder
Public objFile
Public objFolder

Public strWorkFolder
Public strCopyFolder
Public strArchiveFolder
Public str7ZFolder
Public LogStream
Public LogPath 
Public objShellApp
Public WshShell
Public objScriptExec
Public strPingResults
Public dd
Public qq
Public vmon
Public vday

Public MyExt
MyExt=Array("lnk","jpg","jpeg","bmp","png","gif","avi","mkv","wmv","wma","mp3","mp4","exe","wav","cda","apk","msi","znitcve")
Public CompUsers
Public CompUsers2
Public CompUsers3
Public CompUsers3_Name
''strWorkFolder="\d$\Мои документы"
CompUsers=Array("Buh-0","Buh-2","Buh-8","Buh-9","Buh-10","Jurist","Jurist-2","Jurist-3","Ab-8","Ab-1","Ab-15","Pto-1","Pto-6","Pto-7","Ab-2","Peo-1","Peo-2","Peo-3","Tb-2","Ok-1")
''strWorkFolder="\e$\Мои документы"
''CompUsers2=Array("Ok-1")
''strWorkFolder="\c$\Documents and Settings\"
CompUsers3=Array("Pto-5","Pto-8","Vdgo")
CompUsers3_Name=Array("ekarabelnikov","ntkach","tgulakova")
'=======================================================================
'функция копирования файла
'передаем файл и путь к папке копирования
'=======================================================================
Sub CopyFileToArchive(objFile, objArchiveFolder)
    
    On Error Resume Next

    Dim strFolderTocopy

    strFolderTocopy= objArchiveFolder  '.Path '& "\" & Left(objFile.Name, 8)
    objFile.Copy strFolderTocopy+"\"+objFile.Name

    If Err.Number <> 0 Then
        LogStream.WriteLine "Ошибка копирования файла "+strWorkFolder+"\"+objFile.Name+" - "+Err.Description
        Err.Clear
    Else
        LogStream.WriteLine "Файл "+objFile.Name+" скопирован в "+strFolderTocopy& "\"+objFile.Name
    End If
    
End Sub
'=======================================================================
'функция копирования папки
'передаем путь исходной папки и путь к папке копирования
'=======================================================================
Sub CopyFolderToArchive(strFolderPath, strFolderTocopy, objArchiveFolder)
    
    On Error Resume Next
    If NOT objFSO.FolderExists(objArchiveFolder.Path+"\"+strFolderTocopy) Then
	       Set objFolder = objFSO.CreateFolder(objArchiveFolder.Path+"\"+strFolderTocopy)
	end if
    

    LogStream.WriteLine "Копирование "+strFolderTocopy+" в "+objArchiveFolder.Path+"..."
    CopyDir strFolderPath, "", objArchiveFolder+"\"+strFolderTocopy

    If Err.Number <> 0 Then
        LogStream.WriteLine "Ошибка копирования папки "+strFolderTocopy+" - "+Err.Description
        Err.Clear
    Else
        LogStream.WriteLine "Копирование "+strFolderTocopy+" в "+objArchiveFolder.Path+" завершено."
    End If
       
End Sub
'=======================================================================
'=======================================================================
Sub CopyDir ( ByVal Src, byval Name, ByVal Dst )
	On Error Resume Next
	dim fExt
	dim Need
	dim i
	if len(Name)<0 Then
		If NOT objFSO.FolderExists(Dst+"\"+Name) Then
	       Set objFolder = objFSO.CreateFolder(Dst+"\"+Name)
	    end if
	    for each objFile in objFSO.GetFolder(Src+"\"+Name).Files
	        objFSO.CopyFile objFile, Dst+"\"+Name+"\"+objFile.Name
            LogStream.WriteLine "---Файл " & objFile.Name & " скопирован в " + Dst+"\"+Name + "\" & objFile.Name
	    next
	    for each objFolder in objFSO.GetFolder(Src+"\"+Name).Subfolders
	        CopyDir Src+"\"+Name + "\" + objFolder.Name ,objFolder.Name, Dst+"\"+Name +"\"+objFolder.Name
	    next
	else
		If NOT objFSO.FolderExists(Dst) Then
	       Set objFolder = objFSO.CreateFolder(Dst)
	    end if
	    for each objFile in objFSO.GetFolder(Src).Files
	    'если файл изменен менее 8 дней назад то обрабатываем его
	    	'If DateDiff("d", objFile.DateLastModified, Now) < 8 Then 
	    	'делаем полный бэкап
		    	Need=1
	        	fExt = objFSO.GetExtensionName(objFile.Path)
	        	'msgbox fExt
		        for i=0 to Ubound(MyExt)
		        	if LCase(fExt)=LCase(MyExt(i)) then
		        		Need=0
		        		exit for	
		        	end if
		        next
		        if Need = 1 then
		        	objFSO.CopyFile objFile, Dst+"\"+objFile.Name
		        end if
	            LogStream.WriteLine "---Файл "+objFile.Name+" скопирован в "+Dst+"\"+objFile.Name
	        'end if 
	    next
	    for each objFolder in objFSO.GetFolder(Src).Subfolders
	        if not objFolder.Name="Мои рисунки" then
            	LogStream.WriteLine "--Папка " & objFolder.Name & " скопирована."
	        	CopyDir Src+"\"+objFolder.Name ,objFolder.Name, Dst+"\"+objFolder.Name
	        end if
	    next
	    
    end if
End Sub
'=======================================================================
'архивируем файл
'=======================================================================
Sub ArchiveFile (strArchiveFolderName, strArchiveFileName, strFileName)
	'ArchiveFileName="backup_techservice.zip"
	'FileName="backup_techservice.bak"
	if objFSO.FileExists (strArchiveFolderName+"\"+strFileName) then
		Comand = str7ZFolder+" a -tzip -mx9 "+strArchiveFolderName+"\"+strArchiveFileName+" "+strArchiveFolderName+"\"+strFileName
		LogStream.WriteLine "---архивируется "+strFileName+"---"
		'LogStream.WriteLine comand
		WshShell.Run Comand , 0, True
		LogStream.WriteLine "---закончил архивировать "+strFileName+"---"
		objFSO.DeleteFile strArchiveFolderName+"\"+strFileName, True
		LogStream.WriteLine "---удалили "+strFileName+"---"
	else
		LogStream.WriteLine "!!!---НЕТ ФАЙЛА ДЛЯ АРХИВАЦИИ "+strFileName+"---"
	end if
end sub
'=======================================================================
'архивируем папку
'=======================================================================
Sub ArchiveFolder(strArchiveFolderName, strArchiveName, strFolderName)
	'ArchiveFileName="backup_techservice.zip"
	'FileName="backup_techservice.bak"
	Comand = str7ZFolder+" a -tzip -mx9 "+strArchiveFolderName+"\"+strArchiveName+" "+strArchiveFolderName+"\"+strFolderName
	LogStream.WriteLine "---архивируется "+strFolderName+"---"
	'LogStream.WriteLine comand
	WshShell.Run Comand , 0, True
	LogStream.WriteLine "---закончил архивировать "+strFolderName+"---"
	objFSO.DeleteFolder strArchiveFolderName+"\"+strFolderName, True
	LogStream.WriteLine "---удалили "+strFolderName+"---"
end sub
'=======================================================================
'собственно скрипт
'=======================================================================
'on error resume next
set objFSO = CreateObject("Scripting.FileSystemObject")
Set objShellApp = CreateObject("Shell.Application")
dim FilePath
dim FileName
dim ArchiveFileName
dim Comand
dim strComputer
dim i

qq=cdbl(now())
dd=cdate(qq)

vmon=cstr(Month(dd))
if len(vmon)=1 then vmon="0"+vmon

vday=cstr(day(dd))
if len(vday)=1 then vday="0"+vday

'dd="2006-02-13"
dd=cstr(year(dd))+"-"+vmon+"-"+vday
'путь к архиватору
str7ZFolder="F:\BackUps\7-Zip\7z.exe"
'откуда копируем
strWorkFolder = "F:\UserBackUps"
If Not objFSO.FolderExists(strWorkFolder) Then
    objFSO.CreateFolder strWorkFolder
End If
'куда копируем
strArchiveFolder = strWorkFolder+"\"+dd
'путь лог-файла
LogPath = strArchiveFolder
'если нет архивной папки, то создаем ее
If Not objFSO.FolderExists(strArchiveFolder) Then
    objFSO.CreateFolder strArchiveFolder
End If
'если нет лог-файла, то создаем его
 If (objFSO.FileExists(LogPath+"\BackUpUserLog.log")) Then
    Set LogStream = objFSO.OpenTextFile(LogPath+"\BackUpUserLog.log", 8, True)
 Else
    Set LogStream = objFSO.CreateTextFile(LogPath+"\BackUpUserLog.log")
 end if

'пишем в лог
LogStream.WriteLine "--- Начало работы " & Now() & " ---"
LogStream.WriteLine
'получаем архивную папку как объект
Set objArchiveFolder = objFSO.GetFolder(strArchiveFolder)
ArchiveFileName=""
'копируем файлы
'FilePath="\\app-server\G$"
Set WshShell = CreateObject("WScript.Shell")
'копируем папки
strWorkFolder="\d$\Мои документы"
for i=0 to Ubound(CompUsers)
	strComputer=cstr(CompUsers(i))
	Set objScriptExec = WshShell.Exec("%comspec% /c ping.exe -n 2 "+strComputer)
	strPingResults = LCase(objScriptExec.StdOut.ReadAll)
	if InStr(strPingResults, "ttl=") Then
		CopyFolderToArchive "\\"+strComputer+strWorkFolder, strComputer, objArchiveFolder
	else
		LogStream.WriteLine "!!!Не пингуется "+strComputer+". Не могу копировать."
	end if
next

'strWorkFolder="\f$\Мои документы"
'for i=0 to Ubound(CompUsers2)
'	strComputer=cstr(CompUsers2(i))
'	Set objScriptExec = WshShell.Exec("%comspec% /c ping.exe -n 2 "+strComputer)
'	strPingResults = LCase(objScriptExec.StdOut.ReadAll)
'	if InStr(strPingResults, "ttl=") Then
'		CopyFolderToArchive "\\"+strComputer+strWorkFolder, strComputer, objArchiveFolder
'	else
'		LogStream.WriteLine "!!!Не пингуется "+strComputer+". Не могу копировать."
'	end if
'next

strWorkFolder="\c$\Documents and Settings\"
dim strCompUserName
for i=0 to Ubound(CompUsers3)
	strComputer=cstr(CompUsers3(i))
	strCompUserName=cstr(CompUsers3_Name(i))
	Set objScriptExec = WshShell.Exec("%comspec% /c ping.exe -n 2 "+strComputer)
	strPingResults = LCase(objScriptExec.StdOut.ReadAll)
	if InStr(strPingResults, "ttl=") Then
		CopyFolderToArchive "\\"+strComputer+strWorkFolder+strCompUserName+"\Мои Документы", strComputer, objArchiveFolder
	else
		LogStream.WriteLine "!!!Не пингуется "+strComputer+". Не могу копировать."
	end if
next

''Сатанова Дина 
'CopyFolderToArchive "\\Buh-9"+strWorkFolder, "Buh-9", objArchiveFolder
''Сапоненко О.А. 
'CopyFolderToArchive "\\"+strComputer+strWorkFolder, strComputer, objArchiveFolder
''Бржанова Анастасия 
'CopyFolderToArchive "\\Buh-10"+strWorkFolder, "Buh-10", objArchiveFolder
''Лысенко С.Б.
'CopyFolderToArchive "\\Buh-1"+strWorkFolder, "Buh-1", objArchiveFolder
''Малков Павел 
'CopyFolderToArchive "\\Jurist-1"+strWorkFolder, "Jurist-1", objArchiveFolder
''Глухих Елена
'CopyFolderToArchive "\\Jurist-2"+strWorkFolder, "Jurist-2", objArchiveFolder
''Кшановская Людмила
'CopyFolderToArchive "\\Jurist-3"+strWorkFolder, "Jurist-3", objArchiveFolder
''Куприянова Юлия
'CopyFolderToArchive "\\Ab-8"+strWorkFolder, "Ab-8", objArchiveFolder
''Поливянова Алена
'CopyFolderToArchive "\\Ab-15"+strWorkFolder, "Ab-15", objArchiveFolder
''Насибуллина Н.М.
'CopyFolderToArchive "\\Ab-11"+strWorkFolder, "Ab-11", objArchiveFolder

'архивируем
''[путь к 7z][путь и имя архива][путь архивируемого файла (папки)]
'LogStream.WriteLine
'LogStream.WriteLine "---начинаю архивировать---"
'objFSO.MoveFolder objArchiveFolder.Path+"\Bostan Group" , objArchiveFolder.Path+"\Bostan_Group"
'ArchiveFolder objArchiveFolder.Path, "Bostan_Group.zip", "Bostan_Group"

'LogStream.WriteLine
'LogStream.WriteLine "---закончил архивировать---"
LogStream.WriteLine
LogStream.WriteLine "--- Окончание работы " & Now() & " ---"
LogStream.WriteLine

'msgbox "Complete"
WScript.Quit(0)