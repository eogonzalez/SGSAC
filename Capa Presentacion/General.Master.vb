Imports Reglas_del_negocio

Public Class General
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtFecha.Text = DateTime.Now.ToLongDateString()

        If Not IsPostBack Then
            LlenarMenu()
        End If

    End Sub

    Private Sub LlenarMenu()
        Dim CapaNegocios As New cnGeneral
        Dim tbl As New DataTable
        tbl = CapaNegocios.MenuPrincipal.Tables("Menu")

        'Recorer la tabla para llenar los items del Menu principal
        For Each enc As DataRow In tbl.Rows
            Dim Item As New MenuItem
            Dim lkbutton As New HyperLink
            Item.Value = enc("id_opcion")
            Item.Text = enc("nombre")
            Item.ToolTip = enc("descripcion")
            Item.NavigateUrl = enc("url")

            MenuPrincipal.Items.Add(Item)

        Next
    End Sub

End Class