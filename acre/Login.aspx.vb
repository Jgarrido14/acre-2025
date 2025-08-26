Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Public Class Login
    Inherits Page
    Dim token As String = "myGeneratedToken123"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'cantRegistros = 10
        Session("IsLogin") = False
        If Not IsPostBack Then
            ' usua()
        End If
    End Sub

    Sub usua()
        Dim users As String
        users = TextToHex(Session("USUARIO") & "Sec12345")
    End Sub
    Public Function TextToHex(ByVal input As String) As String
        Dim hexOutput As New System.Text.StringBuilder()
        For Each c As Char In input
            ' Convierte cada carácter a su valor hexadecimal y lo agrega al resultado
            hexOutput.AppendFormat("{0:X2}", Convert.ToUInt16(c))
        Next
        Return hexOutput.ToString()
    End Function

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click

        Dim username As String = txtUser.Text
        Dim password As String = txtPass.Text

        Dim getPassw As String = ValidaPassword(username)

        If ValidaUsuario(username) > 0 Then
            If TextToHex(username & password) = ValidaPassword(username) Then
                Dim myCookie As New HttpCookie("usuarioCasos")
                myCookie.Value = username
                myCookie.Expires = DateTime.Now.AddDays(7)
                Response.Cookies.Add(myCookie)
                Session("User") = username
                LoginOK(Session("User"))
                Response.Redirect("Principal.aspx")
            Else
                lblAlert.Text = "Error de clave."

            End If
        Else
            lblAlert.text = "Usuario no encontrado."
            Exit Sub
        End If

    End Sub

    Sub LoginOK(ByVal usuario As String)
        Dim Query As String
        Dim valorCookie As String = Request.Cookies("ASP.NET_SessionId").Value
        Session("Usuario_cookie") = valorCookie
        Query = "Insert Into session (session_inicia, session_usuario, session_cierra, session_cookie) values (GetDate(), '" & usuario & "', NULL, '" & valorCookie & "'   )"
        InsertSolicitud(Query)
    End Sub
End Class

