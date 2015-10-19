Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmCorrelacionSAC
    Inherits System.Web.UI.Page

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
            'tabla_incisos = Nothing
            'With gvAsignarCategorias
            '    .DataSource = tabla_incisos
            '    .DataBind()
            'End With

            LlenarCorrelacionMant()

            btnGuardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btnGuardar, ""))
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

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        If GuardarApertura() Then
            Mensaje("Apertura realizada con éxito.")
            LlenarCorrelacionMant()
            LlenarSeleccionCodigoInciso(txt_codigo_arancel.Text)
            LimpiarAperturaMant()

        Else
            Mensaje("Error al realizar apertura.")
            lkBtt_Nuevo_ModalPopupExtender.Show()
        End If
    End Sub

    Protected Sub lkBtn_Suprimir_Click(sender As Object, e As EventArgs) Handles lkBtn_Suprimir.Click
        Dim codigo_inciso As String
        codigo_inciso = getCodigoIncisoGridView()

        If Not codigo_inciso = Nothing Then
            If GuardarSupresion(codigo_inciso) Then
                Mensaje("Se realizo supresion con éxito.")
                LlenarCorrelacionMant()
                LlenarSeleccionCodigoInciso(txt_codigo_arancel.Text)
            Else
                Mensaje("No se puede realizar supresión.")
            End If
        Else
            Mensaje("Seleccione un inciso para realizar supresión.")
            LlenarCorrelacionMant()
        End If
    End Sub

    Protected Sub gvCorrelacion_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignarCategorias.PageIndexChanging
        gvAsignarCategorias.PageIndex = e.NewPageIndex
        Dim tabla_incisos As DataTable
        tabla_incisos = Session("tabla_incisos")

        With gvAsignarCategorias
            .DataSource = tabla_incisos
            .DataBind()
        End With

    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        LlenarCorrelacionMant()
    End Sub

    Protected Sub lkBtn_Elimar_Click(sender As Object, e As EventArgs) Handles lkBtn_Elimar.Click
        Dim codigo_inciso As String
        Dim inciso_correlacion As String
        codigo_inciso = getCodigoIncisoGridView()
        inciso_correlacion = getIncisoCorrelacionGridView().Replace("&nbsp;", "")

        If Not codigo_inciso = Nothing Then
            If EliminarAccion(codigo_inciso, inciso_correlacion) Then
                Mensaje("Se elimino accion con éxito.")
                LlenarCorrelacionMant()
                LlenarSeleccionCodigoInciso(txt_codigo_arancel.Text)
            Else
                Mensaje("No se puede realizar eliminacion de accion.")
            End If
        Else
            Mensaje("Seleccione un inciso para realizar supresion.")
            LlenarCorrelacionMant()
        End If
    End Sub

#End Region

