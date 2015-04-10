Imports Capa_Datos
Public Class CNInstrumentosComerciales
    Dim objCapaDatos As New CDInstrumentosComerciales
    Public Function LlenarInstrumentos() As DataSet
        Return objCapaDatos.ListadoInstrumentos()
    End Function
End Class
