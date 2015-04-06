Imports System.Security.Cryptography
Imports System.Text
Public Class General

    Public Function EncodePassword(ByVal originalPassword As String) As String

        'Clave que se utilizará para encriptar el usuario y la contraseña
        Dim clave As String = "7f9facc418f74439c5e9709832;0ab8a5:OCOdN5Wl,q8SLIQz8i|8agmu¬s13Q7ZXyno/"

        'Se instancia el objeto sha512 para posteriormente usarlo para calcular la matriz de bytes especificada
        Dim sha512 As SHA512 = New SHA512CryptoServiceProvider()

        'Se crea un arreglo llamada inputbytes donde se convierte el usuario, la contraseña y la clave a una secuencia de bytes.
        Dim inputBytes() As Byte = (New UnicodeEncoding()).GetBytes(originalPassword + clave)

        'Se calcula la matriz de bytes del arreglo anterior y se encripta.
        Dim hash() As Byte = sha512.ComputeHash(inputBytes)


        'Convertimos el arreglo de bytes a cadena.
        Return Convert.ToBase64String(hash)

    End Function

End Class
