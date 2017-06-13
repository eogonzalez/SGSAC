Imports Reglas_del_negocio
Imports Capa_Entidad

Public Class frmAsignaPrecisionTLC
    Inherits System.Web.UI.Page
    Dim objCNAsignaPrecision As New CNAsignaPrecision
    Dim objCEAsignaPrecision As New CEIncisoAsociaCategoria
    Dim objCNAsignaCategoria As New CNAsignaCategoriasSAC
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

    Protected Sub lkBtn_Agregar_Precision_Click(sender As Object, e As EventArgs) Handles lkBtn_Agregar_Precision.Click
        MantPrecision("AGREGA")
    End Sub

    Protected Sub lkBtn_Eliminar_Precision_Click(sender As Object, e As EventArgs) Handles lkBtn_Eliminar_Precision.Click
        MantPrecision("ELIMINA")
    End Sub

    Protected Sub lkBtn_precision_Click(sender As Object, e As EventArgs) Handles lkBtn_precision.Click
        MantPrecision("EDITA")
    End Sub

    Protected Sub gvAsignarPrecision_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignarPrecision.PageIndexChanging
        gvAsignarPrecision.PageIndex = e.NewPageIndex
        Dim tabla_incisos As DataTable
        tabla_incisos = Session("tabla_incisos")

        With gvAsignarPrecision
            .DataSource = tabla_incisos
            .DataBind()
        End With

    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        LimpiarPrecisionMant()
        LlenarSeleccionCodigoInciso(Session("id_instrumento"), Session("codigo_arancel"))
        LlenarPrecisionMant(Session("id_instrumento"))
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If GuardarPrecision() Then
            Mensaje("Precisión agregada con éxito.")
            LlenarPrecisionMant(Session("id_instrumento"))
            LlenarSeleccionCodigoInciso(Session("id_instrumento"), txt_codigo_arancel.Text)
            LimpiarPrecisionMant()
        Else
            Mensaje("Error al agregar precisión.")
            lkBtn_Precision_ModalPopupExtender.Show()
        End If
    End Sub

    Protected Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        Response.Redirect("~/frmTratadosyAcuerdos.aspx")
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

    Function getIncisoPresicionGridView() As String
        Dim inciso_presicion As String = Nothing

        For i As Integer = 0 To gvAsignarPrecision.Rows.Count - 1
            Dim rbutton As RadioButton = gvAsignarPrecision.Rows(i).FindControl("rb_inciso")
            If rbutton.Checked Then

                If Not gvAsignarPrecision.Rows(i).Cells(5).Text = "&nbsp;" Then
                    inciso_presicion = gvAsignarPrecision.Rows(i).Cells(5).Text
                End If
                Exit For
            End If
        Next

        Return inciso_presicion
    End Function

    Function getIdCategoria() As Integer
        Dim id_categoria As Integer
        id_categoria = ddl_categoria_asignar_pnl.SelectedValue

        Return id_categoria
    End Function

    Function getCodigoPrecision() As String
        Return txt_codigo_precision_pnl.Text
    End Function

    Function getTextoPrecision() As String
        Return txt_precision_pnl.Text
    End Function

#End Region

