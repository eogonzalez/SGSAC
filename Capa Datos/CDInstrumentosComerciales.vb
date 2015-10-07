﻿Imports System.Text
Imports System.Data.SqlClient
Imports Capa_Entidad

Public Class CDInstrumentosComerciales
    Dim objConeccion As New ConectarService
    Dim da As SqlDataAdapter
    Dim ds As New DataSet

#Region "Funciones y procedimientos para el Mantenimiento de Instrumentos"

    'Funcion para calcular el DAI 
    Public Function CalcularDAI(ByVal id_instrumento As Integer) As Boolean
        Dim estado As Boolean = False
        Try
            Dim sql_query As String
            Dim dt_RegistroActivo As New DataTable

            'Query para obtener registros activos
            sql_query = "SELECT COALESCE(( " +
                " Select count(1) " +
                " FROM SAC_TRATADOS_BITACORA " +
                " WHERE id_instrumento = @id_instrumento " +
                " AND ESTADO ='A'),0) registro_activo "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                da = New SqlDataAdapter(command)
                da.Fill(dt_RegistroActivo)
            End Using

            If dt_RegistroActivo.Rows(0)("registro_activo") = 0 Then
                estado = False
            Else
                Dim dt_Asociadas As New DataTable
                'Query para obtener cantidad de incisos asociados
                sql_query = "SELECT COALESCE((" +
                    " SELECT COUNT(1) FROM SAC_ASOCIA_CATEGORIA " +
                    " WHERE id_instrumento = @id_instrumento " +
                    " AND ESTADO ='A'),0) as asociadas "

                Using cn = objConeccion.Conectar
                    Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                    command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                    da = New SqlDataAdapter(command)
                    da.Fill(dt_Asociadas)
                End Using
                'If Not (dt_Asociadas.Rows(0)("asociadas") > 99) Then

                If Not (dt_Asociadas.Rows(0)("asociadas") >= 0) Then
                    estado = False
                Else
                    Dim dt_Correlativo As New DataTable
                    Dim corte_version As Integer
                    'Query para obtener el ultimo numero de correlativo
                    sql_query = " SELECT COALESCE(( " +
                        " SELECT MAX(ID_CORTE_VERSION)  " +
                        " FROM SAC_TRATADOS_BITACORA " +
                        " WHERE id_instrumento = @id_instrumento " +
                        " AND ESTADO ='A'),0) version "
                    Using cn = objConeccion.Conectar
                        Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                        command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                        da = New SqlDataAdapter(command)
                        da.Fill(dt_Correlativo)

                    End Using

                    corte_version = Convert.ToInt32(dt_Correlativo.Rows(0)("version").ToString()) + 1
                    Dim dt_Categorias As New DataTable
                    'Query que consulta los tramos por categoria e instrumento
                    sql_query = "SELECT id_categoria " +
                        " FROM IC_Categorias_Desgravacion_Tramos " +
                        " WHERE id_instrumento = @id_instrumento " +
                        " AND activo = 'S' "
                    Using cn = objConeccion.Conectar
                        Dim cuenta As Integer = 0
                        Dim id_catego As Integer = 0

                        Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                        command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                        da = New SqlDataAdapter(command)
                        da.Fill(dt_Categorias)



                        For Each Row As DataRow In dt_Categorias.Rows
                            id_catego = Convert.ToInt32(Row("id_categoria").ToString)

                            sql_query = " INSERT INTO " +
                                " SAC_Dai_Instrumento(id_instrumento, INCISO, CATEGORIA, " +
                                " FACTOR_DESGRAVA, DESGRAVA_TRAMOS_ANTES, " +
                                " DAI_CALC_ABSOLUTO, DAI_BASE, sigla1_instrumento, " +
                                " ID_CORTE_NUEVO, USUARIO_GENERO, FECHA_GENERADA, estado) " +
                                " SELECT " +
                                " @id_instrumento, A.codigo_inciso, A.id_categoria," +
                                " B.factor_desgrava,B.desgrava_tramo_anterior, " +
                                " (I.dai_base-((((((@id_corte_nuevo-B.cortes_ejecutados)+1)*B.factor_desgrava)+B.desgrava_tramo_anterior)/100)*I.dai_base)), " +
                                " I.dai_base,C.sigla, " +
                                " @id_corte_nuevo, " +
                                " 'ADMIN',SYSDATETIME(),'CALC1' " +
                                " FROM " +
                                " SAC_Asocia_Categoria AS A " +
                                " LEFT OUTER JOIN " +
                                " SAC_Incisos AS I ON " +
                                " A.codigo_inciso = I.codigo_inciso AND " +
                                " A.id_categoria = @id_categoria " +
                                " LEFT OUTER JOIN " +
                                " IC_Categorias_Desgravacion_Tramos AS B ON " +
                                " A.id_instrumento = B.id_instrumento And " +
                                " A.id_categoria = B.id_categoria " +
                                " LEFT OUTER JOIN " +
                                " IC_Instrumentos AS C ON " +
                                " A.id_instrumento = C.id_instrumento " +
                                " WHERE " +
                                " I.estado = 'A' AND " +
                                " B.activo = 'S' AND " +
                                " B.cortes_ejecutados <= @id_corte_nuevo And " +
                                " @id_corte_nuevo < (B.cortes_ejecutados + B.cantidad_cortes) " +
                                " ORDER BY " +
                                " A.codigo_inciso, B.id_tramo "

                            Using cn2 = objConeccion.Conectar
                                Dim command2 As SqlCommand = New SqlCommand(sql_query, cn2)
                                command2.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                command2.Parameters.AddWithValue("id_corte_nuevo", corte_version)
                                command2.Parameters.AddWithValue("id_categoria", id_catego)

                                cn2.Open()
                                command2.ExecuteScalar()
                            End Using

                            cuenta = cuenta + 1

                        Next

                        sql_query = " INSERT INTO " +
                            " SAC_TRATADOS_BITACORA(id_version, id_corte_version, id_instrumento, CANTIDAD_CATEGORIA, estado, FECHA_GENERADA)  " +
                            " VALUES " +
                            " (0, @id_instrumento, @id_corte_nuevo, @cuenta, 'A',SYSDATETIME()) "

                        Using cn2 = objConeccion.Conectar
                            Dim command2 As SqlCommand = New SqlCommand(sql_query, cn2)
                            command2.Parameters.AddWithValue("id_instrumento", id_instrumento)
                            command2.Parameters.AddWithValue("id_corte_nuevo", corte_version)
                            command2.Parameters.AddWithValue("cuenta", cuenta)

                            cn2.Open()
                            command2.ExecuteScalar()
                        End Using
                    End Using

                    estado = True
                End If


            End If

        Catch ex As Exception
            estado = False
        Finally

        End Try
        Return estado
    End Function

    'Funcion para obtener datos para el formulario de calculo de DAI
    Public Function SelectInstrumentoCalculoDAI(ByVal id_instrumento As Integer) As DataSet
        Dim sql_query As String


        sql_query = " SELECT " +
            " II.nombre_instrumento, " +
            " II.sigla, " +
            " TI.descripcion, " +
            " ii.fecha_vigencia " +
            " FROM " +
            " IC_Instrumentos II " +
            " LEFT OUTER JOIN " +
            " IC_Tipo_Instrumento TI ON " +
            " II.id_tipo_instrumento = TI.id_tipo_instrumento " +
            " WHERE " +
            " II.id_instrumento = @id_instrumento " +
            " GROUP BY " +
            " II.nombre_instrumento, " +
            " TI.descripcion,  " +
            " II.sigla,  " +
            " II.fecha_vigencia; "

        sql_query = sql_query +
            " SELECT " +
            " COUNT(1) AS cantidad_incisos_calcular " +
            " FROM " +
            " SAC_Asocia_Categoria " +
            " WHERE " +
            " id_instrumento = @id_instrumento;"

        sql_query = sql_query +
            " SELECT " +
            " COUNT(1) as cantidad_cortes_ejecutados, " +
            " MAX(id_corte_version) as ultimo_corte_ejecutado " +
            " FROM " +
            " SAC_Tratados_Bitacora " +
            " WHERE " +
            " id_instrumento = @id_instrumento "


        Using cn = objConeccion.Conectar
            Try
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                da = New SqlDataAdapter(command)

                da.Fill(ds)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR DATOS CALCULO DAI = " + ex.Message.ToString)

            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try
            Return ds
        End Using
    End Function

    'Funcion para llenar el GridView de Instrumentos
    Public Function SelectInstrumentos() As DataSet
        'Se Llena el Data Set por medio del procedimiento almacenado y se retorna el mismo
        Dim sql_query As String

        sql_query = " SELECT id_instrumento, sigla, nombre_instrumento, fecha_firma, fecha_ratificada, fecha_vigencia " +
            " FROM IC_Instrumentos " +
            " WHERE estado = 1 "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                da = New SqlDataAdapter(sql_query, cn)
                da.Fill(ds, "Ls_Instrumentos")

            Catch ex As Exception
                MsgBox("ERROR CONSULTA INSTRUMENTOS = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
                da.Dispose()
            End Try

            Return ds

        End Using
    End Function

    'Funcion para seleccionar listado del combo tipo de instrumetos
    Public Function SelectTipoInstrumento() As DataSet
        Try
            Dim sql_string As String

            sql_string = " SELECT id_tipo_instrumento, descripcion, observaciones " +
                " from IC_Tipo_Instrumento "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "TimpoIns")
            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectTipoInstrumento = " + ex.Message.ToString)
        Finally

        End Try

        Return ds

    End Function

    'Funcion para seleccionar listado del combo tipo de relaciones de instrumentos
    Public Function SelectTipoRelacionInstrumento() As DataSet
        Try
            Dim sql_string As String

            sql_string = "select id_tipo_relacion_instrumento, descripcion, observaciones " +
                " from IC_Tipo_Relacion_Instrumento "

            Using cn = objConeccion.Conectar

                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "TimpoRelIns")

            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectTipoRelacionInstrumento = " + ex.Message.ToString)
        Finally

        End Try

        Return ds

    End Function

    'Funcion para seleccionar el instrumento segun el id_instrumento
    Public Function SelectInstrumentosMant(ByVal id_instrumento As Integer) As DataTable
        Dim sql_query As String
        Dim dtInstrumentos As New DataTable

        sql_query = " SELECT  id_tipo_instrumento, id_tipo_relacion_instrumento, nombre_instrumento, sigla, sigla_alternativa, observaciones, fecha_firma, fecha_ratificada, fecha_vigencia " +
            " FROM IC_Instrumentos " +
            " WHERE estado = 1 " +
            " AND id_instrumento = @id_instrumento "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                da = New SqlDataAdapter(command)

                da.Fill(dtInstrumentos)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTARUSUARIO = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtInstrumentos

        End Using
    End Function

    'Metodo para Insertar nuevo instrumento comercial
    Public Function InsertInstrumento(ByVal objInstrumento As CEInstrumentosMant) As Boolean
        Try
            Dim sql_query As String
            sql_query = " INSERT INTO IC_Instrumentos " +
                " ([id_instrumento] " +
                " ,[id_tipo_instrumento] " +
                " ,[id_tipo_relacion_instrumento] " +
                " ,[nombre_instrumento] " +
                " ,[sigla] " +
                " ,[sigla_alternativa] " +
                " ,[observaciones] " +
                " ,[fecha_firma] " +
                " ,[fecha_ratificada] " +
                " ,[fecha_vigencia] " +
                " ,[estado]) " +
                " VALUES " +
                " (@id_instrumento " +
                " , @id_tipo_instrumento " +
                " , @id_tipo_relacion_instrumento " +
                " , @nombre_instrumento " +
                " , @sigla " +
                " , @sigla_alternativa " +
                " , @observaciones " +
                " , @fecha_firma " +
                " , @fecha_ratificada " +
                " , @fecha_vigencia " +
                " , @estado) "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_instrumento", objInstrumento.id_instrumento)
                command.Parameters.AddWithValue("id_tipo_instrumento", objInstrumento.id_tipo_instrumento)
                command.Parameters.AddWithValue("id_tipo_relacion_instrumento", objInstrumento.id_tipo_relacion_instrumento)
                command.Parameters.AddWithValue("nombre_instrumento", objInstrumento.nombre_instrumento)
                command.Parameters.AddWithValue("sigla", objInstrumento.sigla)
                command.Parameters.AddWithValue("sigla_alternativa", objInstrumento.sigla_alternativa)
                command.Parameters.AddWithValue("observaciones", objInstrumento.observaciones)
                command.Parameters.AddWithValue("fecha_firma", objInstrumento.fecha_firma)
                command.Parameters.AddWithValue("fecha_ratificada", objInstrumento.fecha_ratificada)
                command.Parameters.AddWithValue("fecha_vigencia", objInstrumento.fecha_vigencia)
                command.Parameters.AddWithValue("estado", objInstrumento.estado)
                conexion.Open()
                command.ExecuteScalar()

                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    'Metodo para Actualizar instrumento comercial
    Public Function UpdateInstrumento(ByVal objInstrumento As CEInstrumentosMant) As Boolean
        Try
            Dim sql_query As String
            sql_query = " UPDATE IC_Instrumentos " +
                " SET  " +
                " [id_tipo_instrumento] = @id_tipo_instrumento " +
                " ,[id_tipo_relacion_instrumento] = @id_tipo_relacion_instrumento " +
                " ,[nombre_instrumento] = @nombre_instrumento " +
                " ,[sigla] = @sigla " +
                " ,[sigla_alternativa] = @sigla_alternativa " +
                " ,[observaciones] = @observaciones " +
                " ,[fecha_firma] = @fecha_firma " +
                " ,[fecha_ratificada] = @fecha_ratificada " +
                " ,[fecha_vigencia] = @fecha_vigencia " +
                " ,[estado] = @estado " +
                " WHERE [id_instrumento] = @id_instrumento "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_instrumento", objInstrumento.id_instrumento)
                command.Parameters.AddWithValue("id_tipo_instrumento", objInstrumento.id_tipo_instrumento)
                command.Parameters.AddWithValue("id_tipo_relacion_instrumento", objInstrumento.id_tipo_relacion_instrumento)
                command.Parameters.AddWithValue("nombre_instrumento", objInstrumento.nombre_instrumento)
                command.Parameters.AddWithValue("sigla", objInstrumento.sigla)
                command.Parameters.AddWithValue("sigla_alternativa", objInstrumento.sigla_alternativa)
                command.Parameters.AddWithValue("observaciones", objInstrumento.observaciones)
                command.Parameters.AddWithValue("fecha_firma", objInstrumento.fecha_firma)
                command.Parameters.AddWithValue("fecha_ratificada", objInstrumento.fecha_ratificada)
                command.Parameters.AddWithValue("fecha_vigencia", objInstrumento.fecha_vigencia)
                command.Parameters.AddWithValue("estado", objInstrumento.estado)
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

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Tipo de Instrumentos"

    'Metodo para insertar tipo de instrumento
    Public Function InsertTipoInstrumento(ByVal objTipoInstrumento As CETipoInstrumento) As Boolean
        Try
            Dim sql_query As String
            Dim objGeneral As New General
            Dim id_tipo_instrumento As Integer

            Dim nombreTabla As String = "IC_Tipo_Instrumento"
            Dim llaveTable As String = " id_tipo_instrumento "

            'Obtengo correlativo a insertar
            id_tipo_instrumento = objGeneral.ObtenerCorrelativoId(nombreTabla, llaveTable)

            sql_query = " INSERT INTO IC_Tipo_Instrumento " +
           " ([id_tipo_instrumento] " +
           " ,[descripcion] " +
           " ,[observaciones]) " +
           " VALUES " +
           " (@id_tipo_instrumento " +
           " ,@descripcion " +
           " ,@observaciones) "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_tipo_instrumento", id_tipo_instrumento)
                command.Parameters.AddWithValue("descripcion", objTipoInstrumento.descripcion)
                command.Parameters.AddWithValue("observaciones", objTipoInstrumento.observaciones)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    'Funcion para seleccionar el tipo de instrumento segun el id_tipoInstrumento
    Public Function SelectTipoInstrumentoMant(ByVal id_tipoInstrumento As Integer) As DataTable
        Dim sql_query As String
        Dim dtTipoInstrumentos As New DataTable

        sql_query = " Select descripcion ,observaciones " +
            " FROM IC_Tipo_Instrumento " +
            " WHERE id_tipo_instrumento = @id_tipoInstrumento "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_tipoInstrumento", id_tipoInstrumento)
                da = New SqlDataAdapter(command)

                da.Fill(dtTipoInstrumentos)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTARUSUARIO = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtTipoInstrumentos

        End Using
    End Function

    'Metodo para actualizar tipo de instrumento
    Public Function UpdateTipoInstrumento(ByVal objTipoInstrumento As CETipoInstrumento) As Boolean
        Try
            Dim sql_query As String
            sql_query = " UPDATE IC_Tipo_Instrumento " +
                " SET [descripcion] = @descripcion " +
                " ,[observaciones] = @observaciones " +
                " WHERE [id_tipo_instrumento] = @id_tipo_instrumento "
            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_tipo_instrumento", objTipoInstrumento.id_tipo_instrumento)
                command.Parameters.AddWithValue("descripcion", objTipoInstrumento.descripcion)
                command.Parameters.AddWithValue("observaciones", objTipoInstrumento.observaciones)
                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Tipo Relacion Instrumentos"

    'Metodo para insertar tipo relacion de instrumento
    Public Function InsertTipoRelacionInstrumento(ByVal objTipoRelacionInstrumento As CETipoRelacionInstrumento) As Boolean
        Try
            Dim sql_query As String
            Dim objGeneral As New General
            Dim id_tipo_relacion_instrumento As Integer

            Dim nombre_tabla = "IC_Tipo_Relacion_Instrumento"
            Dim llave_tabla = "id_tipo_relacion_instrumento"

            'Obtengo correlativo a insertar
            id_tipo_relacion_instrumento = objGeneral.ObtenerCorrelativoId(nombre_tabla, llave_tabla)


            sql_query = " INSERT INTO IC_Tipo_Relacion_Instrumento " +
           " ([id_tipo_relacion_instrumento] " +
           " ,[descripcion] " +
           " ,[observaciones]) " +
           " VALUES " +
           " (@id_tipo_relacion_instrumento " +
           " ,@descripcion " +
           " ,@observaciones) "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_tipo_relacion_instrumento", id_tipo_relacion_instrumento)
                command.Parameters.AddWithValue("descripcion", objTipoRelacionInstrumento.descripcion)
                command.Parameters.AddWithValue("observaciones", objTipoRelacionInstrumento.observaciones)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    'Funcion para seleccionar el tipo relacion instrumento segun el id_tipoRelacionInstrumento
    Public Function SelectTipoRelacionInstrumentoMant(ByVal id_tipoRelacionInstrumento As Integer) As DataTable
        Dim sql_query As String
        Dim dtTipoRelacionInstrumentos As New DataTable

        sql_query = " Select descripcion ,observaciones " +
            " FROM IC_Tipo_Relacion_Instrumento " +
            " WHERE id_tipo_relacion_instrumento = @id_tipoRelacionInstrumento "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_tipoRelacionInstrumento", id_tipoRelacionInstrumento)
                da = New SqlDataAdapter(command)

                da.Fill(dtTipoRelacionInstrumentos)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTARUSUARIO = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtTipoRelacionInstrumentos

        End Using

    End Function

    'Metodo para actualizar tipo relacion de instrumento
    Public Function UpdateTipoRelacionInstrumento(ByVal objTipoRelacionInstrumento As CETipoRelacionInstrumento) As Boolean
        Try
            Dim sql_query As String
            sql_query = " UPDATE IC_Tipo_Relacion_Instrumento " +
                " SET [descripcion] = @descripcion " +
                " ,[observaciones] = @observaciones " +
                " WHERE [id_tipo_relacion_instrumento] = @id_tipo_relacion_instrumento "
            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_tipo_relacion_instrumento", objTipoRelacionInstrumento.id_tipo_relacion_instrumento)
                command.Parameters.AddWithValue("descripcion", objTipoRelacionInstrumento.descripcion)
                command.Parameters.AddWithValue("observaciones", objTipoRelacionInstrumento.observaciones)
                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Tipo de Desgravacion"
    'Metodo para insertar tipo de desgravacion
    Public Function InsertTipoDesgravacion(ByVal objTipoDesgravacion As CeTipoDesgravacion) As Boolean
        Try
            Dim sql_query As String
            Dim objGeneral As New General
            Dim id_tipo_desgravacion As Integer

            Dim nombre_tabla As String = "IC_Tipo_Desgravacion"
            Dim llave_table As String = " id_tipo_desgrava "

            'Obtenco correlativo a insertar 
            id_tipo_desgravacion = objGeneral.ObtenerCorrelativoId(nombre_tabla, llave_table)

            sql_query = " INSERT INTO IC_Tipo_Desgravacion " +
           " ([id_tipo_desgrava] " +
           " ,[descripcion] " +
           " ,[observaciones]) " +
           " VALUES " +
           " (@id_tipo_desgrava " +
           " ,@descripcion " +
           " ,@observaciones) "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_tipo_desgrava", id_tipo_desgravacion)
                command.Parameters.AddWithValue("descripcion", objTipoDesgravacion.descripcion)
                command.Parameters.AddWithValue("observaciones", objTipoDesgravacion.observaciones)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    'Funcion para seleccionar el tipo de desgravacion segun el id_tipoDesgravacion
    Public Function SelectTipoDesgravacionMant(ByVal id_tipoDesgravacion As Integer) As DataTable
        Dim sql_query As String
        Dim dtTipoDesgravacion As New DataTable

        sql_query = " Select descripcion ,observaciones " +
            " FROM IC_Tipo_Desgravacion " +
            " WHERE id_tipo_desgrava = @id_tipoDesgravacion "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_tipoDesgravacion", id_tipoDesgravacion)
                da = New SqlDataAdapter(command)

                da.Fill(dtTipoDesgravacion)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR Consultar Tipo Desgravacion = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtTipoDesgravacion

        End Using
    End Function

    'Metodo para actualizar tipo de desgravacion
    Public Function UpdateTipoDesgravacion(ByVal objTipoDesgravacion As CeTipoDesgravacion) As Boolean
        Try
            Dim sql_query As String
            sql_query = " UPDATE IC_Tipo_Desgravacion " +
                " SET [descripcion] = @descripcion " +
                " ,[observaciones] = @observaciones " +
                " WHERE [id_tipo_desgrava] = @id_tipo_desgravacion "
            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_tipo_desgravacion", objTipoDesgravacion.id_tipo_desgravacion)
                command.Parameters.AddWithValue("descripcion", objTipoDesgravacion.descripcion)
                command.Parameters.AddWithValue("observaciones", objTipoDesgravacion.observaciones)
                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Categorias de Desgravacion"

    'Funcion para verificar si categoria ya esta asociada
    Public Function VerificaCategoriaAsocia(ByVal objCategoriaDesgrava As CECategoriaDesgravacion) As Boolean
        Dim estado As Boolean = False
        Dim sql_query As String

        sql_query = " select coalesce(COUNT(1),0) " +
            " from SAC_Asocia_Categoria " +
            " where id_instrumento = @id_instrumento AND " +
            " id_categoria = @id_categoria "

        Using cn = objConeccion.Conectar
            Dim command As New SqlCommand(sql_query, cn)
            command.Parameters.AddWithValue("id_instrumento", objCategoriaDesgrava.id_instrumento)
            command.Parameters.AddWithValue("id_categoria", objCategoriaDesgrava.id_categoria)
            cn.Open()
            If command.ExecuteScalar > 0 Then
                estado = True
            Else
                estado = False
            End If
        End Using

        Return estado
    End Function

    'Funcion para eliminar Categoria
    Public Function DeleteCategoria(ByVal objCategoriaDesgrava As CECategoriaDesgravacion) As Boolean
        Dim estado As Boolean = False

        Try
            Dim sql_query As String

            'Query elimina detalle
            sql_query = " DELETE " +
                " IC_Categorias_Desgravacion_Tramos " +
                " WHERE " +
                " id_instrumento = @id_instrumento And " +
                " id_categoria = @id_categoria "

            Using cn = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", objCategoriaDesgrava.id_instrumento)
                command.Parameters.AddWithValue("id_categoria", objCategoriaDesgrava.id_categoria)
                cn.Open()

                If command.ExecuteNonQuery() > 0 Then

                    'Si elimino detalles, elimina encabezado
                    sql_query = " DELETE " +
                        " IC_Categorias_Desgravacion " +
                        " WHERE " +
                        " id_instrumento = @id_instrumento AND " +
                        " id_categoria = @id_categoria "
                    Using cn2 = objConeccion.Conectar
                        Dim command2 As New SqlCommand(sql_query, cn2)
                        command2.Parameters.AddWithValue("id_instrumento", objCategoriaDesgrava.id_instrumento)
                        command2.Parameters.AddWithValue("id_categoria", objCategoriaDesgrava.id_categoria)
                        cn2.Open()

                        If command2.ExecuteNonQuery() > 0 Then
                            'Elimina encabezado
                            estado = True
                        Else
                            'Error al eliminar encabezado
                            estado = False
                        End If

                    End Using
                Else
                    'Sino devuelve error
                    estado = False
                End If

            End Using


        Catch ex As Exception
            estado = False
        Finally

        End Try

        Return estado
    End Function

    'Funcion para verificar si las categorias han sido aprobadas
    Public Function VerificaCategoriasEstado(ByVal id_instrumento As Integer) As Boolean
        Dim estado As Boolean = False

        Dim sql_query As String
        sql_query = " SELECT " +
            " COALESCE(COUNT(1), 0) as estado " +
            " FROM " +
            " SAC_Tratados_Bitacora " +
            " WHERE " +
            " id_instrumento = @id_instrumento "

        Using cn = objConeccion.Conectar
            Dim command As New SqlCommand(sql_query, cn)
            command.Parameters.AddWithValue("id_instrumento", id_instrumento)
            cn.Open()

            If command.ExecuteScalar() > 0 Then
                'Categorias aprobadas
                estado = True
            Else
                'Categorias no aprobadas
                estado = False
            End If

        End Using

        Return estado
    End Function

