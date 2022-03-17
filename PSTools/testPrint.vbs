Dim w
Set W = CreateObject("Word.Application")
w.Visible = false
w.Documents.Open "D:\Temp\testPrint.docx"
w.ActiveDocument.PrintOut
WScript.Sleep(1000)
w.Quit
Set w = Nothing