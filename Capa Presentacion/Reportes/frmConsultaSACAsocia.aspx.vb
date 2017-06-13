Imports Reglas_del_negocio
Public Class frmConsultaSACAsocia
    Inherits System.Web.UI.Page
    Dim objReporte As New CNReportesGeneral
    

#Region "Funciones del sistema"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LlenarComboInstrumentos()
            LlenarComboCategoria(ddl_instrumento_comercial.SelectedValue)
            lbl_cantidad.Text = 0
            'btn_genera.Enabled = False
        End If

    End Sub

    Protected Sub ddl_instrumento_comercial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_instrumento_comercial.SelectedIndexChanged
        LlenarComboCategoria(ddl_instrumento_comercial.SelectedValue)
    End Sub

    Protected Sub cb_categorias_CheckedChanged(sender As Object, e As EventArgs) Handles cb_categorias.CheckedChanged

        If cb_categorias.Checked Then
            ddl_categoria_asignar.Enabled = False

        Else
            ddl_categoria_asignar.Enabled = True

        End If

    End Sub

    Protected Sub cb_incisos_CheckedChanged(sender As Object, e As EventArgs) Handles cb_incisos.CheckedChanged
        If cb_incisos.Checked Then
            txt_codigo_inciso_rep.Enabled = False
            'btn_seleccionar.Enabled = False
        Else
            txt_codigo_inciso_rep.Enabled = True
            'btn_seleccionar.Enabled = True
        End If
    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click

        LlenarGridView()

    End Sub

    Protected Sub btn_genera_Click(sender As Object, e As EventArgs) Handles btn_genera.Click

        If Not lbl_cantidad.Text = "0" Then
            DataTable_to_CSV(Session("tabla_rep"), Server.MapPath("../Reportes/ConsultaSAC/IncisosSAC.csv"), ",")
            Response.Redirect("../Reportes/ConsultaSAC/IncisosSAC.csv")
        Else
            Mensaje("No existen datos para generar excel.")
        End If

    End Sub

    Protected Sub gv_incisos_sac_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_incisos_sac.PageIndexChanging
        gv_incisos_sac.PageIndex = e.NewPageIndex

        With gv_incisos_sac
            .DataSource = Session("tabla_rep")
            .DataBind()
        End With

    End Sub

#End Region


#Region "Mis funciones"

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub


    Private Sub LlenarComboInstrumentos()
        Dim tbl As New DataTable

        tbl = objReporte.SelectInstrumentoComercial

        With ddl_instrumento_comercial

            .DataSource = tbl
            .DataTextField = "nombre_instrumento"
            .DataValueField = "id_instrumento"
            .DataBind()

        End With


    End Sub

    Private Sub LlenarComboCategoria(ByVal id_instrumento As Integer)
        Dim datatableCat As New DataTable
        datatableCat = Nothing
        datatableCat = objReporte.SelectCategoriasList(id_instrumento)

        With ddl_categoria_asignar
            .Items.Clear()
            .DataSource = datatableCat
            .DataTextField = "Categoria"
            .DataValueField = "id_categoria"
            .DataBind()
        End With

    End Sub

    Private Sub LlenarGridView()        
        Dim id_instrumento As Integer = ddl_instrumento_comercial.SelectedValue
        Dim all_cat As Boolean
        Dim all_incisos As Boolean

        If cb_categorias.Checked Then
            all_cat = True
        Else
            all_cat = False
        End If

        If cb_incisos.Checked Then
            all_incisos = True
        Else
            all_incisos = False

        End If

        Dim dataset As New DataSet
        dataset = objReporte.SelectIncisosAsocia(id_instrumento, txt_codigo_inciso_rep.Text, ddl_categoria_asignar.SelectedValue, all_cat, all_incisos)


        If dataset IsNot Nothing Then
            Dim dataTableList As New DataTable

            With dataset

                If Not all_incisos Then
                    'Si no son todos los incisos
                    'With objCNAsignaCat.SelectDatosCodigoInciso(id_instrumento, txt_codigo_inciso_rep.Text)

                    If .Tables(0).Rows.Count > 0 Then
                        txt_descripcion_capitulo.Text = .Tables(0).Rows(0)("descripcion_capitulo").ToString()
                    End If

                    If .Tables(1).Rows.Count > 0 Then
                        txt_descripcion_partida_rep.Text = .Tables(1).Rows(0)("Descripcion_Partida").ToString()
                    End If

                    If .Tables(2).Rows.Count > 0 Then
                        txt_descripcion_sub_partida.Text = .Tables(2).Rows(0)("texto_subpartida").ToString()
                    End If

                    'If Not IsDBNull(.Tables(2).Rows(0)("texto_subpartida")) Then

                    'End If

                    dataTableList = .Tables(3)

                End If



        If Not IsDBNull(.Tables(3)) Then
            If .Tables(3).Rows.Count = 0 Then

                Dim tbl As New DataTable

                tbl = Nothing
                'tabla_incisos = .Tables(3)
                Session.Add("tabla_rep", .Tables(3))

                With gv_incisos_sac
                    .DataSource = tbl
                    .DataBind()
                End With

                lbl_cantidad.Text = 0
                'btn_genera.Enabled = False

            Else
                lbl_cantidad.Text = .Tables(3).Rows.Count.ToString
                'btn_genera.Enabled = True

                Dim tbl As New DataTable

                tbl = .Tables(3)
                'tabla_incisos = .Tables(3)
                Session.Add("tabla_rep", .Tables(3))

                With gv_incisos_sac
                    .DataSource = tbl
                    .DataBind()
                End With

            End If
        End If

            End With

        End If



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


    
    
    
End Class