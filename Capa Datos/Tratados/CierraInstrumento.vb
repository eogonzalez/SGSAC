Imports System.Data.SqlClient
Public Class CierraInstrumento
    Dim objConeccion As New ConectarService

    Public Function SelectResumenInstrumento(ByVal id_instrumento As Integer) As DataTable
        Dim dt As New DataTable
        Dim sql_query As String
        Dim da As SqlDataAdapter

        sql_query = " SELECT " +
            " SAC.id_categoria, icd.codigo_categoria, count(*) cant_asocia " +
            " FROM " +
            " SAC_Asocia_Categoria SAC, " +
            " IC_Categorias_Desgravacion ICD " +
            " where " +
            " SAC.id_instrumento = @id_instrumento And " +
            " ICD.id_instrumento = SAC.id_instrumento And " +
            " ICD.id_categoria = SAC.id_categoria " +
            " group by SAC.id_categoria, icd.codigo_categoria " +
            " order by SAC.id_categoria "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)

                da = New SqlDataAdapter(command)
                da.Fill(dt)

            Catch ex As Exception
                MsgBox("ERROR CONSULTA  RESUMEN INSTRUMENTOS = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
                da.Dispose()
            End Try

            Return dt

        End Using
    End Function

End Class
