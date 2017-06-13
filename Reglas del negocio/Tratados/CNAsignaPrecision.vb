Imports Capa_Entidad
Imports Capa_Datos
Public Class CNAsignaPrecision
    Dim objCDAsignaPrecision As New AsignaPrecision
    Dim objCDAsignaCategoria As New AsignaCategoriasSAC

    'Funcion que elimina la precision asociada al inciso
    Public Function DeletePrecision(ByVal objCEIncisoAsocia As CEIncisoAsociaCategoria) As Boolean
        Return objCDAsignaPrecision.DeletePrecision(objCEIncisoAsocia)
    End Function

    'Funcion que almacena precision 
    Public Function InsertPrecision(ByVal objCEPrecision As CEIncisoAsociaCategoria) As Boolean
        Return objCDAsignaPrecision.InsertPrecision(objCEPrecision)
    End Function

    'Funcion que devuelve datos del inciso precision
    Public Function SelectIncisoPrecision(ByVal id_instrumento As Integer, ByVal codigo_inciso As String, ByVal inciso_presicion As String) As DataTable
        Return objCDAsignaPrecision.SelectIncisoPrecision(id_instrumento, codigo_inciso, inciso_presicion)
    End Function

    'Funcion para obtener los datos del Mantenimiento de Asigna Precision
    Public Function SelectDatosAsignaPrecisionMant(ByVal id_instrumento As Integer) As DataSet
        'Se reutiliza ws del mantenimiento Asignacion de categoria 
        Return objCDAsignaCategoria.SelectDatosAsignaCategoriaMant(id_instrumento)
    End Function

    'Funcion para obtener los datos para el mantenimiento de precision
    Public Function SelectDatosEncabezadoPrecisionMant(ByVal id_instrumento As Integer) As DataSet
        'Se reutiliza ws del mantenimiento de asignacion de categoria
        Return objCDAsignaCategoria.SelectDatosAsignaCategoriaMant(id_instrumento)
    End Function

End Class
