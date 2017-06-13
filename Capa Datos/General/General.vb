Imports System.Security.Cryptography
Imports System.Text
Imports System.Data.SqlClient
Public Class General
    Dim objConeccion As New ConectarService
    Dim cn As New SqlConnection
    Dim da As New SqlDataAdapter

    Public Function EncodePassword(ByVal originalPassword As String) As String

        'Clave que se utilizará para encriptar el usuario y la contraseña
        Dim clave As String = "7f9facc418f74439c5e9709832;0ab8a5:OCOdN5Wl,q8SLIQz8i|8agmu¬s13Q7ZXyno/"

        'Se instancia el objeto sha512 para posteriormente usarlo para calcular la matriz de bytes especificada
        Dim sha512 As SHA512 = New SHA512CryptoServiceProvider()

        'Se crea un arreglo llamada inputbytes donde se convierte el usuario, la contraseña y la clave a una secuencia de bytes.
        Dim inputBytes() As Byte = (New UnicodeEncoding()).GetBytes(originalPassword + clave)

        'Se calcula la matriz de bytes del arreglo anterior y se encripta.
        Dim hash() As Byte = sha512.ComputeHash(inputBytes)


        'Convertimos el arreglo de bytes a cadena.
        Return Convert.ToBase64String(hash)

    End Function

    Public Function MunuPrincipal() As DataSet
        Dim sql_query As String
        Dim ds As New DataSet

        Try
            sql_query = " SELECT id_opcion " +
                " ,nombre ,descripcion " +
                " ,url ,id_padre " +
                " FROM dbo.g_menu_opcion " +
                " where obligatorio = 1 Or visible = 1 " +
                " order by orden "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                da = New SqlDataAdapter(command)
                da.Fill(ds)
                cn.Close()

            End Using

        Catch ex As Exception

        Finally
            objConeccion.Conectar.Dispose()
            cn.Dispose()

        End Try
        Return ds
    End Function

    Public Function ObtenerCorrelativoId(ByVal nombreTabla As String, ByVal llave_tabla As String, Optional ByVal TieneEstado As Boolean = False, Optional ByVal llave_filtro As String = Nothing, Optional ByVal valor_llave_filtro As Integer = 0) As Integer
        Dim correlativo As Integer
        Dim sql_query As String

        Try
            sql_query = " SELECT max(" + llave_tabla + ") " +
                        " FROM " +
                        nombreTabla

            If TieneEstado = True Then
                sql_query = sql_query + " WHERE estado = 1 "

                If Not llave_filtro = Nothing Then
                    sql_query = sql_query + " AND " + llave_filtro + " = " + valor_llave_filtro.ToString
                End If
            Else
                If Not llave_filtro = Nothing Then
                    sql_query = sql_query + " WHERE " + llave_filtro + " = " + valor_llave_filtro.ToString
                End If
            End If

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_query, cn)
                cn.Open()
                correlativo = IIf(IsDBNull(command.ExecuteScalar), 0, command.ExecuteScalar)
            End Using

        Catch ex As SqlException
            MsgBox("ERROR DE SQL OBTENERCORRELATIVOID = " + ex.Message.ToString)
        Catch ex As Exception
            MsgBox("ERROR OBTENERCORRELATIVOID = " + ex.Message.ToString)
        Finally
            cn.Dispose()
        End Try
        Return correlativo + 1
    End Function

    'Funcion que obtiene el nombre del instrumento
    Public Function ObtenerNombreInstrumento(ByVal id_instrumento) As String
        Dim sql_server
        Dim nombre_instrumento = Nothing

        Try
            sql_server = " SELECT " +
                " sigla+' - '+nombre_instrumento " +
                " FROM " +
                " IC_Instrumentos " +
                " where " +
                " id_instrumento = @id_instrumento "

            Using con = objConeccion.Conectar
                Dim command = New SqlCommand(sql_server, con)
                command.Parameters.AddWithValue("id_instrumento", id_instrumento)

                con.Open()
                nombre_instrumento = command.ExecuteScalar()
                con.Close()


            End Using

        Catch ex As SqlException

        Catch ex As Exception

        End Try

        Return nombre_instrumento
    End Function

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

    'Funcion para seleccionar listado del combo tipo de instrumetos
    Public Function SelectTipoInstrumento() As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As String

            sql_string = " SELECT id_tipo_instrumento, descripcion, observaciones " +
                " from IC_Tipo_Instrumento "

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                Dim da As SqlDataAdapter
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
        Dim ds As New DataSet
        Try
            Dim sql_string As String

            sql_string = "select id_tipo_relacion_instrumento, descripcion, observaciones " +
                " from IC_Tipo_Relacion_Instrumento "

            Using cn = objConeccion.Conectar

                Dim command As SqlCommand = New SqlCommand(sql_string, cn)
                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds, "TimpoRelIns")

            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectTipoRelacionInstrumento = " + ex.Message.ToString)
        Finally

        End Try

        Return ds

    End Function

End Class
