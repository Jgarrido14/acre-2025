Imports System.Data.SqlClient

Module Cnx_Fns
    Private connectionString As String
    Sub New()
        connectionString = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    End Sub

    Public Function ObtenerRegistros(query As String) As DataTable
        Dim dataTable As New DataTable()

        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Using command As New SqlCommand(query, connection)
                    Using adapter As New SqlDataAdapter(command)
                        adapter.Fill(dataTable)
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception("Error al obtener datos: " & ex.Message)
            End Try
        End Using

        Return dataTable
    End Function

    Public Function GetDatabaseValue(ByVal empr As String) As String
        Dim Carpeta As String = String.Empty
        Dim query As String = " select pprd_rutas_direccion from pprd_rutas where pprd_negocio_codig = '" & empr & "'"
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Carpeta = reader("pprd_rutas_direccion").ToString()
                    End If
                End Using
            End Using
        End Using
        Return Carpeta
    End Function

    Public Function ValidaPassword(ByVal usuario As String) As String
        Dim Password As String = String.Empty
        Dim query As String = " select usuario_pass from usuario where usuario_id = '" & usuario & "'"
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Password = reader("usuario_pass").ToString()
                    End If
                End Using
            End Using
        End Using
        Return Password
    End Function

    Public Function validaUnDato(ByVal qry As String) As String
        Dim Respuesta As String = String.Empty
        Dim query As String = qry
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Respuesta = reader("campo").ToString()
                    End If
                End Using
            End Using
        End Using
        Return Respuesta
    End Function

    Public Function ValidaUsuario(ByVal usuario As String) As Integer
        Dim existe As Integer = 0
        Dim query As String = " select count(*) as cant  from usuario where usuario_activo = 1  and usuario_id = '" & usuario & "'"
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        existe = reader("cant")
                    End If
                End Using
            End Using
        End Using
        Return existe
    End Function
    Public Sub InsertSolicitud(ByVal insert As String)
        Dim query As String = insert
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                End Using
            End Using
        End Using
    End Sub

    Public Sub BakSolicitud(ByVal insert As String)
        Dim query As String = insert
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                command.CommandTimeout = 1000
                Using reader As SqlDataReader = command.ExecuteReader()
                End Using
            End Using
        End Using
    End Sub

    Public Function ObtenerValorTipoUsuario(ByVal Usuario As String) As Object
        Dim resultado As Object = Nothing
        Dim consulta As String = "Select usuario_tipo from [usuario] where 1=1 and usuario_activo = 1 and usuario_id =  '" & Usuario & "'"
        Using conexion As New SqlConnection(connectionString)
            Dim comando As New SqlCommand(consulta, conexion)
            Try
                conexion.Open()
                resultado = comando.ExecuteScalar()
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.Message)
            Finally
                conexion.Close()
            End Try
        End Using

        Return resultado
    End Function

    Public Function ObtenerNombreUsuario(ByVal Usuario As String) As Object
        Dim resultado As Object = Nothing
        Dim consulta As String = "Select usuario_tipo from [usuario] where 1=1 and usuario_activo = 1 and usuario_id =  '" & Usuario & " '"
        Using conexion As New SqlConnection(connectionString)
            Dim comando As New SqlCommand(consulta, conexion)
            Try
                conexion.Open()
                resultado = comando.ExecuteScalar()
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.Message)
            Finally
                conexion.Close()
            End Try
        End Using

        Return resultado
    End Function


    Public Function ObtenerTablas(query As String) As DataSet
        Dim dataset As New DataSet()

        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Using command As New SqlCommand(query, connection)
                    Using adapter As New SqlDataAdapter(command)
                        adapter.Fill(dataset)
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception("Error al obtener datos: " & ex.Message)
            End Try
        End Using

        Return dataset
    End Function
    Function SQL_CnvStr(ByVal v As String) As String
        '----
        ' Convierte un string al formato aceptado por SQL
        ' Server.
        '----
        Dim w, o As String
        Dim n As Short
        o = v
        Do
            n = InStr(o, "'")
            If n = 0 Then Exit Do
            w = w & Left(o, n - 1) & "''"
            o = Mid(o, n + 1)
        Loop
        SQL_CnvStr = "'" & w & o & "'"
    End Function

    Function SQL_CnvNum(ByVal num As Object) As String
        '----
        ' Convierte un Numero al formato SQL Server 6.0.
        '----
        ' Recibe: El número, y retorna el string.
        '----
        If STR_ToNum(num) = -1 Then
            SQL_CnvNum = "NULL"
        Else
            SQL_CnvNum = Str(STR_ToNum(num))
        End If
    End Function

    Function STR_ToNum(ByVal num As Object) As Double
        '----
        ' Transforma un String a número.
        '----
        ' Recibe
        '   Num : Numero como "9..9".
        '----
        ' Entrega
        '   Número : o NIL en caso de error.
        '----
        Dim it As Object

        it = num
        If IsNumeric(it) Then
            STR_ToNum = it
            Exit Function
        End If

        STR_ToNum(-1)
    End Function

    Function SQL_CnvDate(ByVal fecha As Object) As String
        '----
        ' Convierte una fecha al formato de SQL Server u Oracle.
        '----
        ' Recibe:
        '   Fecha, y retorna el string convertido.
        '----

        Dim ledat As DateTime

        ' Intentamos convertir el objeto a un tipo DateTime usando TryParse para mayor robustez
        If Not DateTime.TryParse(fecha.ToString(), ledat) Then
            Return " null "
        End If

