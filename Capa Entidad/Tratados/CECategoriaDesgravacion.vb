Public Class CECategoriaDesgravacion
    Private _id_instrumento As Integer
    Private _id_categoria As Integer
    Private _codigo_categoria As String
    Private _id_tipo_desgravacion As Integer
    Private _cantidad_tramos As Integer
    Private _observaciones As String

    Public Property id_instrumento As Integer
        Get
            Return _id_instrumento
        End Get
        Set(value As Integer)
            _id_instrumento = value
        End Set
    End Property

    Public Property id_categoria As Integer
        Get
            Return _id_categoria
        End Get
        Set(value As Integer)
            _id_categoria = value
        End Set
    End Property

    Public Property codigo_categoria As String
        Get
            Return _codigo_categoria
        End Get
        Set(value As String)
            _codigo_categoria = value
        End Set
    End Property

    Public Property id_tipo_desgravacion As Integer
        Get
            Return _id_tipo_desgravacion
        End Get
        Set(value As Integer)
            _id_tipo_desgravacion = value
        End Set
    End Property

    Public Property cantidad_tramos As Integer
        Get
            Return _cantidad_tramos
        End Get
        Set(value As Integer)
            _cantidad_tramos = value
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
