Imports Capa_Datos
Imports Capa_Entidad
Public Class CNCorrelacionSAC
    Dim objCDCorrelacion As New CorrelacionSAC

    'Funcion para obtener los datos para los codigos seleccionados
    Public Function SelectDatosCodigoIncisoCorrelacion(ByVal str_codigo As String, ByVal anio_version As Integer, ByVal id_version As Integer) As DataSet
        Return objCDCorrelacion.SelectDatosCodigoIncisoCorrelacion(str_codigo, anio_version, id_version)
    End Function

    'Funcion para obtener los datos del Mantenimiento de Correlacion 
    Public Function SelectCorrelacionMant() As DataSet
        Return objCDCorrelacion.SelectCorrelacionMant()
    End Function

    'Metodo para Insertar nueva Version SAC
    Public Function InsertVersionSAC(ByVal objVersionSAC As CEEnmiendas) As Boolean
        Return objCDCorrelacion.InsertVersionSAC(objVersionSAC)
    End Function

    'Metodo para Actualizar version SAC
    Public Function UpdateVersionSAC(ByVal objVersionSAC As CEEnmiendas) As Boolean
        Return objCDCorrelacion.UpdateVersionSAC(objVersionSAC)
    End Function

    'Funcion para seleccionar el SAC segun el id_version_sac
    Public Function SelectVersionSACMant(ByVal id_version_sac As Integer, ByVal anio_version As Integer) As DataTable
        Return objCDCorrelacion.SelectVersionSACMant(id_version_sac, anio_version)
    End Function

    'Funcion para llenar el GridView de Enmiendas del SAC
    Public Function SelectEnmiendas() As DataSet
        Return objCDCorrelacion.SelectEnmiendas()
    End Function


    'Funcion que obtiene los datos para el mantenimiento de apertura arancelaria
    Public Function SelectIncisoApertura(ByVal codigo_inciso As String) As DataTable
        Return objCDCorrelacion.SelectIncisoApertura(codigo_inciso)
    End Function

    'Funcion que almacena la correlacion del inciso seleccionado
    Public Function InsertApertura(ByVal objCorrelacion As CEEnmiendas) As Boolean
        Return objCDCorrelacion.InsertApertura(objCorrelacion)
    End Function

    'Funcion para almacenar la supresion del iniciso seleccionado
    Public Function InsertSupresion(ByVal objCorrelacion As CEEnmiendas) As Boolean
        Return objCDCorrelacion.InsertSupresion(objCorrelacion)
    End Function

    'Funcion para borrar accion de enmienda
    Public Function DeleteAccion(ByVal objCECorrelacion As CEEnmiendas) As Boolean
        Return objCDCorrelacion.DeleteAccion(objCECorrelacion)
    End Function

    'Funcion que valida si inciso nuevo ya existe
    Public Function ValidaIncisoNuevo(ByVal objCeCorrelacion As CEEnmiendas) As Boolean
        Return objCDCorrelacion.ValidaIncisoNuevo(objCeCorrelacion)
    End Function

    'Funcion que valida si inciso nuevo ya existe
    Public Function ValidaIncisoNuevo(ByVal codigo_inciso As String) As Boolean
        Return objCDCorrelacion.ValidaIncisoNuevo(codigo_inciso)
    End Function

    'Funcion que obtiene datos de partida y subpartida para Apertura de comieco
    Public Function SelectDatosApertura(ByVal inciso As String) As DataSet
        Return objCDCorrelacion.SelectDatosApertura(inciso)
    End Function

End Class
