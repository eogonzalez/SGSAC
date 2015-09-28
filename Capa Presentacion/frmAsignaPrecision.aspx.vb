Imports Reglas_del_negocio

Public Class frmAsignaPrecisionTLC
    Inherits System.Web.UI.Page

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id_instrumento As Integer

        If Not IsPostBack Then
            id_instrumento = Request.QueryString("id_inst").ToString
            Session.Add("id_instrumento", id_instrumento)

            LlenarPrecisionMant(id_instrumento)

        End If
    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click
        Dim id_instrumento As Integer
        Dim codigo_arancel As String

        id_instrumento = Session("id_instrumento")

        codigo_arancel = txt_codigo_arancel.Text
        Session.Add("codigo_arancel", codigo_arancel)


        LlenarPrecisionMant(id_instrumento)
        LlenarSeleccionCodigoInciso(id_instrumento, codigo_arancel)
    End Sub

    Protected Sub lkBtn_precision_Click(sender As Object, e As EventArgs) Handles lkBtn_precision.Click
        Dim codigo_inciso As String
        Dim id_instrumento As Integer

        codigo_inciso = getCodigoIncisoGridView()
        id_instrumento = Session("id_instrumento")

        If codigo_inciso = Nothing Then
            Mensaje("Seleccione un inciso para asignar precision.")
            LlenarPrecisionMant(id_instrumento)
        Else
            If LlenarEncabezadoPrecision(id_instrumento, codigo_inciso) Then
                lkBtn_Precision_ModalPopupExtender.Show()
                'LlenarPrecisionMant(id_instrumento)
            Else
                Mensaje("No se puede obtener los datos para la precision del inciso.")
            End If
        End If

    End Sub

#End Region

#Region "Obtener valores de panel de precision"

    Function getCodigoIncisoGridView() As String
        Dim codigo_inciso As String = Nothing

        For i As Integer = 0 To gvAsignarPrecision.Rows.Count - 1
            Dim rbutton As RadioButton = gvAsignarPrecision.Rows(i).FindControl("rb_inciso")
            If rbutton.Checked Then
                codigo_inciso = gvAsignarPrecision.Rows(i).Cells(1).Text
            End If
        Next

        Return codigo_inciso
    End Function

#End Region

#Region "Mis Funciones"

    'Metodo para llenar los controles del Mantenimiento
    Sub LlenarPrecisionMant(ByVal id_instrumento As Integer)
        Dim objCNPrecision As New CNInstrumentosComerciales

        With objCNPrecision.SelectDatosAsignaPrecisionMant(id_instrumento)
            If Not .Tables(0).Rows.Count = 0 Then
                txt_año_vigencia.Text = .Tables(0).Rows(0)("anio_version").ToString()
                txt_version_enmienda.Text = .Tables(0).Rows(0)("enmienda").ToString()
                txt_periodo_año_inicial.Text = .Tables(0).Rows(0)("anio_inicia_enmienda").ToString()
                txt_periodo_año_final.Text = .Tables(0).Rows(0)("anio_fin_enmieda").ToString()
            End If

            If Not .Tables(1).Rows.Count = 0 Then
                Dim nombre_instrumento As String = .Tables(1).Rows(0)("nombre_instrumento").ToString()
                Dim sigla As String = .Tables(1).Rows(0)("sigla").ToString()
                txt_nombre_instrumento.Text = nombre_instrumento + " - " + sigla
            End If

        End With
    End Sub

    'Metodo para llenar controles de la seleccion del inciso
    Sub LlenarSeleccionCodigoInciso(ByVal id_instrumento As Integer, ByVal inciso As String)
        Dim objCNAsignaCat As New CNInstrumentosComerciales


        With objCNAsignaCat.SelectDatosCodigoInciso(id_instrumento, inciso)

            If Not .Tables(0).Rows.Count = 0 Then
                txt_descripcion_capitulo.Text = .Tables(0).Rows(0)("descripcion_capitulo").ToString()
            End If

            If Not .Tables(1).Rows.Count = 0 Then
                txt_descripcion_partida.Text = .Tables(1).Rows(0)("Descripcion_Partida").ToString()
            End If

            If Not .Tables(2).Rows.Count = 0 Then
                txt_descripcion_sub_partida.Text = .Tables(2).Rows(0)("texto_subpartida").ToString()
            End If

            If Not .Tables(3).Rows.Count = 0 Then
                Dim tbl As New DataTable
                Dim column As New DataColumn
                Dim tabla_incisos = New DataTable

                tbl = .Tables(3)
                tabla_incisos = .Tables(3)
                With column
                    .ColumnName = "Selected"
                    .DataType = GetType(Boolean)
                    .DefaultValue = False
                End With
                tabla_incisos.Columns.Add(column)


                Session.Add("tabla_incisos", tabla_incisos)

                With gvAsignarPrecision
                    .DataSource = tbl
                    .DataBind()
                End With

            End If

        End With

    End Sub

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    Private Function LlenarEncabezadoPrecision(ByVal id_instrumento As Integer, ByVal codigo_inciso As String) As Boolean
        Dim estado As Boolean = False

        Dim objEncabezadoPrecision As New CNInstrumentosComerciales

        With objEncabezadoPrecision.SelectDatosEncabezadoPrecisionMant(id_instrumento)
            If Not .Tables(0).Rows.Count = 0 Then
                txt_version_enmienda_pnl.Text = .Tables(0).Rows(0)("enmienda").ToString()
                txt_periodo_año_inicial_pnl.Text = .Tables(0).Rows(0)("anio_inicia_enmienda").ToString()
                txt_periodo_año_final_pnl.Text = .Tables(0).Rows(0)("anio_fin_enmieda").ToString()
                estado = True
            Else
                estado = False
            End If

            If Not .Tables(1).Rows.Count = 0 Then
                Dim nombre_instrumento As String = .Tables(1).Rows(0)("nombre_instrumento").ToString()
                Dim sigla As String = .Tables(1).Rows(0)("sigla").ToString()

                txt_nombre_instrumento_pnl.Text = nombre_instrumento + " - " + sigla
                estado = True
            Else
                estado = False
            End If

            If Not .Tables(2).Rows.Count = 0 Then
                ddl_categoria_asignar_pnl.DataTextField = .Tables(2).Columns("codigo_categoria").ToString()
                ddl_categoria_asignar_pnl.DataValueField = .Tables(2).Columns("id_categoria").ToString()

                ddl_categoria_asignar_pnl.DataSource = .Tables(2)
                ddl_categoria_asignar_pnl.DataBind()
                estado = True
            Else
                estado = False
            End If

        End With

        Return estado
    End Function
#End Region


    
End Class