Imports System.Text
Imports System.Data.SqlClient
Imports Capa_Entidad

Public Class CDInstrumentosComerciales
    Dim objConeccion As New ConectarService
    Dim da As SqlDataAdapter
    Dim ds As New DataSet

#Region "Funciones y procedimientos para el Mantenimiento de Instrumentos"

#Region "Funciones para calculo de DAI"

    'Funcion que devuelve registros activos
    Private Function Select_Registro_Activo(ByVal id_instrumento As Integer) As Integer
        Dim registro_activo As Integer = 0
        Dim sql_query As String
        Dim dt_RegistroActivo As New DataTable

        Try
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
            registro_activo = dt_RegistroActivo.Rows(0)("registro_activo")

        Catch ex As Exception

        Finally

        End Try
        Return registro_activo
    End Function

    'Funcion que devuelve cantidad de incisos asociados
    Private Function Select_CantidadIncisos(ByVal id_instrumento As Integer) As Integer
        Dim sql_query As Integer
        Dim dt_Asociadas As New DataTable
        Dim cant_asociadas As Integer = 0
        Try

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
            cant_asociadas = dt_Asociadas.Rows(0)("asociadas")

        Catch ex As Exception

        Finally

        End Try
        Return cant_asociadas
    End Function

    'Funcion que devuelve el id_corte_version
    Private Function Select_Id_Corte_Version(ByVal id_instrumento As Integer) As Integer
        Dim sql_query As String
        Dim dt_Correlativo As New DataTable
        Dim corte_version As Integer
        Try

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
            corte_version = Convert.ToInt32(dt_Correlativo.Rows(0)("version").ToString())

        Catch ex As Exception

        Finally

        End Try
        Return corte_version
    End Function

    'Funcion que devuelve el id_version 
    Private Function Select_Id_Version(ByVal id_instrumento As Integer) As Integer
        Dim sql_query As String
        Dim id_version As Integer = 0

        Try
            Dim dt_Corr As New DataTable

            'Query para obtener el ultimo numero de correlativo
            sql_query = " SELECT COALESCE(( " +
                " SELECT MAX(ID_VERSION)  " +
                " FROM SAC_TRATADOS_BITACORA " +
                " WHERE id_instrumento = @id_instrumento " +
                " AND ESTADO ='A'),0) version "
            Using cn2 = objConeccion.Conectar

                Dim command1 As SqlCommand = New SqlCommand(sql_query, cn2)
                command1.Parameters.AddWithValue("id_instrumento", id_instrumento)
                da = New SqlDataAdapter(command1)
                da.Fill(dt_Corr)

            End Using

            id_version = Convert.ToInt32(dt_Corr.Rows(0)("version").ToString())

        Catch ex As Exception

        Finally

        End Try
        Return id_version
    End Function

    'Funcion para actualizar version A a C en SAC_Tratados_Bitacora
    Private Function Update_Tratados_Bitacora(ByVal id_instrumento As Integer) As Boolean
        Dim estado As Boolean = False
        Dim sql_query As String

        Try
            'Query para actualizar ultimo registro A de tratados bitacora para el instrumento
            sql_query = " UPDATE " +
                " SAC_Tratados_Bitacora " +
                " SET " +
                " estado = 'C' " +
                " WHERE " +
                " id_instrumento = @id_instrumento AND " +
                " estado = 'A' "
            Using cn_up = objConeccion.Conectar
                Dim command_up As SqlCommand = New SqlCommand(sql_query, cn_up)
                command_up.Parameters.AddWithValue("id_instrumento", id_instrumento)
                cn_up.Open()

                If command_up.ExecuteNonQuery() > 0 Then
                    'Si actualiza un registro
                    estado = True
                Else
                    'Si no actualiza un registro
                    estado = False
                End If

            End Using
        Catch ex As Exception
            estado = False
        Finally

        End Try
        Return estado
    End Function

    'Funcion que inserta nueva version del calculo en SAC_Tratados_Bitacora
    Private Function Insert_Tratados_Bitacora(ByVal id_instrumento As Integer, ByVal corte_version As Integer, ByVal id_version As Integer, ByVal cuenta As Integer) As Boolean
        Dim estado As Boolean = False
        Dim sql_query As String
        Try
            sql_query = " INSERT INTO " +
                " SAC_TRATADOS_BITACORA(id_version, id_corte_version, id_instrumento, CANTIDAD_CATEGORIA, estado, FECHA_GENERADA)  " +
                " VALUES " +
                " (@id_version, @id_corte_version, @id_instrumento, @cuenta, 'A',SYSDATETIME()) "

            Using cn3 = objConeccion.Conectar
                Dim command2 As SqlCommand = New SqlCommand(sql_query, cn3)
                command2.Parameters.AddWithValue("id_instrumento", id_instrumento)
                command2.Parameters.AddWithValue("id_corte_version", corte_version)
                command2.Parameters.AddWithValue("id_version", id_version)
                command2.Parameters.AddWithValue("cuenta", cuenta)

                cn3.Open()
                If command2.ExecuteNonQuery() > 0 Then
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

