Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmTipoDesgravacion
    Inherits System.Web.UI.Page
    Dim objCNTipoDesgravacion As New CNInstrumentosComerciales

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvTipoDesgravacion()
            Me.btnGuardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btnGuardar, ""))
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If btnGuardar.CommandName = "editar" Then
            If EditarTipoDesgravacion(hfIdTipoDesgravacion.Value) Then
                Mensaje("Tipo de Desgravacion actualizado con exito")
                Llenar_gvTipoDesgravacion()
                btnGuardar.CommandName = ""
                LimpiarEditarTipoDesgravacion()

            Else
                Mensaje("Error al actualizar Tipo de Desgravacion")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If
        Else
            If GuardarTipoDesgravacion() Then
                Mensaje("Tipo de Desgravacion guardado con éxito")
                Llenar_gvTipoDesgravacion()
                LimpiarEditarTipoDesgravacion()

            Else
                Mensaje("Error al guardar Tipo de Desgravacion")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If

        End If

    End Sub

    Protected Sub lkBtt_Editar_Click(sender As Object, e As EventArgs) Handles lkBtt_Editar.Click
        Dim id_tipo_desgravacion As Integer = 0
        id_tipo_desgravacion = Convert.ToInt32(getIdTipoDesgravacionGridView())
        If id_tipo_desgravacion = 0 Then
            Mensaje("Seleccione un Tipo de Desgravacion")
            Exit Sub
        Else
            LlenarTipoDesgravacionMant("editar", id_tipo_desgravacion)
            btnGuardar.CommandName = "editar"
            hfIdTipoDesgravacion.Value = id_tipo_desgravacion
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

#Region "Mis Funciones"

    'Funcion para editar tipo desgravacion
    Private Function EditarTipoDesgravacion(ByVal id_tipoDesgravacion As Integer) As Boolean
        'Declaro las variables de la capa de datos y entidad
        Dim CE_objTipoDesgravacion As New CeTipoDesgravacion
        Dim CN_objTipoDesgravacion As New CNInstrumentosComerciales

        'Obtengo los valores de los controles
        CE_objTipoDesgravacion.id_tipo_desgravacion = id_tipoDesgravacion
        CE_objTipoDesgravacion.descripcion = getDescripcion()
        CE_objTipoDesgravacion.observaciones = getObservaciones()

        'Envio los valores a la capa entidad con el objeto a la funcion actualizar tipo desgravacion
        Return CN_objTipoDesgravacion.UpdateTipoDesgravacion(CE_objTipoDesgravacion)
    End Function

    'Procedimiento para llenar formulario con el id de tipo de desgravacion
    Sub LlenarTipoDesgravacionMant(ByVal accion As String, ByVal id_tipoDesgravacion As Integer)
        Dim Obj_CNTipoDesgravacionMant As New CNInstrumentosComerciales

        Dim dtTipoDesgravacion As New DataTable
        dtTipoDesgravacion = Obj_CNTipoDesgravacionMant.SelectTipoDesgravacionMant(id_tipoDesgravacion)

        If dtTipoDesgravacion.Rows.Count = 0 Then
            Mensaje("El Tipo de Desgravacion no existe")
            Exit Sub
        Else
            If accion = "editar" Then
                txtDescripcion.Text = dtTipoDesgravacion.Rows(0)("descripcion").ToString
                txtObservaciones.Text = dtTipoDesgravacion.Rows(0)("observaciones").ToString
            End If
        End If
    End Sub

    'Procedimiento para mostrar mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Procedimiento para limpiar editar tipo de desgravacion
    Sub LimpiarEditarTipoDesgravacion()
        txtDescripcion.Text = ""
        txtObservaciones.Text = ""
    End Sub

    Private Function GuardarTipoDesgravacion() As Boolean
        'Declaro las variables de la capa de datos y entidad
        Dim CEObj As New CeTipoDesgravacion
        Dim CNTipoDesgravacionIns As New CNInstrumentosComerciales

        'Obtengo los valores de los controles
        CEObj.descripcion = getDescripcion()
        CEObj.observaciones = getObservaciones()

        'Envio los valores a la capa entidad con el objeto a la funcion guardar nuevo tipo de desgravacion
        Return CNTipoDesgravacionIns.InsertTipoDesgravacion(CEObj)

    End Function

    'Procedimiento para llenar Gridview de Tipo de desgravacion
    Protected Sub Llenar_gvTipoDesgravacion()
        Dim tbl As DataTable
        tbl = objCNTipoDesgravacion.SelectTipoDesgravacion.Tables(0)

        With gvTipoDesgravacion
            .DataSource = tbl
            .DataBind()
        End With
    End Sub

    'Funcion que obtiene del grid el id del tipo de desgravacion
    Function getIdTipoDesgravacionGridView() As String
        Dim idTipoDesgravacion As String = Nothing

        For i As Integer = 0 To gvTipoDesgravacion.Rows.Count - 1
            Dim rbutton As RadioButton = gvTipoDesgravacion.Rows(i).FindControl("rb_tipo_desgravacion")
            If rbutton.Checked Then
                idTipoDesgravacion = gvTipoDesgravacion.Rows(i).Cells(0).Text
            End If
        Next

        Return idTipoDesgravacion
    End Function

#End Region

End Class