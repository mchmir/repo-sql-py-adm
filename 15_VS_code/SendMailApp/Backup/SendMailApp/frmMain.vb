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
            Tools.WriteLog("������ �� " + mailto + " ������� ���������� - idpersonalcabinet=" + CStr(idcontract))
            UpdateContract(idcontract)
        Catch ex As Exception
            Tools.WriteErrLog("SendMessage: " + mailto + " - idpersonalcabinet -" + CStr(idcontract) + " - " + ex.Message.ToString)
        End Try
    End Sub

    Private Sub SendMessageForRequest(ByVal smtpServer As String, ByVal from As String, ByVal password As String, ByVal mailto As String, ByVal caption As String, ByVal message As String, ByVal attachFile As String, ByVal idrequest As Integer, ByVal needupdreq As Boolean, ByVal state As Integer)
        Try
            If mailto.Length = 0 Then
                UpdateRequest(idrequest)
                Tools.WriteErrLog("SendMessageForRequest: �� ������ ����� ����������� �����" + CStr(idrequest))
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
                Tools.WriteLog("������ �� " + mailto + " ������� ����������: idrequest=" + CStr(idrequest) + "; state=" + CStr(state))
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
            Tools.WriteLog("������ �� " + mailto + " ������� ����������: idpersonalcabinetmessage=" + CStr(idmessage))
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
                        SendMessage("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", CStr(dr.GetValue(2)), "������ ��� ����� � ������ ������� ��������", CreateMessageBody(CStr(dr.GetValue(1)), CStr(dr.GetValue(0))), "", CInt(dr.GetValue(3)))
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
                        If CInt(dr.GetValue(1)) = 1 Then '����� ������ 
                            SendMessageForRequest("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", "personal@gorgaz.kz", "����� ������", CreateMessageBodyForNewRequest(CStr(dr.GetValue(3))), "", CInt(dr.GetValue(0)), False, CInt(dr.GetValue(1)))
                            ' SendMessageForRequest("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", "apolivyanova@gorgaz.kz", "����� ������", CreateMessageBodyForNewRequest(CStr(dr.GetValue(3))), "", CInt(dr.GetValue(0)), False, CInt(dr.GetValue(1)))
                        End If
                        SendMessageForRequest("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", CStr(dr.GetValue(2)), "������ ������", CreateMessageBodyForRequest(CInt(dr.GetValue(1))), "", CInt(dr.GetValue(0)), True, CInt(dr.GetValue(1)))
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
                        If CInt(dr.GetValue(4)) = 1 Then '����� 
                            SendMessageForMessageAnswer("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", "personal@gorgaz.kz", "�����", CreateMessageBodyForMessageAnswer(CInt(dr.GetValue(4)), CStr(dr.GetValue(1)), CStr(dr.GetValue(2)), CStr(dr.GetValue(5))), "", CInt(dr.GetValue(0)))
                            SendMessageForMessageAnswer("gg-mail.gorgaz.kz", "online@gorgaz.kz", "TrablGate1$", "mc@gorgaz.kz", "�����", CreateMessageBodyForMessageAnswer(CInt(dr.GetValue(4)), CStr(dr.GetValue(1)), CStr(dr.GetValue(2)), CStr(dr.GetValue(5))), "", CInt(dr.GetValue(0)))
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
                    Tools.WriteLog("��������� ������ ������� ������� - idpersonalcabinet=" + CStr(idpersonalcabinet))
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
                    Tools.WriteLog("������ ��������� - idPersonalCabinetRequest=" + CStr(idrequest))
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
                    Tools.WriteLog("��������� ��������� - idPersonalCabinetMessage=" + CStr(idmessage))
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
        str = "��������� �������! <br>"
        str += "�������� ��� ��������������� ������ ��� ����� � ������ ������� �������� ��� &quot;������-������&quot;. <br><br>"
        str += "������ �� ����: http://cabinet.gorgaz.kz:8890/ <br>"
        str += "�����: <b>" + CStr(acc) + "</b> <br>"
        str += "������: <b>" + CStr(pass) + "</b> <br><br>"
        str += "� ���������� �����������, ��� &quot;������-������&quot; <br>"
        str += "Call-����� �� ����� ��������� - <b>630-400</b> "
        Return str
    End Function

    Private Function CreateMessageBodyForMessageAnswer(ByVal type As Integer, ByVal title As String, ByVal body As String, ByVal acc As String) As String
        Dim str As String
        str = ""
        Select Case type
            Case 1 '�����
                str += "����� ����� � ������ �������� ��������: <br>"
                str += "����: " + title.ToString + " <br>"
                str += "�/����: " + acc.ToString + " <br>"
                str += "���������: " + body.ToString + " <br><br> "
            Case 2 '�����
                str = "��������� �������! <br>"
                str += body.ToString + " <br><br> "
        End Select
        str += "� ���������� �����������, ��� &quot;������-������&quot; <br>"
        str += "Call-����� �� ����� ��������� - <b>630-400</b> "
        Return str
    End Function

    Private Function CreateMessageBodyForNewRequest(ByVal acc As String) As String
        Dim str As String
        str = "��������! <br>"
        str += "��������� ����� ������ �� �������������� ������� � ������ ������� �������� �� " + acc
        Return str
    End Function

    Private Function CreateMessageBodyForRequest(ByVal state As Integer) As String
        Dim str As String
        str = "��������� �������! <br>"
        Select Case state
            Case 1 '���������
                str += "���� ������ �� �������������� ������� � ������ ������� �������� �������. <br>"
                str += "�� ������� ��� � ���������� � ��������� �����. <br><br>"
            Case 2 '��������, ������ �������������
                str += "���� ������ �� �������������� ������� � ������ ������� �������� ������������. <br>"
                str += "��� ��������� ��������������� ������ ��� ���������� ���������� � ����������� ����� ��� &quot;������-������&quot. <br>"
                str += "��� ���� ���������� ����� �������� � ����� ���������, ��������������� ��������.<br>"
                str += "������ ������: ��-�� 8:00-19:00, �� 8:00-14:00.<br><br>"
            Case 3 '��������, ��� ���������
                str += "���� ������ �� �������������� ������� � ������ ������� �������� ������������. <br>"
                str += "��� ��������� ��������������� ������ ��� ���������� ���������� � ����������� ����� ��� &quot;������-������&quot. <br>"
                str += "��� ���� ���������� �����:<br>"
                str += " 1) ��������, �������������� �������� (�������� � �����);<br>"
                str += " 2) �������������������� �������� �� ������ (�����).<br>"
                str += "������ ������: ��-�� 8:00-19:00, �� 8:00-14:00.<br><br>"
            Case 4 '���������
                str += "���� ������ �� �������������� ������� � ������ ������� �������� ���������. <br><br>"
        End Select
        str += "� ���������� �����������, ��� &quot;������-������&quot; <br>"
        str += "Call-����� �� ����� ��������� - <b>630-400</b> "
        Return str
    End Function

    Private Sub bStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bStart.Click
        tmrStart.Interval = 60000 '1 ������
        tmrStart.Start()
        lblInfo.Text = "�������� " + Now.ToString
        bStart.Enabled = False
        bStop.Enabled = True
    End Sub


    Private Sub bStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bStop.Click
        tmrStart.Stop()
        lblInfo.Text = "����������� " + Now.ToString
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
