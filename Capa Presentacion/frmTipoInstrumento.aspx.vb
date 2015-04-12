Imports Reglas_del_negocio
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

    Protected Sub ImageButton5_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton5.Click
        Response.Redirect("~/frmTipoInstrumentoMant.aspx")
    End Sub
End Class