#If DB = 1 Then
    ' Para SQL Server: Convertir a formato compatible con SQL Server
    Return String.Format("{0:yyyy-MM-dd}", ledat)
#Else
        ' Para Oracle: Convertir a formato compatible con Oracle
        Return String.Format("TO_DATE('{0:dd-MM-yyyy}', 'DD-MM-YYYY')", ledat)
#End If

    End Function

    Function SQL_CnvDateTime(ByVal FeHor As Object) As String
        '----
        ' Convierte una fecha y hora al formato de SQL Server u Oracle.
        '----
        ' Recibe:
        '   Fecha, y retorna el string convertido.
        '----
        Dim ledat As DateTime

        ' Intentamos convertir el objeto a un tipo DateTime usando TryParse para mayor robustez
        If Not DateTime.TryParse(FeHor.ToString(), ledat) Then
            Return " NULL "
        End If

#If DB = 1 Then
    ' Para SQL Server: Convertir a formato compatible con SQL Server
    Return String.Format("{0:yyyy-MM-dd HH:mm:ss}", ledat)
#Else
        ' Para Oracle: Convertir a formato compatible con Oracle
        Return String.Format("TO_DATE('{0:dd-MM-yyyy HH:mm}', 'DD-MM-YYYY HH24:MI')", ledat)
#End If

    End Function


    Function STR_ToDate(ByVal it) As Object
        '----
        ' Transforma un string a fecha.
        '----
        ' Recibe
        '   it : Fecha como "dd/mm/aa".
        '----
        ' Entrega
        '   NIL   : si hay error.
        '   Fecha : en otro caso.
        '----
        On Error GoTo STDErr
        If IsDBNull(it) Then GoTo STDErr
        STR_ToDate = CDate(it)

        Exit Function
STDErr:
        Return Nothing
        Exit Function
    End Function

    Public Function TextToHex(ByVal input As String) As String
        Dim hexOutput As New System.Text.StringBuilder()
        For Each c As Char In input
            ' Convierte cada carácter a su valor hexadecimal y lo agrega al resultado
            hexOutput.AppendFormat("{0:X2}", Convert.ToUInt16(c))
        Next
        Return hexOutput.ToString()
    End Function


End Module
