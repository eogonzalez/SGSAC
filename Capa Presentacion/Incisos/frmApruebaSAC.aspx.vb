Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmApruebaSAC
    Inherits System.Web.UI.Page    
    Dim objCNAprueba As New CNApruebaSAC
    Dim objCNCorrelacion As New CNCorrelacionSAC
    Dim c_login As New CNLogin

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            lkb_firmas.Attributes.Add("onclick", "return false;")
            lkb_cancelar.Attributes.Add("onclick", "return false;")
            'lkb_aprobar.Attributes.Add("onclick", "return false;")

            LlenarCorrelacionMant()

        End If


    End Sub

    Protected Sub lkb_aprobar_Click(sender As Object, e As EventArgs) Handles lkb_aprobar.Click
        Dim anio_version_SAC As Integer
        Dim anio_inicia_enmienda As Integer
        Dim anio_final_enmienda As Integer
        Dim anio_version_new As Integer
        Dim id_version As Integer

        If ValidaFirmas() Then
            'Ejecuta Aprobacion


            id_version = Session("id_version")
            anio_version_SAC = Session("anio_version_SAC")
            anio_inicia_enmienda = Session("anio_inicia_enmienda")
            anio_final_enmienda = Session("anio_final_enmienda")
            anio_version_new = Session("anio_version_new")

            If objCNAprueba.VerificaApruebaSAC(anio_version_new) Then

                If objCNAprueba.ApruebaSAC(id_version, anio_version_SAC, anio_inicia_enmienda, anio_final_enmienda, anio_version_new) Then
                    Mensaje("Proceso de aprobacion del SAC ha finalizado con exito.")
                Else
                    Mensaje("Ha ocurrido un error en el Proceso de aprobacion del SAC.")
                End If

            Else

                Mensaje("No es posible realizar a probacion, cree una version nueva antes.")
                Return

            End If

        End If

        LlenarCorrelacionMant()
    End Sub

#End Region
    
#Region "Funcion obtiene valores de los controles"
    Function getPrimerUsuario() As String
        Return txt_primer_usuario.Text
    End Function

    Function getPrimerContraseña() As String
        Return txt_primer_contraseña.Text
    End Function

    Function getSegundoUsuario() As String
        Return txt_segundo_usuario.Text
    End Function

    Function getSegundaContraseña() As String
        Return txt_segunda_contraseña.Text
    End Function

#End Region

#Region "Mis funciones"

    Function VerificaFirma(ByVal usuario As String, ByVal password As String) As Boolean
        Dim estado As Boolean = True

        Return estado
    End Function

    Function ValidaFirmas() As Boolean
        Dim estado As Boolean = True

        If getPrimerUsuario.Length = 0 Then
            Mensaje("Ingrese usuario en primer firma.")
            estado = False
        Else
            If getPrimerContraseña.Length = 0 Then
                Mensaje("Ingrese Primera Contrase;a.")
                estado = False
            Else
                'Valido usuario y contraseña uno
                estado = ValidaUsuario(txt_primer_usuario.Text, txt_primer_contraseña.Text)
            End If
        End If

        If getSegundoUsuario.Length = 0 Then
            Mensaje("Ingrese usuario en segunda firma.")
            estado = False
        Else
            If getSegundaContraseña.Length = 0 Then
                Mensaje("Ingrese Segunda Contrase;a")
                estado = False
            Else
                'Valida usuario y contraseña dos
                estado = ValidaUsuario(txt_segundo_usuario.Text, txt_segunda_contraseña.Text)
            End If
        End If

        Return estado
    End Function

    Sub LlenarCorrelacionMant()


        With objCNCorrelacion.SelectCorrelacionMant()

            If Not .Tables(0).Rows.Count = 0 Then

                txt_año_vigencia.Text = .Tables(0).Rows(0)("anio_version").ToString()

                Session.Add("id_version", .Tables(0).Rows(0)("id_version").ToString())
                Session.Add("anio_version_SAC", txt_año_vigencia.Text)

                txt_version_enmienda.Text = .Tables(0).Rows(0)("enmienda").ToString()
                txt_periodo_año_inicial.Text = .Tables(0).Rows(0)("anio_inicia_enmienda").ToString()
                Session.Add("anio_inicia_enmienda", txt_periodo_año_inicial.Text)

                txt_periodo_año_final.Text = .Tables(0).Rows(0)("anio_fin_enmieda").ToString()
                Session.Add("anio_final_enmienda", txt_periodo_año_final.Text)

                txt_descripcion_version_base.Text = .Tables(0).Rows(0)("descripcion").ToString()
            End If

            If Not .Tables(1).Rows.Count = 0 Then
                txt_nuevo_año_vigencia.Text = .Tables(1).Rows(0)("anio_version").ToString()
                Session.Add("anio_version_new", txt_nuevo_año_vigencia.Text)

                txt_version_nueva_enmienda.Text = .Tables(1).Rows(0)("enmienda").ToString()
                txt_nuevo_periodo_año_inicial.Text = .Tables(1).Rows(0)("anio_inicia_enmienda").ToString()
                txt_nuevo_periodo_año_final.Text = .Tables(1).Rows(0)("anio_fin_enmieda").ToString()
                txt_descripcion_nueva_version.Text = .Tables(1).Rows(0)("descripcion").ToString()
            End If

        End With

    End Sub

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    Function ValidaUsuario(ByVal usuario As String, ByVal contraseña As String) As Boolean
        Dim estado As Boolean = True

        Dim str_usuario As String
        Dim str_contraseña As String


        str_usuario = usuario.Replace(";", "").Replace("--", "")
        str_contraseña = contraseña.Replace(";", "").Replace("--", "")

        If (c_login.Autenticar(str_usuario, str_contraseña)) Then
            Dim int_idUsuario As Integer = c_login.ConsultarUsuario(str_usuario, str_contraseña)

            If int_idUsuario = 0 Then
                estado = False
            Else
                c_login.Seguridad(int_idUsuario, DateTime.Now, Convert.ToString(Request.ServerVariables("REMOTE_ADDR")))

                Session("UsuarioID") = int_idUsuario.ToString()

                estado = True
            End If
        Else
            estado = False
        End If


        Return estado
    End Function


#End Region


End Class