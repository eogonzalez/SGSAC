Imports System.Data.SqlClient
Public Class CDReporteVerSAC
    Dim objConeccion As New ConectarService

    'Funcion que obtiene listado de versiones Activas e historicas del SAC
    Public Function SelectVersionesSAC() As DataTable
        Dim dataTableVerSac As New DataTable
        Dim sql_query As String

        Try
            sql_query = " SELECT " +
                " id_version, " +
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

    'Funcion verifica existe subpartida
    Function ExisteSubpartida(ByVal codigo As String) As Boolean
        Dim existe As Boolean = True
        Dim sql_query As String

        Try
            sql_query = " SELECT  " +
                    " coalesce(count(*), 0) " +
                    " FROM SAC_Subpartidas " +
                    " WHERE  Capitulo = @Capitulo " +
                    " AND  subpartida = @Partida+'%' " +
                    " AND  activo = 'S'"

            Dim capitulo As String = codigo.Substring(0, 2)
            Dim subpartida As String = codigo.Substring(0, 5)

            Using conn = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, conn)
                command.Parameters.AddWithValue("capitulo", capitulo)
                command.Parameters.AddWithValue("subpartida", subpartida)

                If command.ExecuteScalar > 0 Then
                    existe = True
                Else
                    existe = False
                End If

            End Using

        Catch ex As Exception

        End Try

        Return existe
    End Function

    'Funcion que obtiene listado del sac 
    Public Function SelectSACList(ByVal id_version As Integer, ByVal capitulo As String) As DataTable
        Dim dataTableSACList As New DataTable
        Dim sql_query As String

        Try
            sql_query = "  SELECT  " +
                " capitulo as codigo, descripcion_capitulo  as descripcion, ' ' as SAC " +
                " FROM SAC_Capitulos " +
                " WHERE  Capitulo = @Capitulo " +
                " AND   activo = 'S';  "

            Using conn = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, conn)
                command.Parameters.AddWithValue("Capitulo", capitulo)
                Dim dataAdapter As New SqlDataAdapter(command)

                conn.Open()
                dataAdapter.Fill(dataTableSACList)
                conn.Close()

            End Using

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

            Dim new_row As DataRow

            For Each row As DataRow In dataTablePartida.Rows
                'Recorro Cada partida, la agrego.
                'Inserto partida
                new_row = dataTableSACList.NewRow()

                new_row("codigo") = row("codigo")
                new_row("descripcion") = row("descripcion")
                new_row("SAC") = row("SAC")

                dataTableSACList.Rows.Add(new_row)


                'Incerto incisos
                'Agrego incisos
                sql_query = "  SELECT " +
                    " codigo_inciso as codigo, texto_inciso as descripcion, dai_base as SAC " +
                    " FROM " +
                    " SAC_Incisos " +
                    " where " +
                    " codigo_inciso like @partida+'%' " +
                    " and id_version = @id_version "

                Dim dataTableIncisos As New DataTable
                Dim partida As String = row("codigo")

                partida = RTrim(partida)

                Using conn3 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, conn3)
                    command.Parameters.AddWithValue("partida", partida)
                    command.Parameters.AddWithValue("id_version", id_version)
                    Dim dataAdapter As New SqlDataAdapter(command)

                    conn3.Open()
                    dataAdapter.Fill(dataTableIncisos)
                    conn3.Close()

                End Using

                Dim agrego_sub As Boolean = False
                For Each row_inciso As DataRow In dataTableIncisos.Rows
                    'Recorro tabla incisos

                    If ExisteSubpartida(row_inciso("codigo")) And agrego_sub = False Then
                        'Si existe subpartida agrego primero subpartida

                        'Agrego subpartida
                        sql_query = "  SELECT  " +
                            " subpartida as codigo, texto_subpartida as descripcion, ' ' as SAC  " +
                            " FROM SAC_Subpartidas " +
                            " WHERE  Capitulo = @Capitulo " +
                            " AND  partida = @Partida " +
                            " AND  activo = 'S';   "

                        Dim dataTableSubPartidas As New DataTable
                        Using conn2 = objConeccion.Conectar
                            Dim command As New SqlCommand(sql_query, conn2)
                            command.Parameters.AddWithValue("Capitulo", capitulo)
                            command.Parameters.AddWithValue("Partida", partida)
                            Dim dataAdapter As New SqlDataAdapter(command)

                            conn2.Open()
                            dataAdapter.Fill(dataTableSubPartidas)
                            conn2.Close()

                        End Using

                        'Recorro listado de subpartida
                        For Each row_subPartida As DataRow In dataTableSubPartidas.Rows

                            Dim inciso As String
                            inciso = row_inciso("codigo")
                            inciso = inciso.Substring(0, 5)

                            Dim subpartida As String
                            subpartida = row_subPartida("codigo")
                            subpartida = subpartida.Substring(0, 5)

                            If inciso = subpartida Then
                                agrego_sub = True
                                new_row = dataTableSACList.NewRow()

                                new_row("codigo") = row_subPartida("codigo")
                                new_row("descripcion") = row_subPartida("descripcion")
                                new_row("SAC") = row_subPartida("SAC")

                                dataTableSACList.Rows.Add(new_row)
                            End If

                        Next


                    End If

                    new_row = dataTableSACList.NewRow()

                    new_row("codigo") = row_inciso("codigo")
                    new_row("descripcion") = row_inciso("descripcion")
                    new_row("SAC") = row_inciso("SAC")

                    dataTableSACList.Rows.Add(new_row)
                Next

            Next


        Catch ex As Exception

        End Try

        Return dataTableSACList
    End Function

End Class
