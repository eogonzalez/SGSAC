Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmTipoInstrumento
    Inherits System.Web.UI.Page
    Dim objCNTipoInstrumento As New CNTipoInstrumentos
    Dim objCETipoInstrumento As New CETipoInstrumento
    Dim objCNGeneral As New cnGeneral

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvTipoInstrumento()
            Me.btnGuardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btnGuardar, ""))
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If btnGuardar.CommandName = "editar" Then
            If EditarTipoInstrumento(hfIdTipoInstrumento.Value) Then
                Mensaje("Tipo Instrumento actualizado con exito")
                Llenar_gvTipoInstrumento()
                btnGuardar.CommandName = ""
                LimpiarEditarTipoInstrumento()

            Else
                Mensaje("Error al actualizar Tipo Instrumento")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If
        Else
            If GuardarTipoInstrumento() Then
                Mensaje("Tipo Instrumento guardado con éxito")
                Llenar_gvTipoInstrumento()
                LimpiarEditarTipoInstrumento()

            Else
                Mensaje("Error al guardar Tipo Instrumento")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If

        End If

    End Sub

    Protected Sub lkBtt_Editar_Click(sender As Object, e As EventArgs) Handles lkBtt_Editar.Click
        Dim id_tipo_instrumento As Integer = 0
        id_tipo_instrumento = Convert.ToInt32(getIdTipoInstrumentoGridView())
        If id_tipo_instrumento = 0 Then
            Mensaje("Seleccione un Tipo Instrumento")
            Exit Sub
        Else
            LlenarTipoInstrumentoMnat("editar", id_tipo_instrumento)
            btnGuardar.CommandName = "editar"
            hfIdTipoInstrumento.Value = id_tipo_instrumento
            lkBtt_Nuevo_ModalPopupExtender.Show()
        End If
    End Sub

#End Region

#Region "Funciones para capturar valores del formulario"

    Function getDescripcion() As String
        Return txtDescripcion.Text
    End Function

    Function getObservaciones() As String
        Return txtObservaciones.Text
    End Function

#End Region

#Region "Mis funciones"

    'Funcion para editar tipo instrumento
    Private Function EditarTipoInstrumento(ByVal id_tipoInstrumento As Integer) As Boolean
        'Obtengo los valores de los controles
        objCETipoInstrumento.id_tipo_instrumento = id_tipoInstrumento
        objCETipoInstrumento.descripcion = getDescripcion()
        objCETipoInstrumento.observaciones = getObservaciones()

        'Envio los valores a la capa entidad con el objeto a la funcion actualizar tipo instrumento
        Return objCNTipoInstrumento.UpdateTipoInstrumento(objCETipoInstrumento)
    End Function

    'Procedimiento para llenar formulario con el id del instrumento
    Sub LlenarTipoInstrumentoMnat(ByVal accion As String, ByVal id_tipoInstrumento As Integer)
        'Dim Obj_CNTipoInstrumentoMant As New CNInstrumentosComerciales

        Dim dtTipoInstrumento As New DataTable
        dtTipoInstrumento = objCNTipoInstrumento.SelectTipoInstrumentoMant(id_tipoInstrumento)

        If dtTipoInstrumento.Rows.Count = 0 Then
            Mensaje("El Tipo Instrumento no existe")
            Exit Sub
        Else
            If accion = "editar" Then
                txtDescripcion.Text = dtTipoInstrumento.Rows(0)("descripcion").ToString
                txtObservaciones.Text = dtTipoInstrumento.Rows(0)("observaciones").ToString
            End If
        End If
    End Sub

    'Procedimiento para mostrar mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Procedimiento para limbiar editar tipo instrumento
    Sub LimpiarEditarTipoInstrumento()
        txtDescripcion.Text = ""
        txtObservaciones.Text = ""
    End Sub

    'Procedimiento para llenar Gridview de Tipo Instrumento
    Protected Sub Llenar_gvTipoInstrumento()
        Dim tbl As DataTable
        tbl = objCNGeneral.SelectTipoInstrumento.Tables(0)

        With gvTipoInstrumento
            .DataSource = tbl
            .DataBind()
        End With
    End Sub

    'Funcion para guardar nuevo tipo instrumento
    Private Function GuardarTipoInstrumento() As Boolean
        'Obtengo los valores de los controles
        objCETipoInstrumento.descripcion = getDescripcion()
        objCETipoInstrumento.observaciones = getObservaciones()

        'Envio los valores a la capa entidad con el objeto a la funcion guardar nuevo tipo instrumento
        Return objCNTipoInstrumento.InsertTipoInstrumento(objCETipoInstrumento)

    End Function

    'Funcion que obtiene del grid el id del tipo instrumento
    Function getIdTipoInstrumentoGridView() As String
        Dim idTipoInstrumento As String = Nothing

        For i As Integer = 0 To gvTipoInstrumento.Rows.Count - 1
            Dim rbutton As RadioButton = gvTipoInstrumento.Rows(i).FindControl("rb_tipo_instrumento")
            If rbutton.Checked Then
                idTipoInstrumento = gvTipoInstrumento.Rows(i).Cells(0).Text
            End If
        Next

        Return idTipoInstrumento
    End Function

#End Region

End Class