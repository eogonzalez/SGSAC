Imports Capa_Entidad
Imports Reglas_del_negocio

Public Class frmAsignaCategoriasSAC
    Inherits System.Web.UI.Page

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hfIdInstrumento.Value = Request.QueryString("id_inst").ToString

            LlenarAsignaCategoriaMant(hfIdInstrumento.Value)

        End If

    End Sub

#End Region

#Region "Funciones para obtener valores"

#End Region

#Region "Mis funciones"

    'Metodo para llenar los controles del Mantenimiento
    Sub LlenarAsignaCategoriaMant(ByVal id_instrumento As Integer)
        Dim objCNAsignaCat As New CNInstrumentosComerciales

        With objCNAsignaCat.SelectDatosAsignaCategoriaMant(id_instrumento)
            txt_año_vigencia.Text = .Tables(0).Rows(0)("anio_version").ToString()

            txt_version_enmienda.Text = .Tables(0).Rows(0)("enmienda").ToString()
            txt_periodo_año_inicial.Text = .Tables(0).Rows(0)("anio_inicia_enmienda").ToString()
            txt_periodo_año_final.Text = .Tables(0).Rows(0)("anio_fin_enmieda").ToString()

            Dim nombre_instrumento As String = .Tables(1).Rows(0)("nombre_instrumento").ToString()
            Dim sigla As String = .Tables(1).Rows(0)("sigla").ToString()

            txt_nombre_instrumento.Text = nombre_instrumento + " - " + sigla


            ddl_categoria_asignar.DataTextField = .Tables(2).Columns("codigo_categoria").ToString()
            ddl_categoria_asignar.DataValueField = .Tables(2).Columns("id_categoria").ToString()
            ddl_categoria_asignar.DataSource = .Tables(2)
            ddl_categoria_asignar.DataBind()


        End With
    End Sub

#End Region


End Class