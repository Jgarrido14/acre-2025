Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.IO

Imports Font = iTextSharp.text.Font
Public Class RepuestosPDF
    Inherits System.Web.UI.Page
    Dim connectionString = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsLogin") = False Then
            Response.Redirect("Login.aspx")
        End If
        GeneraPDF()

    End Sub

    Sub GeneraPDF()

        Dim dt As New DataTable()
        Dim selectSQL As String
        selectSQL = "Exec [ACRE_ListadoRepuestos_Carrito] 'Nada', '" & Session("User") & "', '" & Session("Usuario_cookie") & "', 'Nada'"
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(selectSQL, conn)
                conn.Open()
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Dim doc As New Document(PageSize.A4.Rotate()) ', 10, 10, 10, 10)
        Dim ms As New MemoryStream()
        Dim writer = PdfWriter.GetInstance(doc, ms)
        doc.Open()

        Dim font As New Font(font.FontFamily.HELVETICA, 8)
        Dim headerFont As New Font(font.FontFamily.HELVETICA, 8, font.BOLD)

        Dim table As New PdfPTable(dt.Columns.Count)
        table.WidthPercentage = 100

        For Each col As DataColumn In dt.Columns
            table.AddCell(New Phrase(col.ColumnName, headerFont))
        Next

        For Each row As DataRow In dt.Rows
            For Each cell As Object In row.ItemArray
                table.AddCell(New Phrase(cell.ToString(), font))
            Next
        Next


        doc.Add(table)
        doc.Close()
        Dim randomGenerator As New Random()

        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=RepuestosAcre-" & Session("User") & "-" & randomGenerator.Next(9999, 99999) & ".pdf")
        Response.BinaryWrite(ms.ToArray())
        Response.End()
    End Sub
End Class