#Файл scannet.ps1 
#Сценарий последовательно сканирует IP подсеть и выдает таблицу:
#MAC-адрес сетевой карты, IP-адрес компа,
#NETBIOS-имя компа, NETBIOS-группа компа,
#DNS-адрес связанный с этим именем
#Результат выводится на экран

Function Get-MAC ($IP) {
  $This_IP=((Get-NetIPConfiguration).IPv4Address).IPv4Address
  if ($IP -eq $This_IP) {
    $InterfaceAlias=(Get-NetIPConfiguration).InterfaceAlias
    $This_HWAddr=(Get-NetAdapter | Where-Object{$_.ifAlias -eq $InterfaceAlias}).MACAddress
    #$This_HWAddr=(Get-NetAdapter).MACAddress | Select-Object -First 1
    $HW_Addr=$This_HWAddr
  } else {
    ping -4 -n 1 -w 2000 $IP > $Null 
    $Cmd_Arp_Out=arp -a | Where-Object {$_.Contains("$IP ")}
    if ($Cmd_Arp_Out -ne $NULL) {
      $HW_Addr=$Cmd_Arp_Out.Substring(24,17)
    }
  }
  $HW_Addr
}#Get-MAC

Function Get-DNSName ($IP) {
  (Resolve-DnsName -Name $IP -ErrorAction SilentlyContinue).NameHost
}

Function Get-NBTNames ($IP) {
  $NETBIOS_Name=""
  $NETBIOS_Group=""
  $Cmd_Nbtstat_Out=nbtstat -A $IP
  if ( ($Cmd_Nbtstat_Out | Select-String(" <00> ")).Count -gt 0) {
    $NETBIOS_Name=$Cmd_Nbtstat_Out  | Where-Object {$_.Contains("<00> Unicalnyi")} | Select-Object -First 1
    if($NETBIOS_Name -ne $NULL) {
      $NETBIOS_Name=$NETBIOS_Name.Substring(4,15)
      $NETBIOS_Group=($Cmd_Nbtstat_Out | Where-Object {$_.Contains("<00>  Gruppa")} | Select-Object -First 1)
      if($NETBIOS_Group -ne $NULL) {
        $NETBIOS_Group=$NETBIOS_Group.Substring(4,15)
      } else {
        $NETBIOS_Group="---------------"
      }
    } else { 
      $NETBIOS_Name="---------------"
    }
  } else {
    $NETBIOS_Name="---------------"
    $NETBIOS_Group="---------------"  
  }
  $NETBIOS_Name
  $NETBIOS_Group
}

Function ScanHost ($IP,$LogFullPath=$Null) {
  $MAC_Address=Get-MAC($IP)
  if ($MAC_Address -ne $Null) {
    $NETBIOS_Names=Get-NBTNames($IP)
    $DNS_Name=Get-DNSName($IP)
    "$MAC_Address $IP $NETBIOS_Names $DNS_Name"
  }
}

"HW_Address        IP        NETBIOS_Names NETBIOS_Group   DNS_Name"
 
#Эта часть скрипта проходит по диапазону IP 192.168.0.1 - 192.168.0.254

$i1=192
$i2=168
$i3=0
For ($i4=1; $i4 -le 254; $i4++) {
  ScanHost "$i1.$i2.$i3.$i4"
}

#----- Конец скрипта scannet.ps1 -----