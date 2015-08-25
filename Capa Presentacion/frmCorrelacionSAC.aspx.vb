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
            
            tabla_incisos = Nothing
            With gvAsignarCategorias
                .DataSource = tabla_incisos
                .DataBind()
            End With

            LlenarCorrelacionMant()
        End If

    End Sub

    Protected Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        Response.Redirect("~/frmEnmiendas.aspx")
    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click
        LlenarCorrelacionMant()
        LlenarSeleccionCodigoInciso(txt_codigo_arancel.Text)
    End Sub

    Protected Sub lkBtn_Nuevo_Click(sender As Object, e As EventArgs) Handles lkBtn_Nuevo.Click
        Dim codigo_inciso As String
        codigo_inciso = getCodigoIncisoGridView()

        If Not codigo_inciso = Nothing Then
            If LlenarEncabezadoApertura(codigo_inciso) Then
                lkBtt_Nuevo_ModalPopupExtender.Show()
                LlenarCorrelacionMant()
            Else
                Mensaje("No se puede obtener los datos para apertura de inciso.")
            End If


        Else
            Mensaje("Seleccione un inciso para realizar apertura.")
            LlenarCorrelacionMant()
        End If

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
    Sub LlenarCorrelacionMant()
        Dim objCNCorrelacion As New CNInstrumentosComerciales

        With objCNCorrelacion.SelectCorrelacionMant()

            If Not .Tables(0).Rows.Count = 0 Then

                txt_año_vigencia.Text = .Tables(0).Rows(0)("anio_version").ToString()

                txt_version_enmienda.Text = .Tables(0).Rows(0)("enmienda").ToString()
                txt_periodo_año_inicial.Text = .Tables(0).Rows(0)("anio_inicia_enmienda").ToString()
                txt_periodo_año_final.Text = .Tables(0).Rows(0)("anio_fin_enmieda").ToString()
                txt_descripcion_version_base.Text = .Tables(0).Rows(0)("descripcion").ToString()
            End If

            If Not .Tables(1).Rows.Count = 0 Then
                txt_nuevo_año_vigencia.Text = .Tables(1).Rows(0)("anio_version").ToString()
                txt_version_nueva_enmienda.Text = .Tables(1).Rows(0)("enmienda").ToString()
                txt_nuevo_periodo_año_inicial.Text = .Tables(1).Rows(0)("anio_inicia_enmienda").ToString()
                txt_nuevo_periodo_año_final.Text = .Tables(1).Rows(0)("anio_fin_enmieda").ToString()
                txt_descripcion_nueva_version.Text = .Tables(1).Rows(0)("descripcion").ToString()
            End If

        End With

    End Sub

    'Funcion que obtiene el codigo inciso del gridview
    Function getCodigoIncisoGridView() As String
        Dim codigo_inciso As String = Nothing

        For i As Integer = 0 To gvAsignarCategorias.Rows.Count - 1
            Dim rbutton As RadioButton = gvAsignarCategorias.Rows(i).FindControl("rb_inciso")
            If rbutton.Checked Then
                codigo_inciso = gvAsignarCategorias.Rows(i).Cells(2).Text
            End If
        Next

        Return codigo_inciso
    End Function

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Metodo que llena el encabezado del formulario de apertura arancelaria
    Private Function LlenarEncabezadoApertura(ByVal codigo_inciso As String) As Boolean
        Dim estado As Boolean = False
        Dim objCorrelacion As New CNInstrumentosComerciales

        With objCorrelacion.SelectCorrelacionMant()
            If Not .Tables(0).Rows.Count = 0 Then
                txt_anio_actual.Text = .Tables(0).Rows(0)("anio_version").ToString()
                txt_descripcion_actual.Text = .Tables(0).Rows(0)("descripcion").ToString()
                estado = True
            Else
                estado = False
            End If

            If Not .Tables(1).Rows.Count = 0 Then
                txt_anio_nueva.Text = .Tables(1).Rows(0)("anio_version").ToString()
                txt_descripcion_nueva.Text = .Tables(1).Rows(0)("descripcion").ToString()
                estado = True
            Else
                estado = False
            End If
        End With

        With objCorrelacion.SelectIncisoApertura(codigo_inciso)
            If Not .Rows.Count = 0 Then
                txt_inciso_actual.Text = codigo_inciso
                txt_dai_actual.Text = .Rows(0)("dai_base").ToString()
                txt_descripcion_inciso.Text = .Rows(0)("texto_inciso").ToString()

                txt_inciso_nuevo.Text = codigo_inciso
                txt_dai_nuevo.Text = .Rows(0)("dai_base").ToString()
                txt_descripcion_inciso_nuevo.Text = .Rows(0)("texto_inciso").ToString()
                estado = True
            Else
                estado = False
            End If
        End With

        Return estado
    End Function
#End Region

End Class