#End Region

    'Funcion para calcular el DAI 
    Public Function CalcularDAI(ByVal id_instrumento As Integer) As Boolean
        Dim estado As Boolean = False
        Try
            Dim sql_query As String
            Dim registro_activo As Integer = 0
            'Funcion para obtener registros activos
            registro_activo = Select_Registro_Activo(id_instrumento)

            If registro_activo = 0 Then
                estado = False
                Exit Try
            Else
                Dim cant_incisos_asociados As Integer = 0
                'Funcion para obtener cantidad de incisos asociados
                cant_incisos_asociados = Select_CantidadIncisos(id_instrumento)

                'If Not (dt_Asociadas.Rows(0)("asociadas") > 99) Then

                If Not (cant_incisos_asociados >= 0) Then
                    estado = False
                    Exit Try
                Else

                    Dim corte_version As Integer
                    'Funcion para obtener el ultimo numero de id_corrte_version
                    corte_version = Select_Id_Corte_Version(id_instrumento) + 1

                    Dim dt_Categorias As New DataTable
                    'Query que consulta los tramos por categoria e instrumento
                    sql_query = "SELECT id_categoria " +
                        " FROM IC_Categorias_Desgravacion_Tramos " +
                        " WHERE id_instrumento = @id_instrumento " +
                        " AND activo = 'S' " +
                        " GROUP BY id_categoria "

                    Using cn = objConeccion.Conectar
                        Dim cuenta As Integer = 0
                        Dim id_catego As Integer = 0

                        Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                        command.Parameters.AddWithValue("id_instrumento", id_instrumento)
                        da = New SqlDataAdapter(command)
                        da.Fill(dt_Categorias)



                        For Each Row As DataRow In dt_Categorias.Rows
                            id_catego = Convert.ToInt32(Row("id_categoria").ToString)

                            sql_query = " INSERT INTO SAC_Dai_Instrumento(id_instrumento, INCISO, CATEGORIA," +
                                " FACTOR_DESGRAVA, DESGRAVA_TRAMOS_ANTES," +
                                " DAI_CALC_ABSOLUTO, DAI_BASE, sigla1_instrumento," +
                                " ID_CORTE_NUEVO, USUARIO_GENERO, FECHA_GENERADA, estado) " +
                                " SELECT @id_instrumento, A.codigo_inciso, A.id_categoria, " +
                                " B.factor_desgrava,B.desgrava_tramo_anterior, " +
                                " (i.dai_base -(((( @id_corte_nuevo * B.factor_desgrava)+B.desgrava_tramo_anterior)/100)* I.dai_base))," +
                                "  I.dai_base, C.sigla, @id_corte_nuevo,'ADMIN',SYSDATETIME(),'CALC1' " +
                                "  FROM SAC_Asocia_Categoria AS A" +
                                "  LEFT OUTER JOIN  SAC_Incisos AS I" +
                                " ON A.codigo_inciso = I.codigo_inciso" +
                                "    AND A.id_categoria = @id_categoria " +
                                "   LEFT OUTER JOIN   IC_Categorias_Desgravacion_Tramos AS B " +
                                " ON A.id_instrumento = @id_instrumento " +
                                "  AND A.id_instrumento = B.id_instrumento " +
                                "  And  A.id_categoria = B.id_categoria " +
                                "   LEFT OUTER JOIN   IC_Instrumentos AS C " +
                                " ON  A.id_instrumento = C.id_instrumento " +
                                " WHERE  I.estado = 'A' AND B.activo = 'S' " +
                                "  AND (B.CORTES_EJECUTADOS - (B.CANTIDAD_CORTES - 1)) <= @id_corte_nuevo " +
                                " AND @id_corte_nuevo <= B.CORTES_EJECUTADOS " +
                                " AND A.ID_CATEGORIA = @id_categoria  AND A.id_instrumento = @id_instrumento " +
                                " ORDER BY  A.codigo_inciso, B.id_tramo "


                            Using cn2 = objConeccion.Conectar
                                Dim command2 As SqlCommand = New SqlCommand(sql_query, cn2)
                                command2.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                command2.Parameters.AddWithValue("id_corte_nuevo", corte_version)
                                command2.Parameters.AddWithValue("id_categoria", id_catego)

                                cn2.Open()
                                If command2.ExecuteNonQuery() > 0 Then
                                    cuenta = cuenta + 1
                                End If
                            End Using

                        Next

                        Dim id_version As Integer
                        'Funcion para obtener el ultimo numero de correlativo
                        id_version = Select_Id_Version(id_instrumento) + 1

                        'Actualizo registro A a C de Sac_Tratados_Bitacora
                        If Update_Tratados_Bitacora(id_instrumento) Then
                            estado = True
                        Else
                            estado = False
                            Exit Try
                        End If



                        If Insert_Tratados_Bitacora(id_instrumento, corte_version, id_version, cuenta) Then
                            estado = True
                        Else
                            estado = False
                            Exit Try
                        End If

                        sql_query = " UPDATE SAC_DAI_TLC SET DAI_CALC_ABSOLUTO = 0 WHERE DAI_CALC_ABSOLUTO < 0"

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
                MsgBox("ERROR CONSULTAR INSTRUMENTO = " + ex.Message.ToString)
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
                            " ORDER BY icd.codigo_categoria, icdt.id_tramo "

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
                                        " id_categoria = @id_categoria AND " +
                                        " id_tramo = @id_tramo "

                                    contador = contador + cantidad_cortes

                                    Using cn3 = objConeccion.Conectar
                                        Dim command3 As SqlCommand = New SqlCommand(sql_query, cn3)
                                        command3.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                        command3.Parameters.AddWithValue("cuenta", contador)
                                        command3.Parameters.AddWithValue("id_categoria", id_categoria)
                                        command3.Parameters.AddWithValue("id_tramo", id_tramo)

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
                                        " id_categoria = @id_categoria AND " +
                                        " id_tramo = @id_tramo "

                                    contador = contador + cantidad_cortes

                                    Using cn3 = objConeccion.Conectar
                                        Dim command3 As SqlCommand = New SqlCommand(sql_query, cn3)
                                        command3.Parameters.AddWithValue("id_instrumento", id_instrumento)
                                        command3.Parameters.AddWithValue("cuenta", contador)
                                        command3.Parameters.AddWithValue("id_categoria", id_categoria)
                                        command3.Parameters.AddWithValue("id_tramo", id_tramo)

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
                                " ,'A') "

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
    Private Function ValidaInciso(ByVal id_instrumento As Integer, ByVal codigo_inciso As String, Optional inciso_presicion As String = Nothing) As Boolean
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
                           " [SAC_Asocia_Categoria] " +
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
                           " [SAC_Asocia_Categoria] " +
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
    Private Function InsertInciso(ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal codigo_inciso As String, Optional codigo_precision As String = Nothing, Optional texto_precision As String = Nothing) As Boolean
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
    Private Function UpdateInciso(ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal codigo_inciso As String, Optional codigo_precision As String = Nothing, Optional texto_precision As String = Nothing) As Boolean
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

            sql_string.AppendLine("UPDATE [IC_Instrumento_Paises]")
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

            sql_string.AppendLine("INSERT INTO [IC_Instrumento_Paises]")
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
            sql_string.AppendLine("  FROM [IC_Instrumento_Paises]")
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
            sql_string.AppendLine("FROM [IC_Tipo_Socio]")

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
            sql_string.AppendLine("FROM [IC_Bloque_Pais]")

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
            sql_string.AppendLine("FROM [G_Paises]")
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
            sql_string.AppendLine("FROM [IC_Instrumentos] as instrumento")
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
                estado = False
            Else
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

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Asignacion de Precision"

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

