Imports Reglas_del_negocio

Public Class Inicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userid As String

        userid = Session("UsuarioID")

        'LlenarMenu()

    End Sub

    'Private Sub LlenarMenu()
    '    Dim CapaNegocios As New cnGeneral
    '    Dim tbl As New DataTable
    '    tbl = CapaNegocios.MenuPrincipal.Tables("Menu")

    '    'Recorer la tabla para llenar los items del Menu principal
    '    For Each enc As DataRow In tbl.Rows
    '        Dim Item As New MenuItem

    '        Item.Value = enc("id_opcion")
    '        Item.Text = enc("nombre")
    '        Item.ToolTip = enc("descripcion")
    '        Item.NavigateUrl = enc("url")

    '        MenuPrincipal.Items.Add(Item)
    '    Next
    'End Sub
End Class