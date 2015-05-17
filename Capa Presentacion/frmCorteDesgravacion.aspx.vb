Public Class frmCorteDesgravacion
    Inherits System.Web.UI.Page

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'hfIdInstrumento.Value = Request.QueryString("id_inst").ToString
            'hfIdCategoria.Value = Request.QueryString("id_ct").ToString
            'hfIdInstrumento.Value = 2
            'hfIdCategoria.Value = 1

        End If
    End Sub


#End Region
    

End Class