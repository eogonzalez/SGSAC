Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmTipoRelacionInstrumento
    Inherits System.Web.UI.Page
    Dim objCNTipoRelacionIns As New CNTipoRelacionInstrumento
    Dim objCETipoRelacion As New CETipoRelacionInstrumento
    Dim objCNGeneral As New cnGeneral
#Region "Funciones del sistema"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvTipoRelacionInstrumento()
            Me.btnGuardar.Attributes.Add("onclick", "this.vale='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btnGuardar, ""))

        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If btnGuardar.CommandName = "editar" Then
            If EditarTipoRelacionInstrumento(hfIdTipoRelacionInstrumento.Value) Then
                Mensaje("Tipo Relacion Instrumento actualizado con éxito.")
                Llenar_gvTipoRelacionInstrumento()
                btnGuardar.CommandName = ""
                LimpiarEdiarTipoRelacionInstrumento()

            Else
                Mensaje("Error al actualizar Tipo Relacion Instrumento")
                lkBtt_Nuevo_ModalPopupExtender.Show()

            End If
        Else
            If GuardarTipoRelacionInstrumento() Then
                Mensaje("Tipo Relacion Intrumento guardado con éxito.")
                Llenar_gvTipoRelacionInstrumento()
                LimpiarEdiarTipoRelacionInstrumento()

            Else
                Mensaje("Error al guardar Tipo Relacion Instrumento")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If
        End If

    End Sub

    Protected Sub lkBtt_Editar_Click(sender As Object, e As EventArgs) Handles lkBtt_Editar.Click
        Dim id_tipoRelacionInstrumento As Integer = 0
        id_tipoRelacionInstrumento = Convert.ToInt32(getIdTipoRelacionInstrumentoGridView)

        If id_tipoRelacionInstrumento = 0 Then
            Mensaje("Seleccione un Tipo Relacion Instrumento")
            Exit Sub
        Else
            LlenarTipoRelacionInstrumentoMant("editar", id_tipoRelacionInstrumento)
            btnGuardar.CommandName = "editar"
            hfIdTipoRelacionInstrumento.Value = id_tipoRelacionInstrumento
            lkBtt_Nuevo_ModalPopupExtender.Show()
        End If
    End Sub

#End Region

#Region "Funciones para obtener valores de los controles"

    Function getDescripcion() As String
        Return txtDescripcion.Text
    End Function

    Function getObservaciones() As String
        Return txtObservaciones.Text
    End Function

#End Region

#Region "Mis funciones"

    'Procedimiento para limpiar editar tipo relacion instrumento
    Sub LimpiarEdiarTipoRelacionInstrumento()
        txtDescripcion.Text = ""
        txtObservaciones.Text = ""
    End Sub

    'Funcion para editar tipo relacion instrumento
    Private Function EditarTipoRelacionInstrumento(ByVal id_tipoRelacionInstrumento As Integer)
        'Obtengo los valores de los controles
        objCETipoRelacion.id_tipo_relacion_instrumento = id_tipoRelacionInstrumento
        objCETipoRelacion.descripcion = getDescripcion()
        objCETipoRelacion.observaciones = getObservaciones()

        'Envio los valores a la capa entidad con el objeto a la funcion actualizar tipo relacion intrumento
        Return objCNTipoRelacionIns.UpdateTipoRelacionInstrumento(objCETipoRelacion)
    End Function

    'Procedimiento para llenar formulario con el id del instrumento
    Sub LlenarTipoRelacionInstrumentoMant(ByVal accion As String, ByVal id_tipoRelacionInstrumento As Integer)

        Dim dtTipoRelacionInstrumento As New DataTable
        dtTipoRelacionInstrumento = objCNTipoRelacionIns.SelectTipoRelacionInstrumentoMant(id_tipoRelacionInstrumento)

        If dtTipoRelacionInstrumento.Rows.Count = 0 Then
            Mensaje("El Tipo Relacion Instrumento no existe")
            Exit Sub
        Else
            If accion = "editar" Then
                txtDescripcion.Text = dtTipoRelacionInstrumento.Rows(0)("descripcion").ToString
                txtObservaciones.Text = dtTipoRelacionInstrumento.Rows(0)("observaciones").ToString
            End If
        End If
    End Sub

    'Procedimiento para mostrar mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Funcion que obtiene id del gridview
    Function getIdTipoRelacionInstrumentoGridView() As String
        Dim idTipoRelacionInstrumento As String = Nothing

        For i As Integer = 0 To gvTipoRelacionInstrumento.Rows.Count - 1
            Dim rbutton As RadioButton = gvTipoRelacionInstrumento.Rows(i).FindControl("rb_tipo_relacion_instrumento")
            If rbutton.Checked Then
                idTipoRelacionInstrumento = gvTipoRelacionInstrumento.Rows(i).Cells(0).Text
            End If
        Next
        Return idTipoRelacionInstrumento
    End Function

    'Procedimiento para llenar Gridview
    Protected Sub Llenar_gvTipoRelacionInstrumento()
        Dim tbl As DataTable
        tbl = objCNGeneral.SelectTipoRelacionInstrumento.Tables(0)

        With gvTipoRelacionInstrumento
            .DataSource = tbl
            .DataBind()
        End With
    End Sub

    'Funcion para guardar nuevo tipo de relacion instrumento
    Private Function GuardarTipoRelacionInstrumento() As Boolean
        'Obtener los valores de los controles
        objCETipoRelacion.descripcion = getDescripcion()
        objCETipoRelacion.observaciones = getObservaciones()

        'Envio de valores a la capa entidad con el objeto a la funcion insertar nuevo tipo relacion instrumento
        Return objCNTipoRelacionIns.InsertTipoRelacionInstrumento(objCETipoRelacion)

    End Function

#End Region

End Class