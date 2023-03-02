Imports System.IO

Public Class Tools


    Public Shared Function DateToString(ByVal datValue As Date) As String
        Return datValue.Year.ToString + "-" + datValue.Month.ToString + "-" + datValue.Day.ToString
    End Function

    Public Shared Function DateToShortString(ByVal datValue As Date) As String
        Return datValue.Year.ToString + "" + datValue.Month.ToString + "" + datValue.Day.ToString
    End Function

    Public Shared Function AppPath() As String

        Return AppDomain.CurrentDomain.BaseDirectory.Replace("/", "\")

    End Function

    Public Shared Function Encrypt(ByVal vString As String) As String

        Dim strCode As String = ""
        Dim i As Integer = 0
        For i = 0 To vString.Length - 1
            strCode = strCode + Chr(vString.Substring(i, 1) ^ 87)
        Next i
        Return strCode
    End Function

    Public Shared Function LoadParameter(ByVal NameParameter As String, ByRef blErr As Boolean) As String

        Dim strConfig As String = ""
        Try

            Dim strArr As String()
            Dim strCode As String = ""
            Dim tmpStr As String
            Dim fStream As FileStream
            fStream = New FileStream(AppPath() + "office.cfg", FileMode.Open)
            Dim tmpRead As StreamReader
            tmpRead = New StreamReader(fStream)
            strCode = Encrypt("6#220>9\a2%>83jgfyffyeggcwfamf`mgc")
            While tmpRead.Read()  '.ReadLine()
                tmpStr = tmpRead.ReadLine
                strCode = Encrypt(tmpStr)
                strArr = strCode.Split("=")
                If strArr(0).ToUpper() = NameParameter.ToUpper() Then
                    tmpRead.Close()
                    blErr = False
                    tmpRead = Nothing
                    fStream = Nothing
                    Return strArr(1)
                End If
            End While

            tmpRead.Close()
            tmpRead = Nothing
            fStream = Nothing
            blErr = True

        Catch e As Exception
            blErr = True
            'EventsLog.SaveLog("Ошибка чтения данных из конфигурационный файл:" + e.Message, EventsLog.TypeLog.enumError)

        End Try
        Return strConfig
    End Function

    Public Shared Sub WriteErrLog(ByVal txt As String)
        Dim writePath As String = AppPath() + "\Log\err" + DateToShortString(Date.Today) + ".txt"
        Try
            Using writer As New StreamWriter(writePath, True, System.Text.Encoding.UTF8)
                writer.WriteLine(Now.ToString + " - " + txt)
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub WriteLog(ByVal txt As String)
        Dim writePath As String = AppPath() + "\Log\log" + DateToShortString(Date.Today) + ".txt"
        Try
            Using writer As New StreamWriter(writePath, True, System.Text.Encoding.UTF8)
                writer.WriteLine(Now.ToString + " - " + txt)
            End Using
        Catch ex As Exception

        End Try
    End Sub

End Class
