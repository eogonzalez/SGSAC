Imports Capa_Entidad
Imports System.Data.SqlClient

Public Class TratadosyAcuerdos
    Dim objConeccion As New ConectarService

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
                Dim da As SqlDataAdapter
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
                Dim da As SqlDataAdapter
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
                Dim da As SqlDataAdapter
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
                Dim da As SqlDataAdapter
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
                        Dim da As SqlDataAdapter
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
                                " (i.dai_base -((((  (B.cantidad_cortes-(B.cortes_ejecutados-@id_corte_nuevo))  * B.factor_desgrava)+B.desgrava_tramo_anterior)/100)* I.dai_base))," +
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
        Dim ds As New DataSet

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
                Dim da As SqlDataAdapter
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
        Dim ds As New DataSet
        Dim da As SqlDataAdapter

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
                Dim da As SqlDataAdapter
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

End Class
