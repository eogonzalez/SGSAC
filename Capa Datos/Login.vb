Imports System.Data.SqlClient
Imports System.Configuration

Public Class LoginService

    'Funcion que verifica la autenticacion del usuario
    Public Function Autenticar(ByVal usuario As String, ByVal contraseña As String) As Boolean
        Dim sql_query As String
        Dim count As Integer
        Dim objConexion As New ConectarService

        'Declaramos la sentencia SQL
        sql_query = " SELECT COUNT(*) FROM g_usuarios  WHERE usuario = @usuario AND password = @contraseña "

        Using conexion = objConexion.Conectar
            Try
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("@usuario", usuario)

                'Encriptamos la contraseña
                Dim encrip As New General
                Dim hash As String = encrip.EncodePassword(usuario + contraseña)

                command.Parameters.AddWithValue("@contraseña", hash)

                'sql_coneccion.Open()
                conexion.Open()
                count = Convert.ToInt32(command.ExecuteScalar())

            Catch ex As Exception
                MsgBox("ERROR AUTENTICAR = " + ex.Message.ToString)
            Finally

            End Try

            If count = 0 Then
                Return False
            Else
                Return True
            End If

        End Using
    End Function

    'Funcion que consulta el Id_usuario 
    Public Function ConsultarUsuario(ByVal usuario As String, ByVal contraseña As String) As Integer
        Dim sql_query As String
        Dim int_idUsuario As Integer = 0
        Dim objConexion As New ConectarService

        sql_query = "  SELECT id_usuario FROM g_usuarios WHERE usuario = @usuario AND password = @contraseña "

        Using conexion = objConexion.Conectar
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)
                command.Parameters.AddWithValue("@usuario", usuario)

                'Encriptamos la contraseña
                Dim encrip As New General
                Dim hash As String = encrip.EncodePassword(usuario + contraseña)
                command.Parameters.AddWithValue("@contraseña", hash)

                conexion.Open()

                int_idUsuario = Convert.ToInt32(command.ExecuteScalar())

            Catch ex As Exception
                MsgBox("ERROR CONSULTARUSUARIO = " + ex.Message.ToString)
            Finally


            End Try
        End Using

        Return int_idUsuario
    End Function

    'La funcion seguridad registra en la tabla g_usuarios_seguridad el ultimo acceso del usuario
    Public Function Seguridad(ByVal id_usuario As Integer, ByVal fecha_acceso As Date, ByVal dir_ip As String)
        Dim sql_query As String
        Dim int_identidad As Integer = 0
        Dim objConexion As New ConectarService

        sql_query = " INSERT INTO g_usuarios_seguridad(id_usuario, fecha_ultimo_acceso, direccion_ip) VALUES(@idUsuario,@fechaAcceso,@dirIP) Select SCOPE_IDENTITY() "

        Using conexion = objConexion.Conectar
            Try
                Dim command As SqlCommand = New SqlCommand(sql_query, conexion)

                command.Parameters.AddWithValue("idUsuario", id_usuario)
                command.Parameters.AddWithValue("fechaAcceso", fecha_acceso)
                command.Parameters.AddWithValue("dirIP", dir_ip)

                conexion.Open()

                int_identidad = Convert.ToInt32(command.ExecuteScalar())

            Catch ex As Exception
                MsgBox("ERROR SEGURIDAD = " + ex.Message.ToString)
            Finally

            End Try
        End Using
        Return int_identidad
    End Function

End Class