#Region "Funciones para aprobar categoria"

    'Funcion para consultar correlativo sac tratados bitacora
    Public Function SelectCorrelativoTratadosBitacora(ByVal id_instrumento As Integer) As Integer
        Dim sql_query As String
        Dim correlativo As Integer = 0

        sql_query = "SELECT coalesce(count(1), 0) as aprobado " +
            " FROM SAC_Tratados_Bitacora " +
            " WHERE id_instrumento = @id_instrumento AND ESTADO ='A' "
        Using cn = objConeccion.Conectar
            Dim command As New SqlCommand(sql_query, cn)
            command.Parameters.AddWithValue("id_instrumento", id_instrumento)
            cn.Open()
            correlativo = command.ExecuteScalar() + 1

        End Using
        Return correlativo
    End Function

    'Funcion para obtener cantidad de categorias aprobar por tratado
    Public Function CantidadCategoriasInstrumento(ByVal id_instrumento As Integer) As Integer
        Dim cantidad As Integer = 0
        Dim sql_query As String

        sql_query = " SELECT " +
            " COUNT(cd.id_categoria) AS cantidad_Categorias " +
            " FROM " +
            " IC_Instrumentos II " +
            " INNER Join " +
            " (SELECT " +
            " icd.id_instrumento, icd.id_categoria " +
            " FROM " +
            " IC_Categorias_Desgravacion ICD, " +
            " IC_Categorias_Desgravacion_Tramos ICDT " +
            " WHERE " +
            " ICD.id_categoria = icdt.id_categoria And " +
            " ICD.id_instrumento = ICDT.id_instrumento " +
            " GROUP by icd.id_instrumento, ICD.id_categoria) as CD ON " +
            " II.id_instrumento = CD.id_instrumento " +
            " WHERE " +
            " II.id_instrumento = @id_instrumento "

        Using cn = objConeccion.Conectar
            Dim command As New SqlCommand(sql_query, cn)
            command.Parameters.AddWithValue("id_instrumento", id_instrumento)
            cn.Open()

            cantidad = command.ExecuteScalar()
        End Using

        Return cantidad
    End Function


