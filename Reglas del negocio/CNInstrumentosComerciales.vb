Imports Capa_Datos
Imports Capa_Entidad
Public Class CNInstrumentosComerciales
    Dim objCDInstrumentos As New CDInstrumentosComerciales
    Dim objCDGeneral As New General

#Region "Funciones y procedimientos para el Mantenimiento de Instrumentos"

    'Funcion para llenar el GridView de Instrumentos
    Public Function SelectInstrumentos() As DataSet
        Return objCDInstrumentos.SelectInstrumentos
    End Function

    'Funcion para seleccionar listado del combo tipo de instrumetos
    Public Function SelectTipoInstrumento() As DataSet
        Return objCDInstrumentos.SelectTipoInstrumento
    End Function

    'Funcion para seleccionar listado del combo tipo de relaciones de instrumentos
    Public Function SelectTipoRelacionInstrumento() As DataSet
        Return objCDInstrumentos.SelectTipoRelacionInstrumento
    End Function

    'Funcion para seleccionar el instrumento segun el id_instrumento
    Public Function SelectInstrumentoMant(ByVal id_instrumento As Integer) As DataTable
        Return objCDInstrumentos.SelectInstrumentosMant(id_instrumento)
    End Function

    'Metodo para Insertar nuevo instrumento comercial
    Public Function InsertInstrumento(ByVal objIns As CEInstrumentosMant) As Boolean
        Return objCDInstrumentos.InsertInstrumento(objIns)
    End Function

    'Metodo para Actualizar instrumento comercial
    Public Function UpdateInstrumento(ByVal objIns As CEInstrumentosMant) As Boolean
        Return objCDInstrumentos.UpdateInstrumento(objIns)
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Tipo de Instrumentos"

    'Metodo para insertar tipo de instrumento
    Public Sub InsertTipoInstrumento(ByVal objTipoIns As CETipoInstrumento)
        objCDInstrumentos.InsertTipoInstrumento(objTipoIns)
    End Sub

    'Metodo para actualizar tipo de instrumento
    Public Sub UpdateTipoInstrumento(ByVal objTipoIns As CETipoInstrumento)
        objCDInstrumentos.UpdateTipoInstrumento(objTipoIns)
    End Sub

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Tipo Relacion Instrumentos"

    'Metodo para insertar tipo relacion de instrumento
    Public Sub InsertTipoRelacionInstrumento(ByVal objTipoRelIns As CETipoRelacionInstrumento)
        objCDInstrumentos.InsertTipoRelacionInstrumento(objTipoRelIns)
    End Sub

    'Metodo para actualizar tipo relacion de instrumento
    Public Sub UpdateTipoRelacionInstrumento(ByRef objTipoRelIns As CETipoRelacionInstrumento)
        objCDInstrumentos.UpdateTipoRelacionInstrumento(objTipoRelIns)
    End Sub

#End Region

End Class
