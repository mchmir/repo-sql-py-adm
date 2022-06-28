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
        'LogStream.WriteLine "Файл "+objFile.Name+" скопирован в "+strFolderTocopy& "\"+objFile.Name
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
            'LogStream.WriteLine "---Файл "+objFile.Name+" скопирован в "+Dst+"\"+objFile.Name
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
str7ZFolder="D:\BackUp\BackUps\7-Zip\7z.exe"
'откуда копируем
strWorkFolder = "D:\BackUp\BackUps"
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
FilePath="G:\\"
''techservice (sql)
FileName="backup_techservice.bak"
if objFSO.FileExists (FilePath+"\"+FileName) then
	set objFile=objFSO.GetFile(FilePath+"\"+FileName)'backup_techservice.bak")
	CopyFileToArchive objFile, objArchiveFolder
end if
''gefest (sql)
'FilePath="\\Gg-app\D$\BackUp\"
'FileName="backup_gefest.bak"
'if objFSO.FileExists (FilePath+"\"+FileName) then
'	set objFile=objFSO.GetFile(FilePath+"\"+FileName)'backup_techservice.bak")
'	CopyFileToArchive objFile, objArchiveFolder
'end if
'копируем папки
''1C
strWorkFolder="D:\1C\1C_8\"
''SeverGaz
''CopyFolderToArchive strWorkFolder+"SeverGaz", "SeverGaz", objArchiveFolder
''
strWorkFolder="D:\1C\"
''Podzemka
''CopyFolderToArchive strWorkFolder+"Podzemka", "Podzemka", objArchiveFolder
''
strWorkFolder="\\Gg-app\D$\1C\"
''GTM
CopyFolderToArchive strWorkFolder+"GTM", "GTM", objArchiveFolder
''GTM2016
CopyFolderToArchive strWorkFolder+"GTM2016", "GTM2016", objArchiveFolder
'''GTMExp2016
CopyFolderToArchive strWorkFolder+"GTMExp2016", "GTMExp2016", objArchiveFolder


''GorgazUniversal
''CopyFolderToArchive strWorkFolder+"GorgazUniversal", "GorgazUniversal", objArchiveFolder
''Aquilon
''CopyFolderToArchive strWorkFolder+"Aquilon", "Aquilon", objArchiveFolder
''Aquilon2016
''CopyFolderToArchive strWorkFolder+"Aquilon2016", "Aquilon2016", objArchiveFolder
''Bostan Group
''CopyFolderToArchive strWorkFolder+"Bostan Group", "Bostan Group", objArchiveFolder
''Gorgaz-ServisNEW
'CopyFolderToArchive strWorkFolder+"Gorgaz-ServisNEW", "Gorgaz-ServisNEW", objArchiveFolder
''Gorgaz-ServisNEW-ForTest
''CopyFolderToArchive strWorkFolder+"Gorgaz-ServisNEW-ForTest", "Gorgaz-ServisNEW-ForTest", objArchiveFolder
''Gorgaz-ServisNEW-ForTest
''CopyFolderToArchive strWorkFolder+"Gorgaz-ServisZarplata-ForTest", "Gorgaz-ServisZarplata-ForTest", objArchiveFolder
''ProfitInvest
''CopyFolderToArchive strWorkFolder+"ProfitInvest", "ProfitInvest", objArchiveFolder
''GAS-SERVICE
''CopyFolderToArchive strWorkFolder+"GAS-SERVICE", "GAS-SERVICE", objArchiveFolder
''GAS-SERVICE-NEW
''CopyFolderToArchive strWorkFolder+"GAS-SERVICE-NEW", "GAS-SERVICE-NEW", objArchiveFolder
''KiziltushKush
''CopyFolderToArchive strWorkFolder+"KiziltushKush", "KiziltushKush", objArchiveFolder
''SevKazTas
''CopyFolderToArchive strWorkFolder+"SevKazTas", "SevKazTas", objArchiveFolder
''PetrolRos
''CopyFolderToArchive strWorkFolder+"Petrol Ros", "Petrol Ros", objArchiveFolder
''PetrolImpex
''CopyFolderToArchive strWorkFolder+"Petrol Impex", "Petrol Impex", objArchiveFolder
''OtinGroup
''CopyFolderToArchive strWorkFolder+"OtinGroup", "OtinGroup", objArchiveFolder
''SmailTrade
''CopyFolderToArchive strWorkFolder+"SmailTrade", "SmailTrade", objArchiveFolder
''TabisMarketing
''CopyFolderToArchive strWorkFolder+"TabisMarketing", "TabisMarketing", objArchiveFolder
''ProfitHouse
''CopyFolderToArchive strWorkFolder+"ProfitHouse", "ProfitHouse", objArchiveFolder
''TelegraphInn
''CopyFolderToArchive strWorkFolder+"TelegraphInn", "TelegraphInn", objArchiveFolder
''******* add 27 dec. 2018 *********************************************************************
''Profit2017
''CopyFolderToArchive strWorkFolder+"Profit2017", "Profit2017", objArchiveFolder
''Profit2019
''CopyFolderToArchive strWorkFolder+"Profit2019", "Profit2019", objArchiveFolder
''Bostan2017
''CopyFolderToArchive strWorkFolder+"Bostan2017", "Bostan2017", objArchiveFolder
''Bostan2019
''CopyFolderToArchive strWorkFolder+"Bostan2019", "Bostan2019", objArchiveFolder
''TabisMarketing2017
''CopyFolderToArchive strWorkFolder+"TabisMarketing2017", "TabisMarketing2017", objArchiveFolder
'' ********************************************************************************************
''******* add 8 aug. 2019 *********************************************************************
''OIT
''CopyFolderToArchive strWorkFolder+"OIT", "OIT", objArchiveFolder
''***********************************************************************************************

strWorkFolder="\\Gg-mail\D$\1С\"
''Gorgaz-ServisNEW
''CopyFolderToArchive strWorkFolder+"Gorgaz-ServisNEW", "Gorgaz-ServisNEW", objArchiveFolder
''CopyFolderToArchive strWorkFolder+"Gorgaz-Servis2019", "Gorgaz-Servis2019", objArchiveFolder
''CopyFolderToArchive strWorkFolder+"Gorgaz-Servis092019", "Gorgaz-Servis092019", objArchiveFolder
'' add 23 jule 2020
CopyFolderToArchive strWorkFolder+"GazTehMontazh-Servis", "GazTehMontazh-Servis", objArchiveFolder
strWorkFolder="\\Gg-mail\D$\1С\"
''Gorgaz-ServisZarplata
CopyFolderToArchive strWorkFolder+"Gorgaz-ServisZarplata", "Gorgaz-ServisZarplata", objArchiveFolder

'архивируем
''[путь к 7z][путь и имя архива][путь архивируемого файла (папки)]
Set WshShell = CreateObject("WScript.Shell")
LogStream.WriteLine
LogStream.WriteLine "---начинаю архивировать " & Now() & " ---"

ArchiveFolder objArchiveFolder.Path, "GazTehMontazh-Servis.rar", "GazTehMontazh-Servis"
ArchiveFolder objArchiveFolder.Path, "Gorgaz-ServisZarplata.zip", "Gorgaz-ServisZarplata"
ArchiveFolder objArchiveFolder.Path, "GTM.zip", "GTM"
ArchiveFolder objArchiveFolder.Path, "GTM2016.zip", "GTM2016"
ArchiveFolder objArchiveFolder.Path, "GTMExp2016.zip", "GTMExp2016"

'ArchiveFile objArchiveFolder.Path, "backup_techservice.zip", "backup_techservice.bak"
'ArchiveFile objArchiveFolder.Path, "backup_gefest.zip", "backup_gefest.bak"
'ArchiveFolder objArchiveFolder.Path, "Aquilon.zip", "Aquilon"
'ArchiveFolder objArchiveFolder.Path, "Aquilon2016.zip", "Aquilon2016"
'objFSO.MoveFolder objArchiveFolder.Path+"\Bostan Group" , objArchiveFolder.Path+"\Bostan_Group"
'ArchiveFolder objArchiveFolder.Path, "Bostan_Group.zip", "Bostan_Group"
'ArchiveFolder objArchiveFolder.Path, "Gorgaz-ServisNEW.rar", "Gorgaz-ServisNEW"
'ArchiveFolder objArchiveFolder.Path, "Gorgaz-Servis2019.rar", "Gorgaz-Servis2019"
'ArchiveFolder objArchiveFolder.Path, "Gorgaz-Servis092019.rar", "Gorgaz-Servis092019"
'ArchiveFolder objArchiveFolder.Path, "ProfitInvest.zip", "ProfitInvest"
'ArchiveFolder objArchiveFolder.Path, "ProfitHouse.zip", "ProfitHouse"
'ArchiveFolder objArchiveFolder.Path, "TelegraphInn.zip", "TelegraphInn"
'' ********* add  27 dec. 2018 **********
'ArchiveFolder objArchiveFolder.Path, "Profit2017", "Profit2017"
'ArchiveFolder objArchiveFolder.Path, "Bostan2017", "Bostan2017"
'ArchiveFolder objArchiveFolder.Path, "TabisMarketing2017", "TabisMarketing2017"
'' **************************************
'' ********* add  24 jun. 2019 **********
'ArchiveFolder objArchiveFolder.Path, "Profit2019", "Profit2019"
'ArchiveFolder objArchiveFolder.Path, "Bostan2019", "Bostan2019"
'' **************************************
'' ********* add  8 aug. 2019 **********
'ArchiveFolder objArchiveFolder.Path, "OIT", "OIT"
''*****************************************************
'ArchiveFolder objArchiveFolder.Path, "SeverGaz.zip", "SeverGaz"
'ArchiveFolder objArchiveFolder.Path, "GAS-SERVICE.zip", "GAS-SERVICE"
'ArchiveFolder objArchiveFolder.Path, "GAS-SERVICE-NEW.zip", "GAS-SERVICE-NEW"
'ArchiveFolder objArchiveFolder.Path, "GorgazUniversal.zip", "GorgazUniversal"
'ArchiveFolder objArchiveFolder.Path, "Podzemka.zip", "Podzemka"
'ArchiveFolder objArchiveFolder.Path, "Gorgaz-ServisZarplata-ForTest.zip", "Gorgaz-ServisZarplata-ForTest"
'ArchiveFolder objArchiveFolder.Path, "Gorgaz-ServisNEW-ForTest.zip", "Gorgaz-ServisNEW-ForTest"
'ArchiveFolder objArchiveFolder.Path, "KiziltushKush.zip", "KiziltushKush"
'ArchiveFolder objArchiveFolder.Path, "SevKazTas.zip", "SevKazTas"
'objFSO.MoveFolder objArchiveFolder.Path+"\Petrol Ros" , objArchiveFolder.Path+"\PetrolRos"
'ArchiveFolder objArchiveFolder.Path, "PetrolRos.zip", "PetrolRos"
'objFSO.MoveFolder objArchiveFolder.Path+"\Petrol Impex" , objArchiveFolder.Path+"\PetrolImpex"
'ArchiveFolder objArchiveFolder.Path, "PetrolImpex.zip", "PetrolImpex"
'ArchiveFolder objArchiveFolder.Path, "OtinGroup.zip", "OtinGroup"
'ArchiveFolder objArchiveFolder.Path, "SmailTrade.zip", "SmailTrade"
'ArchiveFolder objArchiveFolder.Path, "TabisMarketing.zip", "TabisMarketing"

LogStream.WriteLine
LogStream.WriteLine "---закончил архивировать " & Now() & " ---"
LogStream.WriteLine
LogStream.WriteLine "--- Окончание работы " & Now() & " ---"
LogStream.WriteLine

'msgbox "Complete"
WScript.Quit(0)