#Region "Mis Funciones"

    'Metodo para el mantenimiento de precisiones
    Sub MantPrecision(ByVal accion As String)
        Dim codigo_inciso As String
        Dim id_instrumento As Integer
        Dim inciso_precision As String = Nothing

        codigo_inciso = getCodigoIncisoGridView()
        Session.Add("codigo_inciso", codigo_inciso)
        inciso_precision = getIncisoPresicionGridView()


        id_instrumento = Session("id_instrumento")

        If codigo_inciso = Nothing Then
            Mensaje("Seleccione un inciso para asignar precision.")
            LlenarPrecisionMant(id_instrumento)
        Else
            If accion = "ELIMINA" Then
                If inciso_precision Is Nothing Then
                    Mensaje("El Inciso seleccionado no tiene precision.")
                Else
                    If EliminaPrecision(id_instrumento, codigo_inciso, inciso_precision) Then
                        Mensaje("Inciso con precision eliminado con exito.")
                        LlenarSeleccionCodigoInciso(Session("id_instrumento"), txt_codigo_arancel.Text)
                    Else
                        Mensaje("Ha ocurrido un error al tratar de eliminar inciso con precision.")
                    End If
                End If
            Else
                If accion = "AGREGA" Then
                    If LlenarEncabezadoPrecision(accion, id_instrumento, codigo_inciso, inciso_precision) Then
                        lkBtn_Precision_ModalPopupExtender.Show()
                        'LlenarPrecisionMant(id_instrumento)
                    Else
                        Mensaje("No se puede obtener los datos para la precision del inciso.")
                    End If
                Else
                    If inciso_precision Is Nothing Then
                        Mensaje("El Inciso seleccionado no tiene precision.")
                    Else
                        Dim longitud_inciso_precision As Integer
                        longitud_inciso_precision = inciso_precision.Length

                        If longitud_inciso_precision = 0 Then
                            Mensaje("El Inciso seleccionado no tiene precision.")
                        Else
                            If LlenarEncabezadoPrecision(accion, id_instrumento, codigo_inciso, inciso_precision) Then
                                lkBtn_Precision_ModalPopupExtender.Show()
                                'LlenarPrecisionMant(id_instrumento)
                            Else
                                Mensaje("No se puede obtener los datos para la precision del inciso.")
                            End If
                        End If
                    End If

                End If

            End If

        End If
    End Sub

    Private Function EliminaPrecision(ByVal id_instrumento As Integer, ByVal codigo_inciso As String, ByVal inciso_precision As String) As Boolean

        objCEAsignaPrecision.id_instrumento = id_instrumento
        'objCEIncisoAsocia.id_categoria = getIdCategoria()
        objCEAsignaPrecision.codigo_inciso = codigo_inciso
        objCEAsignaPrecision.codigo_precision = inciso_precision

        Return objCNAsignaPrecision.DeletePrecision(objCEAsignaPrecision)
    End Function

    'Metodo para llenar los controles del Mantenimiento
    Sub LlenarPrecisionMant(ByVal id_instrumento As Integer)

        With objCNAsignaPrecision.SelectDatosAsignaPrecisionMant(id_instrumento)
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

        With objCNAsignaCategoria.SelectDatosCodigoInciso(id_instrumento, inciso)

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

    'Metodo que llena el panel de asignacion de precision
    Private Function LlenarEncabezadoPrecision(ByVal accion As String, ByVal id_instrumento As Integer, ByVal codigo_inciso As String, ByVal inciso_presicion As String) As Boolean
        Dim estado As Boolean = False

        With objCNAsignaPrecision.SelectDatosEncabezadoPrecisionMant(id_instrumento)
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



        With objCNAsignaPrecision.SelectIncisoPrecision(id_instrumento, codigo_inciso, inciso_presicion)
            Dim id_categoria As Integer = 0
            Dim codigo_precision As String = Nothing
            Dim texto_precision As String = Nothing

            If Not .Rows.Count = 0 Then
                txt_inciso_actual_pnl.Text = codigo_inciso
                txt_dai_actual_pnl.Text = .Rows(0)("dai_base").ToString()
                txt_descripcion_inciso_pnl.Text = .Rows(0)("texto_inciso").ToString()


                If Not IsDBNull(.Rows(0)("id_categoria")) Then
                    id_categoria = .Rows(0)("id_categoria")
                    ddl_categoria_asignar_pnl.SelectedValue = id_categoria
                End If

                txt_codigo_precision_pnl.Enabled = True

                If accion = "EDITA" Then
                    If Not IsDBNull(.Rows(0)("inciso_presicion")) Then
                        codigo_inciso = .Rows(0)("inciso_presicion")
                        txt_codigo_precision_pnl.Text = codigo_inciso.Substring(8)
                        txt_codigo_precision_pnl.Enabled = False
                    End If


                    If Not IsDBNull(.Rows(0)("texto_precision")) Then
                        texto_precision = .Rows(0)("texto_precision")
                        txt_precision_pnl.Text = texto_precision
                    End If

                End If

            End If
        End With


        Return estado
    End Function

    'Funcion para almacenar la precision
    Private Function GuardarPrecision() As Boolean
        objCEAsignaPrecision.id_instrumento = Session("id_instrumento")
        objCEAsignaPrecision.codigo_inciso = Session("codigo_inciso")
        objCEAsignaPrecision.id_categoria = getIdCategoria()
        objCEAsignaPrecision.codigo_precision = getCodigoPrecision()
        objCEAsignaPrecision.texto_precision = getTextoPrecision()

        Return objCNAsignaPrecision.InsertPrecision(objCEAsignaPrecision)
    End Function

    'Metodo que limpia panel de precision
    Private Sub LimpiarPrecisionMant()
        txt_version_enmienda_pnl.Text = ""
        txt_periodo_año_inicial_pnl.Text = ""
        txt_periodo_año_final_pnl.Text = ""
        txt_nombre_instrumento_pnl.Text = ""
        txt_inciso_actual_pnl.Text = ""
        txt_dai_actual_pnl.Text = ""
        txt_descripcion_inciso_pnl.Text = ""
        txt_codigo_precision_pnl.Text = ""
        txt_precision_pnl.Text = ""
        'txt_observaciones_pnl.Text = ""
    End Sub

#End Region

End Class