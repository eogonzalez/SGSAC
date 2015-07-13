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

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click
        'Ver la manera de no llamar asigna categoria'
        LlenarAsignaCategoriaMant(hfIdInstrumento.Value)

        LlenarSeleccionCodigoInciso(txt_codigo_arancel.Text)
    End Sub

    'Metodo para cambiar el estado de los checkbox del gridview
    Protected Sub cb_seleccionar_todo_CheckedChanged(sender As Object, e As EventArgs)

        Dim check_all As CheckBox = sender

        If check_all.Checked Then
            For i As Integer = 0 To gvAsignarCategorias.Rows.Count - 1
                Dim check_inciso As CheckBox = gvAsignarCategorias.Rows(i).FindControl("cb_inciso")

                If Not check_inciso.Checked Then
                    check_inciso.Checked = True
                End If
            Next
        Else
            For i As Integer = 0 To gvAsignarCategorias.Rows.Count - 1
                Dim check_inciso As CheckBox = gvAsignarCategorias.Rows(i).FindControl("cb_inciso")

                If check_inciso.Checked Then
                    check_inciso.Checked = False
                End If
            Next
        End If


    End Sub


    Protected Sub cb_inciso_CheckedChanged(sender As Object, e As EventArgs)
        'ClientScript.RegisterStartupScript(sender.GetType(), "EstatusCheck", "StatusCheck();", True)
    End Sub

#End Region

#Region "Funciones para obtener valores"

#End Region

#Region "Mis funciones"

    'Metodo para llenar controles de la seleccion del inciso
    Sub LlenarSeleccionCodigoInciso(ByVal inciso As String)
        Dim objCNAsignaCat As New CNInstrumentosComerciales

        With objCNAsignaCat.SelectDatosCodigoInciso(inciso)

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
    Sub LlenarAsignaCategoriaMant(ByVal id_instrumento As Integer)
        Dim objCNAsignaCat As New CNInstrumentosComerciales

        With objCNAsignaCat.SelectDatosAsignaCategoriaMant(id_instrumento)

            If .Tables(0).Rows.Count = 0 Then
                'Esta vacia la tabla

            Else
                txt_año_vigencia.Text = .Tables(0).Rows(0)("anio_version").ToString()

                txt_version_enmienda.Text = .Tables(0).Rows(0)("enmienda").ToString()
                txt_periodo_año_inicial.Text = .Tables(0).Rows(0)("anio_inicia_enmienda").ToString()
                txt_periodo_año_final.Text = .Tables(0).Rows(0)("anio_fin_enmieda").ToString()

            End If

            If .Tables(1).Rows.Count = 0 Then
                'Esta vacia la tabla
            Else
                Dim nombre_instrumento As String = .Tables(1).Rows(0)("nombre_instrumento").ToString()
                Dim sigla As String = .Tables(1).Rows(0)("sigla").ToString()

                txt_nombre_instrumento.Text = nombre_instrumento + " - " + sigla
            End If

            If .Tables(2).Rows.Count = 0 Then
                'Esta vacia la tabla
            Else
                ddl_categoria_asignar.DataTextField = .Tables(2).Columns("codigo_categoria").ToString()
                ddl_categoria_asignar.DataValueField = .Tables(2).Columns("id_categoria").ToString()
                ddl_categoria_asignar.DataSource = .Tables(2)
                ddl_categoria_asignar.DataBind()
            End If


        End With
    End Sub

#End Region


End Class