Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmCierraInstrumento
    Inherits System.Web.UI.Page
    Dim objCN As New CNInstrumentosComerciales
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_ComboInstrumento()

        End If

    End Sub

    Sub Llenar_ComboInstrumento()

        With objCN.SelectInstrumentos
            ddl_instrumento.DataTextField = .Tables(0).Columns("nombre_instrumento").ToString()
            ddl_instrumento.DataValueField = .Tables(0).Columns("id_instrumento").ToString()
            ddl_instrumento.DataSource = .Tables(0)
            ddl_instrumento.DataBind()

            Llenar_gvResumen(ddl_instrumento.SelectedValue.ToString())

            Llenar_DetallesInstrumento(ddl_instrumento.SelectedValue, .Tables(0))

            'txt_fecha_suscripcion.Text = .Tables(0).Columns("fecha_firma").ToString()
            'txt_fecha_vigencia.Text = .Tables(0).Columns("fecha_vigencia").ToString()
        End With

    End Sub

    Sub Llenar_gvResumen(ByVal id_instrumento As Integer)
        Dim tbl As New DataTable

        tbl = objCN.SelectResumenInstrumento(id_instrumento)

        With gvInstrumentos
            .DataSource = tbl
            .DataBind()
        End With

    End Sub

    Protected Sub ddl_instrumento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_instrumento.SelectedIndexChanged
        Dim tbl As DataTable
        tbl = objCN.SelectInstrumentos.Tables(0)

        Llenar_DetallesInstrumento(ddl_instrumento.SelectedValue, tbl)
        Llenar_gvResumen(ddl_instrumento.SelectedValue)
    End Sub

    Sub Llenar_DetallesInstrumento(ByVal idInstrumento As Integer, ByVal tbl As DataTable)

        For Each row As DataRow In tbl.Rows
            If idInstrumento = row("id_instrumento") Then
                txt_fecha_suscripcion.Text = row("fecha_firma")
                txt_fecha_vigencia.Text = row("fecha_vigencia")
            End If
        Next

    End Sub

End Class