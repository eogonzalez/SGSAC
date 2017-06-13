Imports Capa_Entidad
Imports Capa_Datos
Imports System.Data.SqlClient
Public Class PaisesInstrumento
    Dim objConeccion As New ConectarService

    'función para actualizar países instrumento
    Public Function ActualizarInstrumentoPais(ByVal obj As CeInstrumentoPais) As Boolean
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("UPDATE [IC_Instrumento_Paises]")
            sql_string.AppendLine("   SET [ID_PAIS] = " & obj.idPais)
            sql_string.AppendLine("      ,[ID_TIPO_SOCIO] = " & obj.idTipoSocio)
            sql_string.AppendLine("      ,[CODIGO_BLOQUE_PAIS] = " & obj.idBloquePais)
            sql_string.AppendLine("      ,[OBSERVACIONES] = '" & obj.Observaciones & "'")
            sql_string.AppendLine("      ,[FECHA_FIRMA] = '" & obj.FechaFirma & "'")
            sql_string.AppendLine("      ,[FECHA_RATIFICACION] = '" & obj.FechaRatificacion & "'")
            sql_string.AppendLine("      ,[FECHA_VIGENCIA] = '" & obj.FechaVigencia & "'")
            sql_string.AppendLine(" WHERE [ID_INSTRUMENTO] = " & obj.idInstrumento)
            sql_string.AppendLine("	AND ID_PAIS = " & obj.idPais)
            sql_string.AppendLine("	AND CODIGO_BLOQUE_PAIS = " & obj.idBloquePais)
            sql_string.AppendLine("	AND ID_TIPO_SOCIO = " & obj.idTipoSocio)
            sql_string.AppendLine("")

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, conexion)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception

            Return False

        Finally
        End Try
    End Function

    'función para guardar los países
    Public Function GuardarInstrumentoPais(ByVal obj As CeInstrumentoPais) As Boolean
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("INSERT INTO [IC_Instrumento_Paises]")
            sql_string.AppendLine("           (")
            sql_string.AppendLine("           [ID_INSTRUMENTO]")
            sql_string.AppendLine("           ,[ID_PAIS]")
            sql_string.AppendLine("           ,[ID_TIPO_SOCIO]")
            sql_string.AppendLine("           ,[CODIGO_BLOQUE_PAIS]")
            sql_string.AppendLine("           ,[OBSERVACIONES]")
            sql_string.AppendLine("           ,[FECHA_FIRMA]")
            sql_string.AppendLine("           ,[FECHA_RATIFICACION]")
            sql_string.AppendLine("           ,[FECHA_VIGENCIA]")
            sql_string.AppendLine("           )")
            sql_string.AppendLine("     VALUES")
            sql_string.AppendLine("           (")
            sql_string.AppendLine("           " & obj.idInstrumento)
            sql_string.AppendLine("           ," & obj.idPais)
            sql_string.AppendLine("           ," & obj.idTipoSocio)
            sql_string.AppendLine("           ," & obj.idBloquePais)
            sql_string.AppendLine("           ,'" & obj.Observaciones & "'")
            sql_string.AppendLine("           ,'" & obj.FechaFirma & "'")
            sql_string.AppendLine("           ,'" & obj.FechaRatificacion & "'")
            sql_string.AppendLine("           ,'" & obj.FechaVigencia & "'")
            sql_string.AppendLine("           )")
            sql_string.AppendLine("")

            Using conexion = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, conexion)

                conexion.Open()
                command.ExecuteScalar()
                Return True
            End Using

        Catch ex As Exception

            Return False

        Finally
        End Try
    End Function

    'Funcón para traer los datos del instrumento pais para editar
    Public Function PaisesInstrumentoMant(ByVal idInstrumento As Integer, ByVal idPais As Integer, ByVal idTipoSocio As Integer) As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT [ID_INSTRUMENTO]")
            sql_string.AppendLine("      ,[ID_PAIS]")
            sql_string.AppendLine("      ,[ID_TIPO_SOCIO]")
            sql_string.AppendLine("      ,[CODIGO_BLOQUE_PAIS]")
            sql_string.AppendLine("      ,[OBSERVACIONES]")
            sql_string.AppendLine("      ,[FECHA_FIRMA]")
            sql_string.AppendLine("      ,[FECHA_RATIFICACION]")
            sql_string.AppendLine("      ,[FECHA_VIGENCIA]")
            sql_string.AppendLine("  FROM [IC_Instrumento_Paises]")
            sql_string.AppendLine("  WHERE ID_INSTRUMENTO = " & idInstrumento)
            sql_string.AppendLine("  AND ID_PAIS = " & idPais)
            sql_string.AppendLine("  AND ID_TIPO_SOCIO = " & idTipoSocio)
            sql_string.AppendLine("  AND ESTADO = 1")
            sql_string.AppendLine("")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds, "PaisesInstrumentoMant")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Paises Instrumento Mantenimiento = " + ex.Message.ToString)
        End Try

        Return ds

    End Function

    'Función para traer los Instrumentos Paises
    Public Function PaisesInstrumento(ByVal idInstrumento As Integer) As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT IP.ID_INSTRUMENTO")
            sql_string.AppendLine("	,IP.ID_PAIS")
            sql_string.AppendLine("	,IP.ID_TIPO_SOCIO")
            sql_string.AppendLine("	,P.nombre_pais")
            sql_string.AppendLine("	,P.codigo_alfa")
            sql_string.AppendLine("	,IP.FECHA_FIRMA")
            sql_string.AppendLine("	,IP.FECHA_RATIFICACION")
            sql_string.AppendLine("	,IP.FECHA_VIGENCIA")
            sql_string.AppendLine("FROM [dbo].[IC_Instrumento_Paises] AS IP")
            sql_string.AppendLine("INNER JOIN G_Paises AS P")
            sql_string.AppendLine("	ON IP.ID_PAIS = P.id_pais")
            sql_string.AppendLine("WHERE IP.ID_INSTRUMENTO = " & idInstrumento)
            sql_string.AppendLine("	AND IP.ESTADO = 1")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds, "PaisesInstrumento")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Paises Instrumento = " + ex.Message.ToString)
        End Try

        Return ds
    End Function

    'Función para listar los tipos de socios
    Public Function TipoSocios() As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT [id_tipo_socio]")
            sql_string.AppendLine("      ,[descripcion]")
            sql_string.AppendLine("FROM [IC_Tipo_Socio]")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds, "TipoSocio")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Paises = " + ex.Message.ToString)
        End Try

        Return ds

    End Function

    'Función para listar la región del país
    Public Function RegionPais() As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT [ID_BLOQUE_PAIS]")
            sql_string.AppendLine("      ,[DESCRIPCION]")
            sql_string.AppendLine("FROM [IC_Bloque_Pais]")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds, "RegionPais")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Region Pais = " + ex.Message.ToString)
        End Try

        Return ds

    End Function

    'Función para listar los paises
    Public Function Paises() As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT [id_pais]")
            sql_string.AppendLine("      ,[nombre_pais]")
            sql_string.AppendLine("      ,[codigo_alfa]")
            sql_string.AppendLine("FROM [G_Paises]")
            sql_string.AppendLine("WHERE estado = 1")
            sql_string.AppendLine("ORDER BY nombre_pais ASC")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds, "Paises")
            End Using

        Catch ex As Exception
            MsgBox("ERROR Paises = " + ex.Message.ToString)
        End Try

        Return ds

    End Function

    'Funcion para mostrar los datos del instrumento seleccionado
    Public Function DatosInstrumento(ByVal idInstrumento As Integer) As DataSet
        Dim ds As New DataSet
        Try
            Dim sql_string As New System.Text.StringBuilder

            sql_string.AppendLine("SELECT instrumento.[id_instrumento]")
            sql_string.AppendLine("	  ,instrumento.nombre_instrumento")
            sql_string.AppendLine("      ,tipo.descripcion AS TipoInstrumento")
            sql_string.AppendLine("      ,instrumento.[sigla]")
            sql_string.AppendLine("      ,instrumento.[sigla_alternativa]")
            sql_string.AppendLine("      ,relacion.descripcion as AcuerdoEntre")
            sql_string.AppendLine("FROM [IC_Instrumentos] as instrumento")
            sql_string.AppendLine("	inner	JOIN dbo.IC_Tipo_Instrumento as tipo")
            sql_string.AppendLine("		on instrumento.id_tipo_instrumento = tipo.id_tipo_instrumento")
            sql_string.AppendLine("	INNER JOIN dbo.IC_Tipo_Relacion_Instrumento as relacion")
            sql_string.AppendLine("		on instrumento.id_tipo_relacion_instrumento = relacion.id_tipo_relacion_instrumento")
            sql_string.AppendLine("WHERE id_instrumento = " & idInstrumento)
            sql_string.AppendLine("	and estado = 1")

            Using cn = objConeccion.Conectar
                Dim command As SqlCommand = New SqlCommand(sql_string.ToString, cn)
                Dim da As SqlDataAdapter
                da = New SqlDataAdapter(command)
                da.Fill(ds, "DatosInstrumento")
            End Using

        Catch ex As Exception
            MsgBox("ERROR SelectInstrumento = " + ex.Message.ToString)
        Finally

        End Try

        Return ds

    End Function

End Class
