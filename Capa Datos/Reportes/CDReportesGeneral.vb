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

    'Funcion que obtiene encabezados del sac e incisos segun filtros
    Public Function SelectIncisosAsocia(ByVal id_instrumento As Integer, ByVal str_codigo As String, ByVal id_categoria As Integer, ByVal all_catego As Boolean, ByVal all_incisos As Boolean) As DataSet
        Dim sql_string As String
        Dim capitulo As String = Nothing
        Dim partida As String = Nothing
        Dim subpartida As String = Nothing
        Dim dataSet As New DataSet

        Try


            If str_codigo.Length >= 2 Then
                capitulo = str_codigo.Substring(0, 2)
                If str_codigo.Length >= 4 Then
                    partida = str_codigo.Substring(0, 4)
                    If str_codigo.Length >= 5 Then
                        If str_codigo.Length = 5 Then
                            subpartida = str_codigo.Substring(0, 5) + "%"
                        Else
                            If str_codigo.Length >= 6 Then
                                subpartida = str_codigo.Substring(0, 6) + "%"
                            End If
                        End If
                    Else
                        subpartida = str_codigo
                    End If
                Else
                    partida = str_codigo.Substring(0, 1)
                    subpartida = str_codigo.Substring(0, 1)
                End If
            Else
                capitulo = ""
                partida = ""
                subpartida = ""
            End If

            sql_string = " SELECT " +
                " descripcion_capitulo " +
                " FROM " +
                " SAC_Capitulos " +
                " WHERE " +
                " Capitulo = @capitulo AND  " +
                " activo = 'S'; " +
                " SELECT " +
                " Descripcion_Partida " +
                " FROM " +
                " SAC_Partidas " +
                " WHERE " +
                " Capitulo = @capitulo AND " +
                " Partida = @partida AND  " +
                " activo = 'S'; " +
                " SELECT " +
                " texto_subpartida " +
                " FROM " +
                " SAC_Subpartidas " +
                " WHERE " +
                " Capitulo = @capitulo AND " +
                " partida = @partida AND " +
                " subpartida like @subpartida AND " +
                " activo = 'S'; " +
                " " +
                " SELECT " +
                " sac.codigo_inciso, si.texto_inciso, si.dai_base, " +
                " icd.codigo_categoria, SUBSTRING(sac.inciso_presicion, 9, 12) as inciso_presicion, sac.texto_precision, sac.observaciones  " +
                " FROM " +
                " SAC_Asocia_Categoria sac " +
                " Right Join " +
                " SAC_Incisos si ON " +
                " si.codigo_inciso = sac.codigo_inciso And " +
                " si.id_version = sac.id_version And " +
                " si.anio_version = sac.anio_version " +
                " Right Join " +
                " IC_Categorias_Desgravacion ICD ON " +
                " icd.id_categoria = sac.id_categoria And " +
                " icd.id_instrumento = sac.id_instrumento " +
                " where " +
                " sac.id_instrumento = @id_instrumento "


            If Not all_catego Then
                sql_string = sql_string + " and sac.id_categoria = @id_categoria " +
                    " ORDER by sac.codigo_inciso "
            End If

            If Not all_incisos Then
                sql_string = sql_string + " and sac.codigo_inciso like @codigo_inciso+'%' " +
                    " ORDER by sac.codigo_inciso "
            End If




            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                command.Parameters.AddWithValue("capitulo", capitulo)
                command.Parameters.AddWithValue("partida", partida)
                command.Parameters.AddWithValue("subpartida", subpartida)

                If Not all_catego Then
                    command.Parameters.AddWithValue("id_categoria", id_categoria)
                End If

                If Not all_incisos Then
                    command.Parameters.AddWithValue("codigo_inciso", str_codigo)
                End If

                Dim da As New SqlDataAdapter(command)
                da.Fill(dataSet)

            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectDatosInciso = " + ex.Message.ToString)
        Finally

        End Try

        Return dataSet

    End Function

End Class
