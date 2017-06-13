Imports System.Data.SqlClient

Public Class Enmiendas
    Dim objConeccion As New ConectarService

    'Funcion que verifica si existe version pendiente de aprobar
    Public Function ExisteVersionSACPendiente() As Boolean
        Dim estado As Boolean = False
        Try
            Dim sql_query As String

            sql_query = " SELECT coalesce(COUNT(id_version),0) " +
                " from SAC_Versiones_Bitacora " +
                " where estado is null "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                cn.Open()
                If command.ExecuteScalar() >= 1 Then
                    estado = True
                Else
                    estado = False
                End If
            End Using

        Catch ex As Exception
            estado = False
        Finally

        End Try

        Return estado
    End Function

    'Funcion que verifica cuantas versiones de sac pendiente existen
    Public Function CantidadVersionesSACPendientes() As Integer
        Dim cantidad As Integer = 0
        Try
            Dim sql_query As String

            sql_query = " SELECT COUNT(id_version) " +
                " from SAC_Versiones_Bitacora " +
                " where estado is null "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                cn.Open()
                cantidad = command.ExecuteScalar()

            End Using

        Catch ex As Exception

        Finally

        End Try

        Return cantidad
    End Function



End Class
