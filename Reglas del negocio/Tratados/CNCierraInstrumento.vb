Imports Capa_Datos
Public Class CNCierraInstrumento
    Dim objCDCierra As New CierraInstrumento

    Public Function SelectResumenInstrumento(ByVal id_instrumento As Integer) As DataTable
        Return objCDCierra.SelectResumenInstrumento(id_instrumento)
    End Function

End Class
