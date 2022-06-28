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
	        objFSO.CopyFile objFile, Dst+"\"+objFile.Name
            LogStream.WriteLine "---Файл "+objFile.Name+" скопирован в "+Dst+"\"+objFile.Name
	    next
	    for each objFolder in objFSO.GetFolder(Src).Subfolders
            LogStream.WriteLine "--Папка " & objFolder.Name & " скопирована."
	        CopyDir Src+"\"+objFolder.Name ,objFolder.Name, Dst+"\"+objFolder.Name
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
		Comand = str7ZFolder+" a -tzip "+strArchiveFolderName+"\"+strArchiveFileName+" "+strArchiveFolderName+"\"+strFileName
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
	Comand = str7ZFolder+" a -tzip "+strArchiveFolderName+"\"+strArchiveName+" "+strArchiveFolderName+"\"+strFolderName
	LogStream.WriteLine "---архивируется "+strFolderName+"---"
	'LogStream.WriteLine comand
	WshShell.Run Comand , 0, True
	LogStream.WriteLine "---закончил архивировать "+strFolderName+"---"
	objFSO.DeleteFolder strArchiveFolderName+"\"+strFolderName, True
	LogStream.WriteLine "---удалили "+strFolderName+"---"
end sub

'=======================================================================
'функция проверки свободного места на диске
'Drive - буква диска в формате С
'=======================================================================
function  CheckFreeSpace(Drive)
	on error resume next
	CheckFreeSpace=FormatNumber(objFSO.GetDrive(Drive).FreeSpace/1048576/1024, 1)
	If Err.Number <> 0 Then
        Err.Clear
        CheckFreeSpace=100
    Else
    End If
End function 

'=======================================================================
'ищем самую "старую" папку исходя из ее названия
'формат имени папки 2012-05-01
'=======================================================================
Sub GetOlderFolder(Src)
	on error resume next
	dim FolderDate
	dim OlderFolderDate
	dim OlderFolder
	OlderFolderDate=Now()
	'LogStream.WriteLine "Старая Дата "+cstr(OlderFolderDate)
	for each objFolder in objFSO.GetFolder(Src).Subfolders
        'LogStream.WriteLine "Папка "+objFolder.Name
	    FolderDate=cdate(objFolder.Name)
	    if OlderFolderDate>FolderDate then
	    	OlderFolderDate=FolderDate
	    	set OlderFolder=objFolder
	    end if
	    'LogStream.WriteLine "Дата "+cstr(FolderDate)
	next 
	
	'LogStream.WriteLine "Старая папка "+OlderFolder.Path
	'LogStream.WriteLine	CheckFreeSpace("E")
	'if CheckFreeSpace("E")<50 then
		objFSO.DeleteFolder OlderFolder.Path, True
	'end if
	'LogStream.WriteLine "Старая Дата "+cstr(OlderFolderDate)
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
strWorkFolder = "F:\BackUps"
'куда копируем
strArchiveFolder = strWorkFolder+"\"+dd
'путь лог-файла
LogPath = strArchiveFolder
'если нет архивной папки, то создаем ее
If Not objFSO.FolderExists(strArchiveFolder) Then
    objFSO.CreateFolder strArchiveFolder
End If
'если нет лог-файла, то создаем его
 If (objFSO.FileExists(LogPath+"\BackUpLog.log")) Then
    Set LogStream = objFSO.OpenTextFile(LogPath+"\BackUpLog.log", 8, True)
 Else
    Set LogStream = objFSO.CreateTextFile(LogPath+"\BackUpLog.log")
 end if
'пишем в лог
LogStream.WriteLine "--- Начало работы " & Now() & " ---"
LogStream.WriteLine
'проверяем, если место на диске меньше 50Гб
'надо будет удалить старые папки
do while CheckFreeSpace("F")<50
	GetOlderFolder(strWorkFolder)
loop
'получаем архивную папку как объект
Set objArchiveFolder = objFSO.GetFolder(strArchiveFolder)
ArchiveFileName=""
'копируем файлы
FilePath="\\Gtm-1\D$\GTM\"
''techservice (sql)
FileName="GTM.accdb"
Set WshShell = CreateObject("WScript.Shell")
''--
Set objScriptExec = WshShell.Exec("%comspec% /c ping.exe -n 2 GTM-1")
	strPingResults = LCase(objScriptExec.StdOut.ReadAll)
	if InStr(strPingResults, "ttl=") Then
		if objFSO.FileExists (FilePath+"\"+FileName) then
			set objFile=objFSO.GetFile(FilePath+"\"+FileName)'backup_techservice.bak")
			CopyFileToArchive objFile, objArchiveFolder
		end if
	else
		LogStream.WriteLine "!!!Не пингуется GTM-1. Не могу копировать."
	end if
''--
Set objScriptExec = WshShell.Exec("%comspec% /c ping.exe -n 2 buh-1")
	strPingResults = LCase(objScriptExec.StdOut.ReadAll)
	if InStr(strPingResults, "ttl=") Then
		'копируем папки
		''
		strWorkFolder="\\Buh-0\D$\1Cv8\"
		''профит
		''CopyFolderToArchive strWorkFolder+"профит", "профит", objArchiveFolder
		''бостан
		CopyFolderToArchive strWorkFolder+"бостан", "бостан", objArchiveFolder
		''ипиванов
		CopyFolderToArchive strWorkFolder+"ипиванов", "ипиванов", objArchiveFolder
		''профит_11112013
		CopyFolderToArchive strWorkFolder+"профит_11112013", "профит_11112013", objArchiveFolder
		
	else
		LogStream.WriteLine "!!!Не пингуется Buh-1. Не могу копировать."
	end if
''--

'архивируем
''[путь к 7z][путь и имя архива][путь архивируемого файла (папки)]
Set WshShell = CreateObject("WScript.Shell")
LogStream.WriteLine
LogStream.WriteLine "---начинаю архивировать " & Now() & " ---"
ArchiveFile objArchiveFolder.Path, "GTM_DB.zip", "GTM.accdb"
''ArchiveFolder objArchiveFolder.Path, "профит.zip", "профит"
ArchiveFolder objArchiveFolder.Path, "бостан.zip", "бостан"
ArchiveFolder objArchiveFolder.Path, "ипиванов.zip", "ипиванов"
ArchiveFolder objArchiveFolder.Path, "профит_11112013.zip", "профит_11112013"
LogStream.WriteLine
LogStream.WriteLine "---закончил архивировать " & Now() & " ---"
LogStream.WriteLine
LogStream.WriteLine "--- Окончание работы " & Now() & " ---"
LogStream.WriteLine

'msgbox "Complete"
WScript.Quit(0)