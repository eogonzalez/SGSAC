Imports System.Data.SqlClient
Public Class CDPartidas
    Dim objConecccion As New ConectarService

    'Funcion para obtener los datos del encabezado de mantenimiento de partidas
    Public Function SelectPartidasEncMant() As DataTable
        Dim dataTable As New DataTable
        Try
            Dim sql_query As String

            sql_query = "  SELECT id_version, anio_version, enmienda, " +
                " anio_inicia_enmienda,anio_fin_enmieda, " +
                " 'Version '+enmienda+', Enero '+convert(varchar(10),anio_inicia_enmienda) as descripcion " +
                " FROM SAC_VERSIONES_BITACORA " +
                " WHERE estado = 'A' "

            Using cn = objConecccion.Conectar
                Dim command As New SqlCommand(sql_query, cn)
                Dim dataAdapter As New SqlDataAdapter(command)
                cn.Open()
                dataAdapter.Fill(dataTable)
                cn.Close()

            End Using


        Catch ex As SqlException

        Catch ex As Exception

        End Try
        Return dataTable
    End Function

    'Funcion para obtener el listado de partidas segun el codigo
    Public Function SelectDatosPartidas(ByVal str_codigo As String) As DataTable
        Dim sql_query As String
        Dim dataTable As New DataTable

        sql_query = " SELECT " +
            " Capitulo, Partida, Descripcion_Partida " +
            " FROM " +
            " SAC_Partidas " +
            " where " +
            " partida like @str_codigo+'%' AND " +
            " activo = 'S' "

        Using cn = objConecccion.Conectar
            Dim command As New SqlCommand(sql_query, cn)
            command.Parameters.AddWithValue("str_codigo", str_codigo)

            Dim dataAdapter As New SqlDataAdapter(command)

            cn.Open()
            dataAdapter.Fill(dataTable)
            cn.Close()

        End Using

        Return dataTable
    End Function

    'Funcion que verifica si existe partida
    Public Function ExistePartida(ByVal str_codigo As String) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String

        sql_query = " SELECT " +
            " COALESCE(count(*), 0) " +
            " FROM " +
            " SAC_Partidas " +
            " where " +
            " partida = @partida AND " +
            " activo = 'S' "

        Using cn = objConecccion.Conectar
            Dim command As New SqlCommand(sql_query, cn)
            command.Parameters.AddWithValue("partida", str_codigo)

            cn.Open()

            If command.ExecuteScalar > 0 Then
                estado = True
            Else
                estado = False
            End If

        End Using

        Return estado
    End Function

    'Funcion que almacena partida nueva
    Public Function SavePartida(ByVal str_codigo As String, ByVal descripcion As String) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String

        sql_query = " INSERT INTO SAC_Partidas " +
            " ([Capitulo] ,[Partida] " +
            " ,[Descripcion_Partida],[activo]) " +
            " VALUES " +
            " (@Capitulo ,@Partida " +
            " ,@Descripcion_Partida,'S') "

        Try

            Using cn = objConecccion.Conectar
                Dim command As New SqlCommand(sql_query, cn)
                Dim capitulo As String = str_codigo.Substring(0, 2)

                command.Parameters.AddWithValue("Capitulo", capitulo)
                command.Parameters.AddWithValue("Partida", str_codigo)
                command.Parameters.AddWithValue("Descripcion_Partida", descripcion)

                cn.Open()
                command.ExecuteNonQuery()
                cn.Close()

            End Using

        Catch ex As SqlException
            estado = False
        Catch ex As Exception
            estado = False
        End Try

        Return estado
    End Function

    'Funcion que actualiza partida
    Public Function UpdatePartida(ByVal str_codigo As String, ByVal descripcion As String) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String

        sql_query = " UPDATE SAC_Partidas " +
            " SET " +
            " Descripcion_Partida = @Descripcion_Partida " +
            " WHERE " +
            " partida = @Partida "

        Try

            Using cn = objConecccion.Conectar
                Dim command As New SqlCommand(sql_query, cn)

                command.Parameters.AddWithValue("Partida", str_codigo)
                command.Parameters.AddWithValue("Descripcion_Partida", descripcion)

                cn.Open()
                command.ExecuteNonQuery()
                cn.Close()

            End Using

        Catch ex As SqlException
            estado = False
        Catch ex As Exception
            estado = False
        End Try

        Return estado
    End Function

End Class
