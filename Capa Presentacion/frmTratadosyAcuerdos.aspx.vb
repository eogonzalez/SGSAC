Imports Reglas_del_negocio
Imports Capa_Entidad

Public Class frmTratadosyAcuerdos
    Inherits System.Web.UI.Page
    Dim objCapaNegocio As New CNInstrumentosComerciales
    Dim accion As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvInstrumentos()

            LlenarTipoInstrumento()
            LlenarTipoRelacionInstrumento()
        End If
    End Sub

    Protected Sub Llenar_gvInstrumentos()
        Dim tbl As New DataTable

        tbl = objCapaNegocio.LlenarInstrumentos.Tables(0)

        With gvInstrumentos
            .DataSource = tbl
            .DataBind()
        End With

    End Sub

    Protected Sub lkBtt_editar_Click(sender As Object, e As EventArgs) Handles lkBtt_editar.Click
        accion = "editar"

        Dim fila_id As GridViewRow = gvInstrumentos.SelectedRow
        'Dim id_instrumento As String = fila_id.Cells(1).Text

        'If String.IsNullOrEmpty(id_instrumento) Then
        '    MsgBox("Seleccione un registro")
        'Else
        '    Response.Redirect("frmInstrumentosMant.aspx?accion=" + accion + "&id_instrumento=" + id_instrumento)
        'End If



    End Sub


    Sub LlenarTipoInstrumento()
        Dim objCNInstrumentos As New CNInstrumentosComerciales

        With objCNInstrumentos.SelectTipoInstrumento
            ddlstTipoInstrumento.DataTextField = .Tables(0).Columns("descripcion").ToString()
            ddlstTipoInstrumento.DataValueField = .Tables(0).Columns("id_tipo_instrumento").ToString()
            ddlstTipoInstrumento.DataSource = .Tables(0)
            ddlstTipoInstrumento.DataBind()
        End With
    End Sub

    Sub LlenarTipoRelacionInstrumento()
        Dim objCNInstrumentos As New CNInstrumentosComerciales
        With objCNInstrumentos.SelectTipoRelacionInstrumento
            ddlstTipoRelacion.DataTextField = .Tables(0).Columns("descripcion").ToString()
            ddlstTipoRelacion.DataValueField = .Tables(0).Columns("id_tipo_relacion_instrumento").ToString()
            ddlstTipoRelacion.DataSource = .Tables(0)
            ddlstTipoRelacion.DataBind()

        End With

    End Sub

    'Funciones para capturar los valores
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
        fecha_firma = Convert.ToDateTime(txtFechaFirma.Text)
        Return fecha_firma
    End Function

    Function getFechaRatifica() As Date
        Dim fecha_ratifica As Date
        fecha_ratifica = Convert.ToDateTime(txtFechaRatifica.Text)
        Return fecha_ratifica
    End Function

    Function getFechaVigencia() As Date
        Dim fecha_vigencia As Date
        fecha_vigencia = Convert.ToDateTime(txtFechaVigencia.Text)
        Return fecha_vigencia
    End Function

    Sub NuevoInstrumento()
        Dim objeto As New CEInstrumentosMant
        Dim cnInstrumentos As New CNInstrumentosComerciales
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
        cnInstrumentos.InsertInstrumento(objeto)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        NuevoInstrumento()


    End Sub

    Function getIdInstrumentoGridView() As Integer
        Dim id_instrumento As Integer

        'Dim name = gvInstrumentos.DataKeys(1).Values("rb_sigla").ToString().Trim()

        For Each fila As GridViewRow In gvInstrumentos.Rows
            Dim check As RadioButton = fila.FindControl("rb_sigla")
            If check.Checked Then



                Dim id = TryCast(gvInstrumentos.SelectedRow.DataItem, TextBox).Text

                id = Convert.ToInt32(id)
                'id_instrumento = Convert.ToInt32(fila.Cells(0).Text)

            Else

            End If
        Next

        Return id_instrumento

    End Function

    'Protected Sub gvInstrumentos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvInstrumentos.SelectedIndexChanged
    '    'Dim row As GridViewRow = gvInstrumentos.SelectedRow

    '    'MsgBox("Selecciono IndexChanged " & row.Cells(1).Text & ".")
    '    'MsgBox("Selecciono IndexChanged")

    '    'Dim id_instrumento = getIdInstrumentoGridView()

    'End Sub

    'Protected Sub CustomersGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs) Handles gvInstrumentos.SelectedIndexChanging

    '    ' Get the currently selected row. Because the SelectedIndexChanging event
    '    ' occurs before the select operation in the GridView control, the
    '    ' SelectedRow property cannot be used. Instead, use the Rows collection
    '    ' and the NewSelectedIndex property of the e argument passed to this 
    '    ' event handler.
    '    Dim row As GridViewRow = gvInstrumentos.Rows(e.NewSelectedIndex)

    '    ' You can cancel the select operation by using the Cancel
    '    ' property. For this example, if the user selects a customer with 
    '    ' the ID "ANATR", the select operation is canceled and an error message
    '    ' is displayed.
    '    If row.Cells(1).Text = "ANATR" Then
    '        e.Cancel = True
    '        MsgBox("You cannot select " + row.Cells(1).Text & ".")
    '    End If

    'End Sub


End Class