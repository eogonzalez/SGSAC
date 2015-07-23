Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmEnmiendas
    Inherits System.Web.UI.Page
    Dim objCapaNegocio As New CNInstrumentosComerciales

#Region "Funciones del Sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llenar_gv_Enmiendas_Sac()
            Me.btn_Guardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & Me.GetPostBackEventReference(Me.btn_Guardar))
        End If
    End Sub

    Protected Sub lkBtt_editar_Click(sender As Object, e As EventArgs) Handles lkBtt_editar.Click
        Dim id_version As Integer
        id_version = Convert.ToInt32(getIdVersionGridView())
        If id_version = 0 Then
            Mensaje("Seleccione un instrumeto")
            Exit Sub
        Else
            LlenarVersionSACMant("editar", id_version)
            btn_Guardar.CommandName = "editar"
            hfIdVersionSAC.Value = id_version
            lkBtt_nuevo_ModalPopupExtender.Show()
        End If
    End Sub

    Protected Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        LimpiarFormulario()

    End Sub


    Protected Sub lkBtt_categorias_Click(sender As Object, e As EventArgs) Handles lkBtt_categorias.Click
        Dim id_version_sac As Integer = 0
        id_version_sac = Convert.ToInt32(getIdVersionGridView())
        If id_version_sac = 0 Then
            Mensaje("Seleccione una version del SAC.")
            Exit Sub
        Else
            hfIdVersionSAC.Value = id_version_sac
            Response.Redirect("~/frmCorrelacionSAC.aspx?id_vs=" + hfIdVersionSAC.Value)
        End If
    End Sub

#End Region

#Region "Funciones para capturar valores del formulario"

#End Region

#Region "Mis Funciones"

    'Limpiar formulario
    Sub LimpiarFormulario()
        txtAñoVersion.Text = ""
        txtDescripcion.Text = ""
        txtFechaFinVigencia.Text = ""
        txtFechaInicioVigencia.Text = ""
        txtObservaciones.Text = ""
    End Sub

    'Procedimiento para llenar formulario con el id de la version del sac seleccionado
    Sub LlenarVersionSACMant(ByVal accion As String, ByVal id_version_sac As Integer)
        Dim datosVersionSac As New DataTable
        datosVersionSac = objCapaNegocio.SelectVersionSACMant(id_version_sac)

        If datosVersionSac.Rows.Count = 0 Then
            Mensaje("El Version de SAC no existe")
            Exit Sub
        Else
            If accion = "editar" Then
                txtAñoVersion.Text = datosVersionSac.Rows(0)("anio_version").ToString
                txtDescripcion.Text = datosVersionSac.Rows(0)("enmienda").ToString

                'If IsNothing(datosVersionSac.Rows(0)("fecha_inicia_vigencia").ToString) Then

                'Else
                '    txtFechaInicioVigencia.Text = datosVersionSac.Rows(0)("fecha_inicia_vigencia")
                'End If

                'If IsNothing(datosVersionSac.Rows(0)("fecha_fin_vigencia").ToString) Then

                'Else
                '    txtFechaFinVigencia.Text = datosVersionSac.Rows(0)("fecha_fin_vigencia").ToString
                'End If

                'If IsNothing(datosVersionSac.Rows(0)("observaciones").ToString) Then

                'Else
                '    txtObservaciones.Text = datosVersionSac.Rows(0)("observaciones").ToString
                'End If


            End If
        End If

    End Sub

    'Funcion que obtiene del grid el id del instrumento
    Function getIdVersionGridView() As String
        Dim id_version As Integer = Nothing

        For i As Integer = 0 To gv_Versiones_SAC.Rows.Count - 1
            Dim rbutton As RadioButton = gv_Versiones_SAC.Rows(i).FindControl("rb_version")
            If rbutton.Checked Then
                id_version = gv_Versiones_SAC.Rows(i).Cells(0).Text
            End If
        Next

        Return id_version

    End Function

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    ' Procedimiento para llenar GridView de Enmiendas del SAC
    Protected Sub Llenar_gv_Enmiendas_Sac()
        Dim tbl As New DataTable

        tbl = objCapaNegocio.SelectEnmiendas.Tables(0)


        With gv_Versiones_SAC
            .DataSource = tbl
            .DataBind()
        End With
    End Sub

#End Region

    

End Class