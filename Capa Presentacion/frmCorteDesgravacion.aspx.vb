Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmCorteDesgravacion
    Inherits System.Web.UI.Page
    Dim objCNInstrumentos As New CNInstrumentosComerciales

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hfIdInstrumento.Value = Request.QueryString("id_inst").ToString
            hfIdCategoria.Value = Request.QueryString("id_ct").ToString

            LlenargvTramos()
            LlenarTipoDesgravacion()
            LlenarTipoPeriodo()

        End If
    End Sub

    Protected Sub lkBtn_Configurar_Click(sender As Object, e As EventArgs) Handles lkBtn_Configurar.Click
        Dim id_tramo As Integer = 0
        id_tramo = Convert.ToInt32(getIdTramoGridView)

        If id_tramo = 0 Then
            Mensaje("Seleccione un tramo.")
            Exit Sub
        Else
            LlenarTramoCategoriaMant("editar", hfIdInstrumento.Value, hfIdCategoria.Value, id_tramo)
            btn_genera_cortes.CommandName = "editar"
            hfIdTramo.Value = id_tramo
            lkBtt_Configurar_ModalPopupExtender.Show()
        End If
    End Sub

#End Region

#Region "Funciones para obtener valores de los controles"

#End Region

#Region "Mis Funciones"

    'Funcion para llenar los controles con el id_instrumento, id_categoria y id_tramo
    Sub LlenarTramoCategoriaMant(ByVal accion As String, ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal id_tramo As Integer)
        Dim objCE_Tramo As New CETramoCategoria
        Dim objCN_Tramo As New CNInstrumentosComerciales

        Dim datosTramo As New DataTable
        datosTramo = objCN_Tramo.SelectTramoCategoriaMant(id_instrumento, id_categoria, id_tramo)

        If datosTramo.Rows.Count = 0 Then
            Mensaje("Tramo no existe")
            Exit Sub
        Else
            If accion = "editar" Then
                txtCategoria.Text = datosTramo.Rows(0)("codigo_categoria").ToString
                ddl_tipo_desgravacion.DataValueField = datosTramo.Rows(0)("id_tipo_desgrava")
                txtIdEtapa.Text = datosTramo.Rows(0)("id_tramo").ToString
                txt_cantidad_cortes.Text = datosTramo.Rows(0)("cantidad_cortes").ToString
                'ddl_tipo_periodo_corte.DataValueField = datosTramo.Rows(0)("id_tipo_periodo")
                txt_porcen_desgrava_anterior.Text = datosTramo.Rows(0)("desgrava_tramo_anterior").ToString
                txt_porcen_desgrava_final.Text = datosTramo.Rows(0)("desgrava_tramo_final").ToString
                txt_factor_desgravacion.Text = datosTramo.Rows(0)("factor_desgrava").ToString
            End If
        End If
    End Sub

    'Procedimiento para llenar el combo de tipo de periodo
    Sub LlenarTipoPeriodo()
        Dim objCNTramo As New CNInstrumentosComerciales

        With objCNTramo.SelectTipoPeriodo
            ddl_tipo_periodo_corte.DataTextField = .Tables(0).Columns("descripcion").ToString()
            ddl_tipo_periodo_corte.DataValueField = .Tables(0).Columns("id_tipo_periodo").ToString()
            ddl_tipo_periodo_corte.DataSource = .Tables(0)
            ddl_tipo_periodo_corte.DataBind()
        End With

    End Sub

    'Procedimiento para llenar el combo de tipo de desgravacion
    Sub LlenarTipoDesgravacion()
        Dim objCNTramo As New CNInstrumentosComerciales

        With objCNTramo.SelectTipoDesgravacion
            ddl_tipo_desgravacion.DataTextField = .Tables(0).Columns("descripcion").ToString()
            ddl_tipo_desgravacion.DataValueField = .Tables(0).Columns("id_tipo_desgrava").ToString()
            ddl_tipo_desgravacion.DataSource = .Tables(0)
            ddl_tipo_desgravacion.DataBind()
        End With
    End Sub

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Funcion para obtener del grid el id del tramo
    Function getIdTramoGridView() As String
        Dim id_tramo As String = Nothing

        For i As Integer = 0 To gvTramos.Rows.Count - 1
            Dim rbutton As RadioButton = gvTramos.Rows(i).FindControl("rb_tramo")
            If rbutton.Checked Then
                id_tramo = gvTramos.Rows(i).Cells(0).Text
            End If
        Next
        Return id_tramo
    End Function

    'Metodo para llenar el gvCategorias
    Protected Sub LlenargvTramos()
        Dim dtbl As DataTable
        Dim id_instrumento As Integer = hfIdInstrumento.Value
        Dim id_categoria As Integer = hfIdCategoria.Value

        dtbl = objCNInstrumentos.SelectTramoCategoria(id_instrumento, id_categoria)

        With gvTramos
            .DataSource = dtbl
            .DataBind()
        End With


    End Sub

#End Region

End Class