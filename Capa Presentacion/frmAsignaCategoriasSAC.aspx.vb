Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmAsignaCategoriasSAC
    Inherits System.Web.UI.Page

#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim categoria_id As Integer
        Dim id_instrumento As Integer

        If Not IsPostBack Then
            id_instrumento = Request.QueryString("id_inst").ToString
            Session.Add("id_instrumento", id_instrumento)

            categoria_id = 0
            Session.Add("categoria_id", categoria_id)

            LlenarAsignaCategoriaMant(id_instrumento)

            'With gvAsignarCategorias
            '    .DataSource = tabla_incisos
            '    .DataBind()
            'End With

            Me.btn_asigna_categoria.Attributes.Add("onclick", "this.vale='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btn_asigna_categoria, ""))
        End If

    End Sub

    Protected Sub btn_seleccionar_Click(sender As Object, e As EventArgs) Handles btn_seleccionar.Click
        Dim categoria_id As Integer
        Dim codigo_arancel As String
        Dim id_instrumento As Integer

        'Ver la manera de no llamar asigna categoria'

        'Asigno Categoria_id
        categoria_id = getIdCategoria()
        Session("categoria_id") = categoria_id

        codigo_arancel = txt_codigo_arancel.Text
        Session.Add("codigo_arancel", codigo_arancel)

        id_instrumento = Session("id_instrumento")

        LlenarAsignaCategoriaMant(id_instrumento)
        LlenarSeleccionCodigoInciso(id_instrumento, codigo_arancel)
    End Sub

    'Metodo para cambiar el estado de los checkbox del gridview
    Protected Sub cb_seleccionar_todo_CheckedChanged(sender As Object, e As EventArgs)
        Dim tabla_incisos As DataTable
        tabla_incisos = Session("tabla_incisos")

        Dim check_all As System.Web.UI.WebControls.CheckBox = sender

        If check_all.Checked Then
            For i As Integer = 0 To gvAsignarCategorias.Rows.Count - 1
                Dim check_inciso As System.Web.UI.WebControls.CheckBox = gvAsignarCategorias.Rows(i).FindControl("cb_inciso")

                Dim fila As GridViewRow = CType(check_inciso.NamingContainer, GridViewRow)
                Dim codigo_inciso As String = ""

                codigo_inciso = fila.Cells(1).Text

                If Not check_inciso.Checked Then
                    check_inciso.Checked = True

                    'Recorro tabla incisos que almacena la seleccion local
                    'Para chequear en tabla local

                    For Each enc As DataRow In tabla_incisos.Rows
                        If enc("codigo_inciso") = codigo_inciso Then
                            enc("selected") = 1
                            Exit For
                        End If
                    Next

                End If
            Next
        Else
            For i As Integer = 0 To gvAsignarCategorias.Rows.Count - 1
                Dim check_inciso As System.Web.UI.WebControls.CheckBox = gvAsignarCategorias.Rows(i).FindControl("cb_inciso")

                Dim fila As GridViewRow = CType(check_inciso.NamingContainer, GridViewRow)
                Dim codigo_inciso As String = ""

                codigo_inciso = fila.Cells(1).Text

                If check_inciso.Checked Then
                    check_inciso.Checked = False

                    'Recorro tabla incisos que almacena la seleccion local
                    'Para des chequear en tabla local
                    For Each enc As DataRow In tabla_incisos.Rows
                        If enc("codigo_inciso") = codigo_inciso Then
                            enc("selected") = 0
                            Exit For
                        End If
                    Next

                End If
            Next
        End If


    End Sub

    'Metodo que almacena en datatable checkbox selecionado o no seleccionado
    Protected Sub cb_inciso_CheckedChanged(sender As Object, e As EventArgs)
        Dim check As CheckBox = CType(sender, CheckBox)
        Dim fila As GridViewRow = CType(check.NamingContainer, GridViewRow)
        Dim CodigoInciso As String = ""
        Dim tabla_incisos As DataTable
        tabla_incisos = Session("tabla_incisos")

        CodigoInciso = fila.Cells(1).Text

        If check.Checked Then

            For Each enc As DataRow In tabla_incisos.Rows
                If enc("codigo_inciso") = CodigoInciso Then
                    enc("Selected") = 1
                End If
            Next

        Else

            For Each enc As DataRow In tabla_incisos.Rows
                If enc("codigo_inciso") = CodigoInciso Then
                    enc("Selected") = 0
                End If
            Next

        End If
        Session("tabla_incisos") = tabla_incisos
    End Sub

    'Metodo para manejar gridview cuando se cambia de pagina
    Protected Sub gvAsignarCategorias_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignarCategorias.PageIndexChanging
        gvAsignarCategorias.PageIndex = e.NewPageIndex
        Dim tabla_incisos As DataTable
        tabla_incisos = Session("tabla_incisos")

        With gvAsignarCategorias
            .DataSource = tabla_incisos
            .DataBind()
        End With
        'Metodo para marcar checkbox del gridview cuando cambia de pagina
        Check_GridVew()

    End Sub

    Protected Sub btn_asigna_categoria_Click(sender As Object, e As EventArgs) Handles btn_asigna_categoria.Click
        Dim objCN_asigna As New CNInstrumentosComerciales
        Dim objCEAsigna As New CEIncisoAsociaCategoria

        Dim tabla_incisos As DataTable
        Dim id_instrumento As Integer

        Dim categoria_id As Integer
        Dim codigo_arancel As String


        tabla_incisos = Session("tabla_incisos")
        objCEAsigna.lista_incisos = tabla_incisos

        id_instrumento = Session("id_instrumento")
        objCEAsigna.id_instrumento = id_instrumento

        objCEAsigna.id_categoria = getIdCategoria()


        If objCN_asigna.InsertAsignaCategoria(objCEAsigna) Then

            Mensaje("Se asigno categoria con exito.")

            tabla_incisos = Nothing
            gvAsignarCategorias.DataSource = Nothing
            gvAsignarCategorias.DataBind()

            'Asigno categoria_id
            categoria_id = getIdCategoria()
            Session("categoria_id") = categoria_id
            codigo_arancel = Session("codigo_arancel")

            LlenarAsignaCategoriaMant(id_instrumento)
            LlenarSeleccionCodigoInciso(id_instrumento, codigo_arancel)

        Else
            Mensaje("Error al asignar categoria.")
        End If
    End Sub

    Protected Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        'tabla_incisos = Nothing
        Response.Redirect("~/frmTratadosyAcuerdos.aspx")
    End Sub

#End Region

#Region "Funciones para obtener valores"
    Function getIdCategoria() As Integer
        Dim id_categoria As Integer

        id_categoria = Convert.ToInt32(ddl_categoria_asignar.SelectedValue)

        Return id_categoria
    End Function

#End Region

#Region "Mis funciones"

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
        Dim categoria_id As Integer

        With objCNAsignaCat.SelectDatosAsignaCategoriaMant(id_instrumento)

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

            If Not .Tables(2).Rows.Count = 0 Then

                ddl_categoria_asignar.DataTextField = .Tables(2).Columns("codigo_categoria").ToString()
                ddl_categoria_asignar.DataValueField = .Tables(2).Columns("id_categoria").ToString()

                'Obtiene categoria seleccionada
                categoria_id = Session("categoria_id")

                If categoria_id > 0 Then
                    ddl_categoria_asignar.SelectedValue = categoria_id
                End If

                ddl_categoria_asignar.DataSource = .Tables(2)
                ddl_categoria_asignar.DataBind()
            End If

        End With
    End Sub

    'Metodo para marcar check del gridview cuando cambia de pagina
    Sub Check_GridVew()
        Dim tabla_incisos As DataTable
        tabla_incisos = Session("tabla_incisos")

        With gvAsignarCategorias
            'Recorro datatable
            For Each row As DataRow In tabla_incisos.Rows
                'Si la fila esta seleccionada
                If row("Selected") = True Then
                    'Recorro grid view para marcar check
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim codigo_dt As String
                        Dim codigo_gv As String
                        Dim check As CheckBox

                        codigo_dt = row("codigo_inciso")
                        codigo_gv = .Rows(i).Cells(1).Text
                        check = .Rows(i).FindControl("cb_inciso")
                        'Si el el codigo del datatable es igual al del grid view marcar y salir
                        If codigo_dt = codigo_gv Then
                            check.Checked = True
                            Session("tabla_incisos") = tabla_incisos
                            Exit For
                        End If
                    Next
                End If
            Next
        End With

    End Sub

    'Procedimiento para mostrar mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

#End Region


End Class