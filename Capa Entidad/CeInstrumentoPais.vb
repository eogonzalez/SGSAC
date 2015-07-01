Public Class CeInstrumentoPais
    Private _idInstrumento As Integer
    Private _idPais As Integer
    Private _idTipoSocio As Integer
    Private _IdBloquePais As Integer
    Private _Observaciones As String
    Private _FechaFirma As Date
    Private _FechaRatificacion As Date
    Private _FechaVigencia As Date

    Public Property idInstrumento As Integer
        Get
            Return _idInstrumento
        End Get
        Set(value As Integer)
            _idInstrumento = value
        End Set
    End Property

    Public Property idPais As Integer
        Get
            Return _idPais
        End Get
        Set(value As Integer)
            _idPais = value
        End Set
    End Property

    Public Property idTipoSocio As Integer
        Get
            Return _idTipoSocio
        End Get
        Set(value As Integer)
            _idTipoSocio = value
        End Set
    End Property

    Public Property idBloquePais As Integer
        Get
            Return _IdBloquePais
        End Get
        Set(value As Integer)
            _IdBloquePais = value
        End Set
    End Property

    Public Property Observaciones As String
        Get
            Return _Observaciones
        End Get
        Set(value As String)
            _Observaciones = value
        End Set
    End Property

    Public Property FechaFirma As Date
        Get
            Return _FechaFirma
        End Get
        Set(value As Date)
            _FechaFirma = value
        End Set
    End Property

    Public Property FechaRatificacion As Date
        Get
            Return _FechaRatificacion
        End Get
        Set(value As Date)
            _FechaRatificacion = value
        End Set
    End Property

    Public Property FechaVigencia As Date
        Get
            Return _FechaVigencia
        End Get
        Set(value As Date)
            _FechaVigencia = value
        End Set
    End Property

End Class
