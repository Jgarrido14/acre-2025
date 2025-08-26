Imports System.Web.Services

Public Class SaveCookie
    Inherits Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    <WebMethod()>
    Public Shared Function SaveCookieToSession(ByVal cookieValue As String) As String
        ' Guarda el valor de la cookie en la sesión
        HttpContext.Current.Session("usuarioCasos") = cookieValue
        Return "Cookie guardada en la sesión"
    End Function

End Class

