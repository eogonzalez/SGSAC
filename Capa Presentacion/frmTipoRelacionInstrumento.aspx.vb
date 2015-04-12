Imports Reglas_del_negocio
Public Class frmTipoRelacionInstrumento
    Inherits System.Web.UI.Page
    Dim objCNTipoRelacionIns As New CNInstrumentosComerciales

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Llenar_gvTipoRelacionInstrumento()

    End Sub


    Protected Sub Llenar_gvTipoRelacionInstrumento()
        Dim tbl As DataTable
        tbl = objCNTipoRelacionIns.SelectTipoRelacionInstrumento.Tables(0)

        With gvTipoRelacionInstrumento
            .DataSource = tbl
            .DataBind()
        End With
    End Sub

    Protected Sub ImageButton5_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton5.Click
        Response.Redirect("~/frmTipoRelacionInstrumentoMant.aspx")
    End Sub
End Class