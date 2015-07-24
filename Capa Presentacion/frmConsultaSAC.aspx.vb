Imports Reglas_del_negocio
Public Class frmConsultaSAC
    Inherits System.Web.UI.Page

    Dim objReporte As New CNReportes
    Shared _tabla_incisos As DataTable
    Public Shared Property tabla_incisos As DataTable
        Get
            Return _tabla_incisos
        End Get
        Set(value As DataTable)
            _tabla_incisos = value
        End Set
    End Property

#Region "Funciones del Sistema"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InstrumentosComerciales()
            Categorias()
        End If
    End Sub

    Protected Sub ddl_instrumento_comercial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_instrumento_comercial.SelectedIndexChanged
        Categorias()
    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click
        llenar_gv_incisos_sac()
    End Sub
#End Region

#Region "Mis Funciones"
    Private Sub InstrumentosComerciales()
        Dim tbl As New DataTable
        tbl = objReporte.InstrumentoComercial.Tables("InstrumentoComercial")

        With Me.ddl_instrumento_comercial
            .DataSource = tbl
            .DataTextField = "nombre_instrumento"
            .DataValueField = "id_instrumento"
            .DataBind()
        End With
    End Sub

    Private Sub Categorias()
        Dim tbl As New DataTable
        tbl = objReporte.Categorias(ddl_instrumento_comercial.SelectedValue).Tables("Categorias")

        With Me.ddl_categoria_asignar
            .DataSource = tbl
            .DataTextField = "Categoria"
            .DataValueField = "id_categoria"
            .DataBind()
        End With
    End Sub

    Private Sub llenar_gv_incisos_sac()
        Dim objCNAsignaCat As New CNInstrumentosComerciales

        With objCNAsignaCat.SelectDatosCodigoInciso(txt_codigo_arancel.Text)

            If .Tables(0).Rows.Count = 0 Then
                'Esta vacia la tabla
            Else
                txt_descripcion_capitulo.Text = .Tables(0).Rows(0)("descripcion_capitulo").ToString()
            End If

            If .Tables(1).Rows.Count = 0 Then

            Else
                txt_descripcion_partida.Text = .Tables(1).Rows(0)("Descripcion_Partida").ToString()
            End If

            If .Tables(2).Rows.Count = 0 Then

            Else
                txt_descripcion_sub_partida.Text = .Tables(2).Rows(0)("texto_subpartida").ToString()
            End If

            If .Tables(3).Rows.Count = 0 Then

            Else
                Dim tbl As New DataTable

                tbl = .Tables(3)
                tabla_incisos = .Tables(3)

                With gv_incisos_sac
                    .DataSource = tbl
                    .DataBind()
                End With

            End If

        End With
    End Sub

    Public Sub DataTable_to_CSV(ByVal table As DataTable, ByVal filename As String, ByVal sepChar As String)
        Dim writer As System.IO.StreamWriter = Nothing
        Try

            writer = New System.IO.StreamWriter(filename, False, System.Text.Encoding.UTF8)

            ' first write a line with the columns name
            Dim sep As String = ""
            Dim builder As New System.Text.StringBuilder
            For Each col As DataColumn In table.Columns
                builder.Append(sep).Append(col.ColumnName.ToString.Replace(",", " ").Replace(Chr(9), " ").Replace(Chr(10), " ").Replace(vbCr, "").Replace(vbLf, ""))
                sep = sepChar
            Next
            writer.WriteLine(builder.ToString())

            ' then write all the rows
            For Each row As DataRow In table.Rows
                sep = ""
                builder = New System.Text.StringBuilder

                For Each col As DataColumn In table.Columns
                    builder.Append(sep).Append(row(col.ColumnName).ToString.Replace(",", " ").Replace(Chr(9), " ").Replace(Chr(10), " ").Replace(vbCr, "").Replace(vbLf, ""))
                    sep = sepChar
                Next
                writer.WriteLine(builder.ToString())
            Next
        Catch ex As Exception

        Finally
            If Not writer Is Nothing Then writer.Close()
        End Try
    End Sub
#End Region

    Protected Sub btn_genera_Click(sender As Object, e As EventArgs) Handles btn_genera.Click
        DataTable_to_CSV(tabla_incisos, Server.MapPath("Reportes/ConsultaSAC/IncisosSAC.csv"), ",")
        Response.Redirect("Reportes/ConsultaSAC/IncisosSAC.csv")
    End Sub
End Class