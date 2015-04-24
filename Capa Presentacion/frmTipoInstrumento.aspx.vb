Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmTipoInstrumento
    Inherits System.Web.UI.Page
    Dim objCNTipoInstrumento As New CNInstrumentosComerciales
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Llenar_gvTipoInstrumento()

    End Sub

    Protected Sub Llenar_gvTipoInstrumento()
        Dim tbl As DataTable
        tbl = objCNTipoInstrumento.SelectTipoInstrumento.Tables(0)

        With gvTipoInstrumento
            .DataSource = tbl
            .DataBind()
        End With
    End Sub

    Protected Sub Page_LoadMant(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlNuevoTipoInstrumento.Load
        LlenarCorrelativoTipoInstrumento()

    End Sub

    Sub LlenarCorrelativoTipoInstrumento()
        Dim CNInstrumento As New cnGeneral
        Dim nombreTabla As String
        Dim llaveTable As String

        nombreTabla = "IC_Tipo_Instrumento"
        llaveTable = " id_tipo_instrumento "

        'txtIdTipoInstrumento.Text = CNInstrumento.ObtenerCorrelativoId(nombreTabla, llaveTable).ToString
    End Sub

    Function getIdCorrelativoTipoInstrumento() As Integer
        Return Convert.ToInt32(txtIdTipoInstrumento.Text)
    End Function

    Function getDescripcion() As String
        Return txtDescripcion.Text
    End Function

    Function getObservaciones() As String
        Return txtObservaciones.Text
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        NuevoTipoInstrumento()


    End Sub

    Sub NuevoTipoInstrumento()
        Dim CEObj As New CETipoInstrumento
        Dim CNTipoInstrumentoIns As New CNInstrumentosComerciales

        CEObj.id_tipo_instrumento = getIdCorrelativoTipoInstrumento()
        CEObj.descripcion = getDescripcion()
        CEObj.observaciones = getObservaciones()

        CNTipoInstrumentoIns.InsertTipoInstrumento(CEObj)

        gvTipoInstrumento.DataSource = Nothing
        gvTipoInstrumento.DataBind()

        'Llenar_gvTipoInstrumento()

    End Sub
End Class