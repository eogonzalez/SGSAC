Public Class CEIncisoAsociaCategoria
    Private _id_instrumento As Integer
    Private _id_categoria As Integer
    Private _lista_incisos As DataTable
    Private _codigo_inciso As String
    Private _codigo_precision As String
    Private _texto_precision As String

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

    Public Property lista_incisos As DataTable
        Get
            Return _lista_incisos
        End Get
        Set(value As DataTable)
            _lista_incisos = value
        End Set
    End Property

    Public Property codigo_inciso As String
        Get
            Return _codigo_inciso
        End Get
        Set(value As String)
            _codigo_inciso = value
        End Set
    End Property

    Public Property codigo_precision As String
        Get
            Return _codigo_precision
        End Get
        Set(value As String)
            _codigo_precision = value
        End Set
    End Property

    Public Property texto_precision As String
        Get
            Return _texto_precision
        End Get
        Set(value As String)
            _texto_precision = value
        End Set
    End Property

End Class
