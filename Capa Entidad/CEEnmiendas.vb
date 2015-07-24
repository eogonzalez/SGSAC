Public Class CEEnmiendas
    Private _id_version As Integer
    Private _anio_version As Integer
    Private _enmienda As String
    Private _fecha_inicia_vigencia As Date
    Private _fecha_fin_vigencia As Date
    Private _observaciones As String

    Public Property id_version As Integer
        Get
            Return _id_version
        End Get
        Set(value As Integer)
            _id_version = value
        End Set
    End Property

    Public Property anio_version As Integer
        Get
            Return _anio_version
        End Get
        Set(value As Integer)
            _anio_version = value
        End Set
    End Property

    Public Property enmienda As String
        Get
            Return _enmienda
        End Get
        Set(value As String)
            _enmienda = value
        End Set
    End Property


    Public Property fecha_inicia_vigencia As Date
        Get
            Return _fecha_inicia_vigencia
        End Get
        Set(value As Date)
            _fecha_inicia_vigencia = value
        End Set
    End Property

    Public Property fecha_fin_vigencia As Date
        Get
            Return _fecha_fin_vigencia
        End Get
        Set(value As Date)
            _fecha_fin_vigencia = value
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
