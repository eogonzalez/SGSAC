Imports Capa_Datos
Public Class cnGeneral
    Dim objMenu As New General

    Public Function MenuPrincipal() As DataSet
        'Llenar el Menu principal
        Return objMenu.MunuPrincipal
    End Function

    Public Function EncodePassword(ByVal originalPassword As String) As String
        Return objMenu.EncodePassword(originalPassword)
    End Function

    Public Function ObtenerCorrelativoId(ByVal nombretabla As String, ByVal llave_tabla As String, Optional ByVal TieneEstado As Boolean = False) As Integer
        Return objMenu.ObtenerCorrelativoId(nombretabla, llave_tabla, TieneEstado)
    End Function
End Class
