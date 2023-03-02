Imports System
Imports System.Data.SqlClient
Imports System.Data

Public Class SQLConnect

    Public _server As String = "gg-app"
    Public _database As String = "gefest"
    Public _user As String = "agrigoryev"
    Public _password As String = "159753"
    Public _staticConnect As New SqlConnection
    Public ConnString As String = "data source=" + _server + ";initial catalog=" + _database + ";User ID=" + _user + ";Password=" + _password + ";Connect Timeout=7200"

    Private Function LoadParameters() As Boolean
        Dim blErr As Boolean = True
        _server = Tools.LoadParameter("Server", blErr)
        If blErr Then Return Not blErr
        _database = Tools.LoadParameter("Database", blErr)
        If blErr Then Return Not blErr
        _user = Tools.LoadParameter("User", blErr)
        If blErr Then Return Not blErr
        _password = Tools.LoadParameter("Password", blErr)
        Return Not blErr
    End Function


    Public Function GetSQLConnect() As SqlConnection

        Dim strConnect As String = ""

        If _staticConnect Is Nothing Then _staticConnect = New SqlConnection

        If Not _staticConnect.State = ConnectionState.Open Then

            'If LoadParameters() Then

            strConnect = "data source=" + _server
            strConnect += ";initial catalog=" + _database
            strConnect += ";User ID=" + _user
            strConnect += ";Password=" + _password
            strConnect += ";Connect Timeout=45"
            strConnect += ";min pool size=1 "
            strConnect += ";max pool size=1 "
            Try
                '_staticConnect.ConnectionString = strConnect
                _staticConnect.Open()
            Catch
                'EventLog.SaveLog("Ошибка открытия базы данных!",EventsLog.TypeLog.enumError);
                'MessageBox.Show("Ошибка открытия базы данных!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            End Try
            'Else
            'EventsLog.SaveLog ("Ошибка чтения параметров соединения!",EventsLog.TypeLog.enumError);
            'End If

        End If
        Return _staticConnect

    End Function

    Public Sub New()
        _staticConnect = New SqlConnection
        _staticConnect.ConnectionString = ConnString
    End Sub

    Public Function Connect() As Boolean
        Try
            With _staticConnect
                If .State <> System.Data.ConnectionState.Open Then .Open()
                Return (.State = System.Data.ConnectionState.Open)
            End With
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub DisConnect()
        _staticConnect.Close()
    End Sub
End Class
