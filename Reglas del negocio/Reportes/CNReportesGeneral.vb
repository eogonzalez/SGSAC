Imports Capa_Datos
Public Class CNReportesGeneral
    Dim objCDReportesGeneral As New CDReportesGeneral

    'Funcion que obtiene listado de instrumentos comerciales
    Public Function SelectInstrumentoComercial() As DataTable
        Return objCDReportesGeneral.SelectInstrumentoComercial()
    End Function

    'Funcion que obtiene listado de categorias por instrumento comercial
    Public Function SelectCategoriasList(ByVal IdInstrumento As Integer) As DataTable
        Return objCDReportesGeneral.SelectCategoriasList(IdInstrumento)
    End Function

End Class
