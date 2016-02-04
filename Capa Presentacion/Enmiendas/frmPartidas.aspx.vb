Imports Reglas_del_negocio
Public Class frmPartidas
    Inherits System.Web.UI.Page
    Dim objCNPartidas As New CNPartidas

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LlenarPartidasMant()

            btnGuardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btnGuardar, ""))
        End If

    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click
        LlenarPartidasMant()
        LlenarSeleccionCodigoPartida(txt_codigo_cap.Text)
    End Sub

    Protected Sub gridviewPartidas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridviewPartidas.PageIndexChanging
        gridviewPartidas.PageIndex = e.NewPageIndex

        With gridviewPartidas
            .DataSource = objCNPartidas.SelectDatosPartidas(txt_codigo_cap.Text)
            .DataBind()
        End With

    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        LlenarPartidasMant()
        LimpiarMant()
    End Sub

    Protected Sub lkBtn_nuevo_Click(sender As Object, e As EventArgs) Handles lkBtn_nuevo.Click
        txt_codigo_partida.Enabled = True
        lkBtn_Nuevo_ModalPopupExtender.Show()
    End Sub

    Protected Sub lkBtn_editar_Click(sender As Object, e As EventArgs) Handles lkBtn_editar.Click
        If ObtienePartidaGriewView() = Nothing Then
            Mensaje("Seleccione una partida para editar.")
        Else
            Dim codigo As String
            codigo = ObtienePartidaGriewView()

            txt_codigo_partida.Enabled = False
            LlenarPartidas(codigo)
            btnGuardar.CommandName = "editar"
            lkBtn_Nuevo_ModalPopupExtender.Show()
        End If

        LlenarPartidasMant()

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If btnGuardar.CommandName = "editar" Then
            If ActualizaPartida() Then
                Mensaje("Partida actualizada con éxito.")
                LlenarPartidasMant()
                LlenarSeleccionCodigoPartida(txt_codigo_cap.Text)
                LimpiarMant()
            Else
                Mensaje("Ha ocurrido un error al actualizar partida.")
            End If
        Else
            If ExistePartida(txt_codigo_partida.Text) Then
                Mensaje("El codigo de partida ya existe.")
                lkBtn_Nuevo_ModalPopupExtender.Show()
            Else
                If GuardarPartida() Then
                    Mensaje("Partida almacenada con éxito.")

                    LlenarPartidasMant()
                    LlenarSeleccionCodigoPartida(txt_codigo_cap.Text)
                    LimpiarMant()
                Else
                    Mensaje("Ha ocurrido un error al almacenar partida.")
                End If
            End If
        End If
    End Sub

#End Region


#Region "Mis funciones"

    Function ActualizaPartida() As Boolean
        Return objCNPartidas.UpdatePartida(txt_codigo_partida.Text, txt_descripcion_partida.Text)
    End Function

    Function GuardarPartida() As Boolean
        Return objCNPartidas.SavePartida(txt_codigo_partida.Text, txt_descripcion_partida.Text)
    End Function

    Function ExistePartida(ByVal codigo) As Boolean
        Return objCNPartidas.ExistePartida(codigo)
    End Function

    Sub LlenarPartidasMant()

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

    Sub LlenarSeleccionCodigoPartida(ByVal codigo As String)
        Dim dataTable As New DataTable

        With gridviewPartidas
            .DataSource = objCNPartidas.SelectDatosPartidas(codigo)
            .DataBind()
        End With

    End Sub

    Function ObtienePartidaGriewView() As String
        Dim partida As String = Nothing

        For i As Integer = 0 To gridviewPartidas.Rows.Count - 1
            Dim rbutton As RadioButton = gridviewPartidas.Rows(i).FindControl("rb_partida")
            If rbutton.Checked Then
                partida = gridviewPartidas.Rows(i).Cells(2).Text
            End If
        Next

        Return partida
    End Function

    Sub LimpiarMant()
        txt_codigo_partida.Text = ""
        txt_descripcion_partida.Text = ""
    End Sub

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    Sub LlenarPartidas(ByVal codigo As String)
        Dim dataTable As New DataTable
        dataTable = objCNPartidas.SelectDatosPartidas(codigo)

        txt_codigo_partida.Text = dataTable.Rows(0)("partida").ToString
        txt_descripcion_partida.Text = dataTable.Rows(0)("descripcion_partida").ToString

    End Sub

#End Region

    
    
End Class