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
End Class