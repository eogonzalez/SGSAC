Public Class frmCorrelacionSAC
    Inherits System.Web.UI.Page

    Shared _tabla_incisos As DataTable

    Public Shared Property tabla_incisos As DataTable
        Get
            Return _tabla_incisos
        End Get
        Set(value As DataTable)
            _tabla_incisos = tabla_incisos
        End Set
    End Property

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hfIdVersionSAC.Value = Request.QueryString("id_vs").ToString

            With gvAsignarCategorias
                .DataSource = tabla_incisos
                .DataBind()
            End With

        End If
        
    End Sub

    Protected Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        Response.Redirect("~/frmEnmiendas.aspx")
    End Sub

#End Region

End Class