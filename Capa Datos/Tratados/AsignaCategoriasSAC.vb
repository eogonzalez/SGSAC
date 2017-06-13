Imports System.Data.SqlClient
Imports Capa_Entidad
Public Class AsignaCategoriasSAC
    Dim objConeccion As New ConectarService
    Dim objGeneral As New General
    'Funcion para Insertar las Asociaciones de categoria
    Public Function InsertAsignaCategoria(ByVal objCEAsigna As CEIncisoAsociaCategoria) As Boolean
        Dim estado As Boolean = False
        Dim dt_asocia As DataTable = objCEAsigna.lista_incisos
        Dim id_instrumento As Integer = objCEAsigna.id_instrumento
        Dim id_categoria As Integer = objCEAsigna.id_categoria

        Try
            'recorro DataTable 
            For Each row As DataRow In dt_asocia.Rows


                'Si fila esta seleccionada
                If row("Selected") = True Then

                    'Obtengo Codigo Inciso
                    Dim codigo_inciso As String = row("codigo_inciso")

                    If IsDBNull(row("inciso_presicion")) Then

                        'Se llama a la funcion valida inciso para verificar si inciso ya existe con otra categoria
                        If objGeneral.ValidaInciso(id_instrumento, codigo_inciso) Then
                            'Si existe inciso, actuliza categoria para inciso
                            estado = objGeneral.UpdateInciso(id_instrumento, id_categoria, codigo_inciso)
                        Else
                            'Si no existe inciso, inserta
                            estado = objGeneral.InsertInciso(id_instrumento, id_categoria, codigo_inciso)
                        End If

                    Else
                        Dim logitud As Integer = row("inciso_presicion").ToString.Length

                        If (logitud = 0) Then

                            'Se llama a la funcion valida inciso para verificar si inciso ya existe con otra categoria
                            If objGeneral.ValidaInciso(id_instrumento, codigo_inciso) Then
                                'Si existe inciso, actuliza categoria para inciso
                                estado = objGeneral.UpdateInciso(id_instrumento, id_categoria, codigo_inciso)
                            Else
                                'Si no existe inciso, inserta
                                estado = objGeneral.InsertInciso(id_instrumento, id_categoria, codigo_inciso)
                            End If
                        Else

                            Dim inciso_presicion As String = row("inciso_presicion")

                            'Se llama a la funcion valida inciso para verificar si inciso_presicion ya existe con otra categoria
                            If objGeneral.ValidaInciso(id_instrumento, codigo_inciso, inciso_presicion) Then
                                'Si existe inciso, actuliza categoria para inciso
                                estado = objGeneral.UpdateInciso(id_instrumento, id_categoria, codigo_inciso, inciso_presicion)
                            Else
                                'Si no existe inciso, inserta
                                estado = objGeneral.InsertInciso(id_instrumento, id_categoria, codigo_inciso, inciso_presicion)
                            End If
                        End If

                    End If


                End If

            Next

        Catch ex As Exception
            estado = False
        Finally

        End Try
        Return estado
    End Function

    'Funcion para obtener los datos para los codigos seleccionados
    Public Function SelectDatosCodigoInciso(ByVal id_instrumento As Integer, ByVal str_codigo As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As String
            Dim capitulo As String = Nothing
            Dim partida As String = Nothing
            Dim subpartida As String = Nothing

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
                Exit Try
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
                " SELECT " +
                " ci.codigo_inciso, ci.texto_inciso, ci.dai_base, " +
                " icd.codigo_categoria, SAC.inciso_presicion, SAC.texto_precision " +
                " FROM " +
                " SAC_Incisos CI " +
                " left outer join " +
                " (SELECT " +
                " AC.id_version, AC.anio_version, " +
                " AC.codigo_inciso, " +
                " AC.id_categoria, AC.id_instrumento," +
                " AC.inciso_presicion, AC.texto_precision " +
                " FROM " +
                " SAC_Asocia_Categoria AC, " +
                " SAC_Versiones_Bitacora VB " +
                " WHERE " +
                " ac.id_version = vb.id_version AND " +
                " ac.anio_version = vb.anio_version AND " +
                " vb.estado = 'A' AND " +
                " ac.id_instrumento = @id_instrumento" +
                " ) SAC on " +
                " sac.id_version = ci.id_version And " +
                " sac.anio_version = CI.anio_version And " +
                " sac.codigo_inciso = ci.codigo_inciso " +
                " LEFT OUTER JOIN " +
                " IC_Categorias_Desgravacion ICD ON " +
                " icd.id_categoria = sac.id_categoria And " +
                " icd.id_instrumento = sac.id_instrumento " +
                " WHERE " +
                " CI.estado = 'A' AND  " +
                " CI.codigo_inciso LIKE '" + str_codigo + "%' "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                command.Parameters.AddWithValue("capitulo", capitulo)
                command.Parameters.AddWithValue("partida", partida)
                command.Parameters.AddWithValue("subpartida", subpartida)
                command.Parameters.AddWithValue("codigo_inciso", str_codigo)

                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds)

            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectDatosInciso = " + ex.Message.ToString)
        Finally

        End Try

        Return ds
    End Function

    'Funcion para obtener los datos del Mantenimiento de Asignacion de Categoria
    Public Function SelectDatosAsignaCategoriaMant(ByVal id_instrumento As Integer) As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As String

            sql_string = " SELECT anio_version,enmienda,anio_inicia_enmienda,anio_fin_enmieda " +
                " FROM SAC_VERSIONES_BITACORA " +
                " WHERE estado = 'A'; " +
                " SELECT nombre_instrumento, sigla " +
                " FROM IC_Instrumentos " +
                " WHERE id_instrumento = @id_instrumento; " +
                " SELECT " +
                " CATEGO.id_categoria," +
                " (convert(varchar(max),CATEGO.codigo_categoria) + ' - ' + 'Cantidad Tramos: ' + CONVERT(VARCHAR(max),CATEGO.cantidad_tramos) + ' - ' + 'Cantidad Cortes: ' + CONVERT(VARCHAR(max),CATEGO.CANTIDAD_CORTES)) AS codigo_categoria " +
                " FROM " +
                " IC_Instrumentos I " +
                " Left Join" +
                " (SELECT  CD.id_instrumento, " +
                " CD.id_categoria,  " +
                " CD.codigo_categoria, " +
                " TD.descripcion,  " +
                " CD.cantidad_tramos, " +
                " CDT.activo,  " +
                " SUM(CDT.cantidad_cortes) AS CANTIDAD_CORTES  " +
                " FROM " +
                " IC_Categorias_Desgravacion CD,  " +
                " IC_Categorias_Desgravacion_Tramos CDT,  " +
                " IC_Tipo_Desgravacion TD " +
                " WHERE " +
                " CD.id_categoria = CDT.id_categoria AND " +
                " CD.id_instrumento = CDT.id_instrumento AND" +
                " CD.id_tipo_desgrava = TD.id_tipo_desgrava  AND " +
                " cdt.activo = 'S' " +
                " GROUP BY " +
                " CD.id_instrumento, " +
                " CD.id_categoria,  " +
                " CD.codigo_categoria, " +
                " TD.descripcion,  " +
                " CD.cantidad_tramos, " +
                " CDT.activo) CATEGO " +
                " ON  " +
                " I.id_instrumento = CATEGO.id_instrumento " +
                " WHERE " +
                " I.id_instrumento = @id_instrumento; "


            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)

                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds)

            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectDatosAsignaCategoriaMant = " + ex.Message.ToString)
        Finally

        End Try

        Return ds
    End Function


End Class
