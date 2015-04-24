﻿Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmTipoRelacionInstrumentoMant
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LlenarCorrelativoTipoRelacionInstrumento()


    End Sub

    Sub LlenarCorrelativoTipoRelacionInstrumento()
        Dim CNInstrumento As New cnGeneral
        Dim nombreTabla As String
        Dim llaveTabla As String

        nombreTabla = "IC_Tipo_Relacion_Instrumento"
        llaveTabla = "id_tipo_relacion_instrumento"

        txtIdTipoInstrumento.Text = CNInstrumento.ObtenerCorrelativoId(nombreTabla, llaveTabla).ToString

    End Sub

    Function getIdCorrelativoTipoRelacion() As Integer
        Return Convert.ToInt32(txtIdTipoInstrumento.Text)
    End Function

    Function getDescripcion() As String
        Return txtDescripcion.Text
    End Function

    Function getObservaciones() As String
        Return txtObservaciones.Text
    End Function

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        NuevoTipoRelacionInstrumento()
    End Sub

    Sub NuevoTipoRelacionInstrumento()
        Dim CEObj As New CETipoRelacionInstrumento
        Dim CNTipoRelacionIns As New CNInstrumentosComerciales

        CEObj.id_tipo_relacion_instrumento = getIdCorrelativoTipoRelacion()
        CEObj.descripcion = getDescripcion()
        CEObj.observaciones = getObservaciones()

        CNTipoRelacionIns.InsertTipoRelacionInstrumento(CEObj)

    End Sub

End Class