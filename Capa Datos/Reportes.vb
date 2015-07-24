Imports System.Data.SqlClient

Public Class Reportes
    Dim objConeccion As New ConectarService
    Dim da As SqlDataAdapter
    Dim ds As New DataSet

#Region "Reporte Consulta SAC"
    Public Function InstrumentoComercial() As DataSet
        Dim sql As New System.Text.StringBuilder
        sql.AppendLine("SELECT [id_instrumento]")
        sql.AppendLine("	,[nombre_instrumento]")
        sql.AppendLine("FROM [SGSACDB].[dbo].[IC_Instrumentos]")
        sql.AppendLine("WHERE estado = 1")
        sql.AppendLine("")

        Using cn = objConeccion.Conectar
            Dim command As SqlCommand = New SqlCommand(sql.ToString, cn)
            da = New SqlDataAdapter(command)
            da.Fill(ds, "InstrumentoComercial")
        End Using

        Return ds

    End Function

    Public Function Categorias(ByVal IdInstrumento As Integer) As DataSet
        Dim sql As New System.Text.StringBuilder
        sql.AppendLine("SELECT CATEGO.id_categoria,")
        sql.AppendLine("		(convert(varchar(max),CATEGO.codigo_categoria) + ")
        sql.AppendLine("		' - ' + 'Cantidad Tramos: ' + CONVERT(VARCHAR(max),CATEGO.cantidad_tramos)")
        sql.AppendLine("		+ ' - ' + 'Cantidad Cortes: ' + CONVERT(VARCHAR(max),CATEGO.CANTIDAD_CORTES)) AS Categoria")
        sql.AppendLine("FROM  IC_Instrumentos I  ")
        sql.AppendLine("LEFT JOIN  (SELECT  CD.id_instrumento, ")
        sql.AppendLine("					CD.id_categoria,  ")
        sql.AppendLine("					CD.codigo_categoria, ")
        sql.AppendLine("					TD.descripcion,  ")
        sql.AppendLine("					CD.cantidad_tramos, ")
        sql.AppendLine("					CDT.activo,  ")
        sql.AppendLine("					SUM(CDT.cantidad_cortes) AS CANTIDAD_CORTES  ")
        sql.AppendLine("			FROM  IC_Categorias_Desgravacion CD,  IC_Categorias_Desgravacion_Tramos CDT,  IC_Tipo_Desgravacion TD ")
        sql.AppendLine("			WHERE  CD.id_categoria = CDT.id_categoria ")
        sql.AppendLine("			And  CD.id_instrumento = CDT.id_instrumento ")
        sql.AppendLine("			And  CD.id_tipo_desgrava = TD.id_tipo_desgrava  ")
        sql.AppendLine("			GROUP BY  CD.id_instrumento, ")
        sql.AppendLine("						CD.id_categoria,  ")
        sql.AppendLine("						CD.codigo_categoria, ")
        sql.AppendLine("						TD.descripcion,  ")
        sql.AppendLine("						CD.cantidad_tramos, ")
        sql.AppendLine("						CDT.activo) CATEGO ")
        sql.AppendLine("ON  I.id_instrumento = CATEGO.id_instrumento  ")
        sql.AppendLine("WHERE  I.id_instrumento = " & IdInstrumento)
        sql.AppendLine("")

        Using cn = objConeccion.Conectar
            Dim command As SqlCommand = New SqlCommand(sql.ToString, cn)
            da = New SqlDataAdapter(command)
            da.Fill(ds, "Categorias")
        End Using

        Return ds

    End Function
#End Region

End Class
