Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Public Class Logout
    Inherits Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Session("User") = Nothing

        Dim myCookie As New HttpCookie("usuarioCasos")
        myCookie.Expires = DateTime.Now.AddDays(-1)
        Response.Cookies.Add(myCookie)
        Response.Redirect("Login.aspx")
    End Sub

End Class

