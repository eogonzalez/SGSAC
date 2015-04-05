Imports System.Web.HttpResponse

Public Class Inicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim userid As String

        'userid = Session["UsuarioID"]

        'If (!IsPostBack) Then

        '    If (userid = "" Or userid = IsNull) Then
        Response.Redirect("../Login.aspx")

        '    End If

        'End If

    End Sub
End Class