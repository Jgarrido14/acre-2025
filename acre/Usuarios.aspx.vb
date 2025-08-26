Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Drawing
Public Class Usuarios
    Inherits Page
    Dim connectionString = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Dim qryMainUsers As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        pnlEditar.Visible = False
        pnlGrillaUsuarios.Visible = False
        pnlUsuario.Visible = False

        If Session("IsLogin") = False Then
            Response.Redirect("Login.aspx")
        End If

        If Session("SuperAdmin") = False Then
            Response.Redirect("Principal.aspx")
        End If
        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        Dim valorDeLaCookie As String
        If userCookie IsNot Nothing Then
            valorDeLaCookie = userCookie.Value
        End If

        If Session("User") Is Nothing And valorDeLaCookie Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        qryMainUsers = " select trabajador_codig as 'ID Usuario', trabajador_nombre as 'Nombre Usuario', trabajador_correo as 'E-mail Usuario', usuario_fecha_exp as 'Fecha exp. cuenta', (select usuariotipo_descr from usuariotipo where usuariotipo_codig = usuario_tipo)as  'Tipo de usuario', case when usuario_activo = 1 then 'Sí' else 'No' end as Activo  from trabajador t, usuario u where 1 = 1 and t.trabajador_codig = u.usuario_id and usuario_tipo <> 100 order by usuario_fecha_exp desc "
        Session("User") = valorDeLaCookie
        If Not IsPostBack Then
            llenaTipoTrabajador()
            llenaTipoUsuario()
            BindGrid()
        End If

        SaveCookie(Session("User"))
    End Sub

    Sub SaveCookie(ByVal usr As String)
        Dim myCookie As New HttpCookie("usuarioCasos")
        myCookie.Value = usr
        myCookie.Expires = DateTime.Now.AddDays(7)
        Response.Cookies.Add(myCookie)
    End Sub
    Private Sub BindGrid()
        verUsuarios()
    End Sub

    Protected Sub verUsuarios()
        Dim sql = qryMainUsers

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(sql, connection)
                Using adapter As New SqlDataAdapter(command)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Sub usua()
        Dim users As String
        users = TextToHex(Session("User") & "Sec12345")
    End Sub

    Public Function TextToHex(ByVal input As String) As String
        Dim hexOutput As New System.Text.StringBuilder()
        For Each c As Char In input
            ' Convierte cada carácter a su valor hexadecimal y lo agrega al resultado
            hexOutput.AppendFormat("{0:X2}", Convert.ToUInt16(c))
        Next
        Return hexOutput.ToString()
    End Function


    Public Sub IngresarTrabajador()

        Dim idusuario As String = txtUsuaID.Text
        Dim nombre As String = txtNombre.Text
        Dim correo As String = txtEmail.Text
        Dim trabactiv As Integer = 0
        If chkTrabActiv.Checked = True Then
            trabactiv = 1
        End If

        Dim tipotrabajador As String = ddlTipoTrabajador.SelectedValue

        Dim query As String = "  INSERT INTO trabajador values ('" & idusuario & "', '" & nombre & "', '" & correo & "', '" & tipotrabajador & "', " & trabactiv & ")"
        InsertSolicitud(query)

    End Sub
    Public Sub IngresarUsuario()
        Dim idusuario As String = txtUsuaID.Text
        Dim fechaexp As String = txtFechaexp.Text
        Dim usuaactiv As Integer = 0
        If chkUsuaActiv.Checked = True Then
            usuaactiv = 1
        End If
        Dim passw As String = TextToHex(txtUsuaID.Text & txtPassw.Text)
        Dim tipousuario As String = ddlTipoUsuario.SelectedValue
        Dim query As String = "  INSERT INTO usuario values ('" & idusuario & "', '" & passw & "', '" & usuaactiv & "', DATEADD(day, 60, GETDATE()), NULL, " & tipousuario & ")"
        InsertSolicitud(query)

    End Sub
    Protected Sub chkCreaCuenta_CheckedChanged(sender As Object, e As EventArgs) Handles chkCreaCuenta.CheckedChanged
        If chkCreaCuenta.Checked = True Then
            pnlUsuario.Visible = True
        Else
            pnlUsuario.Visible = False
        End If
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        IngresarTrabajador()
        If chkCreaCuenta.Checked = True Then
            IngresarUsuario()
        End If
        Response.Redirect("Principal.aspx")
    End Sub


    Private Sub llenaTipoTrabajador()

        pnlGrillaUsuarios.Visible = True
        pnlUsuario.Visible = True

        ddlTipoTrabajador.Items.Clear()
        ddlTipoTrabajador.DataBind()
        ddlTipoTrabajador.Items.Insert(0, New ListItem("Desarrollador", "dev"))
        ddlTipoTrabajador.Items.Insert(0, New ListItem("Jefe de proyectos", "jp"))
        ddlTipoTrabajador.Items.Insert(0, New ListItem("Soporte", "sop"))
        ddlTipoTrabajador.Items.Insert(0, New ListItem("Gestión", "ges"))
    End Sub

    Sub llenaTipoUsuario()
        ddlTipoUsuario.Items.Clear()
        Dim query As String = "exec getTiposUsuario " & SQL_CnvStr(Session("User"))
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlTipoUsuario.DataSource = dataTable
        ddlTipoUsuario.DataTextField = "usuariotipo_descr"
        ddlTipoUsuario.DataValueField = "usuariotipo_codig"
        ddlTipoUsuario.DataBind()

        ddlTipoUsuario_.Items.Clear()
        ddlTipoUsuario_.DataSource = dataTable
        ddlTipoUsuario_.DataTextField = "usuariotipo_descr"
        ddlTipoUsuario_.DataValueField = "usuariotipo_codig"
        ddlTipoUsuario_.DataBind()
        'ddlTipoUsuario.Items.Insert(0, New ListItem("Seleccionar", "0"))
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Protected Sub lnkVolver_Click(sender As Object, e As EventArgs) Handles lnkVolver.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        buscaUsuario()
    End Sub

    Private Sub buscaUsuario()
        lblAlertBuscar.Text = ""
        Dim usuarioID$ = txtUsuaID_.Text
        Dim query$ = "exec buscarUsuario " & SQL_CnvStr(usuarioID)
        query &= "," & SQL_CnvStr(Session("User"))
        Dim ds As DataSet = ObtenerTablas(query)
        Dim dt As DataTable = ds.Tables(0)
        Dim dt2 As DataTable = ds.Tables(1)
        If dt.Rows.Count > 0 Then
            txtUsuaID_.Text = dt.Rows(0)("id")
            txtNombre_.Text = dt.Rows(0)("nombre")
            txtEmail_.Text = dt.Rows(0)("correo")
            txtFechaexp_.Text = dt.Rows(0)("fecha_exp")
            ddlTipoUsuario_.SelectedValue = dt.Rows(0)("tipo")
            chkTrabActiv_.Checked = IIf(dt.Rows(0)("activo") = 1, True, False)
            lblAlertBuscar.Text = dt2.Rows(0)("msg")
        Else
            limpiarCamposBuscarUsuario()
            lblAlertBuscar.Text = "El usuario buscado no existe."
        End If



    End Sub

    Private Sub limpiarCamposBuscarUsuario()
        txtUsuaID_.Text = ""
        txtNombre_.Text = ""
        txtEmail_.Text = ""
        txtFechaexp_.Text = ""
        ddlTipoUsuario_.SelectedValue = 1
        chkTrabActiv_.Checked = False
    End Sub


    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        editarUsuario()
    End Sub

    Private Sub editarUsuario()
        lblAlertBuscar.Text = ""

        Dim idusuario$ = txtUsuaID_.Text
        Dim nombre$ = txtNombre_.Text
        Dim correo$ = txtEmail_.Text
        Dim trabactiv% = IIf(chkTrabActiv_.Checked, 1, 0)
        Dim tipousuario% = ddlTipoUsuario_.SelectedValue
        Dim query$ = "exec editarUsuario " & SQL_CnvStr(idusuario)
        query &= "," & SQL_CnvStr(nombre)
        query &= "," & SQL_CnvStr(correo)
        query &= "," & SQL_CnvNum(trabactiv)
        query &= "," & SQL_CnvNum(tipousuario)

        Dim dt As DataTable = ObtenerRegistros(query)

        lblAlertBuscar.Text = dt.Rows(0)("msg")
        limpiarCamposBuscarUsuario()
    End Sub

    Private Sub btnCancelar__Click(sender As Object, e As EventArgs) Handles btnCancelar_.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Protected Sub btnVerCrear_Click(sender As Object, e As EventArgs) Handles btnVerCrear.Click
        pnlEditar.Visible = False
        pblCrear.Visible = True
    End Sub

    Protected Sub btnVerModificar_Click(sender As Object, e As EventArgs) Handles btnVerModificar.Click
        pnlEditar.Visible = True
        pblCrear.Visible = False
    End Sub

    Private Sub BindData(Optional sortExpression As String = Nothing, Optional sortDirection As String = "ASC")
        Dim dt As DataTable = ObtenerRegistros(qryMainUsers)
        If Not String.IsNullOrEmpty(sortExpression) Then
            dt.DefaultView.Sort = sortExpression & " " & sortDirection
        End If
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindData(ViewState("SortExpression"), ViewState("SortDirection"))
    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs)
        Dim sortDirection As String = If(ViewState("SortDirection") Is Nothing, "ASC", ViewState("SortDirection").ToString())
        If ViewState("SortExpression") IsNot Nothing AndAlso ViewState("SortExpression").ToString() = e.SortExpression Then
            sortDirection = If(sortDirection = "ASC", "DESC", "ASC")
        End If
        ViewState("SortExpression") = e.SortExpression
        ViewState("SortDirection") = sortDirection
        BindData(e.SortExpression, sortDirection)
    End Sub
End Class

