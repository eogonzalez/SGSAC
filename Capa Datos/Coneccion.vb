Imports System.Data.SqlClient
Imports System.Configuration

Public Class ConectarService
    Dim sql_coneccion As SqlConnection

    Public Function Conectar() As SqlConnection
        sql_coneccion = New SqlConnection(ConfigurationManager.ConnectionStrings("cn").ConnectionString)
        Return sql_coneccion
    End Function
End Class
