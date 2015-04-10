Imports Reglas_del_negocio

Public Class frmTratadosyAcuerdos
    Inherits System.Web.UI.Page
    Dim objCapaNegocio As New CNInstrumentosComerciales

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gvInstrumentos()
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

    Protected Sub ImageButton5_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton5.Click
        Response.Redirect("~/frmInstrumentosMant.aspx")
    End Sub
End Class