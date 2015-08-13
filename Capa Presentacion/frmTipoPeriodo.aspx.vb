Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmTipoPeriodo
    Inherits System.Web.UI.Page
    Dim objCNTipoPeriodoCorte As New CNInstrumentosComerciales

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvTipoPeriodoCorte()
            Me.btnGuardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & Me.GetPostBackEventReference(Me.btnGuardar))
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If btnGuardar.CommandName = "editar" Then
            If EditarTipoPeriodo(hfIdTipoPeriodoCorte.Value) Then
                Mensaje("Tipo Periodo actualizado con exito")
                Llenar_gvTipoPeriodoCorte()
                btnGuardar.CommandName = ""
                LimpiarEditarTipoPeriodo()

            Else
                Mensaje("Error al actualizar Tipo Periodo")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If
        Else
            If GuardarTipoPeriodo() Then
                Mensaje("Tipo Periodo guardado con éxito")
                Llenar_gvTipoPeriodoCorte()
                LimpiarEditarTipoPeriodo()

            Else
                Mensaje("Error al guardar Tipo Periodo")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If

        End If

    End Sub

    Protected Sub lkBtt_Editar_Click(sender As Object, e As EventArgs) Handles lkBtt_Editar.Click
        Dim id_tipo_periodo As Integer = 0
        id_tipo_periodo = Convert.ToInt32(getIdTipoPeriodoGridView())
        If id_tipo_periodo = 0 Then
            Mensaje("Seleccione un Tipo Periodo")
            Exit Sub
        Else
            LlenarTipoPeriodoMant("editar", id_tipo_periodo)
            btnGuardar.CommandName = "editar"
            hfIdTipoPeriodoCorte.Value = id_tipo_periodo
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
    Private Function EditarTipoPeriodo(ByVal id_tipoPeriodo As Integer) As Boolean
        'Declaro las variables de la capa de datos y entidad
        Dim CE_objTipoPeriodo As New CETipoPeriodo
        Dim CN_objTipoPeriodo As New CNInstrumentosComerciales

        'Obtengo los valores de los controles
        CE_objTipoPeriodo.id_tipo_periodo = id_tipoPeriodo
        CE_objTipoPeriodo.descripcion = getDescripcion()
        CE_objTipoPeriodo.observaciones = getObservaciones()

        'Envio los valores a la capa entidad con el objeto a la funcion actualizar tipo periodo
        Return CN_objTipoPeriodo.UpdateTipoPeriodo(CE_objTipoPeriodo)
    End Function

    'Procedimiento para llenar formulario con el id del instrumento
    Sub LlenarTipoPeriodoMant(ByVal accion As String, ByVal id_tipoPeriodo As Integer)
        Dim Obj_CNTipoPeriodoMant As New CNInstrumentosComerciales

        Dim dtTipoPeriodo As New DataTable
        dtTipoPeriodo = Obj_CNTipoPeriodoMant.SelectTipoPeriodoMant(id_tipoPeriodo)

        If dtTipoPeriodo.Rows.Count = 0 Then
            Mensaje("El Tipo Periodo no existe")
            Exit Sub
        Else
            If accion = "editar" Then
                txtDescripcion.Text = dtTipoPeriodo.Rows(0)("descripcion").ToString
                txtObservaciones.Text = dtTipoPeriodo.Rows(0)("observaciones").ToString
            End If
        End If
    End Sub

    'Procedimiento para mostrar mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Procedimiento para limpiar editar tipo periodo
    Sub LimpiarEditarTipoPeriodo()
        txtDescripcion.Text = ""
        txtObservaciones.Text = ""
    End Sub

    'Procedimiento para llenar Gridview de Tipo periodo
    Protected Sub Llenar_gvTipoPeriodoCorte()
        Dim tbl As DataTable
        tbl = objCNTipoPeriodoCorte.SelectTipoPeriodo.Tables(0)

        With gvTipoPeriodoCorte
            .DataSource = tbl
            .DataBind()
        End With
    End Sub

    'Funcion para guardar nuevo tipo periodo
    Private Function GuardarTipoPeriodo() As Boolean
        'Declaro las variables de la capa de datos y entidad
        Dim CEObj As New CETipoPeriodo
        Dim CNTipoPeriodo As New CNInstrumentosComerciales

        'Obtengo los valores de los controles
        CEObj.descripcion = getDescripcion()
        CEObj.observaciones = getObservaciones()

        'Envio los valores a la capa entidad con el objeto a la funcion guardar nuevo tipo instrumento
        Return CNTipoPeriodo.InsertTipoPeriodo(CEObj)

    End Function

    'Funcion que obtiene del grid el id del tipo periodo
    Function getIdTipoPeriodoGridView() As String
        Dim idTipoPeriodo As String = Nothing

        For i As Integer = 0 To gvTipoPeriodoCorte.Rows.Count - 1
            Dim rbutton As RadioButton = gvTipoPeriodoCorte.Rows(i).FindControl("rb_tipo_periodo")
            If rbutton.Checked Then
                idTipoPeriodo = gvTipoPeriodoCorte.Rows(i).Cells(0).Text
            End If
        Next

        Return idTipoPeriodo
    End Function

#End Region

End Class