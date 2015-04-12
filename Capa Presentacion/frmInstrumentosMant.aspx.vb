Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmInstrumentosMant
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LlenarTipoInstrumento()
        LlenarTipoRelacionInstrumento()
        'Dim fecha_actual As String
        'fecha_actual = Convert.ToString(DateTime.Now)
        'txtFechaFirma.Text(fecha_actual)
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
        llave_tabla = " id_instrumentos "

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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        NuevoInstrumento()


    End Sub

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

End Class