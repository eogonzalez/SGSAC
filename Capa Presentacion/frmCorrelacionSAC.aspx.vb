Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmCorrelacionSAC
    Inherits System.Web.UI.Page

    Private Shared _tabla_incisos As DataTable

    Public Shared Property tabla_incisos As DataTable
        Get
            Return _tabla_incisos
        End Get
        Set(value As DataTable)
            _tabla_incisos = tabla_incisos
        End Set
    End Property

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hfIdVersionSAC.Value = Request.QueryString("id_vs").ToString
            hfAnioVersion.Value = Request.QueryString("av").ToString

            tabla_incisos = Nothing
            With gvAsignarCategorias
                .DataSource = tabla_incisos
                .DataBind()
            End With

            LlenarCorrelacionMant(hfIdVersionSAC.Value, hfAnioVersion.Value)

        End If

    End Sub

    Protected Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        Response.Redirect("~/frmEnmiendas.aspx")
    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click

        LlenarCorrelacionMant(hfIdVersionSAC.Value, hfAnioVersion.Value)

        LlenarSeleccionCodigoInciso(txt_codigo_arancel.Text)

    End Sub

#End Region

#Region "Mis Funciones"

    'Metodo para llenar controles de la seleccion del inciso
    Sub LlenarSeleccionCodigoInciso(ByVal inciso As String)
        Dim objCorrelacion As New CNInstrumentosComerciales

        With objCorrelacion.SelectDatosCodigoIncisoCorrelacion(inciso)
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

                With gvAsignarCategorias
                    .DataSource = tbl
                    .DataBind()
                End With

            End If
        End With
    End Sub

    'Metodo para llenar los controles del Mantenimiento
    Sub LlenarCorrelacionMant(ByVal id_version As Integer, ByVal anio_version As Integer)
        Dim objCNCorrelacion As New CNInstrumentosComerciales

        With objCNCorrelacion.SelectCorrelacionMant(id_version, anio_version)

            If Not .Tables(0).Rows.Count = 0 Then

                txt_año_vigencia.Text = .Tables(0).Rows(0)("anio_version").ToString()

                txt_version_enmienda.Text = .Tables(0).Rows(0)("enmienda").ToString()
                txt_periodo_año_inicial.Text = .Tables(0).Rows(0)("anio_inicia_enmienda").ToString()
                txt_periodo_año_final.Text = .Tables(0).Rows(0)("anio_fin_enmieda").ToString()
                txt_descripcion_version_base.Text = .Tables(0).Rows(0)("descripcion").ToString()
            End If

            If Not .Tables(1).Rows.Count = 0 Then
                ddl_version_nueva.DataTextField = .Tables(1).Columns("descripcion").ToString()
                ddl_version_nueva.DataValueField = .Tables(1).Columns("id_version").ToString()
                ddl_version_nueva.DataSource = .Tables(1)
                ddl_version_nueva.DataBind()
            End If

        End With

    End Sub

#End Region


End Class