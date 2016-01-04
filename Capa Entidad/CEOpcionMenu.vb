Public Class CEOpcionMenu
    Private _id_opcion As Integer
    Private _nombre As String
    Private _descripcion As String
    Private _url As String
    Private _orden As Integer
    Private _visible As Boolean
    Private _obligatorio As Boolean
    Private _id_padre As Integer

    Public Property id_opcion As Integer
        Get
            Return _id_opcion
        End Get
        Set(value As Integer)
            _id_opcion = value
        End Set
    End Property

    Public Property nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            _nombre = value
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

    Public Property url As String
        Get
            Return _url
        End Get
        Set(value As String)
            _url = value
        End Set
    End Property

    Public Property orden As Integer
        Get
            Return _orden
        End Get
        Set(value As Integer)
            _orden = value
        End Set
    End Property

    Public Property visible As Boolean
        Get
            Return _visible
        End Get
        Set(value As Boolean)
            _visible = value
        End Set
    End Property

    Public Property obligatorio As Boolean
        Get
            Return _obligatorio
        End Get
        Set(value As Boolean)
            _obligatorio = value
        End Set
    End Property

    Public Property id_padre As Integer
        Get
            Return _id_padre
        End Get
        Set(value As Integer)
            _id_padre = value
        End Set
    End Property

End Class
