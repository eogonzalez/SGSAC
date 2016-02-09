Imports Reglas_del_negocio
Public Class frmConsultaVerSAC
    Inherits System.Web.UI.Page
    Dim objCNReportes As New CNReporteVerSAC

#Region "Funciones del Sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LlenarComboVersionSAC()
            LlenarComboCapitulo()
            LlenarComboPartida(ddl_capitulo.SelectedValue)

        End If

    End Sub

    Protected Sub ddl_capitulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_capitulo.SelectedIndexChanged
        LlenarComboPartida(ddl_capitulo.SelectedValue)
    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click
        LlenarGv_Incisos(ddl_version_SAC.SelectedValue, ddl_capitulo.SelectedValue)
    End Sub

    Protected Sub gv_incisos_sac_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_incisos_sac.PageIndexChanging
        gv_incisos_sac.PageIndex = e.NewPageIndex

        With gv_incisos_sac
            .DataSource = Session("tabla_rep")
            .DataBind()
        End With

    End Sub

#End Region


#Region "Mis Funciones"

    Sub LlenarComboVersionSAC()
        Dim tblVerSac As New DataTable
        tblVerSac = objCNReportes.SelectVersionesSAC()

        With ddl_version_SAC
            .DataSource = tblVerSAc
            .DataTextField = "descripcion"
            .DataValueField = "id_version"
            .DataBind()
        End With

    End Sub

    Sub LlenarComboCapitulo()
        Dim tblCapSAC As New DataTable
        tblCapSAC = objCNReportes.SelectCapitulosSAC()

        With ddl_capitulo
            .DataSource = tblCapSAC
            .DataTextField = "descripcion"
            .DataValueField = "codigo"
            .DataBind()

        End With

    End Sub

    Sub LlenarComboPartida(ByVal capitulo As String)
        Dim tblPartidasSAC As New DataTable
        tblPartidasSAC = objCNReportes.SelectPartidasSAC(capitulo)

        With ddl_partida
            .DataSource = tblPartidasSAC
            .DataTextField = "descripcion"
            .DataValueField = "codigo"
            .DataBind()

        End With

    End Sub

    Sub LlenarGv_Incisos(ByVal id_version_sac As Integer, ByVal capitulo As String)
        Dim tbl_sac_list As New DataTable
        tbl_sac_list = objCNReportes.SelectSACList(id_version_sac, capitulo)

        With tbl_sac_list

            If .Rows.Count = 0 Then

                Dim tbl As New DataTable

                tbl = Nothing
                'tabla_incisos = .Tables(3)
                Session.Add("tabla_rep", tbl)

                With gv_incisos_sac
                    .DataSource = tbl
                    .DataBind()
                End With

                lbl_cantidad.Text = 0
                'btn_genera.Enabled = False

            Else
                lbl_cantidad.Text = .Rows.Count.ToString
                'btn_genera.Enabled = True

                Dim tbl As New DataTable

                tbl = tbl_sac_list
                'tabla_incisos = .Tables(3)
                Session.Add("tabla_rep", tbl)

                With gv_incisos_sac
                    .DataSource = tbl
                    .DataBind()
                End With

            End If

        End With


    End Sub

#End Region

    
    
End Class