Imports System.Data.SqlClient
Imports Capa_Entidad

Public Class CategoriasDesgravacion
    Dim objConeccion As New ConectarService

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
                Dim da As SqlDataAdapter
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
                    Dim da As SqlDataAdapter
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

                Dim da As SqlDataAdapter
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

                Dim da As SqlDataAdapter
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
        Dim ds As New DataSet
        Try
            Dim sql_string As String

            sql_string = " Select id_tipo_desgrava" +
                " ,[descripcion] " +
                " ,[observaciones] " +
                " FROM IC_Tipo_Desgravacion "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)

                Dim da As SqlDataAdapter
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

                Dim da As SqlDataAdapter
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

End Class
