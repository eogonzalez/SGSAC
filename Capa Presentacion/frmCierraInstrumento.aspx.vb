Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmCierraInstrumento
    Inherits System.Web.UI.Page
    Dim objCN As New CNInstrumentosComerciales
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LlenarInstrumento()

        End If

    End Sub

    Sub LlenarInstrumento()

        With objCN.SelectInstrumentos
            ddl_instrumento.DataTextField = .Tables(0).Columns("nombre_instrumento").ToString()
            ddl_instrumento.DataValueField = .Tables(0).Columns("id_instrumento").ToString()
            ddl_instrumento.DataSource = .Tables(0)
            ddl_instrumento.DataBind()

            Llenar_gvResumen(ddl_instrumento.SelectedValue.ToString())
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
End Class