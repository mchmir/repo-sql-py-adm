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

Public dd
Public qq
Public vmon
Public vday

'=======================================================================
'архивируем файл
'=======================================================================
Sub ArchiveFile (strArchiveFolderName, strArchiveFileName, strFileName)
	if objFSO.FileExists (strArchiveFolderName+"\"+strFileName) then
		Comand = str7ZFolder+" a -tzip "+strArchiveFolderName+"\"+strArchiveFileName+" "+strArchiveFolderName+"\"+strFileName
		LogStream.WriteLine "---архивируется "+strFileName+"---"
	
		WshShell.Run Comand , 0, True
		LogStream.WriteLine "---закончил архивировать "+strFileName+"---"
	
	else
		LogStream.WriteLine "!!!---НЕТ ФАЙЛА ДЛЯ АРХИВАЦИИ "+strFileName+"---"
	end if
end sub
'=======================================================================
'архивируем папку
'=======================================================================
Sub ArchiveFolder(strArchiveFolderName, strArchiveName, strFolderName)

	Comand = str7ZFolder+" a -tzip "+strArchiveFolderName+"\"+strArchiveName+" "+strArchiveFolderName+"\"+strFolderName
	LogStream.WriteLine "---архивируется "+strFolderName+"---"

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

'путь к архиватору
str7ZFolder="D:\BackUp\7-Zip\7z.exe"
'откуда копируем
strWorkFolder = "D:\BackUp"
'куда копируем
strArchiveFolder = strWorkFolder
'путь лог-файла
LogPath = strArchiveFolder
'если нет лог-файла, то создаем его
 If (objFSO.FileExists(LogPath+"\BackUpLog.log")) Then
    Set LogStream = objFSO.OpenTextFile(LogPath+"\BackUpLog.log", 8, True)
 Else
    Set LogStream = objFSO.CreateTextFile(LogPath+"\BackUpLog.log")
 end if
'пишем в лог
LogStream.WriteLine "--- Начало работы " & Now() & " ---"
LogStream.WriteLine
'получаем архивную папку как объект
Set objArchiveFolder = objFSO.GetFolder(strArchiveFolder)
ArchiveFileName=""

'архивируем
''[путь к 7z][путь и имя архива][путь архивируемого файла (папки)]
Set WshShell = CreateObject("WScript.Shell")
LogStream.WriteLine
LogStream.WriteLine "---начинаю архивировать " & Now() & " ---"
'msgbox objArchiveFolder.Path
ArchiveFile objArchiveFolder.Path, "backup_gefest_"& Date() &".zip", "backup_gefest.bak"
ArchiveFile objArchiveFolder.Path, "backup_sale_"& Date() &".zip", "sale.bak"
ArchiveFile objArchiveFolder.Path, "backup_DemandTemplates_"& Date() &".zip", "DemandTemplates.bak"


LogStream.WriteLine
LogStream.WriteLine "---закончил архивировать " & Now() & " ---"
LogStream.WriteLine
LogStream.WriteLine "--- Окончание работы " & Now() & " ---"
LogStream.WriteLine

'msgbox "Complete"
WScript.Quit(0)