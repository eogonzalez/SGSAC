Public Class CeInstrumentoPaisMant
    Private _idInstrumento As Integer
    Private _idPais As Integer
    Private _idTipoSocio As Integer
    Private _IdBloquePais As Integer

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

End Class
