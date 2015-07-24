Imports Capa_Datos
Public Class CNReportes
    Dim objReportes As New Reportes

#Region "Reporte Consulta SAC"
    Public Function InstrumentoComercial() As DataSet
        Return objReportes.InstrumentoComercial()
    End Function

    Public Function Categorias(ByVal IdInstrumento As Integer) As DataSet
        Return objReportes.Categorias(IdInstrumento)
    End Function
#End Region
End Class
