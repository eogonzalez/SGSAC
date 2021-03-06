﻿Imports Reglas_del_negocio
Imports Capa_Entidad

Public Class frmTratadosyAcuerdos
    Inherits System.Web.UI.Page
    Dim objCNTratados As New CNTratadosyAcuerdos
    Dim ObjCEInstrumentoMant As New CEInstrumentosMant
    Dim objCNGeneral As New cnGeneral

#Region "Funciones del Sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvInstrumentos()
            LlenarTipoInstrumento()
            LlenarTipoRelacionInstrumento()
            Me.btn_Guardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btn_Guardar, ""))
        End If
    End Sub

    Protected Sub lkBtt_editar_Click(sender As Object, e As EventArgs) Handles lkBtt_editar.Click
        
        Dim id_instrumento As Integer
        id_instrumento = Convert.ToInt32(getIdInstrumentoGridView())
        If id_instrumento = 0 Then
            Mensaje("Seleccione un instrumeto")
            Exit Sub
        Else
            LlenarInstrumentosMant("editar", id_instrumento)
            btn_Guardar.CommandName = "editar"
            hfIdInstrumento.Value = id_instrumento
            lkBtt_nuevo_ModalPopupExtender.Show()
        End If

    End Sub

    Protected Sub btn_Guardar_Click(sender As Object, e As EventArgs) Handles btn_Guardar.Click

        If btn_Guardar.CommandName = "editar" Then
            If EditarInstrumento(hfIdInstrumento.Value) Then
                Mensaje("Instrumento actualizado con éxito")
                Llenar_gvInstrumentos()
                btn_Guardar.CommandName = ""
                LimpiarEditarInstrumento()
            Else
                Mensaje("Error al actualizar Instrumento")
                lkBtt_nuevo_ModalPopupExtender.Show()
            End If
            
        Else
            If GuardarInstrumento() Then
                Mensaje("Instrumento guardado con éxito")
                Llenar_gvInstrumentos()
                LimpiarEditarInstrumento()
            Else
                Mensaje("Error al guardar Instrumento")
                lkBtt_nuevo_ModalPopupExtender.Show()
            End If
            
        End If
    End Sub

    Protected Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        LimpiarEditarInstrumento()
    End Sub

    Protected Sub lkBtt_categorias_Click(sender As Object, e As EventArgs) Handles lkBtt_categorias.Click
        Dim id_intrumento As Integer = 0
        id_intrumento = Convert.ToInt32(getIdInstrumentoGridView())
        If id_intrumento = 0 Then
            Mensaje("Seleccione un instrumento")
            Exit Sub
        Else
            hfIdInstrumento.Value = id_intrumento
            Response.Redirect("~/Tratados/frmCategoriasDesgravacion.aspx?id_inst=" + hfIdInstrumento.Value)
        End If

    End Sub

    Protected Sub lkBtt_pai_Click(sender As Object, e As EventArgs) Handles lkBtt_paises.Click
        Dim id_intrumento As Integer = 0
        id_intrumento = Convert.ToInt32(getIdInstrumentoGridView())
        If id_intrumento = 0 Then
            Mensaje("Seleccione un instrumento")
            Exit Sub
        Else
            hfIdInstrumento.Value = id_intrumento
            Response.Redirect("~/Tratados/frmPaisesInstrumento.aspx?id_inst=" + hfIdInstrumento.Value)
        End If
    End Sub

    Protected Sub lkBtn_asignar_categorias_Click(sender As Object, e As EventArgs) Handles lkBtn_asignar_categorias.Click
        Dim id_instrumento As Integer = 0
        id_instrumento = Convert.ToInt32(getIdInstrumentoGridView())
        If id_instrumento = 0 Then
            Mensaje("Seleccione un instrumento")
            Exit Sub
        Else
            hfIdInstrumento.Value = id_instrumento
            Response.Redirect("~/Tratados/frmAsignaCategoriasSAC.aspx?id_inst=" + hfIdInstrumento.Value)
        End If

    End Sub

    Protected Sub lkBtb_calcula_dai_Click(sender As Object, e As EventArgs) Handles lkBtb_calcula_dai.Click
        Dim id_instrumento As Integer = 0
        id_instrumento = Convert.ToInt32(getIdInstrumentoGridView())

        If id_instrumento = 0 Then
            Mensaje("Seleccione un instrumento.")
            Exit Sub
        Else
            LlenarFormulario_CalculaDAI(id_instrumento)
            lkBtt_Calcula_Dai_ModalPopupExtender.Show()
        End If
    End Sub

    Protected Sub btn_Calcular_Click(sender As Object, e As EventArgs) Handles btn_Calcular.Click
        'Verifica si se puede realizar calculo
        Dim id_instrumento As Integer = 0
        id_instrumento = Convert.ToInt32(getIdInstrumentoGridView())

        If id_instrumento = 0 Then
            Mensaje("Seleccione un instrumento.")
            Exit Sub
        Else
            hfIdInstrumento.Value = id_instrumento
            'Realiza calculo 
            If objCNTratados.CalcularDAI(hfIdInstrumento.Value) Then
                'Calculo se realizo con exito
                Mensaje("Calculo se realizo con éxito.")
            Else
                'No se realizo el calculo
                Mensaje("Calculo no se pudo realizar.")
            End If
        End If

    End Sub

    Protected Sub lkBtn_asignar_precision_Click(sender As Object, e As EventArgs) Handles lkBtn_asignar_precision.Click
        Dim id_instrumento As Integer = 0
        id_instrumento = Convert.ToInt32(getIdInstrumentoGridView())

        If id_instrumento = 0 Then
            Mensaje("Seleccione un instrumento.")
            Exit Sub
        Else
            hfIdInstrumento.Value = id_instrumento
            Response.Redirect("~/Tratados/frmAsignaPrecision.aspx?id_inst=" + hfIdInstrumento.Value)
        End If

    End Sub

