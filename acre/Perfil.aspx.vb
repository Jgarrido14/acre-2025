Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Drawing
Imports System.Net.Mime.MediaTypeNames
Imports System.Text.RegularExpressions

Public Class Perfil
    Inherits Page
    Dim connectionString = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
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
            CargarDatos(Session("User"))
        End If
        SaveCookie(Session("User"))
    End Sub

    Sub SaveCookie(ByVal usr As String)
        Dim myCookie As New HttpCookie("usuarioCasos")
        myCookie.Value = usr
        myCookie.Expires = DateTime.Now.AddDays(7)
        Response.Cookies.Add(myCookie)
    End Sub


    Public Function TextToHex(ByVal input As String) As String
        Dim hexOutput As New System.Text.StringBuilder()
        For Each c As Char In input
            ' Convierte cada carácter a su valor hexadecimal y lo agrega al resultado
            hexOutput.AppendFormat("{0:X2}", Convert.ToUInt16(c))
        Next
        Return hexOutput.ToString()
    End Function


    'Public Sub IngresarTrabajador()

    '    Dim idusuario As String = txtUsuaID.Text
    '    Dim nombre As String = txtNombre.Text
    '    Dim correo As String = txtEmail.Text
    '    Dim trabactiv As Integer = 0
    '    If chkTrabActiv.Checked = True Then
    '        trabactiv = 1
    '    End If

    '    Dim query As String = "  INSERT INTO trabajador values ('" & idusuario & "', '" & nombre & "', '" & correo & "', '" & tipotrabajador & "', " & trabactiv & ")"
    '    InsertSolicitud(query)

    'End Sub
    'Public Sub IngresarUsuario()
    '    Dim idusuario As String = txtUsuaID.Text
    '    Dim fechaexp As String = txtFechaexp.Text
    '    Dim usuaactiv As Integer = 0
    '    If chkUsuaActiv.Checked = True Then
    '        usuaactiv = 1
    '    End If
    '    Dim passw As String = TextToHex(txtUsuaID.Text & txtPassw.Text)
    '    Dim tipousuario As String = ddlTipoUsuario.SelectedValue
    '    Dim query As String = "  INSERT INTO usuario values ('" & idusuario & "', '" & passw & "', '" & usuaactiv & "', DATEADD(day, 60, GETDATE()), NULL, " & tipousuario & ")"
    '    InsertSolicitud(query)
    'End Sub

    Public Sub UpdaterUsuario()
        Dim idusuario As String = txtUsuaID.Text
        Dim usuaactiv As Integer = 0
        Dim passw As String = TextToHex(txtUsuaID.Text & txtPasswNew.Text)
        Dim query As String = " UPDATE usuario SET usuario_pass =  '" & passw & "' WHERE usuario_id = '" & idusuario & "' ; "
        query = query & " UPDATE usuario SET usuario_fecha_exp =  DATEADD(day, 60, GETDATE()) WHERE usuario_id = '" & idusuario & "' "
        InsertSolicitud(query)
    End Sub
    Function ValidaPassword() As Boolean
        lblAlert.ForeColor = Color.Crimson
        If TextToHex(txtUsuaID.Text & txtPassw.Text) <> Session("OLDPASSWORD") Then
            lblAlert.Text = "Su password actual ingresado, no es correcto."
            Return False
        End If
        If txtPasswNew.Text <> txtPasswNewRep.Text Then
            lblAlert.Text = "Su password nueva no coincide."
            Return False
        End If
        If txtPasswNew.Text = txtPassw.Text Or txtPasswNewRep.Text = txtPassw.Text Then
            lblAlert.Text = "Su nueva password no puede ser igual a la actual."
            Return False
        End If
        If Not ValidarLetrasYNumeros(txtPasswNew.Text) Then
            lblAlert.Text = "Su password debe tener letras y números"
            Return False
        End If
        If txtPasswNew.Text.Length < 9 Then
            lblAlert.Text = "Su nueva password es muy corta."
            Return False
        End If
        Return True
    End Function

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If ValidaPassword() Then
            UpdaterUsuario()
            Response.Redirect("Principal.aspx")
        End If

    End Sub


    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Protected Sub lnkVolver_Click(sender As Object, e As EventArgs) Handles lnkVolver.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Private Sub CargarDatos(ByVal user As String)
        Dim queryTrab As String = " select  trabajador_codig, trabajador_nombre, trabajador_correo  from trabajador where trabajador_codig = '" & user & "' "
        Dim queryUser As String = " select usuario_id, usuario_pass, usuario_fecha_exp, (select usuariotipo_descr from usuariotipo where usuariotipo_codig = usuario_tipo ) as tipo from usuario where usuario_id = '" & user & "' "

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(queryTrab, connection)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        txtUsuaID.Text = reader("trabajador_codig").ToString()
                        txtNombre.Text = reader("trabajador_nombre").ToString()
                        txtEmail.Text = reader("trabajador_correo").ToString()
                    End If
                End Using
            End Using
        End Using

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(queryUser, connection)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Session("OLDPASSWORD") = reader("usuario_pass").ToString()
                        txtTipoUsuario.Text = reader("tipo").ToString()
                        txtFechaexp.Text = reader("usuario_fecha_exp").ToString()
                    End If
                End Using
            End Using
        End Using
    End Sub


    Public Function ValidarLetrasYNumeros(texto As String) As Boolean
        Dim tieneLetras As Boolean = Regex.IsMatch(texto, "[A-Za-z]")
        Dim tieneNumeros As Boolean = Regex.IsMatch(texto, "\d")
        Return tieneLetras And tieneNumeros
    End Function
End Class

