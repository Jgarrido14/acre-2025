Imports System.Net
Imports System.Net.Mail

Public Class CorreoHelper
    Private smtpClient As SmtpClient
    Private correoRemitente As String
    Private contraseñaRemitente As String

    ' Constructor para inicializar los parámetros de conexión SMTP
    Public Sub New(Optional ByVal enableSsl As Boolean = True)
        Dim servidorSmtp = System.Configuration.ConfigurationManager.AppSettings("SmtpServer")
        Dim puerto = System.Configuration.ConfigurationManager.AppSettings("SmtpPort")
        Dim correoRemitente = System.Configuration.ConfigurationManager.AppSettings("SmtpEmail")
        Dim contraseña = System.Configuration.ConfigurationManager.AppSettings("SmtpPassword")
        smtpClient = New SmtpClient(servidorSmtp)
        smtpClient.Port = puerto
        smtpClient.Credentials = New NetworkCredential(correoRemitente, contraseña)
        smtpClient.EnableSsl = enableSsl
        Me.correoRemitente = correoRemitente
        Me.contraseñaRemitente = contraseña
    End Sub

    ' Método para enviar correo sin adjunto
    Public Sub EnviarCorreo(destinatario As String, asunto As String, mensaje As String, displayName$, Optional copia As String = "")
        Try
            Dim mail As New MailMessage(), ccList$ = ""
            mail.From = New MailAddress(correoRemitente, displayName$)
            mail.To.Add(destinatario)
            mail.Subject = asunto
            mail.Body = mensaje
            If copia <> "" Then
                mail.CC.Add(copia)
            End If

            mail.IsBodyHtml = True ' Cambiar a False si no se desea enviar en formato HTML
            smtpClient.Send(mail)
            Console.WriteLine("Correo enviado correctamente.")
        Catch ex As Exception
            Console.WriteLine("Error al enviar el correo: " & ex.Message)
        End Try
    End Sub

    ' Método para enviar correo con adjunto
    Public Sub EnviarCorreoConAdjunto(destinatario As String, asunto As String, mensaje As String, rutaAdjunto As String)
        Try
            Dim mail As New MailMessage()
            mail.From = New MailAddress(correoRemitente)
            mail.To.Add(destinatario)
            mail.Subject = asunto
            mail.Body = mensaje
            mail.IsBodyHtml = True

            ' Agregar el archivo adjunto
            If Not String.IsNullOrEmpty(rutaAdjunto) Then
                Dim attachment As New Attachment(rutaAdjunto)
                mail.Attachments.Add(attachment)
            End If

            smtpClient.Send(mail)
            Console.WriteLine("Correo con adjunto enviado correctamente.")
        Catch ex As Exception
            Console.WriteLine("Error al enviar el correo con adjunto: " & ex.Message)
        End Try
    End Sub
End Class