#Region "Obtener valores de panel de apertura"

    Private Function getVersionActual() As Integer
        Dim anio_actual As Integer
        anio_actual = Session("anio_actual")

        If anio_actual > 0 Then
            Return anio_actual
        Else
            Return 0
        End If
        'Return Convert.ToInt32(txt_anio_actual.Text)
    End Function

    Private Function getNuevaVersion() As Integer
        Dim anio_nueva As Integer
        anio_nueva = Session("anio_nueva")

        If anio_nueva > 0 Then
            Return anio_nueva
        Else
            Return 0
        End If
        'Return Convert.ToInt32(txt_anio_nueva.Text)
    End Function

    Private Function getIncisoActual() As String
        Dim inciso_origen As String
        inciso_origen = Session("inciso_origen")

        Return inciso_origen
    End Function

    Private Function getDaiActual() As Decimal
        Dim dai_actual As Decimal
        dai_actual = Session("dai_actual")

        Return dai_actual
        'Return Convert.ToDecimal(txt_dai_actual.Text)
    End Function

    Private Function getIncisoNuevo() As String
        Return txt_inciso_nuevo.Text.ToString()
    End Function

    Private Function getDaiNuevo() As Decimal
        Return Convert.ToDecimal(txt_dai_nuevo.Text)
    End Function

    Private Function getNuevaDescripcion() As String
        Return txt_descripcion_inciso_nuevo.Text.ToString()
    End Function

    Private Function getFechaInicioVigencia() As Date
        Return Convert.ToDateTime(txt_Fecha_Inicio_Vigencia.Text.ToString())
    End Function

    Private Function getFechaFinVigencia() As Date
        Return Convert.ToDateTime(txt_Fecha_Fin_Vigencia.Text.ToString())
    End Function

    Private Function getNormativa() As String
        Return txt_base_normativa.Text.ToString()
    End Function

    Private Function getObservaciones() As String
        Return txt_observaciones.Text.ToString()
    End Function

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
                Dim tabla_incisos As New DataTable

                tbl = .Tables(3)
                tabla_incisos = .Tables(3)
                Session.Add("tabla_incisos", tabla_incisos)

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

    'Funcion que obtiene el inciso correlacion del gridview
    Function getIncisoCorrelacionGridView() As String
        Dim inciso_correlacion As String = Nothing

        For i As Integer = 0 To gvAsignarCategorias.Rows.Count - 1
            Dim rbutton As RadioButton = gvAsignarCategorias.Rows(i).FindControl("rb_inciso")
            If rbutton.Checked Then
                inciso_correlacion = gvAsignarCategorias.Rows(i).Cells(5).Text
            End If
        Next

        Return inciso_correlacion
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
        Dim id_version As Integer
        Dim anio_actual As Integer
        Dim anio_nueva As Integer
        Dim dai_actual As Decimal

        With objCorrelacion.SelectCorrelacionMant()
            If Not .Tables(0).Rows.Count = 0 Then
                id_version = Convert.ToInt32(.Tables(0).Rows(0)("id_version").ToString())
                Session.Add("id_version", id_version)

                txt_anio_actual.Text = .Tables(0).Rows(0)("anio_version").ToString()

                anio_actual = .Tables(0).Rows(0)("anio_version")
                Session.Add("anio_actual", anio_actual)

                txt_descripcion_actual.Text = .Tables(0).Rows(0)("descripcion").ToString()
                estado = True
            Else
                estado = False
            End If

            If Not .Tables(1).Rows.Count = 0 Then
                txt_anio_nueva.Text = .Tables(1).Rows(0)("anio_version").ToString()
                anio_nueva = .Tables(1).Rows(0)("anio_version")
                Session.Add("anio_nueva", anio_nueva)

                txt_descripcion_nueva.Text = .Tables(1).Rows(0)("descripcion").ToString()
                estado = True
            Else
                estado = False
            End If
        End With

        With objCorrelacion.SelectIncisoApertura(codigo_inciso)
            If Not .Rows.Count = 0 Then
                txt_inciso_actual.Text = codigo_inciso
                Session.Add("inciso_origen", codigo_inciso)

                txt_dai_actual.Text = .Rows(0)("dai_base").ToString()
                dai_actual = .Rows(0)("dai_base")
                Session.Add("dai_actual", dai_actual)

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

    'Funcion que almacena la apertura arancelaria
    Private Function GuardarApertura() As Boolean
        Dim objEnmiendas As New CNInstrumentosComerciales
        Dim objCorrelacion As New CEEnmiendas

        objCorrelacion.inciso_origen = getIncisoActual()
        objCorrelacion.inciso_nuevo = getIncisoNuevo()
        objCorrelacion.texto_inciso = getNuevaDescripcion()
        objCorrelacion.observaciones = getObservaciones()
        objCorrelacion.normativa = getNormativa()
        objCorrelacion.dai_base = getDaiActual()
        objCorrelacion.dai_nuevo = getDaiNuevo()
        objCorrelacion.anio_version = getVersionActual()
        objCorrelacion.anio_nueva_version = getNuevaVersion()
        objCorrelacion.id_version = Session("id_version")
        objCorrelacion.fecha_fin_vigencia = getFechaFinVigencia()
        objCorrelacion.fecha_inicia_vigencia = getFechaInicioVigencia()

        Return objEnmiendas.InsertApertura(objCorrelacion)

    End Function

    Private Sub LimpiarAperturaMant()
        txt_anio_actual.Text = ""
        txt_descripcion_actual.Text = ""
        txt_anio_nueva.Text = ""
        txt_descripcion_nueva.Text = ""
        txt_inciso_actual.Text = ""
        txt_dai_actual.Text = ""
        txt_descripcion_inciso.Text = ""
        txt_dai_nuevo.Text = ""
        txt_descripcion_inciso_nuevo.Text = ""
        txt_Fecha_Inicio_Vigencia.Text = ""
        txt_Fecha_Fin_Vigencia.Text = ""
        txt_base_normativa.Text = ""
        txt_observaciones.Text = ""
    End Sub

    Private Function GuardarSupresion(ByVal codigo_inciso As String) As Boolean
        Dim estado As Boolean = False
        If ObtieneValoresSuprimir(codigo_inciso) Then
            Dim objEnmiendas As New CNInstrumentosComerciales
            Dim objCorrelacion As New CEEnmiendas

            objCorrelacion.inciso_origen = getIncisoActual()
            objCorrelacion.dai_base = getDaiActual()
            objCorrelacion.anio_version = getVersionActual()
            objCorrelacion.anio_nueva_version = getNuevaVersion()
            objCorrelacion.id_version = Session("id_version")

            estado = objEnmiendas.InsertSupresion(objCorrelacion)
        Else
            estado = False
        End If
        Return estado
    End Function

    Private Function ObtieneValoresSuprimir(ByVal codigo_inciso As String) As Boolean
        Dim estado As Boolean = False
        Dim objCorrelacion As New CNInstrumentosComerciales
        Dim id_version As Integer
        Dim anio_actual As Integer
        Dim anio_nueva_version As Integer
        Dim dai_actual As Decimal

        With objCorrelacion.SelectCorrelacionMant()
            If Not .Tables(0).Rows.Count = 0 Then
                id_version = Convert.ToInt32(.Tables(0).Rows(0)("id_version").ToString())
                Session.Add("id_version", id_version)

                anio_actual = .Tables(0).Rows(0)("anio_version")
                Session.Add("anio_actual", anio_actual)

                estado = True
            Else
                estado = False

            End If

            If Not .Tables(1).Rows.Count = 0 Then
                anio_nueva_version = Convert.ToInt32(.Tables(1).Rows(0)("anio_version").ToString())
                Session.Add("anio_nueva", anio_nueva_version)

                estado = True
            Else
                estado = False
            End If

        End With

        With objCorrelacion.SelectIncisoApertura(codigo_inciso)
            If Not .Rows.Count = 0 Then

                Session.Add("inciso_origen", codigo_inciso)

                dai_actual = .Rows(0)("dai_base")
                Session.Add("dai_actual", dai_actual)

                estado = True
            Else
                estado = False
            End If
        End With
        Return estado
    End Function

    Private Function EliminarAccion(ByVal codigo_inciso As String, ByVal inciso_correlacion As String) As Boolean
        Dim estado As Boolean = False

        If ObtieneValoresSuprimir(codigo_inciso) Then
            Dim objCNCorrelacion As New CNInstrumentosComerciales
            Dim objCECorrelacion As New CEEnmiendas
            objCECorrelacion.inciso_origen = getIncisoActual()
            objCECorrelacion.inciso_nuevo = inciso_correlacion
            objCECorrelacion.anio_version = getVersionActual()
            objCECorrelacion.anio_nueva_version = getNuevaVersion()
            objCECorrelacion.id_version = Session("id_version")

            estado = objCNCorrelacion.DeleteAccion(objCECorrelacion)
        Else
            estado = False
        End If
        Return estado

    End Function

#End Region

End Class