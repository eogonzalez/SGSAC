Imports System.Data.SqlClient
Public Class CDReportesGeneral
    Dim objConeccion As New ConectarService

    'Funcion que obtiene listado de instrumentos comerciales
    Public Function SelectInstrumentoComercial() As DataTable
        Dim sql_query As String
        Dim dataTable As New DataTable

        sql_query = "SELECT [id_instrumento] " +
            "	,[nombre_instrumento]" +
            " FROM [IC_Instrumentos] " +
            " WHERE estado = 1 "


        Using cn = objConeccion.Conectar
            Dim command As SqlCommand = New SqlCommand(sql_query, cn)
            Dim dataAdapter As New SqlDataAdapter(command)

            dataAdapter.Fill(dataTable)

        End Using

        Return dataTable

    End Function

    'Funcion que obtiene listado de categorias por instrumento comercial
    Public Function SelectCategoriasList(ByVal IdInstrumento As Integer) As DataTable
        Dim dataTable As New DataTable
        Dim sql_query As String

        sql_query = " SELECT CATEGO.id_categoria, " +
            " (convert(varchar(max),CATEGO.codigo_categoria) + " +
            " ' - ' + 'Cantidad Tramos: ' + CONVERT(VARCHAR(max),CATEGO.cantidad_tramos) " +
            " + ' - ' + 'Cantidad Cortes: ' + CONVERT(VARCHAR(max),CATEGO.CANTIDAD_CORTES)) AS Categoria " +
            " FROM  IC_Instrumentos I  " +
            " LEFT JOIN  (SELECT  CD.id_instrumento, " +
            " CD.id_categoria,  " +
            " CD.codigo_categoria, " +
            " TD.descripcion,  " +
            " CD.cantidad_tramos, " +
            " CDT.activo,  " +
            " SUM(CDT.cantidad_cortes) AS CANTIDAD_CORTES  " +
            " FROM  IC_Categorias_Desgravacion CD,  IC_Categorias_Desgravacion_Tramos CDT,  IC_Tipo_Desgravacion TD " +
            " WHERE  CD.id_categoria = CDT.id_categoria " +
            " And  CD.id_instrumento = CDT.id_instrumento " +
            " And  CD.id_tipo_desgrava = TD.id_tipo_desgrava  " +
            " GROUP BY  CD.id_instrumento, " +
            " CD.id_categoria,  " +
            " CD.codigo_categoria, " +
            " TD.descripcion,  " +
            " CD.cantidad_tramos, " +
            " CDT.activo) CATEGO " +
            " ON  I.id_instrumento = CATEGO.id_instrumento  " +
            " WHERE  I.id_instrumento = @IdInstrumento "


        Using cn = objConeccion.Conectar

            Dim command As SqlCommand = New SqlCommand(sql_query, cn)
            command.Parameters.AddWithValue("IdInstrumento", IdInstrumento)
            Dim dataAdapter As New SqlDataAdapter(command)

            dataAdapter.Fill(DataTable)

        End Using

        Return DataTable

    End Function

End Class