#End Region

#Region "Funciones para capturar valores del formulario"
    Function getIdInstrumento() As Integer
        Dim objGeneral As New cnGeneral
        Dim IdInstrumento As Integer
        Dim nombretabla As String
        Dim llave_tabla As String

        nombretabla = " IC_Instrumentos "
        llave_tabla = " id_instrumento "

        IdInstrumento = Convert.ToInt32(objGeneral.ObtenerCorrelativoId(nombretabla, llave_tabla, True))

        Return IdInstrumento
    End Function

    Function getNombreInstrumento() As String
        Return txtNombreInstrumento.Text
    End Function

    Function getTipoInstrumento() As Integer
        Return Convert.ToInt32(ddlstTipoInstrumento.SelectedValue)
    End Function

    Function getSigla() As String
        Return txtSigla.Text
    End Function

    Function getSiglaAlterna() As String
        Return txtSiglaAlterna.Text
    End Function

    Function getTipoRelacionInstrumento() As Integer
        Dim tipo_relacion_instrumento As Integer
        tipo_relacion_instrumento = Convert.ToInt32(ddlstTipoInstrumento.SelectedValue)

        Return tipo_relacion_instrumento
    End Function

    Function getObservaciones() As String
        Return txtObservaciones.Text
    End Function

    Function getFechaFirma() As Date
        Dim fecha_firma As Date
        fecha_firma = txtFechaFirma.Text
        Return fecha_firma
    End Function

    Function getFechaRatifica() As Date
        Dim fecha_ratifica As Date
        fecha_ratifica = txtFechaRatifica.Text
        Return fecha_ratifica
    End Function

    Function getFechaVigencia() As Date
        Dim fecha_vigencia As Date
        fecha_vigencia = txtFechaVigencia.Text
        Return fecha_vigencia
    End Function

#End Region

