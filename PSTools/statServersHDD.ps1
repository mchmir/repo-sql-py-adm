#Set-ExecutionPolicy Unrestricted

#. .\tools.ps1


$cred = Get-Credential 'gorgaz\geser'
$servers = 'gg-doc','gg-app','gg-isp','gg-main','bastion','gg-mail'
$date = Get-Date -Format "dd-MM-yyyy HH-mm"
$file = "Servers_"+$date+".csv"

#Get-DiskFree -Credential $cred -cn $servers -Format | ? { $_.Type -like '**' } | select * -ExcludeProperty Type | Out-GridView -Title 'Windows Servers Storage Statistics'
#Get-DiskFree  -Format | ? { $_.Type -like '*fixed*' } | select * -ExcludeProperty Type | Out-GridView -Title 'Windows Servers Storage Statistics'
Get-DiskFree -Credential $cred -cn $servers -Format | ? { $_.Type -like '**' } | select * -ExcludeProperty Type | export-csv -Delimiter ";" -path  C:\Pstools\ServRep\$file
