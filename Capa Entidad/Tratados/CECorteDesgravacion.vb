Public Class CECorteDesgravacion
    Private _id_instrumento As Integer
    Private _id_categoria As Integer
    Private _id_tramos As Integer
    Private _id_tipo_periodo As Integer
    Private _cantidad_cortes As Integer
    Private _porcen_periodo_anterior As Decimal
    Private _porcen_periodo_final As Decimal
    Private _factor_desgrava As Decimal


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

    Public Property id_tramos As Integer
        Get
            Return _id_tramos
        End Get
        Set(value As Integer)
            _id_tramos = value
        End Set
    End Property

    Public Property id_tipo_periodo As Integer
        Get
            Return _id_tipo_periodo
        End Get
        Set(value As Integer)
            _id_tipo_periodo = value
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

    Public Property porcen_periodo_anterior As Decimal
        Get
            Return _porcen_periodo_anterior
        End Get
        Set(value As Decimal)
            _porcen_periodo_anterior = value
        End Set
    End Property

    Public Property porcen_periodo_final As Decimal
        Get
            Return _porcen_periodo_final
        End Get
        Set(value As Decimal)
            _porcen_periodo_final = value
        End Set
    End Property

    Public Property factor_desgrava As Decimal
        Get
            Return _factor_desgrava
        End Get
        Set(value As Decimal)
            _factor_desgrava = value
        End Set
    End Property


End Class
