Imports Capa_Entidad
Imports Capa_Datos
Public Class CNCategoriasDesgravacion
    Dim objCDCategorias As New CategoriasDesgravacion

    'Funcion para verificar si categoria ya esta asociada
    Public Function VerificaCategoriaAsocia(ByVal objCategoriaDesgrava As CECategoriaDesgravacion) As Boolean
        Return objCDCategorias.VerificaCategoriaAsocia(objCategoriaDesgrava)
    End Function

    'Funcion para eliminar Categoria
    Public Function DeleteCategoria(ByVal objCategoriaDesgrava As CECategoriaDesgravacion) As Boolean
        Return objCDCategorias.DeleteCategoria(objCategoriaDesgrava)
    End Function

    'Funcion para verificar si las categorias han sido aprobadas
    Public Function VerificaCategoriasEstado(ByVal id_instrumento As Integer) As Boolean
        Return objCDCategorias.VerificaCategoriasEstado(id_instrumento)
    End Function

    'Funcion para Aprobar Categoria
    Public Function ApruebaCategoria(ByVal id_instrumento As Integer) As Boolean
        Return objCDCategorias.ApruebaCategoria(id_instrumento)
    End Function

    'Funcion para obtener el nombre del instrumento y cantidad de categorias
    Public Function SelectInstrumentoCategoria(ByVal id_instrumento As Integer) As DataTable
        Return objCDCategorias.SelectInstrumentoCategoria(id_instrumento)
    End Function

    'Funcion para actualizar categorias
    Public Function UpdateCategoriaDesgrava(ByVal objCeCategoria As CECategoriaDesgravacion) As Boolean
        Return objCDCategorias.UpdateCategoriaDesgrava(objCeCategoria)
    End Function

    'Funcion para seleccionar categoria segun id_categoria
    Public Function SelectCategoriaDesgravaMant(ByVal id_categoria As Integer, ByVal id_instrumento As Integer) As DataTable
        Return objCDCategorias.SelectCategoriaDesgravaMant(id_categoria, id_instrumento)
    End Function

    'Funcion para insertar nueva categoria y tramos 
    Public Function InsertCategoriaDesgrava(ByVal objCECategoria As CECategoriaDesgravacion) As Boolean
        Return objCDCategorias.InsertCategoriaDesgrava(objCECategoria)
    End Function

    'Funcion que selecciona tipo desgravacion
    Public Function SelectTipoDesgravacion() As DataSet
        Return objCDCategorias.SelectTipoDesgravacion()
    End Function

    'Funcion para seleccionar categorias segun el id_instrumento para llenar gvCategorias
    Public Function SelectCategoriasDesgrava(ByVal id_instrumento As Integer) As DataTable
        Return objCDCategorias.SelectCategoriasDesgrava(id_instrumento)
    End Function

End Class
