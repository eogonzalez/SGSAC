﻿Imports Reglas_del_negocio
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

    Protected Sub btn_Guardar_Click(sender As Object, e As EventArgs) Handles btn_Guardar.Click
        If btn_Guardar.CommandName = "editar" Then
            If EditarVersionSAC(hfIdVersionSAC.Value) Then
                Mensaje("Versión SAC actualizada con éxito")
                Llenar_gv_Enmiendas_Sac()
                btn_Guardar.CommandName = ""
                LimpiarFormulario()

            Else
                Mensaje("Error al actualizar Versión SAC")
                lkBtt_nuevo_ModalPopupExtender.Show()
            End If

        Else
            'If GuardarVersionSAC() Then
            '    Mensaje("Versión SAC guardada con éxito")
            '    Llenar_gv_Enmiendas_Sac()
            '    LimpiarFormulario()
            'Else
            '    Mensaje("Error al guardar Versión SAC")
            '    lkBtt_nuevo_ModalPopupExtender.Show()
            'End If

        End If
    End Sub

#End Region

#Region "Funciones para capturar valores del formulario"
    Function getAñoVersion() As Integer
        Return Convert.ToInt32(txtAñoVersion.Text)
    End Function

    Function getDescripcion() As String
        Return txtDescripcion.Text
    End Function

    Function getFechaInicia() As Date
        Return txtFechaInicioVigencia.Text
    End Function

    Function getFechaFin() As Date
        Return txtFechaFinVigencia.Text
    End Function

    Function getBaseNormativa() As String
        Return txtObservaciones.Text
    End Function

#End Region

#Region "Mis Funciones"

    Private Function EditarVersionSAC(ByVal id_version_sac) As Boolean
        'Declaro las variables de la capa de datos y entidad
        Dim CEObjeto As New CEEnmiendas

        'Obtengo los valores de los controles
        CEObjeto.id_version = id_version_sac
        CEObjeto.anio_version = getAñoVersion()
        CEObjeto.enmienda = getDescripcion()
        CEObjeto.fecha_inicia_vigencia = getFechaInicia()
        CEObjeto.fecha_fin_vigencia = getFechaFin()
        CEObjeto.observaciones = getBaseNormativa()

        'Envio los valores a la capa entidad con el Objeto a la funcion actualizar instrumentos
        Return objCapaNegocio.UpdateVersionSAC(CEObjeto)

    End Function

    'Private Function GuardarVersionSAC() As Boolean

    'End Function

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

                If Not IsDBNull(datosVersionSac.Rows(0)("enmienda")) Then
                    txtDescripcion.Text = datosVersionSac.Rows(0)("enmienda").ToString
                End If

                If Not IsDBNull(datosVersionSac.Rows(0)("fecha_inicia_vigencia")) Then
                    txtFechaInicioVigencia.Text = datosVersionSac.Rows(0)("fecha_inicia_vigencia").ToString
                End If

                If Not IsDBNull(datosVersionSac.Rows(0)("fecha_fin_vigencia")) Then
                    txtFechaFinVigencia.Text = datosVersionSac.Rows(0)("fecha_fin_vigencia").ToString
                End If

                If Not IsDBNull(datosVersionSac.Rows(0)("observaciones")) Then
                    txtObservaciones.Text = datosVersionSac.Rows(0)("observaciones").ToString
                End If

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