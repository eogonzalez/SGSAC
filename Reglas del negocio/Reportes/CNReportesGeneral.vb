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

    'Funcion que obtiene encabezados del sac e incisos segun filtros
    Public Function SelectIncisosAsocia(ByVal id_instrumento As Integer, ByVal str_codigo As String, ByVal id_categoria As Integer, ByVal all_catego As Boolean, ByVal all_incisos As Boolean) As DataSet
        Return objCDReportesGeneral.SelectIncisosAsocia(id_instrumento, str_codigo, id_categoria, all_catego, all_incisos)
    End Function

End Class
