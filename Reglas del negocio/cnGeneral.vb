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

    Public Function ObtenerCorrelativoId(ByVal nombretabla As String, ByVal llave_tabla As String, Optional ByVal TieneEstado As Boolean = False, Optional ByVal llave_filtro As String = Nothing, Optional ByVal valor_llave_filtro As Integer = 0) As Integer
        Return objMenu.ObtenerCorrelativoId(nombretabla, llave_tabla, TieneEstado, llave_filtro, valor_llave_filtro)
    End Function
End Class
