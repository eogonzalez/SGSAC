Public Class CEInstrumentosMant
    Private _id_instrumento As Integer
    Private _id_tipo_instrumento As Integer
    Private _id_tipo_relacion_instrumento As Integer
    Private _nombre_instrumento As String
    Private _sigla As String
    Private _sigla_alternativa As String
    Private _observaciones As String
    Private _fecha_firma As Date
    Private _fecha_ratificada As Date
    Private _fecha_vigencia As Date
    Private _estado As Boolean

    Public Property id_instrumento As Integer
        Get
            Return _id_instrumento
        End Get
        Set(value As Integer)
            _id_instrumento = value
        End Set
    End Property
    Public Property id_tipo_instrumento As Integer
        Get
            Return _id_tipo_instrumento
        End Get
        Set(value As Integer)
            _id_tipo_instrumento = value
        End Set
    End Property
    Public Property id_tipo_relacion_instrumento As Integer
        Get
            Return _id_tipo_relacion_instrumento
        End Get
        Set(value As Integer)
            _id_tipo_relacion_instrumento = value
        End Set
    End Property
    Public Property nombre_instrumento As String
        Get
            Return _nombre_instrumento
        End Get
        Set(value As String)
            _nombre_instrumento = value
        End Set
    End Property
    Public Property sigla As String
        Get
            Return _sigla
        End Get
        Set(value As String)
            _sigla = value
        End Set
    End Property

    Public Property sigla_alternativa As String
        Get
            Return _sigla_alternativa
        End Get
        Set(value As String)
            _sigla_alternativa = value
        End Set
    End Property

    Public Property observaciones As String
        Get
            Return _observaciones
        End Get
        Set(value As String)
            _observaciones = value
        End Set
    End Property

    Public Property fecha_firma As Date
        Get
            Return _fecha_firma
        End Get
        Set(value As Date)
            _fecha_firma = value
        End Set
    End Property

    Public Property fecha_ratificada As Date
        Get
            Return _fecha_ratificada
        End Get
        Set(value As Date)
            _fecha_ratificada = value
        End Set
    End Property

    Public Property fecha_vigencia As Date
        Get
            Return _fecha_vigencia
        End Get
        Set(value As Date)
            _fecha_vigencia = value
        End Set
    End Property

    Public Property estado As Boolean
        Get
            Return _estado
        End Get
        Set(value As Boolean)
            _estado = value
        End Set
    End Property

End Class