#Region "Mis Funciones"

    'Procedimiento para llenar formulario para calculo de DAI
    Protected Sub LlenarFormulario_CalculaDAI(ByVal id_instrumento As Integer)

        Dim ds_CalculaDaiMant As New DataSet

        ds_CalculaDaiMant = objCNTratados.SelectInstrumentoCalculoDAI(id_instrumento)

        If ds_CalculaDaiMant.Tables(0).Rows.Count = 0 Then
            Mensaje("No existen datos de instrumentos para llenar opción.")
            Exit Sub
        Else
            txt_nombre.Text = ds_CalculaDaiMant.Tables(0).Rows(0)("nombre_instrumento").ToString
            txt_tipo.Text = ds_CalculaDaiMant.Tables(0).Rows(0)("descripcion").ToString
            txt_sigla.Text = ds_CalculaDaiMant.Tables(0).Rows(0)("sigla").ToString
            txt_inicio_vigencia.Text = ds_CalculaDaiMant.Tables(0).Rows(0)("fecha_vigencia").ToString

            If ds_CalculaDaiMant.Tables(1).Rows.Count = 0 Then
                Mensaje("No existen incisos asociados al instrumento para calcular.")
                Exit Sub
            Else
                txt_cantidad_incisos_calcular.Text = ds_CalculaDaiMant.Tables(1).Rows(0)("cantidad_incisos_calcular").ToString
            End If

            If ds_CalculaDaiMant.Tables(2).Rows.Count = 0 Then
                Mensaje("No se han realizado calculados de DAI para el instrumento seleccionado.")
            Else
                txt_cantidad_cortes_ejecutados.Text = ds_CalculaDaiMant.Tables(2).Rows(0)("cantidad_cortes_ejecutados").ToString
                txt_ultimo_corte_ejecutado.Text = ds_CalculaDaiMant.Tables(2).Rows(0)("ultimo_corte_ejecutado").ToString
            End If
        End If

    End Sub

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Limpiar Formulario Editar Instrumento
    Sub LimpiarEditarInstrumento()
        txtNombreInstrumento.Text = ""
        ddlstTipoInstrumento.ClearSelection()
        txtSigla.Text = ""
        txtSiglaAlterna.Text = ""
        ddlstTipoRelacion.ClearSelection()
        txtObservaciones.Text = ""
        txtFechaFirma.Text = ""
        txtFechaRatifica.Text = ""
        txtFechaVigencia.Text = ""
    End Sub

    'Procedimiento para llenar el GridView de instrumentos
    Protected Sub Llenar_gvInstrumentos()
        Dim tbl As New DataTable

        tbl = objCNTratados.SelectInstrumentos.Tables(0)

        With gvInstrumentos
            .DataSource = tbl
            .DataBind()
        End With

    End Sub

    'Procedimiento que llena el combo de tipo relacion instrumento
    Sub LlenarTipoRelacionInstrumento()

        With objCNGeneral.SelectTipoRelacionInstrumento
            ddlstTipoRelacion.DataTextField = .Tables(0).Columns("descripcion").ToString()
            ddlstTipoRelacion.DataValueField = .Tables(0).Columns("id_tipo_relacion_instrumento").ToString()
            ddlstTipoRelacion.DataSource = .Tables(0)
            ddlstTipoRelacion.DataBind()

        End With

    End Sub

    'Procedimiento que llena el combo de tipo instrumento
    Sub LlenarTipoInstrumento()

        With objCNGeneral.SelectTipoInstrumento
            ddlstTipoInstrumento.DataTextField = .Tables(0).Columns("descripcion").ToString()
            ddlstTipoInstrumento.DataValueField = .Tables(0).Columns("id_tipo_instrumento").ToString()
            ddlstTipoInstrumento.DataSource = .Tables(0)
            ddlstTipoInstrumento.DataBind()
        End With
    End Sub

    'Funcion que obtiene del grid el id del instrumento
    Function getIdInstrumentoGridView() As String
        Dim idInstrumento As Integer = Nothing

        For i As Integer = 0 To gvInstrumentos.Rows.Count - 1
            Dim rbutton As RadioButton = gvInstrumentos.Rows(i).FindControl("rb_sigla")
            If rbutton.Checked Then
                idInstrumento = gvInstrumentos.Rows(i).Cells(0).Text
            End If
        Next

        Return idInstrumento

    End Function

    'Procedimiento para llenar formulario con el id del instrumento seleccionado
    Sub LlenarInstrumentosMant(ByVal accion As String, ByVal id_instrumento As Integer)

        Dim datosInstrumentos As New DataTable
        datosInstrumentos = objCNTratados.SelectInstrumentoMant(id_instrumento)

        If datosInstrumentos.Rows.Count = 0 Then

            Mensaje("El instrumento no existe")
            Exit Sub
        Else
            If accion = "editar" Then

                ddlstTipoInstrumento.DataValueField = datosInstrumentos.Rows(0)("id_tipo_instrumento")
                ddlstTipoRelacion.DataValueField = datosInstrumentos.Rows(0)("id_tipo_relacion_instrumento")
                txtNombreInstrumento.Text = datosInstrumentos.Rows(0)("nombre_instrumento").ToString
                txtSigla.Text = datosInstrumentos.Rows(0)("sigla").ToString
                txtSiglaAlterna.Text = datosInstrumentos.Rows(0)("sigla_alternativa").ToString
                txtObservaciones.Text = datosInstrumentos.Rows(0)("observaciones").ToString

                txtFechaFirma.Text = datosInstrumentos.Rows(0)("fecha_firma")
                txtFechaRatifica.Text = datosInstrumentos.Rows(0)("fecha_ratificada")
                txtFechaVigencia.Text = datosInstrumentos.Rows(0)("fecha_vigencia")

            End If
        End If
    End Sub

    'Procedimiento para agregar nuevo instrumento
    Private Function GuardarInstrumento() As Boolean
        'Obtengo los valores de los controles
        ObjCEInstrumentoMant.id_instrumento = getIdInstrumento()
        ObjCEInstrumentoMant.id_tipo_instrumento = getTipoInstrumento()
        ObjCEInstrumentoMant.id_tipo_relacion_instrumento = getTipoRelacionInstrumento()
        ObjCEInstrumentoMant.nombre_instrumento = getNombreInstrumento()
        ObjCEInstrumentoMant.sigla = getSigla()
        ObjCEInstrumentoMant.sigla_alternativa = getSiglaAlterna()
        ObjCEInstrumentoMant.observaciones = getObservaciones()
        ObjCEInstrumentoMant.fecha_firma = getFechaFirma()
        ObjCEInstrumentoMant.fecha_ratificada = getFechaRatifica()
        ObjCEInstrumentoMant.fecha_vigencia = getFechaVigencia()
        ObjCEInstrumentoMant.estado = True

        'Envio los valores a la capa entidad con el objeto a la funcion guardar nuevo instrumento
        Return objCNTratados.InsertInstrumento(ObjCEInstrumentoMant)
    End Function

    'Procedimiento para editar instrumento
    Private Function EditarInstrumento(ByVal idInstrumento As Integer) As Boolean

        'Obtengo los valores de los controles
        ObjCEInstrumentoMant.id_instrumento = idInstrumento
        ObjCEInstrumentoMant.id_tipo_instrumento = getTipoInstrumento()
        ObjCEInstrumentoMant.id_tipo_relacion_instrumento = getTipoRelacionInstrumento()
        ObjCEInstrumentoMant.nombre_instrumento = getNombreInstrumento()
        ObjCEInstrumentoMant.sigla = getSigla()
        ObjCEInstrumentoMant.sigla_alternativa = getSiglaAlterna()
        ObjCEInstrumentoMant.observaciones = getObservaciones()
        ObjCEInstrumentoMant.fecha_firma = getFechaFirma()
        ObjCEInstrumentoMant.fecha_ratificada = getFechaRatifica()
        ObjCEInstrumentoMant.fecha_vigencia = getFechaVigencia()
        ObjCEInstrumentoMant.estado = True

        'Envio los valores a la capa entidad con el Objeto a la funcion actualizar instrumentos
        Return objCNTratados.UpdateInstrumento(ObjCEInstrumentoMant)

    End Function

#End Region

End Class