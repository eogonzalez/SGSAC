Imports Capa_Entidad
Imports Capa_Datos
Public Class CNAsignaCategoriasSAC
    Dim objCDAsignaCategoria As New AsignaCategoriasSAC
    'Funcion para Insertar las Asociaciones de categoria
    Public Function InsertAsignaCategoria(ByVal objCEAsigna As CEIncisoAsociaCategoria) As Boolean
        Return objCDAsignaCategoria.InsertAsignaCategoria(objCEAsigna)
    End Function

    'Funcion para obtener los datos para los codigos seleccionados
    Public Function SelectDatosCodigoInciso(ByVal id_instrumento As Integer, ByVal str_codigo As String) As DataSet
        Return objCDAsignaCategoria.SelectDatosCodigoInciso(id_instrumento, str_codigo)
    End Function

    'Funcion para obtener los datos del Mantenimiento de Asignacion de Categoria
    Public Function SelectDatosAsignaCategoriaMant(ByVal id_instrumento As Integer) As DataSet
        Return objCDAsignaCategoria.SelectDatosAsignaCategoriaMant(id_instrumento)
    End Function

End Class
