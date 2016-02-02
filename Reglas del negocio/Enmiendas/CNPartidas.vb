Imports Capa_Datos
Public Class CNPartidas
    Dim objCDPartidas As New CDPartidas

    'Funcion para obtener los datos del encabezado de mantenimiento de partidas
    Public Function SelectPartidasEncMant() As DataTable
        Return objCDPartidas.SelectPartidasEncMant()
    End Function

    'Funcion para obtener el listado de partidas segun el codigo
    Public Function SelectDatosPartidas(ByVal str_codigo As String) As DataTable
        Return objCDPartidas.SelectDatosPartidas(str_codigo)
    End Function

    'Funcion que verifica si existe partida
    Public Function ExistePartida(ByVal str_codigo As String) As Boolean
        Return objCDPartidas.ExistePartida(str_codigo)
    End Function

    'Funcion que almacena partida nueva
    Public Function SavePartida(ByVal str_codigo As String, ByVal descripcion As String) As Boolean
        Return objCDPartidas.SavePartida(str_codigo, descripcion)
    End Function

    'Funcion que actualiza partida
    Public Function UpdatePartida(ByVal str_codigo As String, ByVal descripcion As String) As Boolean
        Return objCDPartidas.UpdatePartida(str_codigo, descripcion)
    End Function

End Class
