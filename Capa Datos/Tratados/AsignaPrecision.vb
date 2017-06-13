Imports Capa_Entidad
Imports System.Data.SqlClient

Public Class AsignaPrecision
    Dim objConeccion As New ConectarService
    Dim objGeneral As New General

    'Funcion que elimina la precision asociada al inciso
    Public Function DeletePrecision(ByVal objCEIncisoAsocia As CEIncisoAsociaCategoria) As Boolean
        Dim estado As Boolean = False

        Try
            Dim sql_query As String

            sql_query = " SELECT " +
                " VB.id_version, VB.anio_version  " +
                " From " +
                " SAC_Versiones_Bitacora VB " +
                " WHERE " +
                " VB.estado = 'A' "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                conexion.Open()
                Dim valores As SqlDataReader = command.ExecuteReader()

                If valores.Read() Then
                    Dim id_version As Integer = valores("id_version")
                    Dim anio_version As Integer = valores("anio_version")

                    'Query elimina detalle
                    sql_query = " DELETE " +
                        " SAC_Asocia_Categoria " +
                        " WHERE " +
                        " id_instrumento = @id_instrumento AND " +
                        " id_version = @id_version AND" +
                        " anio_version =  @anio_version AND " +
                        " codigo_inciso = @codigo_inciso AND " +
                        " inciso_presicion = @inciso_presicion "

                    Using cn = objConeccion.Conectar
                        Dim command1 As New SqlCommand(sql_query, cn)
                        command1.Parameters.AddWithValue("id_instrumento", objCEIncisoAsocia.id_instrumento)
                        'command1.Parameters.AddWithValue("id_categoria", objCEIncisoAsocia.id_categoria)
                        command1.Parameters.AddWithValue("codigo_inciso", objCEIncisoAsocia.codigo_inciso)
                        command1.Parameters.AddWithValue("inciso_presicion", objCEIncisoAsocia.codigo_precision)
                        command1.Parameters.AddWithValue("id_version", id_version)
                        command1.Parameters.AddWithValue("anio_version", anio_version)
                        cn.Open()

                        If command1.ExecuteNonQuery() > 0 Then
                            'Si elimina inciso categoria
                            estado = True
                        Else
                            'Si ocurre error
                            estado = False
                        End If

                    End Using

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

    'Funcion que almacena precision 
    Public Function InsertPrecision(ByVal objCEPrecision As CEIncisoAsociaCategoria) As Boolean
        Dim estado As Boolean = False
        Dim id_instrumento As Integer = objCEPrecision.id_instrumento
        Dim codigo_inciso As String = objCEPrecision.codigo_inciso
        Dim id_categoria As Integer = objCEPrecision.id_categoria
        Dim codigo_precision As String = objCEPrecision.codigo_precision
        Dim texto_precision As String = objCEPrecision.texto_precision
        Dim inciso_presicion As String = codigo_inciso + codigo_precision

        Try

            'Se llama a la funcion valida inciso para verificar si inciso ya existe con otra categoria
            If objGeneral.ValidaInciso(id_instrumento, codigo_inciso, inciso_presicion) Then
                'Si existe inciso, actuliza categoria para inciso
                estado = objGeneral.UpdateInciso(id_instrumento, id_categoria, codigo_inciso, inciso_presicion, texto_precision)
            Else
                'Si no existe inciso, inserta
                estado = objGeneral.InsertInciso(id_instrumento, id_categoria, codigo_inciso, inciso_presicion, texto_precision)
            End If

        Catch ex As Exception
            estado = False
        Finally

        End Try
        Return estado
    End Function

    'Funcion que devuelve datos del inciso precision
    Public Function SelectIncisoPrecision(ByVal id_instrumento As Integer, ByVal codigo_inciso As String, ByVal inciso_presicion As String) As DataTable
        Dim dt_inciso_precision As New DataTable
        Try
            Dim sql_query As String

            If inciso_presicion Is Nothing Then
                sql_query = "  select " +
                " cast(dai_base as numeric(8,2)) as dai_base, texto_inciso, " +
                " SAC.id_categoria, sac.texto_precision, sac.inciso_presicion " +
                " from " +
                " SAC_Incisos SI " +
                " Left Join " +
                " (SELECT " +
                " id_instrumento, id_categoria, codigo_inciso, texto_precision, inciso_presicion " +
                " FROM " +
                " SAC_Asocia_Categoria " +
                " where " +
                " id_instrumento = @id_instrumento AND " +
                " inciso_presicion is NULL)	as SAC ON " +
                " SAC.codigo_inciso = SI.codigo_inciso " +
                " where " +
                " estado = 'A' AND  " +
                " SI.codigo_inciso = @codigo_inciso "


                Using cn = objConeccion.Conectar
                    Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                    command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                    command.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                    Dim da As SqlDataAdapter
                    da = New SqlDataAdapter(command)
                    da.Fill(dt_inciso_precision)

                End Using
            Else
                sql_query = "  select " +
                " cast(dai_base as numeric(8,2)) as dai_base, texto_inciso, " +
                " SAC.id_categoria, sac.texto_precision, sac.inciso_presicion " +
                " from " +
                " SAC_Incisos SI " +
                " Left Join " +
                " (SELECT " +
                " id_instrumento, id_categoria, codigo_inciso, texto_precision, inciso_presicion " +
                " FROM " +
                " SAC_Asocia_Categoria " +
                " where " +
                " id_instrumento = @id_instrumento)	as SAC ON " +
                " SAC.codigo_inciso = SI.codigo_inciso " +
                " where " +
                " estado = 'A' AND  " +
                " SI.codigo_inciso = @codigo_inciso AND " +
                " SAC.inciso_presicion = @inciso_presicion "

                Using cn = objConeccion.Conectar
                    Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                    command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                    command.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                    command.Parameters.AddWithValue("inciso_presicion", inciso_presicion)
                    Dim da As SqlDataAdapter
                    da = New SqlDataAdapter(command)
                    da.Fill(dt_inciso_precision)

                End Using
            End If



        Catch ex As Exception

        Finally

        End Try

        Return dt_inciso_precision
    End Function


End Class
