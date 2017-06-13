Imports Capa_Entidad
Imports Capa_Datos
Public Class CNTipoRelacionInstrumento
    Dim objCDTipoRelacion As New TipoRelacionInstrumento
    'Metodo para insertar tipo relacion de instrumento
    Public Function InsertTipoRelacionInstrumento(ByVal objTipoRelIns As CETipoRelacionInstrumento) As Boolean
        Return objCDTipoRelacion.InsertTipoRelacionInstrumento(objTipoRelIns)
    End Function

    'Funcion para obtener el tipo de relacion segun el id_tipoRelacionInstrumento
    Public Function SelectTipoRelacionInstrumentoMant(ByVal id_tipoRelacionInstrumento As Integer) As DataTable
        Return objCDTipoRelacion.SelectTipoRelacionInstrumentoMant(id_tipoRelacionInstrumento)
    End Function

    'Metodo para actualizar tipo relacion de instrumento
    Public Function UpdateTipoRelacionInstrumento(ByRef objTipoRelIns As CETipoRelacionInstrumento) As Boolean
        Return objCDTipoRelacion.UpdateTipoRelacionInstrumento(objTipoRelIns)
    End Function

End Class