#Region "Funciones y procedimientos para el Mantenimiento de Configuracion del Menu "

    'Funcion que obtiene listado de opciones del menu
    Public Function SelectOpcionesMenu(Optional ByVal id_padre As Integer = Nothing) As DataTable
        'Se Llena el Data Set por medio consulta
        Dim sql_query As String
        Dim dt As New DataTable

        If Not id_padre = Nothing Then
            'Si id_padre no esta vacio 
            sql_query = " SELECT [id_opcion] " +
            " ,[nombre] " +
            " ,[descripcion] " +
            " ,[url] " +
            " ,obligatorio " +
            " ,visible " +
            " FROM [g_menu_opcion] " +
            " where id_padre = @id_padre " +
            " order by orden "
        Else
            'Si id_padre esta vacio
            sql_query = " SELECT [id_opcion] " +
            " ,[nombre] " +
            " ,[descripcion] " +
            " ,[url] " +
            " ,obligatorio " +
            " ,visible " +
            " FROM [g_menu_opcion] " +
            " where id_padre is null " +
            " order by orden "
        End If
        

        Using cn = objConeccion.Conectar

            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)

                If Not id_padre = Nothing Then
                    'Si padre no esta vacio
                    command.Parameters.AddWithValue("id_padre", id_padre)
                End If

                da = New SqlDataAdapter(command)
                da.Fill(dt)

            Catch ex As Exception
                MsgBox("ERROR CONSULTA OPCIONES DEL MENU = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
                da.Dispose()
            End Try

            Return dt

        End Using
    End Function

    'Funcion que obtiene los valores de la opcion del menu seleccionada
    Public Function SelectOpcionMant(ByVal id_menu_opcion As Integer, Optional ByVal id_padre As Integer = Nothing) As DataTable
        Dim sql_query As String
        Dim dtOpcion As New DataTable

        If Not id_padre = Nothing Then
            'Si id padre no esta vacio
            sql_query = " SELECT [nombre] " +
            " ,[descripcion] " +
            " ,[url] " +
            " ,[orden] " +
            " ,[visible] " +
            " ,[obligatorio] " +
            " FROM [g_menu_opcion] " +
            " where " +
            " id_opcion =  @id_opcion "

        Else
            sql_query = " SELECT [nombre] " +
            " ,[descripcion] " +
            " ,[url] " +
            " ,[orden] " +
            " ,[visible] " +
            " ,[obligatorio] " +
            " FROM [g_menu_opcion] " +
            " where " +
            " id_padre is NULL AND " +
            " id_opcion =  @id_opcion "
        End If

        

        Using cn = objConeccion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                command.Parameters.AddWithValue("id_opcion", id_menu_opcion)
                da = New SqlDataAdapter(command)

                da.Fill(dtOpcion)
                cn.Close()

            Catch ex As Exception
                MsgBox("ERROR CONSULTAR OPCION = " + ex.Message.ToString)
            Finally
                objConeccion.Conectar.Dispose()
                cn.Dispose()
            End Try

            Return dtOpcion

        End Using
    End Function

    'Funcion que almacena nueva opcion de menu
    Public Function SaveOpcionMenu(ByVal obj_CeOpcion As CEOpcionMenu) As Boolean
        Try
            Dim sql_query As String
            Dim id_menu_opcion As Integer
            Dim objGeneral As New General
            id_menu_opcion = objGeneral.ObtenerCorrelativoId("g_menu_opcion", "id_opcion")


            sql_query = " INSERT INTO [g_menu_opcion] " +
                " ([id_opcion], [nombre], [descripcion] "

            If obj_CeOpcion.id_padre = Nothing Then
                sql_query = sql_query + " , [url], [orden] " +
                    " ,[visible],[obligatorio]) " +
                    " VALUES " +
                    " (@id_opcion, @nombre, @descripcion " +
                    " ,@url, @orden " +
                    " ,@visible, @obligatorio) "
            Else
                sql_query = sql_query + " , [url], [id_padre], [orden] " +
                    " ,[visible],[obligatorio]) " +
                    " VALUES " +
                    " (@id_opcion, @nombre, @descripcion " +
                    " ,@url, @id_padre, @orden " +
                    " ,@visible, @obligatorio) "
            End If

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_opcion", id_menu_opcion)
                command.Parameters.AddWithValue("nombre", obj_CeOpcion.nombre)
                command.Parameters.AddWithValue("descripcion", obj_CeOpcion.descripcion)
                command.Parameters.AddWithValue("url", obj_CeOpcion.url)

                If Not obj_CeOpcion.id_padre = Nothing Then
                    command.Parameters.AddWithValue("id_padre", obj_CeOpcion.id_padre)
                End If


                command.Parameters.AddWithValue("orden", obj_CeOpcion.orden)
                command.Parameters.AddWithValue("visible", obj_CeOpcion.visible)
                command.Parameters.AddWithValue("obligatorio", obj_CeOpcion.obligatorio)
                conexion.Open()
                command.ExecuteScalar()

                Return True
            End Using

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    'Funcion que actualiza opcion del menu
    Public Function UpdateOpcionMenu(ByVal obj_CeOpcion As CEOpcionMenu) As Boolean
        Try
            Dim sql_query As String
            Dim objGeneral As New General

            sql_query = " UPDATE [g_menu_opcion] " +
                " set nombre = @nombre,  descripcion = @descripcion, " +
                " url = @url, visible = @visible, obligatorio = @obligatorio, " +
                " orden = @orden "

            If Not obj_CeOpcion.id_padre = Nothing Then

                sql_query = sql_query + " ,id_padre = @id_padre  "
            End If

            sql_query = sql_query + " WHERE " +
                " id_opcion = @id_opcion "

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("id_opcion", obj_CeOpcion.id_opcion)
                command.Parameters.AddWithValue("nombre", obj_CeOpcion.nombre)
                command.Parameters.AddWithValue("descripcion", obj_CeOpcion.descripcion)
                command.Parameters.AddWithValue("url", obj_CeOpcion.url)

                If Not obj_CeOpcion.id_padre = Nothing Then
                    command.Parameters.AddWithValue("id_padre", obj_CeOpcion.id_padre)
                End If


                command.Parameters.AddWithValue("orden", obj_CeOpcion.orden)
                command.Parameters.AddWithValue("visible", obj_CeOpcion.visible)
                command.Parameters.AddWithValue("obligatorio", obj_CeOpcion.obligatorio)
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

#Region "Funcion y procedimientos para los procesos de Aprobacion"

#Region "Funciones de validacion para el proceso de Aprobacion del SAC"

    'Funcion que verifica si es posible realizar el proceso de aprobacion
    Public Function VerificaApruebaSAC(ByVal anioProcesa As Integer) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String

        Try
            sql_query = " select " +
                " count(1) " +
                " FROM " +
                " SAC_Versiones_Bitacora " +
                " where " +
                " anio_version = @anioProcesa AND " +
                " anio_inicia_enmienda = @anioProcesa AND " +
                " estado is null "

            Using conn = objConeccion.Conectar
                Dim command = New SqlCommand(sql_query, conn)
                command.Parameters.AddWithValue("anioProcesa", anioProcesa)
                
                conn.Open()

                If command.ExecuteScalar() > 0 Then
                    estado = True
                Else
                    estado = False
                End If


            End Using

        Catch ex As SqlException
            estado = False
        Catch ex As Exception
            estado = False
        End Try

        Return estado
    End Function


#End Region

    'Funcion que aprueba y genera la siguiente version del SAC
    'Public Function ApruebaSAC() As Boolean
    '    Dim estado As Boolean = True


    '    Try
    '        Using connexion = objConeccion.Conectar
    '            Dim command = New SqlCommand("dbo.APRUEBA_SAC", connexion)
    '            connexion.Open()

    '            If command.ExecuteScalar() > 0 Then
    '                estado = True
    '            Else
    '                estado = False
    '            End If


    '        End Using


    '    Catch ex As SqlException
    '        estado = False
    '    Catch ex As Exception
    '        estado = False
    '    End Try

    '    Return estado
    'End Function

    'Funcion que aprueba y genera la siguiente version del SAC

    Public Function ApruebaSAC(ByVal id_versionAct As Integer, ByVal anio_version As Integer, ByVal anio_inicia_enmienda As Integer, ByVal anio_final_enmienda As Integer, ByVal anio_version_new As Integer) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String
        Try
            '      Se Verifica previamente que no este "APROBADA" la Correlacion
            '       @AnioProcesa es pasado como parametro de la Versión del SAC por Aprobar


            'a) De SAC_Versiones_Bitacora obtener Anio_version y Anio_inicia_enmienda activa "ESTADO= 'A' 

            Dim AnioVERCorAct As Integer = anio_version
            Dim AnioIniciaEnmienda As Integer = anio_inicia_enmienda
            Dim AnioFinEnmienda As Integer = anio_final_enmienda

            'aa) De SAC_Versiones_Bitacora obtener Anio_version DE NUEVA CORRELACION "ESTADO= NULL 
            Dim id_verCorNew As Integer
            Dim AnioVerCorNew As Integer = anio_version_new

            sql_query = " SELECT  id_version " +
                " FROM SAC_Versiones_Bitacora " +
                " WHERE estado Is NULL "

            Using con1 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con1)
                con1.Open()
                id_verCorNew = command.ExecuteScalar()
                con1.Close()
            End Using

            'b) VERIFICA Y ELIMINA TODOS LOS REGISTROS DE LA TABLA: TEMP_INCISOS
            'Validar no aprobar 2 veces o sin terminar

            Dim CanReg As Integer = 0

            sql_query = "DELETE TEM_Incisos"
            Using con2 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con2)
                con2.Open()
                command.ExecuteNonQuery()
                con2.Close()
            End Using

            'c) COPIA de SAC_INCISOS los INCISOS con ESTADO = 'A'  a LA TABLA: TEMP_INCISOS 

            sql_query = " INSERT INTO TEM_Incisos (ID_Version, Anio_Version,Codigo_inciso,texto_inciso, dai_base, Estado ) " +
                " SELECT ID_Version, anio_version, Codigo_inciso, texto_inciso, dai_base, estado " +
                " FROM SAC_Incisos " +
                " WHERE Estado = 'A'  "

            Using con3 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con3)

                con3.Open()
                command.ExecuteNonQuery()
                con3.Close()

            End Using

            'd) Elimina de la tabla: TEMP_INCISOS, los incisos que en SAC_CORRELACION tienen Inciso_nuevo IS NULL
            sql_query = " SELECT distinct Inciso_origen " +
                " FROM SAC_CORRELACION " +
                " WHERE Inciso_nuevo Is NULL " +
                " AND estado Is NULL "

            Dim dataSetTem_Incisos As New DataSet

            Using con4 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con4)
                Dim dataAdapter As New SqlDataAdapter(command)
                con4.Open()
                dataAdapter.Fill(dataSetTem_Incisos)
                con4.Close()

            End Using

            CanReg = 0
            For Each row As DataRow In dataSetTem_Incisos.Tables(0).Rows
                Dim IncisoOrigen As String = row("inciso_origen").ToString

                sql_query = " DELETE TEM_Incisos " +
                    " WHERE  Codigo_inciso = @IncisoOrigen; "

                Using con5 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con5)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    con5.Open()
                    command.ExecuteNonQuery()
                    con5.Close()
                End Using


                CanReg = CanReg + 1

            Next


            'e) Inserta en tabla: TEMP_INCISOS todos los INCISOS  de correlacion con 
            '    "Inciso_Nuevo" IS NOT NULL en SAC_CORRELACION

            sql_query = " SELECT version,anio_nueva_version,Inciso_nuevo, texto_inciso, dai_nuevo " +
                " FROM SAC_CORRELACION " +
                " WHERE Inciso_nuevo Is Not NULL " +
                " AND estado Is NULL " +
                " AND version = @id_versionAct " +
                " AND Anio_Nueva_version =  @AnioVerCorNew " +
                " ORDER BY Inciso_nuevo "

            Dim dataSetINCI_NEW As New DataSet
            Using con6 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con6)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)
                Dim dataAdapter As New SqlDataAdapter(command)

                con6.Open()
                dataAdapter.Fill(dataSetINCI_NEW)
                con6.Close()

            End Using

            Dim inciso As String = "00"

            For Each row As DataRow In dataSetINCI_NEW.Tables(0).Rows
                Dim Id_Version As Integer = row("version")
                Dim AnioNuevaVer As Integer = row("anio_nueva_version")
                Dim IncisoNuevo As String = row("Inciso_nuevo").ToString
                Dim Texto_Inciso As String = row("texto_inciso").ToString
                Dim DAI As Double = row("dai_nuevo")

                If (inciso <> IncisoNuevo) Then

                    sql_query = "  INSERT INTO TEM_Incisos " +
                        " (ID_Version, Anio_Version,Codigo_inciso,texto_inciso, dai_base ) " +
                        " VALUES " +
                        " (@ID_Version, @AnioNuevaVer, @IncisoNuevo, @Texto_Inciso, @DAI); "

                    Using con7 = objConeccion.Conectar
                        Dim command As New SqlCommand(sql_query, con7)
                        command.Parameters.AddWithValue("id_version", Id_Version)
                        command.Parameters.AddWithValue("AnioNuevaVer", AnioNuevaVer)
                        command.Parameters.AddWithValue("IncisoNuevo", IncisoNuevo)
                        command.Parameters.AddWithValue("Texto_Inciso", Texto_Inciso)
                        command.Parameters.AddWithValue("DAI", DAI)
                        con7.Open()
                        command.ExecuteNonQuery()
                        con7.Close()

                    End Using

                    inciso = IncisoNuevo


                End If

            Next


            'Elimina Incisos Repetidos en TEM_INCISOS

            sql_query = " SELECT CODIGO_INCISO, COUNT(1) AS TT FROM TEM_Incisos " +
                " GROUP BY CODIGO_INCISO " +
                " ORDER BY TT  DESC "

            Dim dataSetCOD As New DataSet
            Using con8 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con8)
                Dim dataAdapter As New SqlDataAdapter(command)
                con8.Open()
                dataAdapter.Fill(dataSetCOD)
                con8.Close()


            End Using

            Dim codi As String
            Dim repe As Integer = 0
            Dim i As Integer = 0

            For Each row As DataRow In dataSetCOD.Tables(0).Rows
                codi = row("CODIGO_INCISO")
                repe = row("TT")

                If repe > 1 Then

                    sql_query = " DELETE TEM_Incisos " +
                        " WHERE  CODIGO_INCISO = @Codi " +
                        " AND ESTADO ='A' "

                    Using con9 = objConeccion.Conectar
                        Dim command As New SqlCommand(sql_query, con9)
                        command.Parameters.AddWithValue("Codi", codi)
                        con9.Open()
                        command.ExecuteNonQuery()
                        con9.Close()

                    End Using

                    i = i + 1

                End If

            Next



            ' G) Verifica en cada Instrumentode Impacto generado por la Correlación e Ingresa los
            ' registros en tabla CORRELACION_INSTRUMENTOS con "INCISO_ORIGEN" de la tabla SAC_CORRELACION 

            'f1)Identifica incisos Aperturados que afecten a un Instrumento
            sql_query = " SELECT Inciso_origen " +
                " FROM SAC_CORRELACION " +
                " WHERE estado Is NULL " +
                " GROUP BY Inciso_origen " +
                " ORDER BY Inciso_origen "

            Dim dataSet_IncisoCorre As New DataSet
            Using con10 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con10)
                Dim dataAdapter As New SqlDataAdapter(command)

                con10.Open()
                dataAdapter.Fill(dataSet_IncisoCorre)
                con10.Close()

            End Using

            '/**	BUSCA AFECTACION EN INSTRUMENTOS  **/
            For Each row As DataRow In dataSet_IncisoCorre.Tables(0).Rows

                '/**	CONTEO DE APERTURAS DE CADA INCISO CORRELACION **/
                CanReg = 0
                Dim situacion As String = Nothing
                Dim IncisoOrigen As String = row("Inciso_origen")

                'If IncisoOrigen = "06031093" Then
                '    Dim hola As Integer
                'End If



                sql_query = " SELECT count(1) " +
                    " FROM SAC_Correlacion " +
                    " WHERE INCISO_ORIGEN =@IncisoOrigen " +
                    " AND estado Is NULL " +
                    " AND(INCISO_NUEVO Is Not NULL Or DATALENGTH(INCISO_NUEVO) > 0) "

                Using con11 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con11)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    con11.Open()
                    CanReg = command.ExecuteScalar()
                    con11.Close()

                End Using

                If CanReg > 0 Then
                    situacion = " -APERTURADO- "
                End If

                '/**	IDETIFICA SI EL INCISO SE SUPRIME O NO EN LA CORRELACION **/
                Dim suprime As Integer = 0

                sql_query = " (SELECT count(1) FROM SAC_Correlacion " +
                    " WHERE INCISO_ORIGEN = @IncisoOrigen " +
                    " AND estado Is NULL " +
                    " AND (INCISO_NUEVO IS NULL OR DATALENGTH(INCISO_NUEVO)= 0)) "

                Using con12 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con12)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    con12.Open()
                    suprime = command.ExecuteScalar()
                    con12.Close()

                End Using

                If suprime > 0 Then
                    situacion = situacion + " -SUPRIMIDO- "
                End If

                '/**	OBTIENE DATOS DEL INCISO ORIGINAL  **/
                sql_query = " SELECT texto_inciso,dai_base " +
                    " FROM SAC_Incisos " +
                    " WHERE Estado = 'A' " +
                    " AND CODIGO_INCISO = @IncisoOrigen "

                Dim texto_inciso As String
                Dim DAI_Ori As Double
                Using con13 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con13)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)

                    con13.Open()
                    Dim dataReader As SqlDataReader = command.ExecuteReader()


                    If dataReader.HasRows Then
                        dataReader.Read()

                        texto_inciso = dataReader.GetString(0)
                        DAI_Ori = dataReader.GetDecimal(1)


                    Else
                        Return estado = False

                    End If

                    con13.Close()


                End Using

                '/**	OBTIENE DATOS DE LA CORRELACION  **/

                sql_query = " (SELECT coalesce(max(dai_nuevo),0) " +
                    " FROM SAC_Correlacion " +
                    " WHERE INCISO_ORIGEN = @IncisoOrigen " +
                    " AND estado Is NULL " +
                    " AND (INCISO_NUEVO IS NOT NULL OR DATALENGTH(INCISO_NUEVO)> 0)) "

                Dim dai_max As Integer
                Using con14 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con14)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)

                    con14.Open()
                    dai_max = command.ExecuteScalar()
                    con14.Close()

                End Using

                sql_query = " (SELECT coalesce(min(dai_nuevo),0) " +
                    " FROM SAC_Correlacion " +
                    " WHERE INCISO_ORIGEN = @IncisoOrigen " +
                    " AND estado Is NULL " +
                    " AND (INCISO_NUEVO IS NOT NULL OR DATALENGTH(INCISO_NUEVO)> 0)) "

                Dim dai_min As Integer
                Using con15 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con15)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)

                    con15.Open()
                    dai_min = command.ExecuteScalar()
                    con15.Close()

                End Using


                '/**	OBTIENE DATOS DE LOS TRATADOS  E INSERTA DATOS EN TABLA DE AFECTA TRATADO   **/

                sql_query = " SELECT CODIGO_INCISO,id_instrumento,id_categoria " +
                    " FROM SAC_Asocia_Categoria " +
                    " WHERE estado = 'A' " +
                    " AND  CODIGO_INCISO =  @IncisoOrigen " +
                    " GROUP BY CODIGO_INCISO,id_instrumento,id_categoria; "

                Dim Codigo_Inciso_Inst As String
                Dim Id_instrumento As Integer
                Dim Id_Categoria As Integer
                Dim dTAsociaCat As New DataTable

                Using con16 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con16)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    Dim dataAdapter As New SqlDataAdapter(command)
                    con16.Open()
                    dataAdapter.Fill(dTAsociaCat)
                    con16.Close()

                End Using

                If dTAsociaCat.Rows.Count > 0 Then
                    'Si el inciso afecta tratado

                    For Each rowAsociaCat As DataRow In dTAsociaCat.Rows
                        Codigo_Inciso_Inst = rowAsociaCat("codigo_inciso").ToString
                        Id_instrumento = rowAsociaCat("id_instrumento").ToString
                        Id_Categoria = rowAsociaCat("id_categoria").ToString

                        If Codigo_Inciso_Inst.Length > 4 Then
                            Dim nombre_instrumento As String
                            Dim codigo_categoria As String

                            sql_query = " SELECT Nombre_Instrumento FROM IC_INSTRUMENTOS " +
                                " WHERE id_instrumento = @ID_Instrumento "

                            Using con16 = objConeccion.Conectar
                                Dim command As New SqlCommand(sql_query, con16)
                                command.Parameters.AddWithValue("ID_Instrumento", Id_instrumento)

                                con16.Open()
                                nombre_instrumento = command.ExecuteScalar()
                                con16.Close()

                            End Using


                            sql_query = " SELECT  Codigo_Categoria FROM IC_Categorias_Desgravacion  " +
                                " WHERE id_instrumento = @ID_Instrumento " +
                                " AND ID_Categoria   = @ID_Categoria "

                            Using con17 = objConeccion.Conectar
                                Dim command As New SqlCommand(sql_query, con17)
                                command.Parameters.AddWithValue("ID_Instrumento", Id_instrumento)
                                command.Parameters.AddWithValue("ID_Categoria", Id_Categoria)

                                con17.Open()
                                codigo_categoria = command.ExecuteScalar()
                                con17.Close()

                            End Using



                            sql_query = " INSERT INTO Correlacion_Instrumentos " +
                                " (inciso_original,situacion, id_instrumento, " +
                                " texto_original, dai_original, " +
                                " anio_anterior_cor, canti_aperturas, " +
                                " nombre_instrumento,id_categoria,codigo_categoria, " +
                                " ver_nueva_cor, anio_nueva_cor, " +
                                " dai_max_nuevo,dai_min_nuevo, " +
                                " estado,accion_propuesta, " +
                                " fecha_generada,usuario_generada )   " +
                                " VALUES (@Codigo_Inciso_Inst,@Situacion,@ID_Instrumento, " +
                                " @Texto_Inciso, @DAI_Ori," +
                                " @AnioVerCorAct,@CanReg, " +
                                " @Nombre_Instrumento,@ID_Categoria,@Codigo_Categoria, " +
                                " @ID_VerCorNew, @AnioVerCorNew, " +
                                " @DAI_Max,@DAI_Min, " +
                                " 'PENDIENTE', 'Revisar lista de nuevos incisos aperturados y definir si requiere de asociar la respectiva categoría de desgravación para el tratado o acuerdo', " +
                                " SYSDATETIME(),'USER') "

                            Using con18 = objConeccion.Conectar
                                Dim command As New SqlCommand(sql_query, con18)
                                command.Parameters.AddWithValue("Codigo_Inciso_Inst", Codigo_Inciso_Inst)
                                command.Parameters.AddWithValue("Situacion", situacion)
                                command.Parameters.AddWithValue("ID_Instrumento", Id_instrumento)
                                command.Parameters.AddWithValue("Texto_Inciso", texto_inciso)
                                command.Parameters.AddWithValue("DAI_Ori", DAI_Ori)
                                command.Parameters.AddWithValue("AnioVerCorAct", AnioVERCorAct)
                                command.Parameters.AddWithValue("CanReg", CanReg)
                                command.Parameters.AddWithValue("Nombre_Instrumento", nombre_instrumento)
                                command.Parameters.AddWithValue("ID_Categoria", Id_Categoria)
                                command.Parameters.AddWithValue("Codigo_Categoria", codigo_categoria)
                                command.Parameters.AddWithValue("ID_VerCorNew", id_verCorNew)
                                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                                command.Parameters.AddWithValue("DAI_Max", dai_max)
                                command.Parameters.AddWithValue("DAI_Min", dai_min)

                                con18.Open()
                                command.ExecuteNonQuery()
                                con18.Close()

                            End Using

                        End If

                    Next
                Else
                    'Si el inciso no afecta a ningun tratado
                End If

                situacion = Nothing
                'IncisoCorre =  IncisoOrigen
            Next

            'h) En SAC_INCISOS cambia "el estado" de los registros CON ESTADO = 'I' al ESTADO='H'(historico)**/

            sql_query = " UPDATE SAC_INCISOS SET ESTADO = 'H' " +
                " WHERE  ESTADO = 'I' "

            Using con19 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con19)

                con19.Open()
                command.ExecuteNonQuery()
                con19.Close()

            End Using


            '/** I) En SAC_INCISOS cambia "el estado" de los registros CON ESTADO = 'A' al ESTADO='I'(Inactivo)**/
            sql_query = " UPDATE SAC_INCISOS SET ESTADO = 'I' " +
                " WHERE  ESTADO = 'A' "

            Using con20 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con20)

                con20.Open()
                command.ExecuteNonQuery()
                con20.Close()

            End Using

            '/** J) SE INSERTAN EN SAC_INCISOS los registros de nueva Version y cambia ESTADO = 'A' (Activo) **/


            sql_query = " UPDATE TEM_INCISOS " +
                " SET ID_VERSION = @ID_VerCorNew, anio_version = @AnioVerCorNew; "

            Using con21 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con21)
                command.Parameters.AddWithValue("ID_VerCorNew", id_verCorNew)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)

                con21.Open()
                command.ExecuteNonQuery()
                con21.Close()

            End Using

            sql_query = " INSERT INTO SAC_Incisos (ID_Version, Anio_Version,Codigo_inciso,texto_inciso, dai_base, Estado ) " +
                " SELECT ID_Version, Anio_Version,Codigo_inciso,texto_inciso, dai_base, 'A'  " +
                " FROM TEM_Incisos "

            Using con22 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con22)

                con22.Open()
                command.ExecuteNonQuery()
                con22.Close()

            End Using

            sql_query = " UPDATE SAC_Versiones_Bitacora SET ESTADO = 'I' " +
                " WHERE  ESTADO = 'A'  "

            Using con23 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con23)

                con23.Open()
                command.ExecuteNonQuery()
                con23.Close()

            End Using

            sql_query = " UPDATE SAC_Versiones_Bitacora SET ESTADO = 'A' " +
                " WHERE estado Is NULL "

            Using con24 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con24)

                con24.Open()
                command.ExecuteNonQuery()
                con24.Close()

            End Using

        Catch ex As SqlException
            estado = False
        Catch ex As Exception
            estado = False
        End Try

        Return estado
    End Function


    Public Function SelectResumenInstrumento(ByVal id_instrumento As Integer) As DataTable
        Dim dt As New DataTable
        Dim sql_query As String

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

#End Region


End Class

