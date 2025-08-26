Imports System.Data.SqlClient

Public Class frmDetalleCaso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        Dim valorDeLaCookie As String
        If userCookie IsNot Nothing Then
            valorDeLaCookie = userCookie.Value
        End If

        If Session("User") Is Nothing And valorDeLaCookie Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
    End Sub





End Class