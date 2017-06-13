Imports Capa_Datos
Imports Capa_Entidad

Public Class CNConfigurarMenu
    Dim objCDMenu As New ConfigurarMenu

    'Funcion que obtiene listado de opciones del menu
    Public Function SelectOpcionesMenu(Optional ByVal id_padre As Integer = Nothing) As DataTable
        Return objCDMenu.SelectOpcionesMenu(id_padre)
    End Function

    'Funcion que obtiene los valores de la opcion del menu seleccionada
    Public Function SelectOpcionMant(ByVal id_menu_opcion As Integer, Optional ByVal id_padre As Integer = Nothing) As DataTable
        Return objCDMenu.SelectOpcionMant(id_menu_opcion, id_padre)
    End Function

    'Funcion que almacena nueva opcion de menu
    Public Function SaveOpcionMenu(ByVal obj_CeOpcion As CEOpcionMenu) As Boolean
        Return objCDMenu.SaveOpcionMenu(obj_CeOpcion)
    End Function

    'Funcion que actualiza opcion del menu
    Public Function UpdateOpcionMenu(ByVal obj_CeOpcion As CEOpcionMenu) As Boolean
        Return objCDMenu.UpdateOpcionMenu(obj_CeOpcion)
    End Function

End Class
