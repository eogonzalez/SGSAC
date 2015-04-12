Imports Capa_Datos
Imports Capa_Entidad
Public Class CNInstrumentosComerciales
    Dim objCDInstrumentos As New CDInstrumentosComerciales
    Dim objCDGeneral As New General

    Public Function LlenarInstrumentos() As DataSet
        Return objCDInstrumentos.SelectInstrumentos
    End Function

    Public Function SelectTipoInstrumento() As DataSet
        Return objCDInstrumentos.SelectTipoInstrumento
    End Function

    Public Function SelectTipoRelacionInstrumento() As DataSet
        Return objCDInstrumentos.SelectTipoRelacionInstrumento
    End Function

    Public Sub InsertInstrumento(ByVal objIns As CEInstrumentosMant)
        objCDInstrumentos.InsertInstrumento(objIns)
    End Sub

    Public Sub UpdateInstrumento(ByVal objIns As CEInstrumentosMant)
        objCDInstrumentos.UpdateInstrumento(objIns)
    End Sub

    Public Sub InsertTipoInstrumento(ByVal objTipoIns As CETipoInstrumento)
        objCDInstrumentos.InsertTipoInstrumento(objTipoIns)
    End Sub

    Public Sub UpdateTipoInstrumento(ByVal objTipoIns As CETipoInstrumento)
        objCDInstrumentos.UpdateTipoInstrumento(objTipoIns)
    End Sub

    Public Sub InsertTipoRelacionInstrumento(ByVal objTipoRelIns As CETipoRelacionInstrumento)
        objCDInstrumentos.InsertTipoRelacionInstrumento(objTipoRelIns)
    End Sub

    Public Sub UpdateTipoRelacionInstrumento(ByRef objTipoRelIns As CETipoRelacionInstrumento)
        objCDInstrumentos.UpdateTipoRelacionInstrumento(objTipoRelIns)
    End Sub

End Class