#End Region

    'Funcion para Aprobar Categoria
    Public Function ApruebaCategoria(ByVal id_instrumento As Integer) As Boolean
        Dim estado As Boolean = False
        Try
            Dim sql_query As String
            Dim dt_Aprobado As New DataTable

            'Verifica que las Categorias no esten ya Aprobadas
            sql_query = " SELECT count(1) as aprobado " +
                " FROM SAC_Tratados_Bitacora " +
                " WHERE id_instrumento = @id_instrumento AND ESTADO ='A' "
            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                da = New SqlDataAdapter(command)
                da.Fill(dt_Aprobado)
            End Using

            'Si no esta aprobado
            If (dt_Aprobado.Rows(0)("aprobado") = 0) Then
                Dim dt_CategoriaActiva As New DataTable

                sql_query = " SELECT COALESCE ((SELECT COUNT(1)  " +
                    " FROM IC_Categorias_Desgravacion_Tramos " +
                    " WHERE id_instrumento = @id_instrumento AND " +
                    " activo = 'N' " +
                    " GROUP BY id_instrumento), 0) Categoria_Activa "

                Using cn = objConeccion.Conectar
                    Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                    command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                    da = New SqlDataAdapter(command)
                    da.Fill(dt_CategoriaActiva)

                    If (dt_CategoriaActiva.Rows(0)("Categoria_Activa") >= 1) Then
                        'No se puede aprobar ya que existen categorias sin activar
                        estado = False
                    Else
                        Dim dt_Categorias As New DataTable
                        Dim contador As Integer = 0
                        Dim cantidad_cortes As Integer = 0
                        Dim codigo_categoria As String
                        Dim id_tramo As Integer = 0
                        Dim id_categoria As Integer = 0

                        'Si todas las categorias estan activadas
                        sql_query = " select icd.codigo_categoria, icd.id_categoria, " +
                            " icd.cantidad_tramos, icdt.id_tramo, icdt.cantidad_cortes " +
                            " from IC_Categorias_Desgravacion_Tramos icdt " +
                            " inner Join " +
                            " IC_Categorias_Desgravacion icd on " +
                            " icd.id_categoria = icdt.id_categoria And " +
                            " icd.id_instrumento = icdt.id_instrumento " +
                            " where icdt.id_instrumento = @id_instrumento " +
                            " ORDER BY icd.codigo_categoria "

                        Using cn2 = objConeccion.Conectar
                            Dim command2 As SqlCommand = New SqlCommand(sql_query, cn2)
                            command2.Parameters.AddWithValue("id_instrumento", id_instrumento)
                            da = New SqlDataAdapter(command2)
                            da.Fill(dt_Categorias)

                            '@CodAnteCatego'
                            codigo_categoria = dt_Categorias.Rows(0)("codigo_categoria").ToString

                            '@Cuenta
                            contador = 0

                            For Each Row As DataRow In dt_Categorias.Rows
                                cantidad_cortes = Convert.ToInt32(Row("cantidad_cortes").ToString)
                                id_categoria = Convert.ToInt32(Row("id_categoria").ToString)
                                id_tramo = Convert.ToInt32(Row("id_tramo").ToString)

                                If (Row("codigo_categoria").ToString = codigo_categoria) Then

                                    sql_query = "UPDATE" +
                                        " IC_Categorias_Desgravacion_Tramos " +
                                        " SET " +
                                        " cortes_ejecutados = @cuenta " +
                                        " WHERE " +
                                        " id_instrumento = @id_instrumento AND " +
                                        " id_categoria = @id_categoria "

                                    contador = contador + cantidad_cortes

                                    Using cn3 = objConeccion.Conectar
                                        Dim command3 As SqlCommand = New SqlCommand(sql_query, cn3)
                                        command3.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                        command3.Parameters.AddWithValue("cuenta", contador)
                                        command3.Parameters.AddWithValue("id_categoria", id_categoria)

                                        cn3.Open()
                                        command3.ExecuteScalar()
                                    End Using

                                    'contador = contador + cantidad_cortes
                                Else
                                    codigo_categoria = Row("codigo_categoria").ToString
                                    contador = 0

                                    sql_query = "UPDATE" +
                                        " IC_Categorias_Desgravacion_Tramos " +
                                        " SET " +
                                        " cortes_ejecutados = @cuenta " +
                                        " WHERE " +
                                        " id_instrumento = @id_instrumento AND " +
                                        " id_categoria = @id_categoria "

                                    contador = contador + cantidad_cortes

                                    Using cn3 = objConeccion.Conectar
                                        Dim command3 As SqlCommand = New SqlCommand(sql_query, cn3)
                                        command3.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                        command3.Parameters.AddWithValue("cuenta", contador)
                                        command3.Parameters.AddWithValue("id_categoria", id_categoria)

                                        cn3.Open()
                                        command3.ExecuteScalar()
                                    End Using

                                    'contador = contador + cantidad_cortes
                                End If
                            Next



                            'Consultar la ultima version
                            Dim id_version As Integer = 0
                            Dim cantidad_categoria As Integer = 0

                            id_version = SelectCorrelativoTratadosBitacora(id_instrumento)
                            cantidad_categoria = CantidadCategoriasInstrumento(id_instrumento)


                            'Insertar en la bitacora tratado el primer registro
                            sql_query = "INSERT INTO SAC_Tratados_Bitacora" +
                                " ([id_version] " +
                                " ,[id_corte_version] " +
                                " ,[id_instrumento] " +
                                " ,[cantidad_categoria] " +
                                " ,[fecha_generada] " +
                                " ,[estado]) " +
                                " VALUES " +
                                " (@id_version " +
                                " ,0 " +
                                " ,@id_instrumento " +
                                " ,@cantidad_categoria " +
                                " ,SYSDATETIME()" +
                                " ,'A')"

                            Using cn3 = objConeccion.Conectar
                                Dim command3 As SqlCommand = New SqlCommand(sql_query, cn3)
                                command3.Parameters.AddWithValue("id_version", id_version)
                                command3.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                command3.Parameters.AddWithValue("cantidad_categoria", cantidad_categoria)

                                cn3.Open()
                                command3.ExecuteScalar()
                            End Using

                        End Using

                        estado = True
                    End If
                End Using
            Else
                'Si ya esta aprobado
                estado = False
            End If

        Catch ex As Exception
            estado = False

        Finally

        End Try
        Return estado
    End Function

    'Funcion para obtener el nombre del instrumento y cantidad de categorias
    Public Function SelectInstrumentoCategoria(ByVal id_instrumento As Integer) As DataTable
        Dim sql_query As String
        Dim dtInstrumentoCategoria As New DataTable

        sql_query = " SELECT " +
            " II.nombre_instrumento, ii.sigla, COUNT(cd.id_categoria) AS cantidad_Categorias " +
            " FROM " +
            " IC_Instrumentos II " +
            " INNER Join " +
            " (SELECT " +
            " icd.id_instrumento, icd.id_categoria " +
            " FROM " +
            " IC_Categorias_Desgravacion ICD," +
            " IC_Categorias_Desgravacion_Tramos ICDT " +
            " WHERE " +
            " ICD.id_categoria = icdt.id_categoria And " +
            " ICD.id_instrumento = ICDT.id_instrumento " +
            " GROUP by icd.id_instrumento, ICD.id_categoria) as CD ON " +
            " II.id_instrumento = CD.id_instrumento " +
            " WHERE " +
            " II.id_instrumento =  @id_instrumento " +
            " GROUP by ii.nombre_instrumento, II.sigla "

        Using cn = objConeccion.Conectar
            Try
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                da = New SqlDataAdapter(command)

                da.Fill(dtInstrumentoCategoria)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR CATEGORIA = " + ex.Message.ToString)

            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try
            Return dtInstrumentoCategoria
        End Using
    End Function

    'Funcion para actualizar categorias
    Public Function UpdateCategoriaDesgrava(ByVal objCategoriaDesgrava As CECategoriaDesgravacion) As Boolean
        Try
            Dim sql_query As String
            sql_query = " UPDATE " +
                " IC_Categorias_Desgravacion " +
                " SET " +
                " [id_tipo_desgrava] = @id_tipo_desgrava " +
                " ,[codigo_categoria] = @codigo_categoria " +
                " ,[cantidad_tramos] = @cantidad_tramos " +
                " ,[observaciones] = @observaciones " +
                " WHERE id_categoria = @id_categoria " +
                " AND id_instrumento = @id_instrumento "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_categoria", objCategoriaDesgrava.id_categoria)
                command.Parameters.AddWithValue("id_instrumento", objCategoriaDesgrava.id_instrumento)
                command.Parameters.AddWithValue("id_tipo_desgrava", objCategoriaDesgrava.id_tipo_desgravacion)
                command.Parameters.AddWithValue("codigo_categoria", objCategoriaDesgrava.codigo_categoria)
                command.Parameters.AddWithValue("cantidad_tramos", objCategoriaDesgrava.cantidad_tramos)
                command.Parameters.AddWithValue("observaciones", objCategoriaDesgrava.observaciones)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    'Funcion para seleccionar el tipo de categoria segun el id_categoria
    Public Function SelectCategoriaDesgravaMant(ByVal id_categoria As Integer, ByVal id_instrumento As Integer) As DataTable
        Dim sql_query As String
        Dim dtTipoRelacionInstrumentos As New DataTable

        sql_query = " SELECT " +
            " [id_tipo_desgrava] " +
            " ,[codigo_categoria] " +
            " ,[cantidad_tramos] " +
            " ,[observaciones] " +
            " FROM IC_Categorias_Desgravacion " +
            " WHERE id_categoria = @id_categoria " +
            " And id_instrumento = @id_instrumento "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_categoria", id_categoria)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                da = New SqlDataAdapter(command)

                da.Fill(dtTipoRelacionInstrumentos)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR CATEGORIA = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtTipoRelacionInstrumentos

        End Using
    End Function

    'Funcion para insertar categorias y tramos
    Public Function InsertCategoriaDesgrava(ByVal objCECategorias As CECategoriaDesgravacion) As Boolean
        Try
            Dim sql_query As String
            Dim id_categoria As Integer

            Dim objGeneral As New General
            Dim nombre_tabla As String
            Dim llave_tabla As String
            Dim llave_filtro As String

            nombre_tabla = "IC_Categorias_Desgravacion"
            llave_tabla = "id_categoria"
            llave_filtro = "id_instrumento"

            'Obtengo correlativo a insertar
            id_categoria = objGeneral.ObtenerCorrelativoId(nombre_tabla, llave_tabla, False, llave_filtro, objCECategorias.id_instrumento)

            'Insertar Encabezado de categorias

            sql_query = " INSERT INTO " +
                " IC_Categorias_Desgravacion " +
                " ([id_categoria] " +
                " ,[id_instrumento] " +
                " ,[id_tipo_desgrava] " +
                " ,[codigo_categoria] " +
                " ,[cantidad_tramos] " +
                " ,[observaciones]) " +
                " VALUES " +
                " (@id_categoria " +
                " ,@id_instrumento " +
                " ,@id_tipo_desgrava " +
                " ,@codigo_categoria " +
                " ,@cantidad_tramos " +
                " ,@observaciones) "


            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_categoria", id_categoria)
                command.Parameters.AddWithValue("id_instrumento", objCECategorias.id_instrumento)
                command.Parameters.AddWithValue("id_tipo_desgrava", objCECategorias.id_tipo_desgravacion)
                command.Parameters.AddWithValue("codigo_categoria", objCECategorias.codigo_categoria)
                command.Parameters.AddWithValue("cantidad_tramos", objCECategorias.cantidad_tramos)
                command.Parameters.AddWithValue("observaciones", objCECategorias.observaciones)

                conexion.Open()
                command.ExecuteScalar()

                'Llenar los detalles de los tramos 
                For i As Integer = 1 To objCECategorias.cantidad_tramos
                    sql_query = " INSERT INTO IC_Categorias_Desgravacion_Tramos " +
                        " ([id_tramo] " +
                        " ,[id_categoria] " +
                        " ,[id_instrumento] " +
                        " ,[activo]) " +
                        " VALUES " +
                        " (@id_tramo " +
                        " ,@id_categoria " +
                        " ,@id_instrumento " +
                        " ,@activo) "

                    command = New SqlCommand(sql_query, conexion)
                    command.Parameters.AddWithValue("id_tramo", i)
                    command.Parameters.AddWithValue("id_categoria", id_categoria)
                    command.Parameters.AddWithValue("id_instrumento", objCECategorias.id_instrumento)
                    command.Parameters.AddWithValue("activo", "N")

                    command.ExecuteScalar()

                Next

                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    'Funcion para selectionar los datos del tipo de desgravacion
    Public Function SelectTipoDesgravacion() As DataSet
        Try
            Dim sql_string As String

            sql_string = " Select id_tipo_desgrava" +
                " ,[descripcion] " +
                " ,[observaciones] " +
                " FROM IC_Tipo_Desgravacion "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "TimpoIns")
            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectTipoDesgravacion = " + ex.Message.ToString)
        Finally

        End Try

        Return ds

    End Function

    'Funcion para seleccionar categorias segun el id_instrumento para llenar gvCategorias
    Public Function SelectCategoriasDesgrava(ByVal id_instrumento As Integer) As DataTable
        Dim sql_query As String
        Dim dtCategoriaDesgrava As New DataTable

        sql_query = " SELECT " +
            " CATEGO.id_categoria, I.sigla, " +
            " CATEGO.codigo_categoria AS categoria, " +
            " CATEGO.descripcion AS Tipo_Desgravacion, " +
            " CATEGO.activo, " +
            " CATEGO.cantidad_tramos, " +
            " CATEGO.CANTIDAD_CORTES " +
            " FROM " +
            " IC_Instrumentos I " +
            " INNER JOIN " +
            " (SELECT " +
            " CD.id_instrumento, CD.id_categoria, " +
            " CD.codigo_categoria, TD.descripcion, " +
            " CD.cantidad_tramos, CDT.activo, " +
            " SUM(CDT.cantidad_cortes) AS CANTIDAD_CORTES " +
            " FROM " +
            " IC_Categorias_Desgravacion CD, " +
            " IC_Categorias_Desgravacion_Tramos CDT, " +
            " IC_Tipo_Desgravacion TD " +
            " WHERE " +
            " CD.id_categoria = CDT.id_categoria And " +
            " CD.id_instrumento = CDT.id_instrumento And " +
            " CD.id_tipo_desgrava = TD.id_tipo_desgrava " +
            " GROUP BY " +
            " CD.id_instrumento, CD.id_categoria, " +
            " CD.codigo_categoria, TD.descripcion, " +
            " CD.cantidad_tramos, CDT.activo) CATEGO ON " +
            " I.id_instrumento = CATEGO.id_instrumento " +
            " WHERE " +
            " I.id_instrumento = @id_instrumento "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                da = New SqlDataAdapter(command)

                da.Fill(dtCategoriaDesgrava)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR Categorias Desgravacion = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtCategoriaDesgrava

        End Using
    End Function

