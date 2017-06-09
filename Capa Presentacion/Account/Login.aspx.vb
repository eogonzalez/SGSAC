Imports System.Data.SqlClient
Imports Reglas_del_negocio

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtFecha.Text = DateTime.Now.ToLongDateString()
    End Sub

    Protected Sub btn_ingresar_Click(sender As Object, e As EventArgs) Handles btn_ingresar.Click

        Dim str_usuario As String
        Dim str_contraseña As String
        Dim c_login As New CNLogin


        str_usuario = txt_usuario.Text.Replace(";", "").Replace("--", "")

        str_contraseña = txt_contraseña.Text.Replace(";", "").Replace("--", "")

        If (c_login.Autenticar(str_usuario, str_contraseña)) Then
            'Se verifica en la base de datos el UsuarioID y se almacena en la variable tblUsuario.
            Dim int_idUsuario As Integer = c_login.ConsultarUsuario(str_usuario, str_contraseña)

            If int_idUsuario = 0 Then
                lbl_mensaje_login.Text = " Consuta usuario, Usuario no registrado o contraseña incorrecta"
            Else
                'Como seguridad se almacena en la base de datos el usuarioID, el usuario,  la fecha de ingreso y el ip
                'de todas las veces que el usuario ha ingresado de manera correcta.
                c_login.Seguridad(int_idUsuario, DateTime.Now, Convert.ToString(Request.ServerVariables("REMOTE_ADDR")))

                'se declara y se le da el valor a la variable de sesión UsuarioID
                Session("UsuarioID") = int_idUsuario.ToString()

                'Manda a la principal en caso de ser correcto el login
                Response.Redirect("~/Inicio.aspx")
            End If
        Else
            lbl_mensaje_login.Text = "Autenticar, Usuario no registrado o contraseña incorrecta"
        End If

    End Sub
End Class