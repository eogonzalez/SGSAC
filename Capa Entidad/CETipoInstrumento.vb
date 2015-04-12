Public Class CETipoInstrumento
    Private _id_tipo_instrumento As Integer
    Private _descripcion As String
    Private _observaciones As String

    Public Property id_tipo_instrumento As Integer
        Get
            Return _id_tipo_instrumento
        End Get
        Set(value As Integer)
            _id_tipo_instrumento = value
        End Set
    End Property

    Public Property descripcion As String
        Get
            Return _descripcion
        End Get
        Set(value As String)
            _descripcion = value
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


End Class
