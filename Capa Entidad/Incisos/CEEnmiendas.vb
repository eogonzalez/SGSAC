Public Class CEEnmiendas
    Private _id_version As Integer
    Private _anio_version As Integer
    Private _enmienda As String
    Private _fecha_inicia_vigencia As Date
    Private _fecha_fin_vigencia As Date
    Private _observaciones As String
    'Variables adicionales para correlaciones
    Private _inciso_origen As String
    Private _inciso_nuevo As String
    Private _texto_inciso As String
    Private _normativa As String
    Private _dai_base As Decimal
    Private _dai_nuevo As Decimal
    Private _anio_nueva_version As Integer


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

    Public Property inciso_origen As String
        Get
            Return _inciso_origen
        End Get
        Set(value As String)
            _inciso_origen = value
        End Set
    End Property

    Public Property inciso_nuevo As String
        Get
            Return _inciso_nuevo
        End Get
        Set(value As String)
            _inciso_nuevo = value
        End Set
    End Property

    Public Property texto_inciso As String
        Get
            Return _texto_inciso
        End Get
        Set(value As String)
            _texto_inciso = value
        End Set
    End Property

    Public Property normativa As String
        Get
            Return _normativa
        End Get
        Set(value As String)
            _normativa = value
        End Set
    End Property

    Public Property dai_base As Decimal
        Get
            Return _dai_base
        End Get
        Set(value As Decimal)
            _dai_base = value
        End Set
    End Property


    Public Property dai_nuevo As Decimal
        Get
            Return _dai_nuevo
        End Get
        Set(value As Decimal)
            _dai_nuevo = value
        End Set
    End Property

    Public Property anio_nueva_version As Integer
        Get
            Return _anio_nueva_version
        End Get
        Set(value As Integer)
            _anio_nueva_version = value
        End Set
    End Property
End Class
