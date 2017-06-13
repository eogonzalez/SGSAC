Imports Capa_Entidad
Imports Capa_Datos
Public Class CNTipoInstrumentos
    Dim objCDTipoInstrumentos As New TipoInstrumentos

    'Metodo para insertar tipo de instrumento
    Public Function InsertTipoInstrumento(ByVal objTipoIns As CETipoInstrumento) As Boolean
        Return objCDTipoInstrumentos.InsertTipoInstrumento(objTipoIns)
    End Function

    'Funcion para seleccionar tipo instrumento segun el id_tipoInstrumento
    Public Function SelectTipoInstrumentoMant(ByVal id_tipoInstrumento As Integer) As DataTable
        Return objCDTipoInstrumentos.SelectTipoInstrumentoMant(id_tipoInstrumento)
    End Function

    'Metodo para actualizar tipo de instrumento
    Public Function UpdateTipoInstrumento(ByVal objTipoIns As CETipoInstrumento) As Boolean
        Return objCDTipoInstrumentos.UpdateTipoInstrumento(objTipoIns)
    End Function

End Class
