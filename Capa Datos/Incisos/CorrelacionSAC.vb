Imports System.Data.SqlClient
Imports Capa_Entidad
Public Class CorrelacionSAC
    Dim objConeccion As New ConectarService
    Dim da As SqlDataAdapter
    Dim ds As New DataSet

    'Funcion para obtener los datos para los codigos seleccionados
    Public Function SelectDatosCodigoIncisoCorrelacion(ByVal str_codigo As String, ByVal anio_version As Integer, ByVal id_version As Integer) As DataSet
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
                "  " +
                "  SELECT " +
                " ci.codigo_inciso, ci.texto_inciso, ci.dai_base,  " +
                " sc.estado,  " +
                " sc.inciso_nuevo as codigo_inciso_corr,  " +
                " sc.texto_inciso as texto_inciso_corr, sc.dai_nuevo as dai_corr  " +
                " FROM " +
                " SAC_Incisos CI " +
                " LEFT OUTER JOIN " +
                " (SELECT  " +
                " inciso_origen, inciso_nuevo, " +
                " texto_inciso, dai_nuevo,  " +
                " anio_version, version, " +
                " CASE WHEN (inciso_nuevo IS NULL)  THEN 'SUPRIMIDA' ELSE 'APERTURA' END as estado " +
                " FROM " +
                " SAC_Correlacion) SC ON " +
                " sc.inciso_origen = ci.codigo_inciso And " +
                " sc.version = ci.id_version And " +
                " sc.anio_version = ci.anio_version " +
                " WHERE " +
                " CI.estado = 'A' AND " +
                " ci.anio_version = @anio_version AND " +
                " ci.id_version = @id_version AND " +
                " CI.codigo_inciso LIKE @codigo_inciso+'%' " +
                " union " +
                " (SELECT " +
                " inciso_nuevo, " +
                " texto_inciso, " +
                " dai_nuevo,  " +
                " CASE WHEN (inciso_nuevo IS NULL)  THEN 'SUPRIMIDA' ELSE 'APERTURA' END as estado , " +
                " '' as codigo_inciso_corr, '' as texto_inciso_corr, 0.00 as dai_corr " +
                " FROM " +
                " SAC_Correlacion " +
                " where inciso_origen Is NULL " +
                " and anio_version = @anio_version " +
                " and version = @id_version " +
                " and inciso_nuevo like @codigo_inciso+'%') "


            '" SELECT " +
            '" ci.codigo_inciso, ci.texto_inciso, ci.dai_base, " +
            '" sc.estado, " +
            '" sc.inciso_nuevo as codigo_inciso_corr, " +
            '" sc.texto_inciso as texto_inciso_corr, sc.dai_nuevo as dai_corr " +
            '" FROM " +
            '" SAC_Incisos CI " +
            '" LEFT OUTER JOIN" +
            '" (SELECT " +
            '" inciso_origen, inciso_nuevo, " +
            '" texto_inciso, dai_nuevo, " +
            '" anio_version, version," +
            '" CASE WHEN (inciso_nuevo IS NULL)  THEN 'SUPRIMIDA' ELSE 'APERTURA' END as estado " +
            '" FROM " +
            '" SAC_Correlacion) SC ON " +
            '" sc.inciso_origen = ci.codigo_inciso And " +
            '" sc.version = ci.id_version And " +
            '" sc.anio_version = ci.anio_version " +
            '" WHERE " +
            '" CI.estado = 'A' AND  " +
            '" CI.codigo_inciso LIKE '" + str_codigo + "%' "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                command.Parameters.AddWithValue("capitulo", capitulo)
                command.Parameters.AddWithValue("partida", partida)
                command.Parameters.AddWithValue("subpartida", subpartida)
                command.Parameters.AddWithValue("codigo_inciso", str_codigo)
                command.Parameters.AddWithValue("anio_version", anio_version)
                command.Parameters.AddWithValue("id_version", id_version)

                da = New SqlDataAdapter(command)
                da.Fill(ds)

            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectDatosInciso = " + ex.Message.ToString)
        Finally

        End Try

        Return ds
    End Function

    'Funcion para obtener los datos del Mantenimiento de Correlacion 
    Public Function SelectCorrelacionMant() As DataSet
        Try
            Dim sql_string As String

            sql_string = "  SELECT id_version, anio_version, enmienda, " +
                " anio_inicia_enmienda,anio_fin_enmieda, " +
                " 'Version '+enmienda+', Enero '+convert(varchar(10),anio_inicia_enmienda) as descripcion " +
                " FROM SAC_VERSIONES_BITACORA " +
                " WHERE estado = 'A' " +
                " SELECT anio_version, enmienda, " +
                " anio_inicia_enmienda,anio_fin_enmieda, " +
                " 'Version '+enmienda+', Enero '+convert(varchar(10),anio_inicia_enmienda) as descripcion " +
                " FROM SAC_VERSIONES_BITACORA" +
                " WHERE estado Is NULL"

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds)

            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectCorrelacionMant = " + ex.Message.ToString)
        Finally

        End Try

        Return ds
    End Function

    'Metodo para Insertar nueva Version SAC
    Public Function InsertVersionSAC(ByVal objVersionSAC As CEEnmiendas) As Boolean
        Try
            Dim sql_query As String
            Dim id_version As Integer

            sql_query = "select MAX(id_version)" +
                " from SAC_Versiones_Bitacora " +
                " where anio_version = @anio_version "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("anio_version", objVersionSAC.anio_version)
                cn.Open()
                id_version = IIf(IsDBNull(command.ExecuteScalar), 0, command.ExecuteScalar) + 1
            End Using

            sql_query = " INSERT INTO SAC_Versiones_Bitacora " +
                " ([id_version] " +
                " ,[anio_version] " +
                " ,[enmienda] " +
                " ,[observaciones] " +
                " ,[anio_inicia_enmienda] " +
                " ,[anio_fin_enmieda] " +
                " ,[fecha_inicia_vigencia] " +
                " ,[fecha_fin_vigencia]) " +
                " VALUES " +
                " (@id_version " +
                " ,@anio_version " +
                " ,@enmienda " +
                " ,@observaciones " +
                " ,YEAR(@fecha_inicia_vigencia) " +
                " ,YEAR(@fecha_fin_vigencia) " +
                " ,@fecha_inicia_vigencia " +
                " ,@fecha_fin_vigencia ) "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_version", id_version)
                command.Parameters.AddWithValue("anio_version", objVersionSAC.anio_version)
                command.Parameters.AddWithValue("enmienda", objVersionSAC.enmienda)
                command.Parameters.AddWithValue("observaciones", objVersionSAC.observaciones)
                command.Parameters.AddWithValue("fecha_inicia_vigencia", objVersionSAC.fecha_inicia_vigencia)
                command.Parameters.AddWithValue("fecha_fin_vigencia", objVersionSAC.fecha_fin_vigencia)
                conexion.Open()
                command.ExecuteScalar()

                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    'Metodo para Actualizar version SAC
    Public Function UpdateVersionSAC(ByVal objVersionSAC As CEEnmiendas) As Boolean
        Try
            Dim sql_query As String
            sql_query = " UPDATE SAC_Versiones_Bitacora " +
                " SET " +
                " enmienda = @enmienda " +
                " ,observaciones = @observaciones " +
                " ,fecha_inicia_vigencia = @fecha_inicia_vigencia " +
                " ,fecha_fin_vigencia = @fecha_fin_vigencia " +
                " , anio_inicia_enmienda = YEAR(@fecha_inicia_vigencia) " +
                " , anio_fin_enmieda = YEAR(@fecha_fin_vigencia ) " +
                " WHERE " +
                " id_version = @id_version AND " +
                " anio_version = @anio_version "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_version", objVersionSAC.id_version)
                command.Parameters.AddWithValue("anio_version", objVersionSAC.anio_version)
                command.Parameters.AddWithValue("enmienda", objVersionSAC.enmienda)
                command.Parameters.AddWithValue("observaciones", objVersionSAC.observaciones)
                command.Parameters.AddWithValue("fecha_inicia_vigencia", objVersionSAC.fecha_inicia_vigencia)
                command.Parameters.AddWithValue("fecha_fin_vigencia", objVersionSAC.fecha_fin_vigencia)
                conexion.Open()
                command.ExecuteScalar()

                Return True
            End Using
        Catch ex As SqlException
            Return False
        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    'Funcion para seleccionar el SAC segun el id_version_sac
    Public Function SelectVersionSACMant(ByVal id_version_sac As Integer, ByVal anio_version As Integer) As DataTable
        Dim sql_query As String
        Dim dtSAC As New DataTable

        sql_query = " SELECT anio_version, " +
            " enmienda, fecha_inicia_vigencia, " +
            " fecha_fin_vigencia, observaciones" +
            " FROM SAC_Versiones_Bitacora " +
            " WHERE id_version = @id_version AND " +
            " anio_version = @anio_version "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_version", id_version_sac)
                command.Parameters.AddWithValue("anio_version", anio_version)
                da = New SqlDataAdapter(command)

                da.Fill(dtSAC)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR VERSION SAC = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtSAC

        End Using
    End Function

    'Funcion para llenar el GridView de Enmiendas del SAC
    Public Function SelectEnmiendas() As DataSet
        'Se Llena el Data Set por medio del la consulta y se retorna el mismo
        Dim sql_query As String

        sql_query = " SELECT id_version, anio_version, " +
            " enmienda, fecha_inicia_vigencia, " +
            " fecha_fin_vigencia, estado, observaciones" +
            " FROM SAC_Versiones_Bitacora " +
            " order BY anio_version DESC, id_version DESC "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                da = New SqlDataAdapter(sql_query, cn)
                da.Fill(ds)

            Catch ex As Exception
                MsgBox("ERROR CONSULTA VERSIONES ENMIENDAS SAC = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
                da.Dispose()
            End Try

            Return ds

        End Using
    End Function


    'Funcion que obtiene los datos para el mantenimiento de apertura arancelaria
    Public Function SelectIncisoApertura(ByVal codigo_inciso As String) As DataTable
        Dim dt_inciso_apertura As New DataTable
        Try
            Dim sql_query As String

            Dim codigo_partida As String = codigo_inciso.Substring(0, 4)
            Dim codigo_subpartida As String = codigo_inciso.Substring(0, 6)


            sql_query = " select cast(SI.dai_base as numeric(8,2)) as dai_base, " +
                " si.texto_inciso, @codigo_partida as partida, sp.descripcion_partida, " +
                " @codigo_subpartida as subpartida, ss.texto_subpartida " +
                " from SAC_Incisos SI " +
                " left Join " +
                " SAC_Partidas sp ON " +
                " sp.Partida like @codigo_partida+'%' AND " +
                " sp.activo = 'S' " +
                " left Join " +
                " SAC_Subpartidas ss ON " +
                " ss.subpartida like @codigo_subpartida+'%' AND " +
                " ss.activo = 'S' " +
                " where si.estado = 'A' AND  " +
                " si.codigo_inciso = @codigo_inciso "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                command.Parameters.AddWithValue("codigo_partida", codigo_partida)
                command.Parameters.AddWithValue("codigo_subpartida", codigo_subpartida)

                da = New SqlDataAdapter(command)
                da.Fill(dt_inciso_apertura)

            End Using

        Catch ex As Exception

        Finally

        End Try

        Return dt_inciso_apertura
    End Function

    'Funcion que almacena la correlacion del inciso seleccionado
    Public Function InsertApertura(ByVal objCorrelacion As CEEnmiendas) As Boolean
        Dim estado As Boolean = False
        Try
            Dim sql_query As String
            sql_query = " INSERT INTO SAC_Correlacion " +
                " ([inciso_origen] ,[inciso_nuevo] " +
                " ,[texto_inciso] ,[comentarios] " +
                " ,[normativa] ,[dai_base] " +
                " ,[dai_nuevo] ,[anio_version] " +
                " ,[anio_nueva_version] ,[version] " +
                " ,[fin_vigencia] ,[inicio_vigencia], [fecha_inicio_vigencia]) " +
                " VALUES " +
                " (@inciso_origen ,@inciso_nuevo " +
                " ,@texto_inciso ,@comentarios " +
                " ,@normativa ,@dai_base " +
                " ,@dai_nuevo ,@anio_version " +
                " ,@anio_nueva_version ,@version " +
                " ,@fin_vigencia ,@inicio_vigencia, @fecha_inicio_vigencia) "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                If objCorrelacion.inciso_origen Is Nothing Then
                    command.Parameters.AddWithValue("inciso_origen", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("inciso_origen", objCorrelacion.inciso_origen)
                End If

                command.Parameters.AddWithValue("inciso_nuevo", objCorrelacion.inciso_nuevo)
                command.Parameters.AddWithValue("texto_inciso", objCorrelacion.texto_inciso)
                command.Parameters.AddWithValue("comentarios", objCorrelacion.observaciones)
                command.Parameters.AddWithValue("normativa", objCorrelacion.normativa)
                command.Parameters.AddWithValue("dai_base", objCorrelacion.dai_base)
                command.Parameters.AddWithValue("dai_nuevo", objCorrelacion.dai_nuevo)
                command.Parameters.AddWithValue("anio_version", objCorrelacion.anio_version)
                command.Parameters.AddWithValue("anio_nueva_version", objCorrelacion.anio_nueva_version)
                command.Parameters.AddWithValue("version", objCorrelacion.id_version)
                command.Parameters.AddWithValue("fin_vigencia", objCorrelacion.fecha_fin_vigencia.Year)
                command.Parameters.AddWithValue("inicio_vigencia", objCorrelacion.fecha_inicia_vigencia.Year)
                command.Parameters.AddWithValue("fecha_inicio_vigencia", objCorrelacion.fecha_inicia_vigencia)
                cn.Open()
                command.ExecuteScalar()
                estado = True
            End Using


        Catch ex As Exception
            estado = False
        Finally

        End Try
        Return estado
    End Function

    'Funcion para almacenar la supresion del iniciso seleccionado
    Public Function InsertSupresion(ByVal objCorrelacion As CEEnmiendas) As Boolean
        Dim estado As Boolean = False
        Try
            Dim sql_query As String
            sql_query = " INSERT INTO SAC_Correlacion " +
                " ([inciso_origen] ,[dai_base] " +
                " ,[anio_version], [anio_nueva_version] ,[version])" +
                " VALUES " +
                " (@inciso_origen ,@dai_base " +
                " ,@anio_version, @anio_nueva_version, @version)  "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("inciso_origen", objCorrelacion.inciso_origen)
                command.Parameters.AddWithValue("dai_base", objCorrelacion.dai_base)
                command.Parameters.AddWithValue("anio_version", objCorrelacion.anio_version)
                command.Parameters.AddWithValue("anio_nueva_version", objCorrelacion.anio_nueva_version)
                command.Parameters.AddWithValue("version", objCorrelacion.id_version)

                cn.Open()
                command.ExecuteScalar()
                estado = True
            End Using
        Catch ex As Exception
            estado = False
        Finally

        End Try
        Return estado
    End Function

    'Funcion para borrar accion de enmienda
    Public Function DeleteAccion(ByVal objCECorrelacion As CEEnmiendas) As Boolean
        Dim estado As Boolean = False

        Try
            Dim sql_query As String
            Dim longitud_inciso_corr As Integer

            longitud_inciso_corr = objCECorrelacion.inciso_nuevo.Length

            If longitud_inciso_corr > 0 Then
                'Si inciso correlacion no esta vacio

                'Query elimina detalle
                sql_query = " DELETE " +
                    " SAC_Correlacion " +
                    " where " +
                    " anio_version = @anio_version AND " +
                    " anio_nueva_version = @anio_nueva_version AND " +
                    " version = @version AND " +
                    " inciso_origen = @codigo_inciso AND " +
                    " inciso_nuevo = @inciso_correlacion "

                Using cn = objConeccion.Conectar
                    Dim command1 As New SqlCommand(sql_query, cn)
                    command1.Parameters.AddWithValue("anio_version", objCECorrelacion.anio_version)
                    command1.Parameters.AddWithValue("anio_nueva_version", objCECorrelacion.anio_nueva_version)
                    command1.Parameters.AddWithValue("version", objCECorrelacion.id_version)
                    command1.Parameters.AddWithValue("codigo_inciso", objCECorrelacion.inciso_origen)
                    command1.Parameters.AddWithValue("inciso_correlacion", objCECorrelacion.inciso_nuevo)
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
                'Si inciso correlacion esta vacio

                'Query elimina detalle
                sql_query = " DELETE " +
                    " SAC_Correlacion " +
                    " where " +
                    " anio_version = @anio_version AND " +
                    " anio_nueva_version = @anio_nueva_version AND " +
                    " version = @version AND " +
                    " inciso_origen = @codigo_inciso AND " +
                    " inciso_nuevo Is NULL "

                Using cn = objConeccion.Conectar
                    Dim command1 As New SqlCommand(sql_query, cn)
                    command1.Parameters.AddWithValue("anio_version", objCECorrelacion.anio_version)
                    command1.Parameters.AddWithValue("anio_nueva_version", objCECorrelacion.anio_nueva_version)
                    command1.Parameters.AddWithValue("version", objCECorrelacion.id_version)
                    command1.Parameters.AddWithValue("codigo_inciso", objCECorrelacion.inciso_origen)
                    cn.Open()

                    If command1.ExecuteNonQuery() > 0 Then
                        'Si elimina inciso categoria
                        estado = True
                    Else
                        'Si ocurre error
                        estado = False
                    End If

                End Using

            End If

        Catch ex As Exception
            estado = False
        Finally

        End Try

        Return estado
    End Function

    'Funcion que valida si inciso nuevo ya existe y si ya esta supreso
    Public Function ValidaIncisoNuevo(ByVal objCeCorrelacion As CEEnmiendas) As Boolean
        Dim estado As Boolean = True
        Dim estado_incisos As Boolean = True
        Dim estado_correlacion As Boolean = True
        Try
            Dim sql_query As String
            sql_query = " SELECT " +
                " COALESCE(count(1), 0) " +
                " FROM " +
                " SAC_Incisos " +
                " where " +
                " id_version = @id_version AND anio_version = @anio_version and codigo_inciso = @codigo_inciso " +
                " AND estado = 'A' "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_version", objCeCorrelacion.id_version)
                command.Parameters.AddWithValue("anio_version", objCeCorrelacion.anio_version)
                command.Parameters.AddWithValue("codigo_inciso", objCeCorrelacion.inciso_nuevo)
                cn.Open()

                If command.ExecuteScalar() > 0 Then
                    estado_incisos = True
                Else
                    estado_incisos = False
                End If
            End Using

            sql_query = " SELECT " +
                " COALESCE(count(1), 0) " +
                " From " +
                " SAC_Correlacion " +
                " where " +
                " inciso_origen = @codigo_inciso " +
                " AND version = @id_version  " +
                " AND anio_version = @anio_version " +
                " AND inciso_nuevo is NULL "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_version", objCeCorrelacion.id_version)
                command.Parameters.AddWithValue("anio_version", objCeCorrelacion.anio_version)
                command.Parameters.AddWithValue("codigo_inciso", objCeCorrelacion.inciso_nuevo)
                cn.Open()

                If command.ExecuteScalar() > 0 Then
                    estado_correlacion = True
                Else
                    estado_correlacion = False
                End If

            End Using

            If estado_correlacion And estado_incisos Then
                'Si existe inciso y existe suprecion 
                estado = False
            ElseIf Not estado_correlacion And Not estado_incisos Then
                'Si no existe inciso y tampoco suprecion
                estado = False
            Else
                'si existe inciso pero no suprecion
                estado = True
            End If

        Catch ex As Exception

        End Try
        Return estado
    End Function

    'Funcion que valida si inciso nuevo ya existe
    Public Function ValidaIncisoNuevo(ByVal codigo_inciso As String) As Boolean
        Dim estado As Boolean = True
        Dim estadoIncisos As Boolean = True
        Dim estadoCorrelacion As Boolean = True
        Try
            Dim sql_query As String
            sql_query = " SELECT " +
                " COALESCE(count(1), 0) " +
                " FROM " +
                " SAC_Incisos " +
                " where " +
                " codigo_inciso = @codigo_inciso " +
                " AND estado = 'A' "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)

                command.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                cn.Open()

                If command.ExecuteScalar() > 0 Then
                    estadoIncisos = True
                Else
                    estadoIncisos = False
                End If
            End Using

            sql_query = " SELECT " +
                " COALESCE(count(1), 0) " +
                " FROM " +
                " SAC_Correlacion " +
                " where " +
                " inciso_nuevo = @codigo_inciso "


            Using cn = objConeccion.Conectar
                Dim Command As SqlCommand = New SqlCommand(sql_query, cn)

                Command.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                cn.Open()

                If Command.ExecuteScalar() > 0 Then
                    estadoCorrelacion = True
                Else
                    estadoCorrelacion = False
                End If

            End Using

            If estadoIncisos Or estadoCorrelacion Then
                estado = True
            Else
                estado = False
            End If


        Catch ex As Exception

        End Try
        Return estado
    End Function

    'Funcion que obtiene datos de partida y subpartida para Apertura de comieco
    Public Function SelectDatosApertura(ByVal inciso As String) As DataSet
        Dim Respuesta As New DataSet
        Dim sql_query As String
        Try

            sql_query = " select " +
                " partida, Descripcion_Partida " +
                " FROM " +
                " SAC_Partidas " +
                " where " +
                " Partida LIKE @partida "

            Using cn = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, cn)
                Dim partida As String
                partida = inciso.Substring(0, 4)
                command.Parameters.AddWithValue("partida", partida)
                Dim DataAdapter As New SqlDataAdapter(command)
                cn.Open()
                DataAdapter.Fill(Respuesta)
                cn.Close()
            End Using

            sql_query = " SELECT " +
                " subpartida, texto_subpartida " +
                " from " +
                " SAC_Subpartidas " +
                " where " +
                " rtrim(subpartida)  like  @subpartida+'%' "

            Using cn = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, cn)
                Dim dataSet As New DataSet

                Dim subpartida As String
                subpartida = inciso.Substring(0, 5)
                command.Parameters.AddWithValue("subpartida", subpartida)
                Dim dataAdapter As New SqlDataAdapter(command)
                cn.Open()
                dataAdapter.Fill(dataSet)

                For Each row As DataRow In dataSet.Tables(0).Rows
                    If row("subpartida").ToString.Trim.Length = 5 Then
                        If row("subpartida").ToString.Trim = inciso.Substring(0, 5) Then

                            Dim datatable As DataTable = Respuesta.Tables.Add("subpartidas")
                            datatable.Columns.Add("subpartida", Type.GetType("System.String"))
                            datatable.Columns.Add("texto_subpartida", Type.GetType("System.String"))
                            datatable.ImportRow(row)

                            Exit For
                        End If
                    ElseIf row("subpartida").ToString.Trim.Length = 6 Then
                        If row("subpartida").ToString.Trim = inciso.Substring(0, 6) Then

                            Dim datatable As DataTable = Respuesta.Tables.Add("subpartidas")
                            datatable.Columns.Add("subpartida", Type.GetType("System.String"))
                            datatable.Columns.Add("texto_subpartida", Type.GetType("System.String"))
                            datatable.ImportRow(row)

                            Exit For
                        End If
                    ElseIf row("subpartida").ToString.Trim.Length = 7 Then
                        If row("subpartida").ToString.Trim = inciso.Substring(0, 7) Then
                            Dim datatable As DataTable = Respuesta.Tables.Add("subpartidas")
                            datatable.Columns.Add("subpartida", Type.GetType("System.String"))
                            datatable.Columns.Add("texto_subpartida", Type.GetType("System.String"))
                            datatable.ImportRow(row)

                            Exit For
                        End If
                    Else
                        Dim datatable As DataTable = Respuesta.Tables.Add("subpartidas")
                        datatable.Columns.Add("subpartida", Type.GetType("System.String"))
                        datatable.Columns.Add("texto_subpartida", Type.GetType("System.String"))

                    End If
                Next

            End Using


        Catch ex As SqlException

        Catch ex As Exception

        End Try

        Return Respuesta
    End Function

End Class
