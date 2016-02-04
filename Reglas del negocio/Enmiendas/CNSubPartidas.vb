Imports Capa_Datos
Public Class CNSubPartidas
    Dim objCDSubPartidas As New CDSubPartidas

    'Funcion para obtener el listado de subpartidas segun el codigo
    Public Function SelectDatosSubPartidas(ByVal str_codigo As String) As DataTable
        Return objCDSubPartidas.SelectDatosSubPartidas(str_codigo)
    End Function

    'Funcion que verifica si existe subpartida
    Public Function ExisteSubPartida(ByVal str_codigo As String) As Boolean
        Return objCDSubPartidas.ExisteSubPartida(str_codigo)
    End Function

    'Funcion que almacena subpartida nueva
    Public Function SaveSubPartida(ByVal str_codigo As String, ByVal descripcion As String) As Boolean
        Return objCDSubPartidas.SaveSubPartida(str_codigo, descripcion)
    End Function

    'Funcion que actualiza subpartida
    Public Function UpdateSubPartida(ByVal str_codigo As String, ByVal descripcion As String) As Boolean
        Return objCDSubPartidas.UpdateSubPartida(str_codigo, descripcion)
    End Function

End Class
