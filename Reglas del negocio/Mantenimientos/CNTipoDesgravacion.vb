Imports Capa_Datos
Imports Capa_Entidad
Public Class CNTipoDesgravacion
    Dim objCDTipoDesgravacion As New TipoDesgravacion
    'Metodo para insertar tipo de desgravacion
    Public Function InsertTipoDesgravacion(ByVal objTipoDesgravacion As CeTipoDesgravacion) As Boolean
        Return objCDTipoDesgravacion.InsertTipoDesgravacion(objTipoDesgravacion)
    End Function

    'Funcion para seleccionar tipo de desgravacion segun el id_tipoDesgravacion
    Public Function SelectTipoDesgravacionMant(ByVal id_tipoDesgravacion As Integer) As DataTable
        Return objCDTipoDesgravacion.SelectTipoDesgravacionMant(id_tipoDesgravacion)
    End Function

    'Metodo para actualizar tipo de desgravacion
    Public Function UpdateTipoDesgravacion(ByVal objTipoDesgravacion As CeTipoDesgravacion) As Boolean
        Return objCDTipoDesgravacion.UpdateTipoDesgravacion(objTipoDesgravacion)
    End Function

End Class
