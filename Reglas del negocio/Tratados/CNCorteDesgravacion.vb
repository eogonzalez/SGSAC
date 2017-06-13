Imports Capa_Datos
Imports Capa_Entidad
Public Class CNCorteDesgravacion
    Dim objCDCorte As New CorteDesgravacion

    Public Function UpdateTramoCategoriaMant(ByVal ObjCETramo As CECorteDesgravacion) As Boolean
        Return objCDCorte.UpdateTramoCategoriaMant(ObjCETramo)
    End Function

    'Funcion para seleccionar el tramo segun el id_instrumento, id_catetoria y id_tramo
    Public Function SelectTramoCategoriaMant(ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal id_tramo As Integer) As DataTable
        Return objCDCorte.SelectTramoCategoriaMant(id_instrumento, id_categoria, id_tramo)
    End Function

    'Funciones que selecciona los tramos de la categoria
    Public Function SelectTramoCategoria(ByVal id_instrumento As Integer, ByVal id_categoria As Integer) As DataTable
        Return objCDCorte.SelectTramoCategoria(id_instrumento, id_categoria)
    End Function

End Class
