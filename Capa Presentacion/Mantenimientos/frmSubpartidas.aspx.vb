Imports Reglas_del_negocio
Public Class frmSubpartidas
    Inherits System.Web.UI.Page
    Dim objCNPartidas As New CNPartidas
    Dim objCNSubPartidas As New CNSubPartidas

#Region "Funciones del sistema"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LlenarSupPartidasEnc()

            btnGuardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btnGuardar, ""))
        End If

    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click
        LlenarSupPartidasEnc()
        LlenarSeleccionCodigoSubPartida(txt_codigo_subp.Text)
    End Sub

    Protected Sub lkBtn_nuevo_Click(sender As Object, e As EventArgs) Handles lkBtn_nuevo.Click
        txt_codigo_subpartida.Enabled = True
        lkBtn_Nuevo_ModalPopupExtender.Show()
    End Sub

    Protected Sub lkBtn_editar_Click(sender As Object, e As EventArgs) Handles lkBtn_editar.Click
        If ObtieneSubPartidaGriewView() = Nothing Then
            Mensaje("Seleccione una Subpartida para editar.")
        Else
            Dim codigo As String
            codigo = ObtieneSubPartidaGriewView()

            txt_codigo_subpartida.Enabled = False
            LlenarSubPartidasMant(codigo)
            btnGuardar.CommandName = "editar"
            lkBtn_Nuevo_ModalPopupExtender.Show()
        End If

        LlenarSupPartidasEnc()
    End Sub

    Protected Sub gvSubPartidas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvSubPartidas.PageIndexChanging
        gvSubPartidas.PageIndex = e.NewPageIndex

        With gvSubPartidas
            .DataSource = objCNSubPartidas.SelectDatosSubPartidas(txt_codigo_subp.Text)
            .DataBind()
        End With

    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        LlenarSupPartidasEnc()
        LimpiarMant()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If btnGuardar.CommandName = "editar" Then
            If ActualizaSubPartida() Then
                Mensaje("SubPartida actualizada con éxito.")
                LlenarSupPartidasEnc()
                LlenarSeleccionCodigoSubPartida(txt_codigo_subp.Text)
                LimpiarMant()
            Else
                Mensaje("Ha ocurrido un error al actualizar subpartida.")
            End If
        Else
            If ExisteSubPartida(txt_codigo_subpartida.Text) Then
                Mensaje("El codigo de subpartida ya existe.")
                lkBtn_Nuevo_ModalPopupExtender.Show()
            Else
                If GuardarSubPartida() Then
                    Mensaje("SubPartida almacenada con éxito.")

                    LlenarSupPartidasEnc()
                    LlenarSeleccionCodigoSubPartida(txt_codigo_subp.Text)
                    LimpiarMant()
                Else
                    Mensaje("Ha ocurrido un error al almacenar subpartida.")
                End If
            End If
        End If
    End Sub


#End Region

#Region "Mis funciones"

    Function ActualizaSubPartida() As Boolean
        Return objCNSubPartidas.UpdateSubPartida(txt_codigo_subpartida.Text, txt_descripcion_subpartida.Text)
    End Function

    Function GuardarSubPartida() As Boolean
        Return objCNSubPartidas.SaveSubPartida(txt_codigo_subpartida.Text, txt_descripcion_subpartida.Text)
    End Function

    Function ExisteSubPartida(ByVal codigo) As Boolean
        Return objCNSubPartidas.ExisteSubPartida(codigo)
    End Function

    Sub LimpiarMant()
        txt_codigo_subpartida.Text = ""
        txt_descripcion_subpartida.Text = ""
    End Sub

    Sub LlenarSeleccionCodigoSubPartida(ByVal codigo As String)
        Dim dataTable As New DataTable

        With gvSubPartidas
            .DataSource = objCNSubPartidas.SelectDatosSubPartidas(codigo)
            .DataBind()
        End With

    End Sub

    Sub LlenarSubPartidasMant(ByVal codigo As String)
        Dim dataTable As New DataTable

        dataTable = objCNSubPartidas.SelectDatosSubPartidas(codigo)

        txt_codigo_subpartida.Text = dataTable.Rows(0)("subpartida").ToString
        txt_descripcion_subpartida.Text = dataTable.Rows(0)("texto_subpartida").ToString

    End Sub

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    Function ObtieneSubPartidaGriewView() As String
        Dim subpartida As String = Nothing

        For i As Integer = 0 To gvSubPartidas.Rows.Count - 1
            Dim rbutton As RadioButton = gvSubPartidas.Rows(i).FindControl("rb_subpartida")
            If rbutton.Checked Then
                subpartida = gvSubPartidas.Rows(i).Cells(3).Text
            End If
        Next

        Return subpartida
    End Function

    Sub LlenarSupPartidasEnc()
        With objCNPartidas.SelectPartidasEncMant()

            If Not .Rows.Count = 0 Then
                txt_año_vigencia.Text = .Rows(0)("anio_version").ToString()

                txt_version_enmienda.Text = .Rows(0)("enmienda").ToString()
                txt_periodo_año_inicial.Text = .Rows(0)("anio_inicia_enmienda").ToString()
                txt_periodo_año_final.Text = .Rows(0)("anio_fin_enmieda").ToString()
                txt_descripcion_version_base.Text = .Rows(0)("descripcion").ToString()

            End If

        End With
    End Sub

#End Region


End Class