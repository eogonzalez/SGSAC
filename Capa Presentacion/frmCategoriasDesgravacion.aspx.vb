Imports Capa_Entidad
Imports Reglas_del_negocio
Public Class frmCategoriasDesgravacion
    Inherits System.Web.UI.Page
    Dim objCNInstrumentos As New CNInstrumentosComerciales
#Region "Funciones del sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hfIdInstrumento.Value = Request.QueryString("id_inst").ToString

            LlenargvCategorias()
            LlenarTipoDesgravacion()

            Me.btn_Guardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & Me.GetPostBackEventReference(Me.btn_Guardar))
        End If
    End Sub

    Protected Sub lkBtn_Editar_Click(sender As Object, e As EventArgs) Handles lkBtn_Editar.Click
        Dim id_categoria As Integer = 0
        id_categoria = Convert.ToInt32(getIdCategoriaGridView)

        If id_categoria = 0 Then
            Mensaje("Seleccione una categoria.")
            Exit Sub
        Else
            LlenarCategoriasMant("editar", id_categoria, hfIdInstrumento.Value)
            btn_Guardar.CommandName = "editar"
            hfIdCategoria.Value = id_categoria
            lkBtt_Nuevo_ModalPopupExtender.Show()
        End If

    End Sub

    Protected Sub btn_Guardar_Click(sender As Object, e As EventArgs) Handles btn_Guardar.Click
        If btn_Guardar.CommandName = "editar" Then
            If EditarCategoria(hfIdInstrumento.Value, hfIdCategoria.Value) Then
                Mensaje("Categoria actualizada con éxito.")
                LlenargvCategorias()
                btn_Guardar.CommandName = ""
                LimpiarMantenimientoCategoria()
            Else
                Mensaje("Error al actualizar categoria")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If
        Else
            If GuardarCategoriaDesgrava() Then
                Mensaje("Categoria agregada con éxito.")
                LlenargvCategorias()
                LimpiarMantenimientoCategoria()

            Else
                Mensaje("Error al agregar categoria.")
                lkBtt_Nuevo_ModalPopupExtender.Show()
            End If
        End If
    End Sub

    Protected Sub lkBtn_Config_Click(sender As Object, e As EventArgs) Handles lkBtn_Config.Click

    End Sub

#End Region

#Region "Funciones para obtener valores de los controles"
    Function getCodigoCategoria() As String
        Return txtCategoria.Text
    End Function

    Function getCantidadTramos() As Integer
        Return Convert.ToInt32(txtCantidadTramos.Text)
    End Function

    Function getTipoDesgravacion() As Integer
        Return Convert.ToInt32(ddl_tipo_desgravacion.SelectedValue)
    End Function

    Function getObservaciones() As String
        Return txtObservaciones.Text
    End Function
#End Region

#Region "Mis Funciones"

    'Funcion  para actualizar categoria
    Protected Function EditarCategoria(ByVal id_instrumento As Integer, ByVal id_categoria As Integer) As Boolean
        'Declaro las variables de la capa de datos y entidad
        Dim CEObjeto As New CECategoriaDesgravacion
        Dim CNCategoria As New CNInstrumentosComerciales

        'Obtengo los valores de los controles
        CEObjeto.id_instrumento = id_instrumento
        CEObjeto.id_categoria = hfIdCategoria.Value
        CEObjeto.codigo_categoria = getCodigoCategoria()
        CEObjeto.id_tipo_desgravacion = getTipoDesgravacion()
        CEObjeto.cantidad_tramos = getCantidadTramos()
        CEObjeto.observaciones = getObservaciones()

        Return CNCategoria.UpdateCategoriaDesgrava(CEObjeto)

    End Function

    'Funcion para llenar los controles con el id_categoria
    Sub LlenarCategoriasMant(ByVal accion As String, ByVal id_categoria As Integer, ByVal id_instrumento As Integer)
        Dim objCe_Categorias As New CECategoriaDesgravacion
        Dim objCN_Categorias As New CNInstrumentosComerciales

        Dim datosCategoria As New DataTable
        datosCategoria = objCN_Categorias.SelectCategoriaDesgravaMant(id_categoria, id_instrumento)

        If datosCategoria.Rows.Count = 0 Then
            Mensaje("Categoria no existe")
            Exit Sub
        Else
            If accion = "editar" Then
                txtCategoria.Text = datosCategoria.Rows(0)("codigo_categoria").ToString
                ddl_tipo_desgravacion.DataValueField = datosCategoria.Rows(0)("id_tipo_desgrava")
                txtCantidadTramos.Text = datosCategoria.Rows(0)("cantidad_tramos").ToString
                txtObservaciones.Text = datosCategoria.Rows(0)("observaciones").ToString

            End If
        End If

    End Sub

    'Funcion que obtiene del grid el id de la categoria
    Function getIdCategoriaGridView() As String
        Dim id_categoria As String = Nothing

        For i As Integer = 0 To gvCategorias.Rows.Count - 1
            Dim rbutton As RadioButton = gvCategorias.Rows(i).FindControl("rb_categoria")
            If rbutton.Checked Then
                id_categoria = gvCategorias.Rows(i).Cells(0).Text
            End If
        Next
        Return id_categoria
    End Function

    'Guardar nueva categoria
    Protected Function GuardarCategoriaDesgrava() As Boolean
        'Declaro los objetos de las clases de datos y negocio
        Dim objCN_Categorias As New CNInstrumentosComerciales
        Dim objCE_Categorias As New CECategoriaDesgravacion

        'Obtengo valores de los controles
        objCE_Categorias.id_instrumento = hfIdInstrumento.Value
        objCE_Categorias.codigo_categoria = getCodigoCategoria()
        objCE_Categorias.cantidad_tramos = getCantidadTramos()
        objCE_Categorias.id_tipo_desgravacion = getTipoDesgravacion()
        objCE_Categorias.observaciones = getObservaciones()

        'Envio el objeto al metodo para insertar categorias y tramos
        Return objCN_Categorias.InsertCategoriaDesgrava(objCE_Categorias)
    End Function

    'Procedimiento para llenar el combo de tipo de desgravacion
    Sub LlenarTipoDesgravacion()
        Dim objCNCategoria As New CNInstrumentosComerciales

        With objCNCategoria.SelectTipoDesgravacion
            ddl_tipo_desgravacion.DataTextField = .Tables(0).Columns("descripcion").ToString()
            ddl_tipo_desgravacion.DataValueField = .Tables(0).Columns("id_tipo_desgrava").ToString()
            ddl_tipo_desgravacion.DataSource = .Tables(0)
            ddl_tipo_desgravacion.DataBind()

        End With
    End Sub

    'Limpiar formulario Categoria
    Sub LimpiarMantenimientoCategoria()
        txtCategoria.Text = ""
        txtCantidadTramos.Text = ""
        ddl_tipo_desgravacion.ClearSelection()
        txtObservaciones.Text = ""
    End Sub

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    'Metodo para llenar el gvCategorias
    Protected Sub LlenargvCategorias()
        Dim dtbl As DataTable
        Dim id_instrumento As Integer = hfIdInstrumento.Value

        dtbl = objCNInstrumentos.SelectCategoriasDesgrava(id_instrumento)

        With gvCategorias
            .DataSource = dtbl
            .DataBind()
        End With


    End Sub

#End Region


End Class