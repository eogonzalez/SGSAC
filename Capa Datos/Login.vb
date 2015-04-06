Imports System.Data.SqlClient
Imports System.Configuration

Public Class LoginService

    'Funcion que verifica la autenticacion del usuario
    Public Function Autenticar(ByVal usuario As String, ByVal contraseña As String) As Boolean
        Dim sql_query As String
        Dim count As Integer

        'Declaramos la sentencia SQL
        sql_query = " SELECT COUNT(*) FROM g_usuarios  WHERE usuario = @usuario AND password = @contraseña "

        Using sql_coneccion = New SqlConnection(ConfigurationManager.ConnectionStrings("cn").ConnectionString)
            Try

                Dim command As SqlCommand = New SqlCommand(sql_query, sql_coneccion)
                command.Parameters.AddWithValue("@usuario", usuario)

                'Encriptamos la contraseña
                Dim encrip As New General
                Dim hash As String = encrip.EncodePassword(usuario + contraseña)

                command.Parameters.AddWithValue("@contraseña", hash)

                sql_coneccion.Open()
                count = Convert.ToInt32(command.ExecuteScalar())

            Catch ex As Exception
                MsgBox(ex.Message.ToString)
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
        Dim sql As String
        Dim int_idUsuario As Integer = 0

        sql = "  SELECT id_usuario FROM g_usuarios WHERE usuario = @usuario AND password = @contraseña "

        Using sql_coneccion As New SqlConnection(ConfigurationManager.ConnectionStrings("cn").ConnectionString)
            Try
                sql_coneccion.Open()

                Dim command As SqlCommand = New SqlCommand(sql, sql_coneccion)

                command.Parameters.AddWithValue("@usuario", usuario)

                'Encriptamos la contraseña
                Dim encrip As New General
                Dim hash As String = encrip.EncodePassword(usuario + contraseña)
                command.Parameters.AddWithValue("@contraseña", hash)

                int_idUsuario = Convert.ToInt32(command.ExecuteScalar())

            Catch ex As Exception
                MsgBox("Error ConsultarUsuario" + ex.Message.ToString)
            Finally


            End Try
        End Using

        Return int_idUsuario
    End Function

    'La funcion seguridad registra en la tabla g_usuarios_seguridad el ultimo acceso del usuario
    Public Function Seguridad(ByVal id_usuario As Integer, ByVal fecha_acceso As Date, ByVal dir_ip As String)
        Dim sql As String
        Dim int_identidad As Integer = 0

        sql = " INSERT INTO g_usuarios_seguridad(id_usuario, fecha_ultimo_acceso, direccion_ip) VALUES(@idUsuario,@fechaAcceso,@dirIP) Select SCOPE_IDENTITY() "

        Using sql_coneccion As New SqlConnection(ConfigurationManager.ConnectionStrings("cn").ConnectionString)
            Try
                sql_coneccion.Open()

                Dim command As SqlCommand = New SqlCommand(sql, sql_coneccion)

                command.Parameters.AddWithValue("idUsuario", id_usuario)
                command.Parameters.AddWithValue("fechaAcceso", fecha_acceso)
                command.Parameters.AddWithValue("dirIP", dir_ip)

                int_identidad = Convert.ToInt32(command.ExecuteScalar())

            Catch ex As Exception
                MsgBox("ERROR SEGURIDAD " + ex.Message.ToString)
            Finally

            End Try
        End Using
        Return int_identidad
    End Function

End Class
