Внутри папки Modules необходимо создать подпапку с именем нашего модуля. 
Сохраняем наш файл внутрь этой подпапки и даем ему точно такое же имя и расширение .psm1 — 
получаем файл C:\Users\Administrator\Documents\WindowsPowerShell\Modules\Имя_модуля\Имя_модуля.psm1
чтобы узнать где лежат модули $env:Psmodulepath
Get-Module -ListAvailable