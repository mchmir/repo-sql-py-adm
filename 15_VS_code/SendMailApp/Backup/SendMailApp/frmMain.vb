Imports System.Net.Mail
Imports System.Data.SqlClient

Public Class frmMain



    Private Sub SendMessage(ByVal smtpServer As String, ByVal from As String, ByVal password As String, ByVal mailto As String, ByVal caption As String, ByVal message As String, ByVal attachFile As String, ByVal idcontract As Integer)
        Try
            Dim mail As MailMessage
            mail = New MailMessage
            mail.From = New MailAddress(from)
            mail.To.Add(New MailAddress(mailto))
            mail.Subject = caption
            mail.Body = message
            mail.IsBodyHtml = True
            If attachFile.Length > 0 Then mail.Attachments.Add(New Attachment(attachFile))
            Dim client As SmtpClient
            client = New SmtpClient
            client.Host = smtpServer
            client.Port = 587
            client.Timeout = 10000
            client.Credentials = New Net.NetworkCredential(from, password) '(Split(from,"@",0)), password)
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.Send(mail)
            mail.Dispose()
            Tools.WriteLog("Письмо на " + mailto + " успешно отправлено - idpersonalcabinet=" + CStr(idcontract))
            UpdateContract(idcontract)
        Catch ex As Exception
            Tools.WriteErrLog("SendMessage: " + mailto + " - idpersonalcabinet -" + CStr(idcontract) + " - " + ex.Message.ToString)
        End Try
    End Sub

    Private Sub SendMessageForRequest(ByVal smtpServer As String, ByVal from As String, ByVal password As String, ByVal mailto As String, ByVal caption As String, ByVal message As String, ByVal attachFile As String, ByVal idrequest As Integer, ByVal needupdreq As Boolean, ByVal state As Integer)
        Try
            If mailto.Length = 0 Then
                UpdateRequest(idrequest)
                Tools.WriteErrLog("SendMessageForRequest: не указан адрес электронной почты" + CStr(idrequest))
            Else
                Dim mail As MailMessage
                mail = New MailMessage
                mail.From = New MailAddress(from)
                mail.To.Add(New MailAddress(mailto))
                mail.Subject = caption
                mail.Body = message
                mail.IsBodyHtml = True
                If attachFile.Length > 0 Then mail.Attachments.Add(New Attachment(attachFile))
                Dim client As SmtpClient
                client = New SmtpClient
                client.Host = smtpServer
                client.Port = 587
                client.Timeout = 10000
                client.Credentials = New Net.NetworkCredential(from, password) '(Split(from,"@",0)), password)
                client.DeliveryMethod = SmtpDeliveryMethod.Network
                client.Send(mail)
                mail.Dispose()
                Tools.WriteLog("Письмо на " + mailto + " успешно отправлено: idrequest=" + CStr(idrequest) + "; state=" + CStr(state))
                If needupdreq = True Then UpdateRequest(idrequest)
            End If
        Catch ex As Exception
            Tools.WriteErrLog("SendMessageForRequest: " + mailto + ": " + ex.Message.ToString)
        End Try
    End Sub

    Private Sub SendMessageForMessageAnswer(ByVal smtpServer As String, ByVal from As String, ByVal password As String, ByVal mailto As String, ByVal caption As String, ByVal message As String, ByVal attachFile As String, ByVal idmessage As Integer)
        Try
            Dim mail As MailMessage
            mail = New MailMessage
            mail.From = New MailAddress(from)
            mail.To.Add(New MailAddress(mailto))
            mail.Subject = caption
            mail.Body = message
            mail.IsBodyHtml = True
            If attachFile.Length > 0 Then mail.Attachments.Add(New Attachment(attachFile))
            Dim client As SmtpClient
            client = New SmtpClient
            client.Host = smtpServer
            client.Port = 587
            client.Timeout = 10000
            client.Credentials = New Net.NetworkCredential(from, password) '(Split(from,"@",0)), password)
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.Send(mail)
            mail.Dispose()
            Tools.WriteLog("Письмо на " + mailto + " успешно отправлено: idpersonalcabinetmessage=" + CStr(idmessage))
            UpdateMessage(idmessage)
        Catch ex As Exception
            Tools.WriteErrLog("SendMessageForMessageAnswer: " + ex.Message.ToString)
        End Try
    End Sub

    Public Sub GetMailToSendMessageForLogin()
        Dim dbc As New SQLConnect
        Dim sSelect As String
        Dim str As String = ""
        Try
            str = "try:"
            With dbc._staticConnect.CreateCommand
                sSelect = "select account, pcpass, pcemail, idpersonalcabinet from personalcabinet " _
                + "where len(isnull(pcpass,''))>0 and len(isnull(pcemail,''))>0"
                str += "get command text:"
                If dbc.Connect Then
                    .CommandText = sSelect
                    Dim dr As SqlDataReader
                    dr = .ExecuteReader
                    str += "execute reader"
                    While dr.Read
                        str += "dr read:"
                        SendMessage("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", CStr(dr.GetValue(2)), "Данные для входа в Личный Кабинет Абонента", CreateMessageBody(CStr(dr.GetValue(1)), CStr(dr.GetValue(0))), "", CInt(dr.GetValue(3)))
                    End While
                End If
            End With
        Catch ex As Exception
            Try
                Tools.WriteErrLog("GetMailToSendMessageForLogin: " + str + ex.InnerException.ToString)
            Catch
                Tools.WriteErrLog("GetMailToSendMessageForLogin: " + str + ex.Message.ToString)
            End Try
        Finally
            dbc.DisConnect()
        End Try
    End Sub

    Public Sub GetMailToSendMessageForRequest()
        Dim dbc As New SQLConnect
        Dim sSelect As String
        Dim str As String = ""
        Try
            str = "try:"
            With dbc._staticConnect.CreateCommand
                sSelect = "select idPersonalCabinetRequest,state, email, account from personalcabinetrequest " _
                + "where needsendmessage=1"
                str += "get command text:"
                If dbc.Connect Then
                    .CommandText = sSelect
                    Dim dr As SqlDataReader
                    dr = .ExecuteReader
                    str += "execute reader:"
                    While dr.Read
                        str += "dr read:"
                        If CInt(dr.GetValue(1)) = 1 Then 'новая заявка 
                            SendMessageForRequest("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", "personal@gorgaz.kz", "Новая заявка", CreateMessageBodyForNewRequest(CStr(dr.GetValue(3))), "", CInt(dr.GetValue(0)), False, CInt(dr.GetValue(1)))
                            ' SendMessageForRequest("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", "apolivyanova@gorgaz.kz", "Новая заявка", CreateMessageBodyForNewRequest(CStr(dr.GetValue(3))), "", CInt(dr.GetValue(0)), False, CInt(dr.GetValue(1)))
                        End If
                        SendMessageForRequest("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", CStr(dr.GetValue(2)), "Статус заявки", CreateMessageBodyForRequest(CInt(dr.GetValue(1))), "", CInt(dr.GetValue(0)), True, CInt(dr.GetValue(1)))
                    End While
                End If
            End With
        Catch ex As Exception
            Try
                Tools.WriteErrLog("GetMailToSendMessageForRequest: " + str + ex.InnerException.ToString)
            Catch
                Tools.WriteErrLog("GetMailToSendMessageForRequest: " + str + ex.Message.ToString)
            End Try
        Finally
            dbc.DisConnect()
        End Try
    End Sub

    Public Sub GetMailToSendMessageForMessageAnswer()
        Dim dbc As New SQLConnect
        Dim sSelect As String
        Dim str As String = ""
        Try
            str = "try:"
            With dbc._staticConnect.CreateCommand
                sSelect = "select pcm.idPersonalCabinetMessage,pcm.MessageTitle,pcm.MessageBody, pc.pcemail, pcm.MessageType, pcm.account from personalcabinetmessage pcm " _
                + "inner join PersonalCabinet pc on pc.account=pcm.account " _
                + "where pcm.needsendmessage=1"
                str += "get command text:"
                If dbc.Connect Then
                    .CommandText = sSelect
                    Dim dr As SqlDataReader
                    dr = .ExecuteReader
                    str += "execute reader:"
                    While dr.Read
                        str += "dr read:"
                        If CInt(dr.GetValue(4)) = 1 Then 'отзыв 
                            SendMessageForMessageAnswer("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", "personal@gorgaz.kz", "Отзыв", CreateMessageBodyForMessageAnswer(CInt(dr.GetValue(4)), CStr(dr.GetValue(1)), CStr(dr.GetValue(2)), CStr(dr.GetValue(5))), "", CInt(dr.GetValue(0)))
                            SendMessageForMessageAnswer("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", "mc@gorgaz.kz", "Отзыв", CreateMessageBodyForMessageAnswer(CInt(dr.GetValue(4)), CStr(dr.GetValue(1)), CStr(dr.GetValue(2)), CStr(dr.GetValue(5))), "", CInt(dr.GetValue(0)))
                        Else
                            SendMessageForMessageAnswer("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", CStr(dr.GetValue(3)), CStr(dr.GetValue(1)), CreateMessageBodyForMessageAnswer(CInt(dr.GetValue(4)), CStr(dr.GetValue(1)), CStr(dr.GetValue(2)), CStr(dr.GetValue(5))), "", CInt(dr.GetValue(0)))
                        End If
                    End While
                End If
            End With
        Catch ex As Exception
            Try
                Tools.WriteErrLog("GetMailToSendMessageForRequest: " + str + ex.InnerException.ToString)
            Catch
                Tools.WriteErrLog("GetMailToSendMessageForRequest: " + str + ex.Message.ToString)
            End Try
        Finally
            dbc.DisConnect()
        End Try
    End Sub

    Public Sub UpdateContract(ByVal idpersonalcabinet As Integer)
        Dim dbc As New SQLConnect
        Try
            With dbc._staticConnect.CreateCommand
                .CommandText = "UPDATE personalcabinet SET pcpass = '' " _
                     + " WHERE idpersonalcabinet=" + idpersonalcabinet.ToString
                If dbc.Connect Then
                    .ExecuteNonQuery()
                    Tools.WriteLog("Временный пароль успешно сброшен - idpersonalcabinet=" + CStr(idpersonalcabinet))
                End If
            End With
        Catch ex As Exception
            Tools.WriteErrLog("UpdateContract:" + ex.Message.ToString)
        Finally
            dbc.DisConnect()
        End Try
    End Sub

    Public Sub UpdateRequest(ByVal idrequest As Integer)
        Dim dbc As New SQLConnect
        Try
            With dbc._staticConnect.CreateCommand
                .CommandText = "UPDATE personalcabinetrequest SET needsendmessage = 0 " _
                     + " WHERE idPersonalCabinetRequest=" + idrequest.ToString
                If dbc.Connect Then
                    .ExecuteNonQuery()
                    Tools.WriteLog("Заявка обновлена - idPersonalCabinetRequest=" + CStr(idrequest))
                End If
            End With
        Catch ex As Exception
            Tools.WriteErrLog("UpdateRequest:" + ex.Message.ToString)
        Finally
            dbc.DisConnect()
        End Try
    End Sub

    Public Sub UpdateMessage(ByVal idmessage As Integer)
        Dim dbc As New SQLConnect
        Try
            With dbc._staticConnect.CreateCommand
                .CommandText = "UPDATE personalcabinetmessage SET needsendmessage = 0 " _
                     + " WHERE idPersonalCabinetmessage=" + idmessage.ToString
                If dbc.Connect Then
                    .ExecuteNonQuery()
                    Tools.WriteLog("Сообщение обновлено - idPersonalCabinetMessage=" + CStr(idmessage))
                End If
            End With
        Catch ex As Exception
            Tools.WriteErrLog("UpdateMessage:" + ex.Message.ToString)
        Finally
            dbc.DisConnect()
        End Try
    End Sub

    Private Function CreateMessageBody(ByVal pass As String, ByVal acc As String) As String
        Dim str As String
        str = "Уважаемый абонент! <br>"
        str += "Сообщаем Вам регистрационные данные для входа в Личный Кабинет Абонента ТОО &quot;Горгаз-сервис&quot;. <br><br>"
        str += "Ссылка на вход: http://cabinet.gorgaz.kz:8890/ <br>"
        str += "Логин: <b>" + CStr(acc) + "</b> <br>"
        str += "Пароль: <b>" + CStr(pass) + "</b> <br><br>"
        str += "С наилучшими пожеланиями, ТОО &quot;Горгаз-сервис&quot; <br>"
        str += "Call-центр по приёму показаний - <b>630-400</b> "
        Return str
    End Function

    Private Function CreateMessageBodyForMessageAnswer(ByVal type As Integer, ByVal title As String, ByVal body As String, ByVal acc As String) As String
        Dim str As String
        str = ""
        Select Case type
            Case 1 'отзыв
                str += "Новый отзыв в Личном Кабинете Абонента: <br>"
                str += "Тема: " + title.ToString + " <br>"
                str += "Л/счет: " + acc.ToString + " <br>"
                str += "Сообщение: " + body.ToString + " <br><br> "
            Case 2 'ответ
                str = "Уважаемый абонент! <br>"
                str += body.ToString + " <br><br> "
        End Select
        str += "С наилучшими пожеланиями, ТОО &quot;Горгаз-сервис&quot; <br>"
        str += "Call-центр по приёму показаний - <b>630-400</b> "
        Return str
    End Function

    Private Function CreateMessageBodyForNewRequest(ByVal acc As String) As String
        Dim str As String
        str = "Внимание! <br>"
        str += "Поступила новая заявка на предоставление доступа в Личный Кабинет Абонента от " + acc
        Return str
    End Function

    Private Function CreateMessageBodyForRequest(ByVal state As Integer) As String
        Dim str As String
        str = "Уважаемый абонент! <br>"
        Select Case state
            Case 1 'оформлена
                str += "Ваша заявка на предоставление доступа в Личный Кабинет Абонента принята. <br>"
                str += "Мы сообщим Вам о результате в ближайшее время. <br><br>"
            Case 2 'одобрена, только удостоверение
                str += "Ваша заявка на предоставление доступа в Личный Кабинет Абонента подтверждена. <br>"
                str += "Для получения авторизационных данных Вам необходимо обратиться в Абонентский отдел ТОО &quot;Горгаз-сервис&quot. <br>"
                str += "При себе необходимо иметь оригинал и копию документа, удостоверяющего личность.<br>"
                str += "График работы: Пн-Пт 8:00-19:00, Сб 8:00-14:00.<br><br>"
            Case 3 'одобрена, все документы
                str += "Ваша заявка на предоставление доступа в Личный Кабинет Абонента подтверждена. <br>"
                str += "Для получения авторизационных данных Вам необходимо обратиться в Абонентский отдел ТОО &quot;Горгаз-сервис&quot. <br>"
                str += "При себе необходимо иметь:<br>"
                str += " 1) документ, удостоверяющий личность (оригинал и копия);<br>"
                str += " 2) правоустанавливающий документ на жилище (копия).<br>"
                str += "График работы: Пн-Пт 8:00-19:00, Сб 8:00-14:00.<br><br>"
            Case 4 'завершена
                str += "Ваша заявка на предоставление доступа в Личный Кабинет Абонента завершена. <br><br>"
        End Select
        str += "С наилучшими пожеланиями, ТОО &quot;Горгаз-сервис&quot; <br>"
        str += "Call-центр по приёму показаний - <b>630-400</b> "
        Return str
    End Function

    Private Sub bStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bStart.Click
        tmrStart.Interval = 60000 '1 минута
        tmrStart.Start()
        lblInfo.Text = "Запущено " + Now.ToString
        bStart.Enabled = False
        bStop.Enabled = True
    End Sub


    Private Sub bStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bStop.Click
        tmrStart.Stop()
        lblInfo.Text = "Остановлено " + Now.ToString
        bStart.Enabled = True
        bStop.Enabled = False
    End Sub

    Private Sub tmrStart_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrStart.Tick
        GetMailToSendMessageForLogin()
        GetMailToSendMessageForRequest()
        GetMailToSendMessageForMessageAnswer()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bStart_Click(Nothing, Nothing)
    End Sub
End Class
