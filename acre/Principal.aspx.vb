Imports System.Diagnostics

Public Class Principal
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        Dim valorDeLaCookie As String
        If userCookie IsNot Nothing Then
            valorDeLaCookie = userCookie.Value
        End If

        If Session("User") Is Nothing And valorDeLaCookie Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        Session("User") = valorDeLaCookie

        If Not IsPostBack Then
            ValidaLogin()
        End If
        SaveCookie(Session("User"))
    End Sub


    Sub SaveCookie(ByVal usr As String)
        Dim myCookie As New HttpCookie("usuarioCasos")
        myCookie.Value = usr
        myCookie.Expires = DateTime.Now.AddDays(7)
        Response.Cookies.Add(myCookie)
    End Sub
    Sub ValidaLogin()
        Dim consulta As String
        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        consulta = "SELECT usuario_tipo as campo from usuario Where usuario_id = '" & userCookie.Value & "'"

        'If validaUnDato(consulta) = 1 Then
        '    pnlPostLogin.Visible = True
        '    pnlAdmin.Visible = True
        '    pnlGenerar.Visible = True
        'Else
        '    pnlPostLogin.Visible = True
        '    pnlAdmin.Visible = False
        '    pnlGenerar.Visible = True
        'End If
        Session("IsLogin") = True
        Session("UserBak") = Session("User")
        Session("SuperAdmin") = False
        Select Case validaUnDato(consulta)
            Case 1
                pnlPostLogin.Visible = True
                pnlAdmin.Visible = False
               ' pnlGenerar.Visible = True
            Case 2
                pnlPostLogin.Visible = True
                pnlAdmin.Visible = False
                'pnlGenerar.Visible = False
            Case 3
                pnlPostLogin.Visible = True
                pnlAdmin.Visible = False
                'pnlGenerar.Visible = True
            Case 10
                pnlPostLogin.Visible = True
                pnlAdmin.Visible = False
                ' pnlGenerar.Visible = True
            Case 100
                pnlPostLogin.Visible = True
                pnlAdmin.Visible = True
                Session("SuperAdmin") = True
                ' pnlGenerar.Visible = True
        End Select

    End Sub

    Public Sub RestartAppPool(appPoolName As String)
        Try
            Dim processInfo As New ProcessStartInfo()
            processInfo.FileName = "cmd.exe"
            processInfo.Arguments = $"/C %windir%\system32\inetsrv\appcmd.exe stop apppool /apppool.name:""{appPoolName}"""
            processInfo.WindowStyle = ProcessWindowStyle.Hidden
            processInfo.CreateNoWindow = True
            processInfo.UseShellExecute = False

            Dim process As Process = Process.Start(processInfo)
            process.WaitForExit()
        Catch ex As Exception
            ' Manejar cualquier excepción
            Console.WriteLine("Error al reiniciar el Application Pool: " & ex.Message)
        End Try
    End Sub

End Class