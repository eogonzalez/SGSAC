Imports Reglas_del_negocio

Public Class frmConfigurarMenu
    Inherits System.Web.UI.Page
    Dim objCN As New CNInstrumentosComerciales

#Region "Funciones del Sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvOpcionesMenu()

            'Valores por defecto si es nuevo
            txtOrden.Text = 0
            cb_obligatorio.Checked = False
            cb_visible.Checked = True

            Me.btn_Guardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btn_Guardar, ""))
        End If

    End Sub

    Protected Sub lkBtt_editar_Click(sender As Object, e As EventArgs) Handles lkBtt_editar.Click

        Dim id_menu_opcion As Integer
        id_menu_opcion = getIdMenuOpcionGridView()
        Session.Add("id_menu_opcion", id_menu_opcion)

        If id_menu_opcion = 0 Then
            Mensaje("Seleccione una opcion.")
            Exit Sub
        Else
            LlenarMenuOpcionMant(id_menu_opcion)
            btn_Guardar.CommandName = "editar"

            lkBtt_nuevo_ModalPopupExtender.Show()
        End If

    End Sub

    Protected Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        LimpiarEditarOpcion()
    End Sub

    Protected Sub btn_Guardar_Click(sender As Object, e As EventArgs) Handles btn_Guardar.Click

        If btn_Guardar.CommandName = "editar" Then
            'Cuando el cambio se da por edicion

        Else
            'Cuando el cambio se da por nueva opcion

        End If
    End Sub


#End Region

#Region "Mis Funciones"

    Protected Sub Llenar_gvOpcionesMenu()
        Dim tbl As New DataTable

        tbl = objCN.SelectOpcionesMenu()

        With gvOpcionesMenu
            .DataSource = tbl
            .DataBind()
        End With

    End Sub

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Funcion que obtiene del grid el id del instrumento
    Function getIdMenuOpcionGridView() As Integer
        Dim id_menu_opcion As Integer = Nothing

        For i As Integer = 0 To gvOpcionesMenu.Rows.Count - 1
            Dim rbutton As RadioButton = gvOpcionesMenu.Rows(i).FindControl("rb_opcion")
            If rbutton.Checked Then
                id_menu_opcion = gvOpcionesMenu.Rows(i).Cells(0).Text
            End If
        Next
        Return id_menu_opcion
    End Function

    Sub LlenarMenuOpcionMant(ByVal id_menu_opcion As Integer)
        Dim dtOpcion As New DataTable

        dtOpcion = objCN.SelectOpcionMant(id_menu_opcion)

        If dtOpcion.Rows.Count = 0 Then
            Mensaje("La opcion no existe.")
            Exit Sub
        Else
            txtNombreOpcion.Text = dtOpcion.Rows(0)("nombre").ToString
            txtDescripcionOpcion.Text = dtOpcion.Rows(0)("descripcion").ToString
            txtURL.Text = dtOpcion.Rows(0)("url").ToString

            If IsDBNull(dtOpcion.Rows(0)("obligatorio")) Then
                'Si obligatorio es nulo
                cb_obligatorio.Checked = False

                If IsDBNull(dtOpcion.Rows(0)("visible")) Then
                    'Si visible es nulo
                    cb_visible.Checked = False
                Else
                    'Si visible no es nulo
                    If dtOpcion.Rows(0)("visible") Then
                        'Si opcion es visible
                        cb_visible.Checked = True
                        cb_visible.Enabled = True
                    Else
                        cb_visible.Checked = False
                        cb_visible.Enabled = True
                    End If

                End If

            Else
                'Si obligatorio no es nulo
                If dtOpcion.Rows(0)("obligatorio") Then
                    'Si opcion es obligatoria
                    cb_obligatorio.Enabled = False
                    cb_obligatorio.Checked = True

                    If IsDBNull(dtOpcion.Rows(0)("visible")) Then
                        'Si visible es nulo
                        cb_visible.Checked = False
                    Else
                        'Si visible no es nulo
                        If dtOpcion.Rows(0)("visible") Then
                            'Si opcion es visible
                            cb_visible.Checked = True
                            cb_visible.Enabled = False
                        Else
                            cb_visible.Checked = False
                            cb_visible.Enabled = True
                        End If

                    End If

                Else
                    'Si opcion no es obligatorio
                    cb_obligatorio.Enabled = True
                    cb_obligatorio.Checked = False

                    If IsDBNull(dtOpcion.Rows(0)("visible")) Then
                        'Si visible es nulo
                        cb_visible.Checked = False
                    Else
                        'Si visible no es nulo
                        If dtOpcion.Rows(0)("visible") Then
                            'Si opcion es visible
                            cb_visible.Checked = True
                            cb_visible.Enabled = True
                        Else
                            cb_visible.Checked = False
                            cb_visible.Enabled = True
                        End If

                    End If

                End If
            End If


        End If

    End Sub

    'Metodo que limpia el formulario al salir
    Sub LimpiarEditarOpcion()
        txtNombreOpcion.Text = ""
        txtDescripcionOpcion.Text = ""
        txtURL.Text = ""
        txtOrden.Text = ""
    End Sub

#End Region

    
End Class