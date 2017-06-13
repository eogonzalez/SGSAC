Imports Capa_Datos
Public Class CNEnmiendas
    Dim objCDEnmiendas As New Enmiendas

    'Funcion que verifica si existe version pendiente de aprobar
    Public Function ExisteVersionSACPendiente() As Boolean
        Return objCDEnmiendas.ExisteVersionSACPendiente()
    End Function


    'Funcion que verifica cuantas versiones de sac pendiente existen
    Public Function CantidadVersionesSACPendientes() As Integer
        Return objCDEnmiendas.CantidadVersionesSACPendientes()
    End Function

End Class
