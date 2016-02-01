Imports Reglas_del_negocio

Public Class General
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CType(Session("UsuarioID"), Integer) >= 1 Then
            txtFecha.Text = DateTime.Now.ToLongDateString()

            If Not IsPostBack Then
                LlenarMenu()
            End If
        Else
            Response.Redirect("~/Login.aspx")
        End If

        

    End Sub

    Public Sub LlenarMenu()
        Dim CapaNegocios As New cnGeneral
        Dim tbl As New DataTable
        tbl = CapaNegocios.MenuPrincipal.Tables(0)

        'Recorer la tabla para llenar los items del Menu principal
        For Each enc As DataRow In tbl.Rows
            Dim Item As New MenuItem

            If IsDBNull(enc("id_padre")) Then
                Item.Value = enc("id_opcion")
                Item.Text = enc("nombre")
                Item.ToolTip = enc("descripcion")
                Item.NavigateUrl = enc("url")

                MenuPrincipal.Items.Add(Item)

                LlenarSubMenu(Item, tbl)
            End If

        Next
    End Sub

    Private Sub LlenarSubMenu(ByVal Menus As MenuItem, ByVal Datos As Data.DataTable)

        For Each enc As DataRow In Datos.Rows
            Dim Item As New MenuItem
            If Not IsDBNull(enc("id_padre")) Then
                If Menus.Value = enc("id_padre") Then
                    Item.Value = enc("id_opcion")
                    Item.Text = enc("nombre")
                    Item.ToolTip = enc("descripcion")
                    Item.NavigateUrl = enc("url")

                    Menus.ChildItems.Add(Item)

                    LlenarSubMenu(Item, Datos)
                End If
            End If
        Next
    End Sub

End Class