#End Region

#Region "Funciones y procedimentos para el Mantenimiento de Tramos de Desgravacion"
    'Funcion para actualizar tramos
    Public Function UpdateTramoCategoriaMant(ByVal ObjCETramo As CECorteDesgravacion) As Boolean
        Try
            Dim sql_query As String
            sql_query = "UPDATE IC_Categorias_Desgravacion_Tramos " +
                " SET " +
                " [id_tipo_periodo] = @id_tipo_periodo " +
                " ,[cantidad_cortes] = @cantidad_cortes " +
                " ,[desgrava_tramo_anterior] = @desgrava_tramo_anterior " +
                " ,[desgrava_tramo_final] = @desgrava_tramo_final " +
                " ,[factor_desgrava] = @factor_desgrava " +
                " ,[activo] = 'S' " +
                " WHERE " +
                " id_tramo = @id_tramo AND " +
                " id_instrumento = @id_instrumento AND " +
                " id_categoria = @id_categoria "


            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_categoria", ObjCETramo.id_categoria)
                command.Parameters.AddWithValue("id_instrumento", ObjCETramo.id_instrumento)
                command.Parameters.AddWithValue("id_tramo", ObjCETramo.id_tramos)
                command.Parameters.AddWithValue("id_tipo_periodo", ObjCETramo.id_tipo_periodo)
                command.Parameters.AddWithValue("cantidad_cortes", ObjCETramo.cantidad_cortes)
                command.Parameters.AddWithValue("desgrava_tramo_anterior", ObjCETramo.porcen_periodo_anterior)
                command.Parameters.AddWithValue("desgrava_tramo_final", ObjCETramo.porcen_periodo_final)
                command.Parameters.AddWithValue("factor_desgrava", ObjCETramo.factor_desgrava)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    'Funcion para seleccionar el tramo segun el id_instrumento, id_catetoria y id_tramo
    Public Function SelectTramoCategoriaMant(ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal id_tramo As Integer) As DataTable
        Dim sql_query As String
        Dim dtCategoriaDesgrava As New DataTable

        sql_query = " SELECT " +
            " II.nombre_instrumento, ICD.codigo_categoria, " +
            " ICDT.id_tramo, ITD.id_tipo_desgrava, " +
            " ICDT.id_tipo_periodo , ICDT.factor_desgrava, " +
            " ICDT.cantidad_cortes, ICDT.desgrava_tramo_anterior, " +
            " ICDT.desgrava_tramo_final " +
            " FROM " +
            " IC_Categorias_Desgravacion_Tramos ICDT" +
            " inner Join" +
            " IC_Instrumentos II" +
            " on" +
            " II.id_instrumento = ICDT.id_instrumento" +
            " inner Join" +
            " IC_Categorias_Desgravacion ICD" +
            " on" +
            " ICD.id_categoria = ICDT.id_categoria And" +
            " ICD.id_instrumento = II.id_instrumento" +
            " inner Join" +
            " IC_Tipo_Desgravacion ITD" +
            " on " +
            " ITD.id_tipo_desgrava = ICD.id_tipo_desgrava" +
            " Left Join" +
            " IC_Tipo_Periodo_Corte ITPC" +
            " on" +
            " ITPC.id_tipo_periodo = icdt.id_tipo_periodo " +
            " WHERE " +
            " ICDT.id_categoria = @id_categoria And " +
            " ICDT.id_instrumento = @id_instrumento AND " +
            " ICDT.id_tramo = @id_tramo "



        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                command.Parameters.AddWithValue("id_categoria", id_categoria)
                command.Parameters.AddWithValue("id_tramo", id_tramo)
                da = New SqlDataAdapter(command)

                da.Fill(dtCategoriaDesgrava)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR TRAMO = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtCategoriaDesgrava

        End Using
    End Function

    'Funcion para Seleccionar tramos segun el id_instrumento y id_categoria para llenar gvTramo
    Public Function SelectTramoCategoria(ByVal id_instrumento As Integer, ByVal id_categoria As Integer) As DataTable
        Dim sql_query As String
        Dim dtCategoriaDesgrava As New DataTable

        sql_query = " SELECT " +
            " II.sigla, ICD.codigo_categoria, " +
            " ICDT.id_tramo, ITD.descripcion, " +
            " ITPC.descripcion as periodo_corte, ICDT.factor_desgrava, " +
            " icdt.activo, ICDT.cantidad_cortes " +
            " FROM " +
            " IC_Categorias_Desgravacion_Tramos ICDT " +
            " inner Join " +
            " IC_Instrumentos II " +
            " on " +
            " ICDT.id_instrumento = II.id_instrumento " +
            " inner Join " +
            " IC_Categorias_Desgravacion ICD " +
            " on " +
            " ICD.id_categoria = ICDT.id_categoria And " +
            " ICD.id_instrumento = ICDT.id_instrumento" +
            " inner Join " +
            " IC_Tipo_Desgravacion ITD " +
            " on " +
            " ITD.id_tipo_desgrava = ICD.id_tipo_desgrava " +
            " Left Join " +
            " IC_Tipo_Periodo_Corte ITPC" +
            " on " +
            " ITPC.id_tipo_periodo = ICDT.id_tipo_periodo" +
            " WHERE " +
            " ICDT.id_categoria = @id_categoria And " +
            " ICDT.id_instrumento = @id_instrumento "


        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                command.Parameters.AddWithValue("id_categoria", id_categoria)
                da = New SqlDataAdapter(command)

                da.Fill(dtCategoriaDesgrava)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR TRAMOS = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtCategoriaDesgrava

        End Using
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Tipo de Periodo de corte"
    'Funcion para seleccionar listado del combo tipo de periodo
    Public Function SelectTipoPeriodo() As DataSet
        Try
            Dim sql_string As String

            sql_string = " SELECT id_tipo_periodo, descripcion, observaciones " +
                " from IC_Tipo_Periodo_Corte "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "TimpoIns")
            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectTipoPeriodo = " + ex.Message.ToString)
        Finally

        End Try

        Return ds

    End Function

    'Metodo para insertar tipo de periodo
    Public Function InsertTipoPeriodo(ByVal objTipoPeriodo As CETipoPeriodo) As Boolean
        Try
            Dim sql_query As String
            Dim objGeneral As New General
            Dim id_tipo_periodo As Integer

            Dim nombreTabla As String = "IC_Tipo_Periodo_Corte"
            Dim llaveTable As String = " id_tipo_periodo "

            'Obtengo correlativo a insertar
            id_tipo_periodo = objGeneral.ObtenerCorrelativoId(nombreTabla, llaveTable)

            sql_query = " INSERT INTO IC_Tipo_Periodo_Corte " +
           " ([id_tipo_periodo] " +
           " ,[descripcion] " +
           " ,[observaciones]) " +
           " VALUES " +
           " (@id_tipo_periodo " +
           " ,@descripcion " +
           " ,@observaciones) "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_tipo_periodo", id_tipo_periodo)
                command.Parameters.AddWithValue("descripcion", objTipoPeriodo.descripcion)
                command.Parameters.AddWithValue("observaciones", objTipoPeriodo.observaciones)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    'Funcion para seleccionar el tipo de periodo segun el id_tipoPeriodo
    Public Function SelectTipoPeriodoMant(ByVal id_tipoPeriodo As Integer) As DataTable
        Dim sql_query As String
        Dim dtTipoPeriodo As New DataTable

        sql_query = " Select descripcion ,observaciones " +
            " FROM IC_Tipo_Periodo_Corte " +
            " WHERE id_tipo_periodo = @id_tipoPeriodo "

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_tipoPeriodo", id_tipoPeriodo)
                da = New SqlDataAdapter(command)

                da.Fill(dtTipoPeriodo)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR TIPO PERIODO = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtTipoPeriodo

        End Using
    End Function

    'Metodo para actualizar tipo de periodo
    Public Function UpdateTipoPeriodo(ByVal objTipoPeriodo As CETipoPeriodo) As Boolean
        Try
            Dim sql_query As String
            sql_query = " UPDATE IC_Tipo_Periodo_Corte " +
                " SET [descripcion] = @descripcion " +
                " ,[observaciones] = @observaciones " +
                " WHERE [id_tipo_periodo] = @id_tipo_periodo "
            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_tipo_periodo", objTipoPeriodo.id_tipo_periodo)
                command.Parameters.AddWithValue("descripcion", objTipoPeriodo.descripcion)
                command.Parameters.AddWithValue("observaciones", objTipoPeriodo.observaciones)
                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Asignacion Categorias"

