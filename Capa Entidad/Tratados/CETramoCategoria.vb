Public Class CETramoCategoria
    Private _id_instrumento As Integer
    Private _id_categoria As Integer
    Private _id_tramo As Integer
    Private _codigo_categoria As String
    Private _id_tipo_desgravacion As Integer
    Private _cantidad_cortes As Integer
    Private _id_periodo_corte As Integer
    Private _porcen_desgrava_anterior As Decimal
    Private _porcen_desgrava_final As Decimal
    Private _porcen_factor_desgrava As Decimal


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

    Public Property id_tramo As Integer
        Get
            Return _id_tramo
        End Get
        Set(value As Integer)
            _id_tramo = value
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

    Public Property cantidad_cortes As Integer
        Get
            Return _cantidad_cortes
        End Get
        Set(value As Integer)
            _cantidad_cortes = value
        End Set
    End Property

    Public Property id_periodo_corte As Integer
        Get
            Return _id_periodo_corte
        End Get
        Set(value As Integer)
            _id_periodo_corte = value
        End Set
    End Property

    Public Property porcen_desgrava_anterior As Decimal
        Get
            Return _porcen_desgrava_anterior
        End Get
        Set(value As Decimal)
            _porcen_desgrava_anterior = value
        End Set
    End Property

    Public Property porcen_desgrava_final As Decimal
        Get
            Return _porcen_desgrava_final
        End Get
        Set(value As Decimal)
            _porcen_desgrava_final = value
        End Set
    End Property

    Public Property porcen_factor_desgrava As Decimal
        Get
            Return _porcen_factor_desgrava
        End Get
        Set(value As Decimal)
            _porcen_factor_desgrava = value
        End Set
    End Property
End Class
