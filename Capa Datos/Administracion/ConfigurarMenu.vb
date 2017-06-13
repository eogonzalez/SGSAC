Imports System.Data.SqlClient
Imports Capa_Entidad
Public Class ConfigurarMenu
    Dim objConeccion As New ConectarService
    Dim da As SqlDataAdapter

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


End Class
