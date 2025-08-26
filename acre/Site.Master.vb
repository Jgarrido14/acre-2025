Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        Dim valorDeLaCookie As String
        If userCookie IsNot Nothing Then
            valorDeLaCookie = userCookie.Value
        End If
        Session("User") = valorDeLaCookie
        If Session("User") Is Nothing And valorDeLaCookie Is Nothing Then

            PnlMenu.Visible = False
        End If
        If Session("User") Is Nothing Or Session("User") = "" Then
            lnkLogout.Text = ""
        Else
            lnkLogout.Text = "Salir de (" & Session("User").ToString() & ")"
        End If
        SaveCookie(Session("User"))
    End Sub

    Sub SaveCookie(ByVal usr As String)
        Dim myCookie As New HttpCookie("usuarioCasos")
        myCookie.Value = usr
        myCookie.Expires = DateTime.Now.AddDays(7)
        Response.Cookies.Add(myCookie)
    End Sub

    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs) Handles lnkLogout.Click
        Response.Redirect("LogOut.aspx")
    End Sub
End Class