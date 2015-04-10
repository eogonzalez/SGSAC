Imports System.Text
Imports System.Data.SqlClient
Public Class CDInstrumentosComerciales
    Dim objConeccion As New ConectarService
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet



    Public Function ListadoInstrumentos() As DataSet
        'Se Llena el Data Set por medio del procedimiento almacenado y se retorna el mismo
        'Dim ds As New DataSet
        Dim sql_query As String

        sql_query = " SELECT id_instrumento, sigla, nombre_instrumento, fecha_firma, fecha_ratificada, fecha_vigencia FROM IC_Instrumentos WHERE estado = 1 "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)

                da = New SqlDataAdapter(sql_query, cn)
                da.Fill(ds, "Ls_Instrumentos")



            Catch ex As Exception
                MsgBox("ERROR CONSULTARUSUARIO = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
                da.Dispose()
            End Try

            Return ds

        End Using
    End Function

End Class
