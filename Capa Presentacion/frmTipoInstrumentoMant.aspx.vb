Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmTipoInstrumentoMant
    Inherits System.Web.UI.Page

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    LlenarCodigoTipoInstrumento()


    'End Sub

    'Sub LlenarCodigoTipoInstrumento()
    '    Dim cnObj As New cnGeneral
    '    Dim nombreTabla As String
    '    Dim llaveTabla As String

    '    nombreTabla = "IC_Tipo_Instrumento "
    '    llaveTabla = "id_tipo_instrumento"

    '    txtIdTipoInstrumento.Text = cnObj.ObtenerCorrelativoId(nombreTabla, llaveTabla).ToString
    'End Sub

    'Function getIdTipoInstrumento() As Integer
    '    Dim idTipoInstrumento As Integer
    '    idTipoInstrumento = Convert.ToInt32(txtIdTipoInstrumento.Text)
    '    Return idTipoInstrumento
    'End Function

    'Function getTipoInstrumento() As String
    '    Return txtDescripcion.Text
    'End Function

    'Function getObservaciones() As String
    '    Return txtObservaciones.Text
    'End Function

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    NuevoTipoInstrumento()
    'End Sub

    'Sub NuevoTipoInstrumento()
    '    Dim CE_Objeto As New CETipoInstrumento
    '    Dim CNTipoInstrumento As New CNInstrumentosComerciales

    '    CE_Objeto.id_tipo_instrumento = getIdTipoInstrumento()
    '    CE_Objeto.descripcion = getTipoInstrumento()
    '    CE_Objeto.observaciones = getObservaciones()

    '    CNTipoInstrumento.InsertTipoInstrumento(CE_Objeto)
    'End Sub
End Class