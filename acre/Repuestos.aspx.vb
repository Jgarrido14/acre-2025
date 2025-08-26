Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing

Public Class Repuestos
    Inherits Page
    Dim connectionString = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Dim cantRegistros As Integer
    Dim qrySolicitudes As String
    Dim qryOrder As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'cantRegistros = 10
        pnlPlano.Visible = False
        Panel1.Visible = False

        If Session("IsLogin") = False Then
            Response.Redirect("Login.aspx")
        End If
        Dim userCookie As HttpCookie = Request.Cookies("usuarioCasos")
        Dim valorDeLaCookie As String
        If userCookie IsNot Nothing Then
            valorDeLaCookie = userCookie.Value
        End If

        If Session("User") Is Nothing And valorDeLaCookie Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        Session("User") = valorDeLaCookie
        SaveCookie(Session("User"))

        If Not IsPostBack Then
            'CantidadRegistrosSQL()
            'CantidadRegistrosPorHoja()
            'BindGrid()
            BindGridCarrito("Nada", "Nada")
            ManejoBtnLimpiar()
            ' GridView1.PageSize = ddlRegPorPag.SelectedValue

        End If
    End Sub

    Sub SaveCookie(ByVal usr As String)
        Dim myCookie As New HttpCookie("usuarioCasos")
        myCookie.Value = usr
        myCookie.Expires = DateTime.Now.AddDays(7)
        Response.Cookies.Add(myCookie)
    End Sub


    'Sub CantidadRegistrosSQL()
    '    ddlTopRegSQL.Items.Add(New ListItem("Últimos 10", "10"))
    '    ddlTopRegSQL.Items.Add(New ListItem("Últimos 25", "25"))
    '    ddlTopRegSQL.Items.Add(New ListItem("Últimos 50", "50"))
    '    ddlTopRegSQL.Items.Add(New ListItem("Últimos 100", "100"))
    '    ddlTopRegSQL.Items.Add(New ListItem("Últimos 1000", "1000"))
    'End Sub

    'Sub CantidadRegistrosPorHoja()
    '    ddlRegPorPag.Items.Add(New ListItem("10", "10"))
    '    ddlRegPorPag.Items.Add(New ListItem("25", "25"))
    '    ddlRegPorPag.Items.Add(New ListItem("50", "50"))
    '    ddlRegPorPag.Items.Add(New ListItem("100", "100"))
    '    ddlRegPorPag.Items.Add(New ListItem("1000", "1000"))
    'End Sub

    Function CreaConsulta(ByVal busqueda As String) As String
        Dim consulta As String
        'consulta = "Select top " & ddlTopRegSQL.SelectedValue & qrySolicitudes
        If busqueda <> "" Then
            consulta = "exec ACRE_ING_SEG '" & Session("User") & "', '" & busqueda & "'"
            InsertSolicitud(consulta)
        End If
        Dim sql As String
        sql = " ACRE_ListadoRepuestos " & SQL_CnvStr("%" & busqueda & "%")

        Return sql
    End Function

    Function WhereString(ByVal textoIn As String) As String
        Return "'" & textoIn & "'"
    End Function

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = GridView1.SelectedRow
        Dim valorARTICULO As String = row.Cells(2).Text
        Dim valorUN As String = row.Cells(7).Text
        Session("ARTICULO_SEL") = row.Cells(2).Text
        Dim centrosel As String
        centrosel = row.Cells(7).Text
        Session("UN_SEL") = row.Cells(7).Text

        lblMsje.Text = "<b>Código: " & valorARTICULO & " - Centro: " & centrosel & "</b>"
        lblMsje.ForeColor = Color.Blue
        btnAgregar.Visible = True

    End Sub

    Private Sub BindGrid()
        Dim query As String = CreaConsulta(txtCriterio.Text)
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Using adapter As New SqlDataAdapter(command)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindGridView(sortExpression As String, sortDirection As String)
        Dim dt As DataTable

        Dim query As String = CreaConsulta(txtCriterio.Text)
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Using adapter As New SqlDataAdapter(command)

                    adapter.Fill(dt)
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                End Using
            End Using
        End Using
        If dt IsNot Nothing Then
            dt.DefaultView.Sort = sortExpression & " " & sortDirection
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If
    End Sub

    Private Function ObtenerDatos() As DataTable
        Dim dt As New DataTable()
        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = CreaConsulta(txtCriterio.Text)
                Using cmd As New SqlCommand(query, conn)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using

            Catch ex As Exception
            End Try
        End Using
        Return dt
    End Function


    Private Sub BindData(Optional sortExpression As String = Nothing, Optional sortDirection As String = "ASC")
        Dim dt As DataTable = ObtenerDatos()
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

    'Protected Sub ddlTopRegSQL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTopRegSQL.SelectedIndexChanged
    '    BindGrid()
    'End Sub

    'Protected Sub ddlRegPorPag_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegPorPag.SelectedIndexChanged
    '    GridView1.PageSize = ddlRegPorPag.SelectedValue
    '    BindGrid()
    'End Sub



    Protected Sub lnkVolver_Click(sender As Object, e As EventArgs) Handles lnkVolver.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click


        lblAgregados.Text = lblAgregados.Text + "<br />" + Session("ARTICULO_SEL")
        LlenaCarrito(Session("ARTICULO_SEL"), Session("UN_SEL"))
        BindGridCarrito(Session("ARTICULO_SEL"), Session("UN_SEL"))
        ManejoBtnLimpiar()
    End Sub
    Sub LlenaCarrito(ByVal artic As String, ByVal un As String)
        Dim query As String
        query = "Exec ACRE_Agrega_Carrito '" & artic & "', '" & Session("User") & "', '" & Session("Usuario_cookie") & "', '" & un & "'"
        InsertSolicitud(query)
    End Sub
    Private Sub BindGridCarrito(ByVal artic As String, ByVal un As String)

        pnlPlano.Visible = True
        Panel1.Visible = True

        Dim query As String = "Exec [ACRE_ListadoRepuestos_Carrito] '" & artic & "', '" & Session("User") & "', '" & Session("Usuario_cookie") & "', '" & un & "'"
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Using adapter As New SqlDataAdapter(command)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    GridView2.DataSource = dt
                    GridView2.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Protected Sub txtCriterio_TextChanged(sender As Object, e As EventArgs) Handles txtCriterio.TextChanged
        CreaConsulta(txtCriterio.Text)
        BindGrid()
    End Sub

    Protected Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        LimpiarCarrito()
        ManejoBtnLimpiar()
    End Sub
    Sub ManejoBtnLimpiar()
        If GridView2.Rows.Count = 0 Then
            BtnLimpiar.Visible = False
            BtnPDF.Visible = False
        Else
            BtnLimpiar.Visible = True
            BtnPDF.Visible = True
        End If
    End Sub
    Sub LimpiarCarrito()
        Dim query As String
        query = "Exec ACRE_Limpiar_Carrito '" & Session("User") & "', '" & Session("Usuario_cookie") & "'"
        InsertSolicitud(query)
        BindGridCarrito(Session("ARTICULO_SEL"), Session("UN_SEL"))
    End Sub



    Protected Sub BtnPDF_Click(sender As Object, e As EventArgs) Handles BtnPDF.Click
        Response.Redirect("RepuestosPDF.aspx")

    End Sub
End Class

