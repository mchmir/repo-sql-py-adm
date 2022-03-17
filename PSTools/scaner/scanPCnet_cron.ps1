#Файл scannet_cron.ps1
#Скрипт запускается из планировщика 
#В параметре получает папку куда записывает файл результата
#Файл результата имеет имя дата_время_милисекунды.log

Param ($LogPath)

$LogFile=Get-Date -Format yyyy_MM_dd_hh_mm_ss_FFFFFFF

if ($LogPath -ne $Null) {
  $LogFullPath=$LogPath + "\" + $LogFile + ".log"
} else {
  $LogFullPath=".\" + $LogFile + ".log"
}
C:\PsTools\scaner\canPCnet.ps1 > $LogFullPath 

#----- Конец скрипта scannet_cron.ps1 -----