Imports Capa_Datos
Public Class cnGeneral
    Dim objMenu As New General

    Public Function MenuPrincipal() As DataSet
        'Llenar el Menu principal
        Return objMenu.MunuPrincipal
    End Function
End Class
