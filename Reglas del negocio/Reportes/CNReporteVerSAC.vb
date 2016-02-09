Imports Capa_Datos
Public Class CNReporteVerSAC
    Dim objCDReportes As New CDReporteVerSAC

    'Funcion que obtiene listado de versiones Activas e historicas del SAC
    Public Function SelectVersionesSAC() As DataTable
        Return objCDReportes.SelectVersionesSAC()
    End Function

    'Funcion que obtiene listado de capitulos del SAC
    Public Function SelectCapitulosSAC() As DataTable
        Return objCDReportes.SelectCapitulosSAC()
    End Function

    'Funcion que obtiene listado de partidas del SAC
    Public Function SelectPartidasSAC(ByVal capitulo As String) As DataTable
        Return objCDReportes.SelectPartidasSAC(capitulo)
    End Function

    'Funcion que obtiene listado del sac 
    Public Function SelectSACList(ByVal id_version As Integer, ByVal capitulo As String) As DataTable
        Return objCDReportes.SelectSACList(id_version, capitulo)
    End Function

End Class
