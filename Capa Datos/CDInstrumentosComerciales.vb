﻿Imports System.Text
Imports System.Data.SqlClient
Imports Capa_Entidad

Public Class CDInstrumentosComerciales
    Dim objConeccion As New ConectarService
    Dim da As SqlDataAdapter
    Dim ds As New DataSet

#Region "Funciones y procedimientos para el Mantenimiento de Instrumentos"

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
                command.Parameters.AddWithValue("id_tipo_desgrava", objTipoDesgravacion.id_tipo_desgravacion)
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

                sql_query = " SELECT COUNT(1) Categoria_Activa " +
                    " FROM IC_Categorias_Desgravacion_Tramos " +
                    " WHERE id_instrumento = @id_instrumento AND " +
                    " activo = 'N' " +
                    " GROUP BY id_instrumento "

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
                            contador = 1

                            For Each Row In dt_Categorias.Rows
                                cantidad_cortes = Convert.ToInt32(dt_Categorias.Rows("cantidad_cortes").ToString)
                                id_categoria = Convert.ToInt32(dt_Categorias.Rows("id_categoria").ToString)
                                id_tramo = Convert.ToInt32(dt_Categorias.Rows("id_tramo").ToString)

                                If (dt_Categorias.Rows("codigo_categoria").ToString = codigo_categoria) Then

                                    sql_query = "UPDATE" +
                                        " IC_Categorias_Desgravacion_Tramos " +
                                        " SET " +
                                        " cortes_ejecutados = @cuenta " +
                                        " WHERE " +
                                        " id_categoria = @id_categoria and " +
                                        " id_tramo = @id_tramo "
                                    Using cn3 = objConeccion.Conectar
                                        Dim command3 As SqlCommand = New SqlCommand(sql_query, cn3)
                                        command3.Parameters.AddWithValue("cuenta", contador)
                                        command3.Parameters.AddWithValue("id_categoria", id_categoria)
                                        command3.Parameters.AddWithValue("id_tramo", id_tramo)
                                        cn3.Open()
                                        command3.ExecuteScalar()
                                    End Using

                                    contador = contador + cantidad_cortes
                                Else
                                    codigo_categoria = dt_Categorias.Rows("codigo_categoria").ToString
                                    contador = 1

                                    sql_query = "UPDATE" +
                                        " IC_Categorias_Desgravacion_Tramos " +
                                        " SET " +
                                        " cortes_ejecutados = @cuenta " +
                                        " WHERE " +
                                        " id_categoria = @id_categoria and " +
                                        " id_tramo = @id_tramo "
                                    Using cn3 = objConeccion.Conectar
                                        Dim command3 As SqlCommand = New SqlCommand(sql_query, cn3)
                                        command3.Parameters.AddWithValue("cuenta", contador)
                                        command3.Parameters.AddWithValue("id_categoria", id_categoria)
                                        command3.Parameters.AddWithValue("id_tramo", id_tramo)
                                        cn3.Open()
                                        command3.ExecuteScalar()
                                    End Using

                                    contador = contador + cantidad_cortes
                                End If
                            Next
                            Dim id_version As Integer = 0
                            'Consultar la ultima version


                            'Insertar en la bitacora tratado el primer registro
                            sql_query = "INSERT INTO SAC_Tratados_Bitacora" +
                                " ([id_version] " +
                                " ,[id_corte_version] " +
                                " ,[id_instrumento] " +
                                " ,[cantidad_categoria] " +
                                " ,[fecha_generada]) " +
                                " VALUES " +
                                " (@id_version " +
                                " ,0 " +
                                " ,@id_instrumento " +
                                " ,0 " +
                                " ,SYSDATETIME())"

                            Using cn3 = objConeccion.Conectar
                                Dim command3 As SqlCommand = New SqlCommand(sql_query, cn3)
                                command3.Parameters.AddWithValue("id_version", id_version)
                                command3.Parameters.AddWithValue("id_instrumento", id_instrumento)
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

        sql_query = "select II.nombre_instrumento , II.sigla, COUNT(ICD.id_categoria) as cantidad_categorias" +
            " from IC_Instrumentos II " +
            " left outer join " +
            " IC_Categorias_Desgravacion_Tramos ICD on " +
            " II.id_instrumento = ICD.id_instrumento " +
            " where II.id_instrumento = @id_instrumento " +
            " group by II.nombre_instrumento, II.sigla, ICD.id_categoria "

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
            " ICCD.id_categoria, ICI.sigla AS SIGLA, ICCD.codigo_categoria AS CATEGORIA, " +
            " ICTD.descripcion AS TIPO_DESGRAVACION, " +
            " ICCDT.activo AS ACTIVO, ICCD.cantidad_tramos AS CANTIDAD_TRAMOS, " +
            " SUM(ICCDT.cantidad_cortes) AS CANTIDAD_CORTES " +
            " FROM " +
            " IC_Instrumentos ICI, " +
            " IC_Tipo_Desgravacion ICTD, " +
            " IC_Categorias_Desgravacion ICCD, " +
            " IC_Categorias_Desgravacion_Tramos ICCDT, " +
            " IC_Tipo_Periodo_Corte ICTPC " +
            " WHERE " +
            " ICI.id_instrumento = ICCD.id_instrumento And " +
            " ICCD.id_instrumento = ICCDT.id_instrumento And " +
            " ICCD.id_categoria = ICCDT.id_categoria And " +
            " ICCD.id_tipo_desgrava = ICTD.id_tipo_desgrava And " +
            " ICI.id_instrumento = @id_instrumento " +
            " GROUP BY " +
            " ICCD.id_categoria, ICI.sigla, ICCD.codigo_categoria , " +
            " ICTD.descripcion , " +
            " ICCDT.activo , ICCD.cantidad_tramos ," +
            " ICCDT.cantidad_cortes "

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
            " ICD.id_categoria = ICDT.id_categoria And " +
            " ICD.id_categoria = ICDT.id_categoria " +
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

    'Funcion para obtener los datos para los codigos seleccionados
    Public Function SelectDatosCodigoInciso(ByVal str_codigo As String) As DataSet
        Try
            Dim sql_string As String
            Dim capitulo As String = Nothing
            Dim partida As String = Nothing
            Dim subpartida As String = Nothing

            capitulo = str_codigo
            partida = str_codigo
            subpartida = str_codigo

            sql_string = " SELECT " +
                " descripcion_capitulo " +
                " FROM " +
                " SAC_Capitulos " +
                " WHERE " +
                " Capitulo = @capitulo AND  " +
                " activo = 'A'; " +
                " SELECT " +
                " Descripcion_Partida " +
                " FROM " +
                " SAC_Partidas " +
                " WHERE " +
                " Capitulo = @capitulo AND " +
                " Partida = @partida AND  " +
                " activo = 'A'; " +
                " SELECT " +
                " texto_subpartida " +
                " FROM " +
                " SAC_Subpartidas " +
                " WHERE " +
                " Capitulo = @capitulo AND " +
                " partida = @partida AND " +
                " subpartida = @subpartida AND " +
                " activo = 'A'; " +
                " SELECT " +
                " ci.codigo_inciso, ci.texto_inciso, ci.dai_base, " +
                " icd.codigo_categoria, SAC.inciso_presicion, SAC.texto_precision " +
                " FROM " +
                " SAC_Incisos CI " +
                " left outer join " +
                " SAC_Asocia_Categoria sac on " +
                " sac.id_version = ci.id_version And " +
                " sac.anio_version = CI.anio_version And " +
                " sac.codigo_inciso = ci.codigo_inciso " +
                " LEFT OUTER JOIN " +
                " IC_Categorias_Desgravacion ICD ON " +
                " icd.id_categoria = sac.id_categoria And " +
                " icd.id_instrumento = sac.id_instrumento " +
                " WHERE " +
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
                " select id_categoria, codigo_categoria " +
                " from IC_Categorias_Desgravacion " +
                " WHERE id_instrumento = @id_instrumento; "

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

End Class