#Region "Funciones para incisos-categoria"

    'Funcion que valida si el inciso a insertar ya existe
    Public Function ValidaInciso(ByVal id_instrumento As Integer, ByVal codigo_inciso As String, Optional inciso_presicion As String = Nothing) As Boolean
        Dim estado As Boolean = False
        Try
            Dim sql_query As String

            If inciso_presicion Is Nothing Then
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

                        sql_query = " SELECT coalesce(count(codigo_inciso),0) " +
                           " FROM " +
                           " [SGSACDB].[dbo].[SAC_Asocia_Categoria] " +
                           " WHERE " +
                           " id_instrumento = @id_instrumento AND " +
                           " id_version = @id_version AND " +
                           " anio_version = @anio_version AND " +
                           " codigo_inciso = @codigo_inciso AND " +
                           " inciso_presicion IS NULL "

                        Using cn = objConeccion.Conectar
                            Dim command1 As SqlCommand = New SqlCommand(sql_query, cn)
                            command1.Parameters.AddWithValue("id_instrumento", id_instrumento)
                            command1.Parameters.AddWithValue("id_version", id_version)
                            command1.Parameters.AddWithValue("anio_version", anio_version)
                            command1.Parameters.AddWithValue("codigo_inciso", codigo_inciso)

                            cn.Open()

                            If command1.ExecuteScalar() = 1 Then
                                estado = True
                            Else
                                estado = False
                            End If

                        End Using
                    Else
                        estado = False
                    End If

                End Using
            Else
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

                        sql_query = " SELECT coalesce(count(codigo_inciso),0) " +
                           " FROM " +
                           " [SGSACDB].[dbo].[SAC_Asocia_Categoria] " +
                           " WHERE " +
                           " id_instrumento = @id_instrumento AND " +
                           " id_version = @id_version AND " +
                           " anio_version = @anio_version AND " +
                           " codigo_inciso = @codigo_inciso AND " +
                           " inciso_presicion = @inciso_presicion "

                        Using cn = objConeccion.Conectar
                            Dim command1 As SqlCommand = New SqlCommand(sql_query, cn)
                            command1.Parameters.AddWithValue("id_instrumento", id_instrumento)
                            command1.Parameters.AddWithValue("id_version", id_version)
                            command1.Parameters.AddWithValue("anio_version", anio_version)
                            command1.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                            command1.Parameters.AddWithValue("inciso_presicion", inciso_presicion)

                            cn.Open()

                            If command1.ExecuteScalar() = 1 Then
                                estado = True
                            Else
                                estado = False
                            End If

                        End Using

                    Else
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

    'Funcion que inserta correlacion inciso-categoria nuevo
    Public Function InsertInciso(ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal codigo_inciso As String, Optional codigo_precision As String = Nothing, Optional texto_precision As String = Nothing) As Boolean
        Dim estado As Boolean = False
        Try
            Dim sql_query As String

            If codigo_precision = Nothing Then
                'Si codigo precision esta vacio
                sql_query = " INSERT INTO " +
                    " SAC_Asocia_Categoria " +
                    " ([id_instrumento] " +
                    " ,[id_categoria] " +
                    " ,[id_version] " +
                    " ,[anio_version] " +
                    " ,[codigo_inciso] " +
                    " ,[estado]) " +
                    " (SELECT " +
                    " CD.id_instrumento, CD.id_categoria, " +
                    " VB.id_version, VB.anio_version,  " +
                    " @codigo_inciso , 'A' " +
                    " From " +
                    " SAC_Versiones_Bitacora VB, " +
                    " IC_Categorias_Desgravacion CD " +
                    " WHERE " +
                    " CD.id_categoria = @id_categoria AND " +
                    " CD.id_instrumento = @id_instrumento AND " +
                    " VB.estado = 'A') "

                Using conexion = objConeccion.Conectar
                    Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                    command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                    command.Parameters.AddWithValue("id_categoria", id_categoria)
                    command.Parameters.AddWithValue("codigo_inciso", codigo_inciso)

                    conexion.Open()
                    command.ExecuteScalar()
                    estado = True
                End Using
            Else
                'Si codigo precision no esta vacio
                sql_query = "  INSERT INTO " +
                    " SAC_Asocia_Categoria " +
                    " ([id_instrumento] " +
                    " ,[id_categoria] " +
                    " ,[id_version] " +
                    " ,[anio_version] " +
                    " ,[codigo_inciso] " +
                    " ,[inciso_presicion] " +
                    " ,[texto_precision] " +
                    " ,[estado]) " +
                    " (SELECT " +
                    " CD.id_instrumento, CD.id_categoria, " +
                    " VB.id_version, VB.anio_version,  " +
                    " @codigo_inciso , @codigo_precision, @texto_precision, 'A' " +
                    " From " +
                    " SAC_Versiones_Bitacora VB, " +
                    " IC_Categorias_Desgravacion CD " +
                    " WHERE " +
                    " CD.id_categoria = @id_categoria AND " +
                    " CD.id_instrumento = @id_instrumento AND " +
                    " VB.estado = 'A') "

                Using conexion = objConeccion.Conectar
                    Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                    command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                    command.Parameters.AddWithValue("id_categoria", id_categoria)
                    command.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                    command.Parameters.AddWithValue("codigo_precision", codigo_precision)
                    command.Parameters.AddWithValue("texto_precision", texto_precision)

                    conexion.Open()
                    command.ExecuteScalar()
                    estado = True
                End Using
            End If



        Catch ex As Exception
            estado = False
        Finally

        End Try


        Return estado
    End Function

    ''' <sumary>
    ''' Funcion que actualiza categoria a inciso
    ''' </sumary>
    Public Function UpdateInciso(ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal codigo_inciso As String, Optional codigo_precision As String = Nothing, Optional texto_precision As String = Nothing) As Boolean
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

                    If codigo_precision = Nothing Then
                        'Si esta vacio codigo precision
                        sql_query = " UPDATE " +
                        " SAC_Asocia_Categoria " +
                        " SET " +
                        " id_categoria = @id_categoria " +
                        " WHERE " +
                        " id_instrumento = @id_instrumento AND " +
                        " id_version = @id_version AND " +
                        " anio_version = @anio_version AND " +
                        " codigo_inciso = @codigo_inciso AND " +
                        " inciso_presicion IS NULL "

                        Using cn = objConeccion.Conectar
                            Dim command2 As SqlCommand = New SqlCommand(sql_query, cn)
                            command2.Parameters.AddWithValue("id_categoria", id_categoria)
                            command2.Parameters.AddWithValue("id_instrumento", id_instrumento)
                            command2.Parameters.AddWithValue("id_version", id_version)
                            command2.Parameters.AddWithValue("anio_version", anio_version)
                            command2.Parameters.AddWithValue("codigo_inciso", codigo_inciso)

                            cn.Open()
                            command2.ExecuteScalar()
                            estado = True
                        End Using
                    Else
                        If Not texto_precision Is Nothing Then
                            'si no esta vacio codigo precision
                            sql_query = " UPDATE " +
                            " SAC_Asocia_Categoria " +
                            " SET " +
                            " id_categoria = @id_categoria, " +
                            " texto_precision = @texto_precision " +
                            " WHERE " +
                            " id_instrumento = @id_instrumento AND " +
                            " id_version = @id_version AND " +
                            " anio_version = @anio_version AND " +
                            " inciso_presicion = @codigo_precision "

                            Using cn = objConeccion.Conectar
                                Dim command2 As SqlCommand = New SqlCommand(sql_query, cn)
                                command2.Parameters.AddWithValue("id_categoria", id_categoria)
                                command2.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                command2.Parameters.AddWithValue("id_version", id_version)
                                command2.Parameters.AddWithValue("anio_version", anio_version)
                                'command2.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                                command2.Parameters.AddWithValue("codigo_precision", codigo_precision)
                                command2.Parameters.AddWithValue("texto_precision", texto_precision)

                                cn.Open()
                                command2.ExecuteScalar()
                                estado = True
                            End Using
                        Else
                            'si no esta vacio codigo precision
                            sql_query = " UPDATE " +
                            " SAC_Asocia_Categoria " +
                            " SET " +
                            " id_categoria = @id_categoria " +
                            " WHERE " +
                            " id_instrumento = @id_instrumento AND " +
                            " id_version = @id_version AND " +
                            " anio_version = @anio_version AND " +
                            " inciso_presicion = @codigo_precision "

                            Using cn = objConeccion.Conectar
                                Dim command2 As SqlCommand = New SqlCommand(sql_query, cn)
                                command2.Parameters.AddWithValue("id_categoria", id_categoria)
                                command2.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                command2.Parameters.AddWithValue("id_version", id_version)
                                command2.Parameters.AddWithValue("anio_version", anio_version)
                                'command2.Parameters.AddWithValue("codigo_inciso", codigo_inciso)
                                command2.Parameters.AddWithValue("codigo_precision", codigo_precision)
                                'command2.Parameters.AddWithValue("texto_precision", texto_precision)

                                cn.Open()
                                command2.ExecuteScalar()
                                estado = True
                            End Using
                        End If


                    End If


                Else
                    estado = False
                End If
            End Using



        Catch ex As Exception

        Finally

        End Try
        Return estado
    End Function

#End Region

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
                        If ValidaInciso(id_instrumento, codigo_inciso) Then
                            'Si existe inciso, actuliza categoria para inciso
                            estado = UpdateInciso(id_instrumento, id_categoria, codigo_inciso)
                        Else
                            'Si no existe inciso, inserta
                            estado = InsertInciso(id_instrumento, id_categoria, codigo_inciso)
                        End If

                    Else
                        Dim logitud As Integer = row("inciso_presicion").ToString.Length

                        If (logitud = 0) Then

                            'Se llama a la funcion valida inciso para verificar si inciso ya existe con otra categoria
                            If ValidaInciso(id_instrumento, codigo_inciso) Then
                                'Si existe inciso, actuliza categoria para inciso
                                estado = UpdateInciso(id_instrumento, id_categoria, codigo_inciso)
                            Else
                                'Si no existe inciso, inserta
                                estado = InsertInciso(id_instrumento, id_categoria, codigo_inciso)
                            End If
                        Else

                            Dim inciso_presicion As String = row("inciso_presicion")

                            'Se llama a la funcion valida inciso para verificar si inciso_presicion ya existe con otra categoria
                            If ValidaInciso(id_instrumento, codigo_inciso, inciso_presicion) Then
                                'Si existe inciso, actuliza categoria para inciso
                                estado = UpdateInciso(id_instrumento, id_categoria, codigo_inciso, inciso_presicion)
                            Else
                                'Si no existe inciso, inserta
                                estado = InsertInciso(id_instrumento, id_categoria, codigo_inciso, inciso_presicion)
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

                da = New SqlDataAdapter(command)
                da.Fill(ds)

            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectDatosAsignaCategoriaMant = " + ex.Message.ToString)
        Finally

        End Try

        Return ds
    End Function
#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Paises Instrumentos"

    'función para actualizar países instrumento
    Public Function ActualizarInstrumentoPais(ByVal obj As CeInstrumentoPais, ByVal objMant As CeInstrumentoPaisMant) As Boolean
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("UPDATE [SGSACDB].[dbo].[IC_Instrumento_Paises]")
            sql_string.AppendLine("   SET [ID_PAIS] = " & obj.idPais)
            sql_string.AppendLine("      ,[ID_TIPO_SOCIO] = " & obj.idTipoSocio)
            sql_string.AppendLine("      ,[CODIGO_BLOQUE_PAIS] = " & obj.idBloquePais)
            sql_string.AppendLine("      ,[OBSERVACIONES] = '" & obj.Observaciones & "'")
            sql_string.AppendLine("      ,[FECHA_FIRMA] = '" & obj.FechaFirma & "'")
            sql_string.AppendLine("      ,[FECHA_RATIFICACION] = '" & obj.FechaRatificacion & "'")
            sql_string.AppendLine("      ,[FECHA_VIGENCIA] = '" & obj.FechaVigencia & "'")
            sql_string.AppendLine(" WHERE [ID_INSTRUMENTO] = " & objMant.idInstrumento)
            sql_string.AppendLine("	AND ID_PAIS = " & objMant.idPais)
            sql_string.AppendLine("	AND CODIGO_BLOQUE_PAIS = " & objMant.idBloquePais)
            sql_string.AppendLine("	AND ID_TIPO_SOCIO = " & objMant.idTipoSocio)
            sql_string.AppendLine("")

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, conexion)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception

            Return False

        Finally
        End Try
    End Function

    'función para guardar los países
    Public Function GuardarInstrumentoPais(ByVal obj As CeInstrumentoPais) As Boolean
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("INSERT INTO [SGSACDB].[dbo].[IC_Instrumento_Paises]")
            sql_string.AppendLine("           (")
            sql_string.AppendLine("           [ID_INSTRUMENTO]")
            sql_string.AppendLine("           ,[ID_PAIS]")
            sql_string.AppendLine("           ,[ID_TIPO_SOCIO]")
            sql_string.AppendLine("           ,[CODIGO_BLOQUE_PAIS]")
            sql_string.AppendLine("           ,[OBSERVACIONES]")
            sql_string.AppendLine("           ,[FECHA_FIRMA]")
            sql_string.AppendLine("           ,[FECHA_RATIFICACION]")
            sql_string.AppendLine("           ,[FECHA_VIGENCIA]")
            sql_string.AppendLine("           )")
            sql_string.AppendLine("     VALUES")
            sql_string.AppendLine("           (")
            sql_string.AppendLine("           " & obj.idInstrumento)
            sql_string.AppendLine("           ," & obj.idPais)
            sql_string.AppendLine("           ," & obj.idTipoSocio)
            sql_string.AppendLine("           ," & obj.idBloquePais)
            sql_string.AppendLine("           ,'" & obj.Observaciones & "'")
            sql_string.AppendLine("           ,'" & obj.FechaFirma & "'")
            sql_string.AppendLine("           ,'" & obj.FechaRatificacion & "'")
            sql_string.AppendLine("           ,'" & obj.FechaVigencia & "'")
            sql_string.AppendLine("           )")
            sql_string.AppendLine("")

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, conexion)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception

            Return False

        Finally
        End Try
    End Function

    'Funcón para traer los datos del instrumento pais para editar
    Public Function PaisesInstrumentoMant(ByVal idInstrumento As Integer, ByVal idPais As Integer, ByVal idTipoSocio As Integer) As DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT [ID_INSTRUMENTO]")
            sql_string.AppendLine("      ,[ID_PAIS]")
            sql_string.AppendLine("      ,[ID_TIPO_SOCIO]")
            sql_string.AppendLine("      ,[CODIGO_BLOQUE_PAIS]")
            sql_string.AppendLine("      ,[OBSERVACIONES]")
            sql_string.AppendLine("      ,[FECHA_FIRMA]")
            sql_string.AppendLine("      ,[FECHA_RATIFICACION]")
            sql_string.AppendLine("      ,[FECHA_VIGENCIA]")
            sql_string.AppendLine("  FROM [SGSACDB].[dbo].[IC_Instrumento_Paises]")
            sql_string.AppendLine("  WHERE ID_INSTRUMENTO = " & idInstrumento)
            sql_string.AppendLine("  AND ID_PAIS = " & idPais)
            sql_string.AppendLine("  AND ID_TIPO_SOCIO = " & idTipoSocio)
            sql_string.AppendLine("  AND ESTADO = 1")
            sql_string.AppendLine("")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "PaisesInstrumentoMant")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Paises Instrumento Mantenimiento = " + ex.Message.ToString)
        End Try

        Return ds

    End Function

    'Función para traer los Instrumentos Paises
    Public Function PaisesInstrumento(ByVal idInstrumento As Integer) As DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT IP.ID_INSTRUMENTO")
            sql_string.AppendLine("	,IP.ID_PAIS")
            sql_string.AppendLine("	,IP.ID_TIPO_SOCIO")
            sql_string.AppendLine("	,P.nombre_pais")
            sql_string.AppendLine("	,P.codigo_alfa")
            sql_string.AppendLine("	,IP.FECHA_FIRMA")
            sql_string.AppendLine("	,IP.FECHA_RATIFICACION")
            sql_string.AppendLine("	,IP.FECHA_VIGENCIA")
            sql_string.AppendLine("FROM [dbo].[IC_Instrumento_Paises] AS IP")
            sql_string.AppendLine("INNER JOIN G_Paises AS P")
            sql_string.AppendLine("	ON IP.ID_PAIS = P.id_pais")
            sql_string.AppendLine("WHERE IP.ID_INSTRUMENTO = " & idInstrumento)
            sql_string.AppendLine("	AND IP.ESTADO = 1")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "PaisesInstrumento")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Paises Instrumento = " + ex.Message.ToString)
        End Try

        Return ds
    End Function

    'Función para listar los tipos de socios
    Public Function TipoSocios() As DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT [id_tipo_socio]")
            sql_string.AppendLine("      ,[descripcion]")
            sql_string.AppendLine("FROM [SGSACDB].[dbo].[IC_Tipo_Socio]")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "TipoSocio")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Paises = " + ex.Message.ToString)
        End Try

        Return ds

    End Function

    'Función para listar la región del país
    Public Function RegionPais() As DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT [ID_BLOQUE_PAIS]")
            sql_string.AppendLine("      ,[DESCRIPCION]")
            sql_string.AppendLine("FROM [SGSACDB].[dbo].[IC_Bloque_Pais]")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "RegionPais")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Region Pais = " + ex.Message.ToString)
        End Try

        Return ds

    End Function

    'Función para listar los paises
    Public Function Paises() As DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT [id_pais]")
            sql_string.AppendLine("      ,[nombre_pais]")
            sql_string.AppendLine("      ,[codigo_alfa]")
            sql_string.AppendLine("FROM [SGSACDB].[dbo].[G_Paises]")
            sql_string.AppendLine("WHERE estado = 1")
            sql_string.AppendLine("ORDER BY nombre_pais ASC")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "Paises")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Paises = " + ex.Message.ToString)
        End Try

        Return ds

    End Function

    'Funcion para mostrar los datos del instrumento seleccionado
    Public Function DatosInstrumento(ByVal idInstrumento As Integer) As DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT instrumento.[id_instrumento]")
            sql_string.AppendLine("	  ,instrumento.nombre_instrumento")
            sql_string.AppendLine("      ,tipo.descripcion AS TipoInstrumento")
            sql_string.AppendLine("      ,instrumento.[sigla]")
            sql_string.AppendLine("      ,instrumento.[sigla_alternativa]")
            sql_string.AppendLine("      ,relacion.descripcion as AcuerdoEntre")
            sql_string.AppendLine("FROM [SGSACDB].[dbo].[IC_Instrumentos] as instrumento")
            sql_string.AppendLine("	inner	JOIN dbo.IC_Tipo_Instrumento as tipo")
            sql_string.AppendLine("		on instrumento.id_tipo_instrumento = tipo.id_tipo_instrumento")
            sql_string.AppendLine("	INNER JOIN dbo.IC_Tipo_Relacion_Instrumento as relacion")
            sql_string.AppendLine("		on instrumento.id_tipo_relacion_instrumento = relacion.id_tipo_relacion_instrumento")
            sql_string.AppendLine("WHERE id_instrumento = " & idInstrumento)
            sql_string.AppendLine("	and estado = 1")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds, "DatosInstrumento")
            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectInstrumento = " + ex.Message.ToString)
        Finally

        End Try

        Return ds

    End Function
#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Enmiendas del SAC"

    'Funcion para obtener los datos para los codigos seleccionados
    Public Function SelectDatosCodigoIncisoCorrelacion(ByVal str_codigo As String) As DataSet
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
                " sc.estado, " +
                " sc.inciso_nuevo as codigo_inciso_corr, " +
                " sc.texto_inciso as texto_inciso_corr, sc.dai_nuevo as dai_corr " +
                " FROM " +
                " SAC_Incisos CI " +
                " LEFT OUTER JOIN" +
                " (SELECT " +
                " inciso_origen, inciso_nuevo, " +
                " texto_inciso, dai_nuevo, " +
                " anio_version, version," +
                " CASE WHEN (inciso_nuevo IS NULL)  THEN 'SUPRIMIDA' ELSE 'APERTURA' END as estado " +
                " FROM " +
                " SAC_Correlacion) SC ON " +
                " sc.inciso_origen = ci.codigo_inciso And " +
                " sc.version = ci.id_version And " +
                " sc.anio_version = ci.anio_version " +
                " WHERE "+
                " CI.estado = 'A' AND  " +
                " CI.codigo_inciso LIKE '" + str_codigo + "%' "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                command.Parameters.AddWithValue("capitulo", capitulo)
                command.Parameters.AddWithValue("partida", partida)
                command.Parameters.AddWithValue("subpartida", subpartida)
                command.Parameters.AddWithValue("codigo_inciso", str_codigo)

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
            " FROM SAC_Versiones_Bitacora "

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

    'Funcion que obtiene los datos para el mantenimiento de apertura arancelaria
    Public Function SelectIncisoApertura(ByVal codigo_inciso As String) As DataTable
        Dim dt_inciso_apertura As New DataTable
        Try
            Dim sql_query As String
            sql_query = " select cast(dai_base as numeric(8,2)) as dai_base, texto_inciso " +
                " from SAC_Incisos " +
                " where estado = 'A' AND  " +
                " codigo_inciso = @codigo_inciso "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("codigo_inciso", codigo_inciso)

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
                " ,[fin_vigencia] ,[inicio_vigencia]) " +
                " VALUES " +
                " (@inciso_origen ,@inciso_nuevo " +
                " ,@texto_inciso ,@comentarios " +
                " ,@normativa ,@dai_base " +
                " ,@dai_nuevo ,@anio_version " +
                " ,@anio_nueva_version ,@version " +
                " ,@fin_vigencia ,@inicio_vigencia) "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("inciso_origen", objCorrelacion.inciso_origen)
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
                " ,[anio_version] ,[version])" +
                " VALUES " +
                " (@inciso_origen ,@dai_base " +
                " ,@anio_version ,@version)  "
                
            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("inciso_origen", objCorrelacion.inciso_origen)
                command.Parameters.AddWithValue("dai_base", objCorrelacion.dai_base)
                command.Parameters.AddWithValue("anio_version", objCorrelacion.anio_version)
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

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Asignacion de Precision"

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
            If ValidaInciso(id_instrumento, codigo_inciso, inciso_presicion) Then
                'Si existe inciso, actuliza categoria para inciso
                estado = UpdateInciso(id_instrumento, id_categoria, codigo_inciso, inciso_presicion, texto_precision)
            Else
                'Si no existe inciso, inserta
                estado = InsertInciso(id_instrumento, id_categoria, codigo_inciso, inciso_presicion, texto_precision)
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

                    da = New SqlDataAdapter(command)
                    da.Fill(dt_inciso_precision)

                End Using
            End If

            

        Catch ex As Exception

        Finally

        End Try

        Return dt_inciso_precision
    End Function

    
#End Region

End Class
