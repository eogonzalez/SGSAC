Imports Capa_Entidad
Imports Capa_Datos

Public Class CNTipoPeriodo
    Dim objCDTipoPeriodo As New TipoPeriodo
    'Funcion para seleccionar listado del combo tipo de periodo
    Public Function SelectTipoPeriodo() As DataSet
        Return objCDTipoPeriodo.SelectTipoPeriodo()
    End Function

    'Metodo para insertar tipo de peiodo
    Public Function InsertTipoPeriodo(ByVal objTipoPeriodo As CETipoPeriodo) As Boolean
        Return objCDTipoPeriodo.InsertTipoPeriodo(objTipoPeriodo)
    End Function

    'Funcion para seleccionar tipo periodo segun el id_tipoPeriodo
    Public Function SelectTipoPeriodoMant(ByVal id_tipoPeriodo As Integer) As DataTable
        Return objCDTipoPeriodo.SelectTipoPeriodoMant(id_tipoPeriodo)
    End Function

    'Metodo para actualizar tipo de periodo
    Public Function UpdateTipoPeriodo(ByVal objTipoPeriodo As CETipoPeriodo) As Boolean
        Return objCDTipoPeriodo.UpdateTipoPeriodo(objTipoPeriodo)
    End Function

End Class
