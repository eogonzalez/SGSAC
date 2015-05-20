Imports Reglas_del_negocio
Imports Capa_Entidad

Public Class frmTratadosyAcuerdos
    Inherits System.Web.UI.Page
    Dim objCapaNegocio As New CNInstrumentosComerciales

#Region "Funciones del Sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvInstrumentos()
            LlenarTipoInstrumento()
            LlenarTipoRelacionInstrumento()
            Me.btn_Guardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & Me.GetPostBackEventReference(Me.btn_Guardar))
        End If
    End Sub

    Protected Sub lkBtt_editar_Click(sender As Object, e As EventArgs) Handles lkBtt_editar.Click
        
        Dim id_instrumento As Integer
        id_instrumento = Convert.ToInt32(getIdInstrumentoGridView())
        If id_instrumento = 0 Then
            MsgBox("Seleccione un instrumeto")
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
            Response.Redirect("~/frmCategoriasDesgravacion.aspx?id_inst=" + hfIdInstrumento.Value)
        End If

    End Sub

    Protected Sub lkBtt_paises_Click(sender As Object, e As EventArgs) Handles lkBtt_paises.Click

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

        tbl = objCapaNegocio.SelectInstrumentos.Tables(0)

        With gvInstrumentos
            .DataSource = tbl
            .DataBind()
        End With

    End Sub

    'Procedimiento que llena el combo de tipo relacion instrumento
    Sub LlenarTipoRelacionInstrumento()
        Dim objCNInstrumentos As New CNInstrumentosComerciales
        With objCNInstrumentos.SelectTipoRelacionInstrumento
            ddlstTipoRelacion.DataTextField = .Tables(0).Columns("descripcion").ToString()
            ddlstTipoRelacion.DataValueField = .Tables(0).Columns("id_tipo_relacion_instrumento").ToString()
            ddlstTipoRelacion.DataSource = .Tables(0)
            ddlstTipoRelacion.DataBind()

        End With

    End Sub

    'Procedimiento que llena el combo de tipo instrumento
    Sub LlenarTipoInstrumento()
        Dim objCNInstrumentos As New CNInstrumentosComerciales

        With objCNInstrumentos.SelectTipoInstrumento
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


        Dim ObjCEInstrumentoMant As New CEInstrumentosMant
        Dim ObjCNInstrumentoMant As New CNInstrumentosComerciales

        Dim datosInstrumentos As New DataTable
        datosInstrumentos = ObjCNInstrumentoMant.SelectInstrumentoMant(id_instrumento)

        If datosInstrumentos.Rows.Count = 0 Then
            MsgBox("El usuario no existe")
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
        'Declaro las varialbes de la capa de datos y entidad
        Dim objeto As New CEInstrumentosMant
        Dim cnInstrumentos As New CNInstrumentosComerciales

        'Obtengo los valores de los controles
        objeto.id_instrumento = getIdInstrumento()
        objeto.id_tipo_instrumento = getTipoInstrumento()
        objeto.id_tipo_relacion_instrumento = getTipoRelacionInstrumento()
        objeto.nombre_instrumento = getNombreInstrumento()
        objeto.sigla = getSigla()
        objeto.sigla_alternativa = getSiglaAlterna()
        objeto.observaciones = getObservaciones()
        objeto.fecha_firma = getFechaFirma()
        objeto.fecha_ratificada = getFechaRatifica()
        objeto.fecha_vigencia = getFechaVigencia()
        objeto.estado = True

        'Envio los valores a la capa entidad con el objeto a la funcion guardar nuevo instrumento
        Return cnInstrumentos.InsertInstrumento(objeto)
    End Function

    'Procedimiento para editar instrumento
    Private Function EditarInstrumento(ByVal idInstrumento As Integer) As Boolean
        'Declaro las variables de la capa de datos y entidad
        Dim CEObjeto As New CEInstrumentosMant
        Dim CNInstrumentos As New CNInstrumentosComerciales

        'Obtengo los valores de los controles
        CEObjeto.id_instrumento = idInstrumento
        CEObjeto.id_tipo_instrumento = getTipoInstrumento()
        CEObjeto.id_tipo_relacion_instrumento = getTipoRelacionInstrumento()
        CEObjeto.nombre_instrumento = getNombreInstrumento()
        CEObjeto.sigla = getSigla()
        CEObjeto.sigla_alternativa = getSiglaAlterna()
        CEObjeto.observaciones = getObservaciones()
        CEObjeto.fecha_firma = getFechaFirma()
        CEObjeto.fecha_ratificada = getFechaRatifica()
        CEObjeto.fecha_vigencia = getFechaVigencia()
        CEObjeto.estado = True

        'Envio los valores a la capa entidad con el Objeto a la funcion actualizar instrumentos
        Return CNInstrumentos.UpdateInstrumento(CEObjeto)

    End Function

#End Region



End Class