Imports System.Text
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

End Class
