Imports Reglas_del_negocio

Public Class frmTratadosyAcuerdos
    Inherits System.Web.UI.Page
    Dim objCapaNegocio As New CNInstrumentosComerciales
    Dim accion As String

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

    Protected Sub lkBtt_nuevo_Click(sender As Object, e As EventArgs) Handles lkBtt_nuevo.Click
        accion = "nuevo"

        Response.Redirect("frmInstrumentosMant.aspx?accion" + accion)

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
End Class