Imports Capa_Datos
Public Class CNLogin
    Dim objCDLogin As New LoginService

    Public Function Autenticar(ByVal usuario As String, ByVal contraseña As String) As Boolean
        Return objCDLogin.Autenticar(usuario, contraseña)
    End Function

    Public Function ConsultarUsuario(ByVal usuario As String, ByVal contraseña As String)
        Return objCDLogin.ConsultarUsuario(usuario, contraseña)
    End Function

    Public Function Seguridad(ByVal id_usuario As Integer, ByVal fecha_acceso As Date, ByVal dir_ip As String)
        Return objCDLogin.Seguridad(id_usuario, fecha_acceso, dir_ip)
    End Function
End Class
