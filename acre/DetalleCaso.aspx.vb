Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Drawing
Imports System.ComponentModel
Public Class DetalleCaso
    Inherits Page
    Dim connectionString = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Dim adminSPP As String = System.Configuration.ConfigurationManager.AppSettings("Admins")
    Dim idCaso As String
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
            If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                idCaso = Request.QueryString("ID")
                Session("NumCaso") = idCaso
                ValidaLogin(Session("User"))
                ValidaPermisoEnCaso(idCaso, valorDeLaCookie)
                llenaEstado()
                CargarDatos(idCaso)
                CargarComentarios(idCaso)

            Else
                '   lblResultado.Text = "No se encontró la variable en la URL."
            End If
        End If
        If lblCaso.Text = "SINCASO" Then
            Response.Redirect("frmDetalleCaso.aspx")
        End If

        SaveCookie(Session("User"))
    End Sub

    Sub SaveCookie(ByVal usr As String)
        Dim myCookie As New HttpCookie("usuarioCasos")
        myCookie.Value = usr
        myCookie.Expires = DateTime.Now.AddDays(7)
        Response.Cookies.Add(myCookie)
    End Sub
    Private Sub CargarDatos(ByVal idCaso As Integer)
        Dim query As String = "SELECT pprd_solicitud_codig, (select pprd_negocio_descr from pprd_negocio where pprd_negocio_codig = tbl.pprd_negocio_codig ) empresa,  (select pprd_modulo_descr from pprd_modulo  where pprd_modulo_codig = tbl.pprd_modulo_codig ) modulo,  (select pprd_sistemas_descr from pprd_sistemas where pprd_sistemas_codig = tbl.pprd_sistema_codig ) sistema, (select pprd_tiposolic_descr from pprd_tiposolic  where pprd_tiposolic_codig = tbl.pprd_tiposolic_codig )  tiposolic_codig, (select pprd_trabajador_nombre from pprd_trabajador where pprd_trabajador_codig = tbl.pprd_trabajador_dev )  trabajador_dev, (select pprd_trabajador_nombre from pprd_trabajador where pprd_trabajador_codig = tbl.pprd_trabajador_solicita )  trabajador_solicita,"
        query = query + " pprd_solicitud_nombrearch, pprd_solicitud_fecha, pprd_solicitud_estado,  (select pprd_solicestado_descr from pprd_solicestado where pprd_solicestado_codig = tbl.pprd_solicitud_estado )   solicitud_estado, pprd_solicitud_sql ,pprd_solicitud_obs      ,pprd_solicitud_commitImpl  "
        query = query + " ,pprd_solicitud_commitPlib, pprd_solicitud_commitPlan, pprd_solicitud_comparaBD, pprd_solicitud_scriptBD, pprd_solicitud_deployzip,  (select case when pprd_solicitud_casopadre = 0 then 'Sin caso padre' else (select convert(varchar(100), pprd_solicitud_codig) + ' - '+ pprd_solicitud_nombrearch from pprd_solicitud cp where cp.pprd_solicitud_codig = tbl.pprd_solicitud_casopadre ) end) pprd_solicitud_casopadre   "
        query = query + " ,  (select pprd_prioridad_descr from pprd_prioridad where pprd_prioridad_codig = tbl.pprd_prioridad_codig ) solicitud_prioridad  "
        query = query + " FROM pprd_solicitud tbl  WHERE pprd_solicitud_codig =  " & idCaso


        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then

                        lblFechaSol.Text = reader("pprd_solicitud_fecha").ToString()
                        lblCaso.Text = " " & reader("pprd_solicitud_codig").ToString()
                        lblEmpresa.Text = reader("empresa").ToString()
                        lblModulo.Text = reader("modulo").ToString()

                        lblSistema.Text = reader("sistema").ToString()
                        lblTipoSolic.Text = reader("tiposolic_codig").ToString()
                        lblDev.Text = reader("trabajador_dev").ToString()
                        lblSolicitadoPor.Text = reader("trabajador_solicita").ToString()
                        txtObs.Text = reader("pprd_solicitud_obs").ToString()

                        lblCommitImpr.Text = reader("pprd_solicitud_commitImpl").ToString()
                        lblCommitPlib.Text = reader("pprd_solicitud_commitPlib").ToString()
                        lblCommitPlan.Text = reader("pprd_solicitud_commitPlan").ToString()

                        chkReqSQL.Checked = reader("pprd_solicitud_sql").ToString()
                        chkCompareDB.Checked = reader("pprd_solicitud_comparaBD").ToString()
                        chkGenScript.Checked = reader("pprd_solicitud_scriptBD").ToString()
                        chkGenDeploy.Checked = reader("pprd_solicitud_deployzip").ToString()

                        lblNombreArchivo.Text = reader("pprd_solicitud_nombrearch").ToString()
                        lblEstado.Text = reader("solicitud_estado").ToString()
                        lblPrioridad.Text = reader("solicitud_prioridad").ToString()

                        lblEstado.ForeColor = Color.Black
                        Select Case reader("pprd_solicitud_estado")
                            Case 1
                                lblEstado.ForeColor = Color.Green
                            Case 2
                                lblEstado.ForeColor = Color.Green
                            Case 3
                                lblEstado.ForeColor = Color.Blue
                            Case 4
                                lblEstado.ForeColor = Color.Crimson
                            Case 5
                                lblEstado.ForeColor = Color.Navy
                            Case 6
                                lblEstado.ForeColor = Color.Navy
                        End Select
                        lblCasoPadre.Text = reader("pprd_solicitud_casopadre").ToString()
                    End If
                End Using
            End Using
        End Using
    End Sub
    Sub ValidaLogin(ByVal user As String)
        Dim consulta As String

        consulta = "SELECT usuario_tipo as campo from usuario Where usuario_id = '" & user & "'"
        If validaUnDato(consulta) = 1 Or validaUnDato(consulta) = 2 Or validaUnDato(consulta) = 4 Then
            Session("VerTodosCasos") = True
            pblEstado.Visible = True
            If validaUnDato(consulta) = 1 Then
                pblEstado.Visible = True
            Else
                pblEstado.Visible = False
            End If
        Else
            Session("VerTodosCasos") = False
            pblEstado.Visible = False
        End If
    End Sub
    Sub ValidaPermisoEnCaso(ByVal caso As Integer, ByVal user As String)
        Dim consulta As String
        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        consulta = "Select   COUNT(*) as campo from pprd_solicitud  WHERE pprd_solicitud_codig =  " & caso & " And pprd_trabajador_dev = '" & user & "' or pprd_trabajador_solicita = '" & user & "'  and pprd_solicitud_codig =  " & caso & "  and ( select usuario_tipo  from usuario where usuario_id  =  '" & user & "' ) not in (3) "
        If Session("VerTodosCasos") = False Then
            If validaUnDato(consulta) < 1 Then
                Response.Redirect("frmDetalleCaso.aspx")
            Else
            End If
        End If
    End Sub
    Sub llenaEstado()
        ddlFiltroEstado.Items.Clear()
        Dim query As String = " Select pprd_solicestado_codig, pprd_solicestado_descr from pprd_solicestado where 1=1    order by 2 asc"
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlFiltroEstado.DataSource = dataTable
        ddlFiltroEstado.DataTextField = "pprd_solicestado_descr"
        ddlFiltroEstado.DataValueField = "pprd_solicestado_codig"
        ddlFiltroEstado.DataBind()

    End Sub


    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        AtualizaCaso()
        Response.Redirect("frmDetalleCaso.aspx")
    End Sub

    Public Sub AtualizaCaso()

        Dim estado As Integer = ddlFiltroEstado.SelectedValue
        Dim requiereSql As Integer = 0
        Dim query As String = "UPDATE  pprd_solicitud SET pprd_solicitud_estado = " & estado & " where pprd_solicitud_codig = " & lblCaso.Text
        InsertSolicitud(query)

        If txtComentario.Text.Length > 25 Then
            Dim comentario As String = txtComentario.Text & " (Estado: " & ddlFiltroEstado.SelectedItem.Text & ")"
            query = "Insert Into pprd_soliccoment (pprd_solicitud_codig, pprd_soliccoment_comentario, pprd_soliccoment_fecha, pprd_soliccoment_estado) values (" & lblCaso.Text & ", '" & comentario & "', GetDate() , " & ddlFiltroEstado.SelectedValue & ")"
            InsertSolicitud(query)

            query = "Exec pprd_notificaCaso_comentario '" & lblCaso.Text & "',  '" & comentario & "', '" & ddlFiltroEstado.SelectedValue & "', '" & Session("User") & "'"
            InsertSolicitud(query)
        End If
        enviarCorreo("comentario")
    End Sub

    Protected Sub txtComentario_TextChanged(sender As Object, e As EventArgs) Handles txtComentario.TextChanged

        If txtComentario.Text.Length < 16 And txtComentario.Text <> "" Then
            lblAlert.Text = "Por favor ingrese un comentario más descriptivo."
            btnAceptar.Enabled = False
        End If
        If txtComentario.Text = "" Or txtComentario.Text.Length > 15 Then
            lblAlert.Text = ""
            btnAceptar.Enabled = True
        End If

    End Sub


    Private Sub CargarComentarios(ByVal id_Caso As Integer)
        Dim query As String = "SELECT convert(varchar(10), pprd_soliccoment_fecha, 103) Fecha, convert(varchar(5), pprd_soliccoment_fecha, 108) Hora, pprd_soliccoment_comentario Comentario, (Select pprd_solicestado_descr from pprd_solicestado where  pprd_soliccoment_estado = pprd_solicestado_codig ) Estado FROM pprd_soliccoment Where pprd_solicitud_codig = " & id_Caso
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Using adapter As New SqlDataAdapter(command)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    gvComentarios.DataSource = dt
                    gvComentarios.DataBind()
                End Using
            End Using
        End Using
    End Sub


    Private Sub enviarCorreo(ByVal coment As String)
        Dim correo As New CorreoHelper()
        ' Enviar correo sin adjunto
        Dim fechaHoraActual As DateTime = Now

        Dim sql = "  Select top 1 [pprd_correoHtml_para], [pprd_correoHtml_cc] , [pprd_correoHtml_bcc] , [pprd_correoHtml_subject], [pprd_correoHtml_body] , (select pprd_trabajador_nombre from pprd_trabajador where pprd_trabajador_codig  = [pprd_correoHtml_ingresadopor]) ingresador from  [pprd_correoHtml] Where pprd_solicitud_codig = '" & Session("NumCaso") & "' order by [pprd_correoHtml_codig] desc"
        Dim dt = ObtenerRegistros(sql)
        Dim para, copia, copiaoculta, asunto, cuerpo, ingresador As String

        For Each dr In dt.Rows
            para = (dr("pprd_correoHtml_para"))
            copia = (dr("pprd_correoHtml_cc"))
            copiaoculta = (dr("pprd_correoHtml_bcc"))
            asunto = (dr("pprd_correoHtml_subject"))
            cuerpo = (dr("pprd_correoHtml_body"))
            ingresador = (dr("ingresador"))
        Next

        'para = adminSPP
        Session("param_display_email") = "Sistema de pasos PRD"
        correo.EnviarCorreo(para, asunto, ingresador & " " & cuerpo & "<br />[" & fechaHoraActual.ToString() & "]", Session("param_display_email"), copia)
    End Sub



End Class

