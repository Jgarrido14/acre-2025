Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Drawing
Public Class Contact
    Inherits Page

    Dim adminSPP As String = System.Configuration.ConfigurationManager.AppSettings("Admins")
    Dim AppHostParse = System.Configuration.ConfigurationManager.AppSettings("AppHostParse")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        Dim valorDeLaCookie As String
        If userCookie IsNot Nothing Then
            valorDeLaCookie = userCookie.Value
        End If

        If Session("User") Is Nothing And valorDeLaCookie Is Nothing Then
            Response.Redirect("Login.aspx")
        End If


        If Not IsPostBack Then
            llenaEmpresa()
            llenaModulo()
            llenaTipo()
            LlenaDev()
            LlenaSistema()
            LlenaSolicita()
            LlenaPrioridad()
            ddlDev.SelectedValue = Session("User")
            ValidaLogin()
        Else
            Session("User") = valorDeLaCookie

        End If
        SaveCookie(Session("User"))
    End Sub

    Sub SaveCookie(ByVal usr As String)
        Dim myCookie As New HttpCookie("usuarioCasos")
        myCookie.Value = usr
        myCookie.Expires = DateTime.Now.AddDays(7)
        Response.Cookies.Add(myCookie)
    End Sub

    Sub llenaEmpresa()
        ddlEmpresa.Items.Clear()
        Dim query As String = " select pprd_negocio_codig,  pprd_negocio_descr from pprd_negocio where pprd_negocio_activ = 1 order by 2 asc"
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlEmpresa.DataSource = dataTable
        ddlEmpresa.DataTextField = "pprd_negocio_descr"
        ddlEmpresa.DataValueField = "pprd_negocio_codig"
        ddlEmpresa.DataBind()
        ddlEmpresa.Items.Insert(0, New ListItem("Seleccione una opción", "0"))
    End Sub
    Sub llenaModulo()
        ddlModulo.Items.Clear()
        Dim query As String = "select pprd_modulo_codig,  pprd_modulo_descr from pprd_modulo where pprd_modulo_activ = 1 order by 2 asc"
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlModulo.DataSource = dataTable
        ddlModulo.DataTextField = "pprd_modulo_descr"
        ddlModulo.DataValueField = "pprd_modulo_codig"
        ddlModulo.DataBind()
        ddlModulo.Items.Insert(0, New ListItem("Seleccione una opción", "0"))
    End Sub
    Sub llenaTipo()
        ddlTipo.Items.Clear()
        Dim query As String = "  select pprd_tiposolic_codig,  pprd_tiposolic_descr from pprd_tiposolic where pprd_tiposolic_activ = 1 order by 2 asc"
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlTipo.DataSource = dataTable
        ddlTipo.DataTextField = "pprd_tiposolic_descr"
        ddlTipo.DataValueField = "pprd_tiposolic_codig"
        ddlTipo.DataBind()
        ddlTipo.Items.Insert(0, New ListItem("Seleccione una opción", "0"))
    End Sub
    Private Sub LlenaDev()
        ddlDev.Items.Clear()
        Dim query As String = "select pprd_trabajador_codig, pprd_trabajador_nombre from pprd_trabajador where pprd_trabajador_activ = 1 and pprd_trabajador_tipo in ('dev', 'ges') order by 2 asc"
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlDev.DataSource = dataTable
        ddlDev.DataTextField = "pprd_trabajador_nombre"
        ddlDev.DataValueField = "pprd_trabajador_codig"
        ddlDev.DataBind()
        ddlDev.Items.Insert(0, New ListItem("Seleccione una opción", "0"))
    End Sub
    Private Sub LlenaSolicita()
        ddlSolicita.Items.Clear()
        Dim query As String = "select pprd_trabajador_codig, pprd_trabajador_nombre from pprd_trabajador where pprd_trabajador_activ = 1  order by 2 asc"
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlSolicita.DataSource = dataTable
        ddlSolicita.DataTextField = "pprd_trabajador_nombre"
        ddlSolicita.DataValueField = "pprd_trabajador_codig"
        ddlSolicita.DataBind()
        ddlSolicita.Items.Insert(0, New ListItem("Seleccione una opción", "0"))
    End Sub
    Private Sub LlenaSistema()
        ddlSistemas.Items.Clear()
        Dim query As String = "select pprd_sistemas_codig,  pprd_sistemas_descr from pprd_sistemas where pprd_sistemas_activ = 1 order by 2 asc"
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlSistemas.DataSource = dataTable
        ddlSistemas.DataTextField = "pprd_sistemas_descr"
        ddlSistemas.DataValueField = "pprd_sistemas_codig"
        ddlSistemas.DataBind()
        ddlSistemas.Items.Insert(0, New ListItem("Seleccione una opción", "0"))
    End Sub
    Private Sub LlenaPrioridad()
        ddlPrioridad.Items.Clear()
        Dim query As String = "select pprd_prioridad_codig, pprd_prioridad_descr from pprd_prioridad order by 1 asc "
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlPrioridad.DataSource = dataTable
        ddlPrioridad.DataTextField = "pprd_prioridad_descr"
        ddlPrioridad.DataValueField = "pprd_prioridad_codig"
        ddlPrioridad.DataBind()
        ' ddlSistemas.Items.Insert(0, New ListItem("Seleccione una opción", "0"))
        ddlPrioridad.SelectedValue = 3
    End Sub
    Function ValidaCombos() As Boolean


        If ddlEmpresa.SelectedValue = "0" Then
            lblAlert.Text = "Debe seleccionar empresa."
            Return False
        End If
        If ddlModulo.SelectedValue = "0" Then
            lblAlert.Text = "Debe seleccionar módulo."
            Return False
        End If
        If ddlSistemas.SelectedValue = "0" Then
            lblAlert.Text = "Debe seleccionar sistema."
            Return False
        End If
        If ddlTipo.SelectedValue = "0" Then
            lblAlert.Text = "Debe seleccionar tipo de solicitud."
            Return False
        End If
        If ddlDev.SelectedValue = "0" Then
            lblAlert.Text = "Debe seleccionar desarrollador."
            Return False
        End If
        If ddlSolicita.SelectedValue = "0" Then
            lblAlert.Text = "Debe seleccionar quién solicitó el requerimiento."
            Return False
        End If
        'If ddlPrioridad.SelectedValue = "0" Then
        '    lblAlert.Text = "Debe seleccionar prioridad."
        '    Return False
        'End If
        If txtObs.Text.Length < 30 Then
            lblAlert.Text = "Ingresar un detalle válido."
            Return False
        End If
        lblAlert.Text = "Agregando caso..."


        If txtCommitImpl.Text.Length < 5 Then
            lblAlert.Text = "Ingresar Commit de Implementación."
            Return False
        End If
        If chkPlib.Checked = True And txtCommitPlib.Text.Length < 5 Then
            lblAlert.Text = "Ingresar Commit de Plib Núcleo."
            Return False
        End If
        If chkPlan.Checked = True And txtCommitPlan.Text.Length < 5 Then
            lblAlert.Text = "Ingresar Commit de Plan Núcleo."
            Return False
        End If

        If chkReqSQL.Checked = True Then
            If chkCompareDB.Checked = False Then
                lblAlert.Text = "Debe realizar comparación de BD si hay cambios para Producción."
                Return False
            End If

            If chkGenScript.Checked = False Then
                lblAlert.Text = "Debe generar Script de BD si hay cambios para Producción."
                Return False
            End If
        End If
        If chkGenDeploy.Checked = False Then
            lblAlert.Text = "Debe comprimir Deploy en .Zip para procesar."
            Return False
        End If

        Dim fileExtension As String = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower()
        If fileExtension <> ".zip" Then
            lblAlert.Text = "Debe comprimir Deploy en .Zip para procesar."
            Return False
        End If

        If ddlTipo.SelectedValue = "Fix" And ddlCasoPadre.SelectedValue = "0" Then
            lblAlert.Text = "Debe seleccionar el caso padre de la correción."
            Return False
        End If

        Return True
    End Function
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        lblAlert.ForeColor = Color.Crimson
        If ValidaCombos() Then
            If FileUpload1.HasFile Then
                Try
                    Dim savePath As String = Server.MapPath("~/" & GetEmpresa() & "/")
                    If Not Directory.Exists(savePath) Then
                        Directory.CreateDirectory(savePath)
                    End If
                    Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                    If ddlEmpresa.SelectedValue = "VSPT" Then
                        Dim RutaVspt = "\\PLAPCI-WEHV\App_PasosPRD"
                        Dim rutaScript As String = savePath + "eliminaVSPT.cmd"

                        Dim origen As String = savePath & fileName
                        origen = "" & origen & ""
                        Dim cmdCopy As String = "copy /Y """ & origen & """ ""\\PLAPCI-WEHV\App_PasosPRD"""

                        FileUpload1.SaveAs(Path.Combine(savePath, fileName))

                        Process.Start("cmd", cmdCopy)


                    Else
                        FileUpload1.SaveAs(Path.Combine(savePath, fileName))
                    End If
                    ' IngresarSolicitud(fileName)
                    lblAlert.ForeColor = Color.Green
                    Dim fechaHoraActual As DateTime = Now
                    lblAlert.Text = "Solicitud ingresada correctamente. [" & fechaHoraActual.ToString() & "]"
                Catch ex As Exception
                    lblAlert.Text = "El archivo no se pudo subir. Error: " & ex.Message
                End Try
            Else
                lblAlert.Text = "Por favor seleccione un archivo para subir."
            End If
        Else

        End If

    End Sub
    Function GetEmpresa() As String
        Dim carpetaDestino As String
        carpetaDestino = GetDatabaseValue(ddlEmpresa.SelectedValue)
        Return carpetaDestino
    End Function


    Public Sub IngresarSolicitud(ByVal nombreArchivo As String)

        Dim empresa As String = ddlEmpresa.SelectedValue
        Dim modulo As String = ddlModulo.SelectedValue
        Dim sistema As String = ddlSistemas.SelectedValue
        Dim tiposolicitud As String = ddlTipo.SelectedValue
        Dim dev As String = ddlDev.SelectedValue
        Dim solicita As String = ddlSolicita.SelectedValue
        Dim priori As String = ddlPrioridad.SelectedValue


        Dim filemane As String = nombreArchivo
        Dim requiereSql As Integer = 0
        If chkReqSQL.Checked = True Then
            requiereSql = 1
        End If
        Dim obs As String = txtObs.Text


        Dim comparBD As Integer = 0
        If chkReqSQL.Checked = True Then
            comparBD = 1
        End If
        Dim generaBD As Integer = 0
        If chkReqSQL.Checked = True Then
            generaBD = 1
        End If
        Dim generaZip As Integer = 0
        If chkReqSQL.Checked = True Then
            generaZip = 1
        End If

        Dim commitImpl As String = txtCommitImpl.Text
        Dim commitPlib As String = txtCommitPlib.Text
        Dim commitPlan As String = txtCommitPlan.Text
        Dim casopadre As Integer = 0
        If ddlCasoPadre.SelectedValue <> "" Then
            casopadre = ddlCasoPadre.SelectedValue
        End If


        Dim query As String = "INSERT INTO pprd_solicitud (pprd_negocio_codig, pprd_modulo_codig, pprd_sistema_codig, pprd_tiposolic_codig, pprd_trabajador_dev, pprd_trabajador_solicita, pprd_solicitud_nombrearch, pprd_solicitud_fecha, pprd_solicitud_estado, pprd_solicitud_sql, pprd_solicitud_obs, pprd_solicitud_comparaBD, pprd_solicitud_scriptBD, pprd_solicitud_deployzip, pprd_solicitud_commitImpl, pprd_solicitud_commitPlib, pprd_solicitud_commitPlan, pprd_solicitud_casopadre, pprd_prioridad_codig, usuario_ingresa) "
        query = query & "  VALUES ('" & empresa & "','" & modulo & "','" & sistema & "','" & tiposolicitud & "','" & dev & "','" & solicita & "','" & filemane & "', GetDate(), 1, " & requiereSql & ", '" & obs & "', " & comparBD & ", " & generaBD & ", " & generaZip & ", '" & commitImpl & "', '" & commitPlib & "', '" & commitPlan & "', " & casopadre & ", " & priori & ", '" & Session("User") & "' )"

        InsertSolicitud(query)

        If AppHostParse = 1 Then
            enviarCorreo(filemane)
        End If
    End Sub

    Protected Sub chkReqSQL_CheckedChanged(sender As Object, e As EventArgs) Handles chkReqSQL.CheckedChanged
        If chkReqSQL.Checked = True Then
            chkGenScript.Enabled = True
            chkCompareDB.Enabled = True
        Else
            chkGenScript.Enabled = False
            chkCompareDB.Enabled = False
            chkGenScript.Checked = False
            chkCompareDB.Checked = False
        End If
    End Sub

    Protected Sub lnkVolver_Click(sender As Object, e As EventArgs) Handles lnkVolver.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Sub ValidaLogin()
        Dim consulta As String
        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        consulta = "Select usuario_tipo As campo from usuario Where usuario_id = '" & userCookie.Value & "'"
        If validaUnDato(consulta) = 1 Then
            Session("UserAdmin") = True
            ddlDev.Enabled = True
        Else
            Session("UserAdmin") = False
            ddlDev.Enabled = False
        End If
    End Sub

    Protected Sub ddlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipo.SelectedIndexChanged
        If ddlTipo.SelectedValue = "Fix" Then
            LlenaCasoPadre()
            ddlCasoPadre.Visible = True
        Else
            ddlCasoPadre.Visible = False
        End If
    End Sub

    Private Sub LlenaCasoPadre()
        ddlCasoPadre.Items.Clear()
        Dim query As String = " Select   pprd_solicitud_codig, convert(varchar(100), pprd_solicitud_codig) + ' - ' + pprd_solicitud_nombrearch as descr from pprd_solicitud  where 1=1 and pprd_negocio_codig = '" & ddlEmpresa.SelectedValue & "' and pprd_modulo_codig = '" & ddlModulo.SelectedValue & "' and pprd_sistema_codig = '" & ddlSistemas.SelectedValue & "' order by 1 asc"
        Dim dataTable As DataTable = ObtenerRegistros(query)
        ddlCasoPadre.DataSource = dataTable
        ddlCasoPadre.DataTextField = "descr"
        ddlCasoPadre.DataValueField = "pprd_solicitud_codig"
        ddlCasoPadre.DataBind()
        ddlCasoPadre.Items.Insert(0, New ListItem("Seleccione caso padre", "0"))
    End Sub


    Private Sub enviarCorreo(ByVal nombreArchivo As String)
        Dim correo As New CorreoHelper()
        ' Enviar correo sin adjunto
        Dim fechaHoraActual As DateTime = Now
        Dim cuerpo As String

        Dim prioridad As String = ddlPrioridad.SelectedItem.Text
        Dim empresa As String = ddlEmpresa.SelectedItem.Text
        Dim modulo As String = ddlModulo.SelectedItem.Text
        Dim sistema As String = ddlSistemas.SelectedItem.Text
        Dim tiposolic As String = ddlTipo.SelectedItem.Text
        Dim dev As String = ddlDev.SelectedItem.Text
        Dim solic As String = ddlSolicita.SelectedItem.Text
        Dim obs As String = txtObs.Text

        cuerpo = "<h3>Se ha ingresado un nuevo caso de prioridad " & prioridad & ":</h3><br /> "
        cuerpo = cuerpo & "<table width='45%'><tr  ><td colspan='2' bgcolor='#5399d9'><center><b>RESUMEN  </b></center></td></tr>"
        cuerpo = cuerpo & "<tr bgcolor='#5acbf7'><td><b>Empresa</b></td><td><b>" & empresa & "</b></td> </tr>"
        cuerpo = cuerpo & "<tr bgcolor='#afdef0'><td><b>Módulo</b></td><td><b>" & modulo & " </b></td> </tr>"
        cuerpo = cuerpo & "<tr bgcolor='#5acbf7'><td><b>Sistema</b></td><td><b>" & sistema & "</b></td> </tr>"
        cuerpo = cuerpo & "<tr bgcolor='#afdef0'><td><b>Tipo Solicitud</b></td><td><b>" & tiposolic & " </b></td> </tr>"
        cuerpo = cuerpo & "<tr bgcolor='#5acbf7'><td><b>Desarrollador</b></td><td><b>" & dev & "</b></td> </tr>"
        cuerpo = cuerpo & "<tr bgcolor='#afdef0'><td><b>Solicitado por</b></td><td><b>" & solic & " </b></td> </tr>"
        cuerpo = cuerpo & "<tr bgcolor='#5acbf7'><td><b>Nombre archivo</b></td><td><b>" & nombreArchivo & "</b></td> </tr>"
        cuerpo = cuerpo & "<tr bgcolor='#afdef0'><td><b>Estado</b></td><td><b> Ingresada </b></td> </tr>"
        cuerpo = cuerpo & "<tr bgcolor='#5acbf7'><td><b>Detalles</b></td><td><b>" & obs & " </b></td> </tr> </table>"
        cuerpo = cuerpo & "<br/ ><br />Saludos cordiales"

        Dim para, copia, copiaoculta, asunto As String


        copia = validaUnDato("select pprd_trabajador_correo as campo from pprd_trabajador  where pprd_trabajador_codig = '" & ddlSolicita.SelectedValue & "' ") & ", " & validaUnDato("select pprd_trabajador_correo  as campo from pprd_trabajador  where pprd_trabajador_codig = '" & ddlDev.SelectedValue & "' ")
        copiaoculta = "jgarrido@parse.cl"
        asunto = "Nueva solicitud SPP"
        para = adminSPP
        Session("param_display_email") = "ACRE - Repuestos"
        correo.EnviarCorreo(para, asunto, cuerpo & "<br />[" & fechaHoraActual.ToString() & "]", Session("param_display_email"), copia)

    End Sub


    Private Sub EjecutarArchivoCMD(rutaCMD As String)
        Dim psi As New ProcessStartInfo()
        psi.FileName = "cmd.exe"
        psi.Arguments = "/c """ & rutaCMD & """"
        psi.UseShellExecute = False
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.CreateNoWindow = True

        Dim proceso As Process = Process.Start(psi)
        Dim salida As String = proceso.StandardOutput.ReadToEnd()
        Dim errorSalida As String = proceso.StandardError.ReadToEnd()
        proceso.WaitForExit()


    End Sub

End Class

