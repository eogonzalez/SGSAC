Imports Capa_Datos
Public Class CNApruebaSAC
    Dim objCDAprueba As ApruebaSAC

    'Funcion que verifica si es posible realizar el proceso de aprobacion
    Public Function VerificaApruebaSAC(ByVal anioProcesa As Integer) As Boolean
        Return objCDAprueba.VerificaApruebaSAC(anioProcesa)
    End Function

    'Funcion que aprueba y genera la siguiente version del SAC
    Public Function ApruebaSAC(ByVal id_version As Integer, ByVal anio_version As Integer, ByVal anio_inicia_enmienda As Integer, ByVal anio_final_enmienda As Integer, ByVal anio_version_new As Integer) As Boolean
        Return objCDAprueba.ApruebaSAC(id_version, anio_version, anio_inicia_enmienda, anio_final_enmienda, anio_version_new)
    End Function

End Class
