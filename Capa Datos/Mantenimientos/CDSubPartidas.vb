Imports System.Data.SqlClient
Public Class CDSubPartidas
    Dim objConecccion As New ConectarService

    'Funcion para obtener el listado de subpartidas segun el codigo
    Public Function SelectDatosSubPartidas(ByVal str_codigo As String) As DataTable
        Dim sql_query As String
        Dim dataTable As New DataTable

        sql_query = " SELECT " +
            " capitulo, partida, subpartida, texto_subpartida " +
            " FROM " +
            " SAC_Subpartidas " +
            " WHERE " +
            " subpartida like @str_codigo+'%' AND " +
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

    'Funcion que verifica si existe subpartida
    Public Function ExisteSubPartida(ByVal str_codigo As String) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String

        sql_query = " SELECT " +
            " COALESCE(count(*), 0) " +
            " FROM " +
            " SAC_Subpartidas " +
            " where " +
            " subpartida = @subpartida AND " +
            " activo = 'S' "

        Using cn = objConecccion.Conectar
            Dim command As New SqlCommand(sql_query, cn)
            command.Parameters.AddWithValue("subpartida", str_codigo)

            cn.Open()

            If command.ExecuteScalar > 0 Then
                estado = True
            Else
                estado = False
            End If

        End Using

        Return estado
    End Function

    'Funcion que almacena subpartida nueva
    Public Function SaveSubPartida(ByVal str_codigo As String, ByVal descripcion As String) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String

        sql_query = " INSERT INTO SAC_Subpartidas " +
            " ([Capitulo] ,[Partida], [subpartida] " +
            " ,[texto_subpartida],[activo]) " +
            " VALUES " +
            " (@Capitulo ,@Partida, @subpartida " +
            " ,@texto_subpartida,'S') "

        Try

            Using cn = objConecccion.Conectar
                Dim command As New SqlCommand(sql_query, cn)
                Dim capitulo As String = str_codigo.Substring(0, 2)
                Dim partida As String = str_codigo.Substring(0, 4)

                command.Parameters.AddWithValue("Capitulo", capitulo)
                command.Parameters.AddWithValue("Partida", partida)
                command.Parameters.AddWithValue("subpartida", str_codigo)
                command.Parameters.AddWithValue("texto_subpartida", descripcion)

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

    'Funcion que actualiza subpartida
    Public Function UpdateSubPartida(ByVal str_codigo As String, ByVal descripcion As String) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String

        sql_query = " UPDATE SAC_Subpartidas " +
            " SET " +
            " texto_subpartida = @Descripcion " +
            " WHERE " +
            " subpartida = @subpartida "

        Try

            Using cn = objConecccion.Conectar
                Dim command As New SqlCommand(sql_query, cn)

                command.Parameters.AddWithValue("subpartida", str_codigo)
                command.Parameters.AddWithValue("Descripcion", descripcion)

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
