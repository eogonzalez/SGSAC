Imports System.Data.SqlClient
Public Class CDReporteVerSAC
    Dim objConeccion As New ConectarService

    'Funcion que obtiene listado de versiones Activas e historicas del SAC
    Public Function SelectVersionesSAC() As DataTable
        Dim dataTableVerSac As New DataTable
        Dim sql_query As String

        Try
            sql_query = " SELECT " +
                " ((id_version*10000)+anio_version) as id_ver, id_version, " +
                " enmienda +' - '+ cast(anio_inicia_enmienda as VARCHAR(5))+' - '+Cast(anio_fin_enmieda as varchar(5)) as descripcion " +
                " FROM " +
                " SAC_Versiones_Bitacora " +
                " where estado IS NOT NULL; "

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                Dim dataAdapter As New SqlDataAdapter(command)

                con.Open()
                dataAdapter.Fill(dataTableVerSac)
                con.Close()

            End Using

        Catch ex As SqlException

        Catch ex As Exception

        End Try

        Return dataTableVerSac
    End Function

    'Funcion que obtiene listado de capitulos del SAC
    Public Function SelectCapitulosSAC() As DataTable
        Dim dataTableCapSac As New DataTable
        Dim sql_query As String

        Try
            sql_query = "  SELECT  " +
                " capitulo as codigo, capitulo+' - '+descripcion_capitulo  as descripcion " +
                " FROM SAC_Capitulos " +
                " WHERE " +
                " activo = 'S';  "

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                Dim dataAdapter As New SqlDataAdapter(command)

                con.Open()
                dataAdapter.Fill(dataTableCapSac)
                con.Close()

            End Using

        Catch ex As SqlException

        Catch ex As Exception

        End Try

        Return dataTableCapSac
    End Function

    'Funcion que obtiene listado de partidas del SAC
    Public Function SelectPartidasSAC(ByVal capitulo As String) As DataTable
        Dim dataTablePartidasSac As New DataTable
        Dim sql_query As String

        Try
            sql_query = " SELECT " +
                " Partida as codigo, partida+' - '+Descripcion_Partida as descripcion " +
                " FROM " +
                " SAC_Partidas " +
                " WHERE  Capitulo = @capitulo " +
                " AND   activo = 'S'; "

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                command.Parameters.AddWithValue("capitulo", capitulo)

                Dim dataAdapter As New SqlDataAdapter(command)

                con.Open()
                dataAdapter.Fill(dataTablePartidasSac)
                con.Close()

            End Using

        Catch ex As SqlException

        Catch ex As Exception

        End Try

        Return dataTablePartidasSac
    End Function

    'Funcion que verifica si existe partida
    Function ExistePartida(ByVal codigo As String) As Boolean
        Dim existe As Boolean = True
        Dim sql_query As String

        Try

            sql_query = "  SELECT " +
                " COALESCE(COUNT(1), 0) " +
                " FROM " +
                " SAC_Partidas " +
                " where " +
                " Partida = @partida " +
                " and activo = 'S' "

            Dim partida As String = codigo.Substring(0, 4)

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                command.Parameters.AddWithValue("partida", partida)
                con.Open()

                If command.ExecuteScalar > 0 Then
                    existe = True
                Else
                    existe = False
                End If

                con.Close()

            End Using

        Catch ex As Exception

        End Try

        Return existe
    End Function

    'Funcion verifica existe subpartida
    Function ExisteSubpartida(ByVal codigo As String) As Boolean
        Dim existe As Boolean = True
        Dim sql_query As String

        Try
            sql_query = " SELECT  " +
                    " coalesce(count(*), 0) " +
                    " FROM SAC_Subpartidas " +
                    " WHERE  Capitulo = @Capitulo " +
                    " AND  subpartida like @subPartida+'%' " +
                    " AND  activo = 'S'"

            Dim capitulo As String = codigo.Substring(0, 2)
            Dim subpartida As String = codigo.Substring(0, 5)

            Using conn = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, conn)
                command.Parameters.AddWithValue("capitulo", capitulo)
                command.Parameters.AddWithValue("subpartida", subpartida)

                conn.Open()
                If command.ExecuteScalar > 0 Then
                    existe = True
                Else
                    existe = False
                End If
                conn.Close()

            End Using

        Catch ex As Exception

        End Try

        Return existe
    End Function

    'Funcion que obtiene listado del sac 
    Public Function SelectSACList(ByVal id_version As Integer, ByVal anio_version As Integer, ByVal capitulo As String, ByVal all_capitulos As Boolean) As DataTable
        Dim dataTableSACList As New DataTable
        Dim sql_query As String

        Try
            sql_query = "  SELECT  " +
                " capitulo as codigo, descripcion_capitulo  as descripcion, ' ' as SAC " +
                " FROM SAC_Capitulos " +
                " WHERE  "

            If all_capitulos Then
                sql_query = sql_query +
                    " activo = 'S' "
            Else
                sql_query = sql_query +
                    " Capitulo = @Capitulo " +
                    " AND   activo = 'S';  "
            End If

            Dim dataTable_Capitulos As New DataTable
            Using conn = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, conn)

                If Not all_capitulos Then
                    command.Parameters.AddWithValue("Capitulo", capitulo)
                End If

                Dim dataAdapter As New SqlDataAdapter(command)

                conn.Open()
                dataAdapter.Fill(dataTable_Capitulos)
                conn.Close()

            End Using

            Dim new_row_cap As DataRow
            Dim cont As Integer = 0

            'Recorro lista de capitulos
            For Each row_cap As DataRow In dataTable_Capitulos.Rows

                If cont = 0 Then
                    Dim column As New DataColumn
                    column.DataType = Type.GetType("System.String")
                    column.ColumnName = "codigo"
                    dataTableSACList.Columns.Add(column)

                    column = New DataColumn
                    column.DataType = Type.GetType("System.String")
                    column.ColumnName = "descripcion"
                    dataTableSACList.Columns.Add(column)

                    column = New DataColumn
                    column.ColumnName = "SAC"
                    dataTableSACList.Columns.Add(column)

                End If
                cont = cont + 1

                new_row_cap = dataTableSACList.NewRow()
                new_row_cap("codigo") = row_cap("codigo")
                capitulo = row_cap("codigo")
                new_row_cap("descripcion") = row_cap("descripcion")
                new_row_cap("SAC") = row_cap("SAC")

                dataTableSACList.Rows.Add(new_row_cap)

                'Query para seleccionar las partidas del capitulo
                sql_query = "  SELECT " +
                    " Partida as codigo, Descripcion_Partida as descripcion, ' ' as SAC " +
                    " FROM " +
                    " SAC_Partidas " +
                    " WHERE  Capitulo = @Capitulo " +
                    " AND   activo = 'S'; "

                Dim dataTablePartida As New DataTable
                Using conn = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, conn)
                    command.Parameters.AddWithValue("Capitulo", capitulo)
                    Dim dataAdapet As New SqlDataAdapter(command)

                    conn.Open()
                    dataAdapet.Fill(dataTablePartida)
                    conn.Close()

                End Using

                'Query que consulta subpartidas del capitulo
                sql_query = "  SELECT  " +
                    " subpartida as codigo, texto_subpartida as descripcion, ' ' as SAC  " +
                    " FROM SAC_Subpartidas " +
                    " WHERE  Capitulo = @Capitulo " +
                    " AND  activo = 'S';   "

                Dim dataTableSubPartidas As New DataTable
                Using conn2 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, conn2)
                    command.Parameters.AddWithValue("Capitulo", capitulo)
                    Dim dataAdapter As New SqlDataAdapter(command)

                    conn2.Open()
                    dataAdapter.Fill(dataTableSubPartidas)
                    conn2.Close()

                End Using

                'Query para seleccionar los incisos del capitulo
                sql_query = "  SELECT " +
                    " codigo_inciso as codigo, texto_inciso as descripcion, dai_base as SAC " +
                    " FROM " +
                    " SAC_Incisos " +
                    " where " +
                    " codigo_inciso like @capitulo+'%' " +
                    " and id_version = @id_version " +
                    " and anio_version = @anio_version "

                Dim dataTableIncisos As New DataTable

                Using conn3 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, conn3)
                    command.Parameters.AddWithValue("capitulo", capitulo)
                    command.Parameters.AddWithValue("id_version", id_version)
                    command.Parameters.AddWithValue("anio_version", anio_version)
                    Dim dataAdapter As New SqlDataAdapter(command)

                    conn3.Open()
                    dataAdapter.Fill(dataTableIncisos)
                    conn3.Close()

                End Using

                '----------------------'
                'Recorro lista de incisos'
                Dim new_row As DataRow

                For Each row_inciso As DataRow In dataTableIncisos.Rows

                    If ExistePartida(row_inciso("codigo")) Then
                        'Si existe recorro listado de partidas y agrego

                        'Recorro lista de partidas del capitulo
                        For Each row_partida As DataRow In dataTablePartida.Rows
                            Dim partida As String = row_partida("codigo")

                            If partida = row_inciso("codigo").ToString.Substring(0, 4) Then
                                new_row = dataTableSACList.NewRow()
                                new_row("codigo") = partida
                                new_row("descripcion") = row_partida("descripcion")
                                new_row("SAC") = row_partida("SAC")

                                dataTableSACList.Rows.Add(new_row)
                                dataTablePartida.Rows.Remove(row_partida)

                                Exit For
                            End If
                        Next
                        'Fin recorrer lista partidas del capitulo

                    End If
                    'Fin ExistePartida

                    If ExisteSubpartida(row_inciso("codigo")) Then
                        'Si existe subpartida agrego primero subpartida

                        'Recorro listado de subpartida
                        For i = 0 To dataTableSubPartidas.Rows.Count - 1
                            Dim cantSubPartidas As Integer
                            Dim inciso As String
                            Dim subpartida As String
                            Dim tamanio_subpartida As Integer

                            cantSubPartidas = dataTableSubPartidas.Rows.Count
                            inciso = row_inciso("codigo")
                            subpartida = dataTableSubPartidas.Rows(i)("codigo")
                            subpartida = RTrim(subpartida)
                            tamanio_subpartida = subpartida.Length

                            If tamanio_subpartida = 5 Then
                                inciso = inciso.Substring(0, 5)
                            ElseIf tamanio_subpartida = 6 Then
                                inciso = inciso.Substring(0, 6)
                            ElseIf tamanio_subpartida = 7 Then
                                inciso = inciso.Substring(0, 7)
                            End If

                            If inciso = subpartida Then
                                new_row = dataTableSACList.NewRow()
                                new_row("codigo") = dataTableSubPartidas.Rows(i)("codigo")
                                new_row("descripcion") = dataTableSubPartidas.Rows(i)("descripcion")
                                new_row("SAC") = dataTableSubPartidas.Rows(i)("SAC")

                                dataTableSACList.Rows.Add(new_row)
                                dataTableSubPartidas.Rows(i).Delete()
                            End If
                        Next
                        dataTableSubPartidas.AcceptChanges()
                        'Fin Recorrer lista subpartidas

                    End If
                    'Fin ExisteSubPartidas

                    new_row = dataTableSACList.NewRow()
                    new_row("codigo") = row_inciso("codigo")
                    new_row("descripcion") = row_inciso("descripcion")
                    new_row("SAC") = row_inciso("SAC")

                    dataTableSACList.Rows.Add(new_row)
                Next
                'Fin Recorrer lista incisos'
                '----------------------'

            Next
            'Fin Recorrer lista de capitulos

        Catch ex As SqlException

        Catch ex As DataException

        Catch ex As Exception

        End Try

        Return dataTableSACList
    End Function

End Class
