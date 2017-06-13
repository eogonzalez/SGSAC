Imports Capa_Entidad
Imports Capa_Datos
Public Class CNPaisesInstrumento
    Dim objCDPaises As New PaisesInstrumento

    'Función para traer los paises instrumento
    Public Function PaisesInstrumento(ByVal IdInstrumento As Integer) As DataSet
        Return objCDPaises.PaisesInstrumento(IdInstrumento)
    End Function

    'Función para mostrar los datos del intrumento para editarlo
    Public Function PaisesInstrumentoMant(ByVal idInstrumento As Integer, ByVal idPais As Integer, ByVal idTipoSocio As Integer) As DataSet
        Return objCDPaises.PaisesInstrumentoMant(idInstrumento, idPais, idTipoSocio)
    End Function

    'Funcion para mostrar los datos del instrumento seleccionado
    Public Function DatosInstrumento(ByVal IdInstrumento As Integer) As DataSet
        Return objCDPaises.DatosInstrumento(IdInstrumento)
    End Function

    'Función para listar el tipo de socio
    Public Function TipoSocio() As DataSet
        Return objCDPaises.TipoSocios
    End Function

    'Función para listar los paises
    Public Function Paises() As DataSet
        Return objCDPaises.Paises
    End Function

    'Función para listar el tipo de socio
    Public Function RegionPais() As DataSet
        Return objCDPaises.RegionPais
    End Function

    'Función para guardar los paises instrumento
    Public Function GuardarInstrumentoPais(ByVal obj As CeInstrumentoPais) As Boolean
        Return objCDPaises.GuardarInstrumentoPais(obj)
    End Function

    'Función para actualizar los paises instrumento
    Public Function ActualizarInstrumentoPais(ByVal obj As CeInstrumentoPais) As Boolean
        Return objCDPaises.ActualizarInstrumentoPais(obj)
    End Function
End Class
