Imports Reglas_del_negocio
Imports Capa_Entidad

Public Class frmTratadosyAcuerdos
    Inherits System.Web.UI.Page
    Dim objCapaNegocio As New CNInstrumentosComerciales
    Dim accion As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvInstrumentos()
            LlenarCodigoTipoInstrumento()
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

    Sub LlenarCodigoTipoInstrumento()
        Dim cnObj As New cnGeneral
        Dim nombreTabla As String
        Dim llaveTabla As String

        nombreTabla = "IC_Tipo_Instrumento "
        llaveTabla = "id_tipo_instrumento"

        txtIdTipoInstrumento.Text = cnObj.ObtenerCorrelativoId(nombreTabla, llaveTabla).ToString
    End Sub

    Function getIdTipoInstrumento() As Integer
        Dim idTipoInstrumento As Integer
        idTipoInstrumento = Convert.ToInt32(txtIdTipoInstrumento.Text)
        Return idTipoInstrumento
    End Function

    Function getTipoInstrumento() As String
        Return txtDescripcion.Text
    End Function

    Function getObservaciones() As String
        Return txtObservaciones.Text
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        NuevoTipoInstrumento()
    End Sub

    Sub NuevoTipoInstrumento()
        Dim CE_Objeto As New CETipoInstrumento
        Dim CNTipoInstrumento As New CNInstrumentosComerciales

        CE_Objeto.id_tipo_instrumento = getIdTipoInstrumento()
        CE_Objeto.descripcion = getTipoInstrumento()
        CE_Objeto.observaciones = getObservaciones()

        CNTipoInstrumento.InsertTipoInstrumento(CE_Objeto)
    End Sub
End Class