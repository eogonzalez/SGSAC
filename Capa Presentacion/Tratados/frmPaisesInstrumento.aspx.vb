Imports Reglas_del_negocio
Imports Capa_Entidad
Public Class frmPaisesIntrumento
    Inherits System.Web.UI.Page

    Dim objCapaNegocio As New CNInstrumentosComerciales

#Region "Funciones del Sistema"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PaisesInstrumento(Request.QueryString("id_inst"))
            btnGuardar.Attributes.Add("onclick", "this.value='Guardando Espere...';this.disabled=true;" & ClientScript.GetPostBackEventReference(btnGuardar, ""))
        End If
    End Sub

    Protected Sub lkBtn_Nuevo_Click(sender As Object, e As EventArgs) Handles lkBtn_Nuevo.Click
        DatosInstrumento(Request.QueryString("id_inst"))
        Llenar_Paises()
        mpePaises.Show()
        llenar_TipoSocio()
        llenar_RegionPais()
    End Sub

    Protected Sub ddlPaises_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaises.SelectedIndexChanged
        Dim tbl As New DataTable
        tbl = objCapaNegocio.Paises().Tables("Paises")

        Llenar_CodigoPais(ddlPaises.SelectedValue, tbl)
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Validar() Then
            If btnGuardar.CommandName = "editar" Then
                If Editar() Then
                    LimpiarFormulario()
                    Mensaje("País actualizado con éxito")
                    PaisesInstrumento(Request.QueryString("id_inst"))
                Else
                    Mensaje("Error al actualizar país")
                End If
            Else
                If Guardar() Then
                    LimpiarFormulario()
                    Mensaje("País guardado con éxito")
                    PaisesInstrumento(Request.QueryString("id_inst"))
                Else
                    Mensaje("Error al guardar país")
                End If
            End If
        Else
            mpePaises.Show()
        End If
    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        LimpiarFormulario()
    End Sub

    Protected Sub lkBtt_editar_Click(sender As Object, e As EventArgs) Handles lkBtt_editar.Click
        Dim Datos As String
        Dim DatosSplit As String()
        Dim idInstrumento As New Integer
        Dim idPais As New Integer
        Dim idTipoSocio As New Integer

        Datos = getIdInstrumentoGridView()
        DatosSplit = Datos.Split(New Char() {","c})

        For i As Integer = 0 To DatosSplit.Count - 1
            If i = 0 Then
                idInstrumento = DatosSplit(i)
            End If
            If i = 1 Then
                idPais = DatosSplit(i)
            End If
            If i = 2 Then
                idTipoSocio = DatosSplit(i)
            End If
        Next

        If idInstrumento = 0 Then

            Mensaje("Seleccione un instrumento.")
            Exit Sub
        Else

            DatosInstrumento(Request.QueryString("id_inst"))
            Llenar_Paises()
            mpePaises.Show()
            llenar_TipoSocio()
            llenar_RegionPais()

            Dim tbl As New DataTable
            tbl = objCapaNegocio.PaisesInstrumentoMant(idInstrumento, idPais, idTipoSocio).Tables("PaisesInstrumentoMant")

            For Each enc As DataRow In tbl.Rows
                ddlPaises.SelectedValue = enc("ID_PAIS")
                ddlRegionPais.SelectedValue = enc("CODIGO_BLOQUE_PAIS")
                ddlTipoSocio.SelectedValue = enc("ID_TIPO_SOCIO")
                txtFechaFirma.Text = enc("FECHA_FIRMA")
                txtFechaRatificacion.Text = enc("FECHA_RATIFICACION")
                txtFechaVigencia.Text = enc("FECHA_VIGENCIA")
                txtObservaciones.Text = enc("OBSERVACIONES")

                hfIdInstrumento.Value = enc("ID_INSTRUMENTO")
                hfIdPais.Value = enc("ID_PAIS")
                hfBloquePais.Value = enc("CODIGO_BLOQUE_PAIS")
                hfTipoSocio.Value = enc("ID_TIPO_SOCIO")
            Next

            btnGuardar.CommandName = "editar"
            mpePaises.Show()

        End If
    End Sub

#End Region

#Region "Mis Funciones"

    'Mensajes en el formulario
    Sub Mensaje(ByVal texto As String)
        Dim jv As String = "<script>alert('" & texto & "');</script>"
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", jv, False)
    End Sub

    Sub LimpiarFormulario()
        ddlPaises.ClearSelection()
        ddlTipoSocio.ClearSelection()
        ddlRegionPais.ClearSelection()
        txtObservaciones.Text = ""
        txtFechaFirma.Text = ""
        txtFechaRatificacion.Text = ""
        txtFechaVigencia.Text = ""
    End Sub

    Public Function Editar() As Boolean
        Dim objeto As New CeInstrumentoPais
        Dim objetoMant As New CeInstrumentoPaisMant

        objetoMant.idInstrumento = hfIdInstrumento.Value
        objetoMant.idPais = hfIdPais.Value
        objetoMant.idBloquePais = hfBloquePais.Value
        objetoMant.idTipoSocio = hfTipoSocio.Value

        objeto.idPais = ddlPaises.SelectedValue
        objeto.idTipoSocio = ddlTipoSocio.SelectedValue
        objeto.idBloquePais = ddlRegionPais.SelectedValue
        objeto.FechaFirma = txtFechaFirma.Text
        objeto.FechaRatificacion = txtFechaRatificacion.Text
        objeto.FechaVigencia = txtFechaVigencia.Text
        objeto.Observaciones = txtObservaciones.Text

        Return objCapaNegocio.ActualizarInstrumentoPais(objeto, objetoMant)
    End Function

    Public Function Guardar() As Boolean
        Dim objeto As New CeInstrumentoPais

        objeto.idInstrumento = Request.QueryString("id_inst")
        objeto.idPais = ddlPaises.SelectedValue
        objeto.idTipoSocio = ddlTipoSocio.SelectedValue
        objeto.idBloquePais = ddlRegionPais.SelectedValue
        objeto.FechaFirma = txtFechaFirma.Text
        objeto.FechaRatificacion = txtFechaRatificacion.Text
        objeto.FechaVigencia = txtFechaVigencia.Text
        objeto.Observaciones = txtObservaciones.Text

        Return objCapaNegocio.GuardarInstrumentoPais(objeto)

    End Function

    Public Function Validar() As Boolean
        Try
            'Validar si el pais ya fue ingresado
            If Not (hfIdPais.Value = ddlPaises.SelectedValue And hfTipoSocio.Value = ddlTipoSocio.SelectedValue And btnGuardar.CommandName = "editar") Then
                If ValidarPais(Request.QueryString("id_inst"), ddlPaises.SelectedValue, ddlTipoSocio.SelectedValue) Then
                    Throw New Exception("El país y tipo de socio seleccionado ya fueron ingresados para el instrumento " & txtNombre.Text)
                End If
            End If

            'Fecha de Firma
            If txtFechaFirma.Text.Trim = "" Then
                Throw New Exception("Fecha de firma es requerida")
                txtFechaFirma.Focus()
            End If
            If Not IsDate(txtFechaFirma.Text.Trim) Then
                Throw New Exception("Fecha de firma es incorrecta")
                txtFechaFirma.Focus()
            End If
            If Len(txtFechaFirma.Text.Trim) < 10 Then
                Throw New Exception("Fecha de firma es incorrecta")
                txtFechaFirma.Focus()
            End If

            'Fecha de ratificación
            If txtFechaRatificacion.Text.Trim = "" Then
                Throw New Exception("Fecha de ratificación es requerida")
                txtFechaRatificacion.Focus()
            End If
            If Not IsDate(txtFechaRatificacion.Text.Trim) Then
                Throw New Exception("Fecha de ratificación es incorrecta")
                txtFechaRatificacion.Focus()
            End If
            If Len(txtFechaRatificacion.Text.Trim) < 10 Then
                Throw New Exception("Fecha de ratificación es incorrecta")
                txtFechaRatificacion.Focus()
            End If

            'Fecha de vigencia
            If txtFechaVigencia.Text.Trim = "" Then
                Throw New Exception("Fecha de vigencia es requerida")
                txtFechaVigencia.Focus()
            End If
            If Not IsDate(txtFechaVigencia.Text.Trim) Then
                Throw New Exception("Fecha de vigencia es incorrecta")
                txtFechaVigencia.Focus()
            End If
            If Len(txtFechaVigencia.Text.Trim) < 10 Then
                Throw New Exception("Fecha de vigencia es incorrecta")
                txtFechaVigencia.Focus()
            End If

            If Len(txtObservaciones.Text) > 250 Then
                Throw New Exception("No es permitido ingresar mas de 250 carecteres en las observaciones")
                txtObservaciones.Focus()
            End If

            Return True
        Catch ex As Exception
            Mensaje(ex.Message)
            Return False
            mpePaises.Show()
        End Try

    End Function

    Private Function ValidarPais(ByVal idInstrumento As Integer, ByVal idPais As Integer, ByVal idTipoSocio As Integer) As Boolean
        Dim ingresado As Boolean = False

        For i As Integer = 0 To Me.gvPaisesInstrumento.Rows.Count - 1
            If idInstrumento = Me.gvPaisesInstrumento.Rows(i).Cells(0).Text _
                And idPais = Me.gvPaisesInstrumento.Rows(i).Cells(1).Text _
                And idTipoSocio = Me.gvPaisesInstrumento.Rows(i).Cells(2).Text Then
                ingresado = True
            End If
        Next

        Return ingresado
    End Function
    Sub PaisesInstrumento(ByVal IdInstrumento As Integer)
        Dim tbl As New DataTable
        tbl = objCapaNegocio.PaisesInstrumento(IdInstrumento).Tables("PaisesInstrumento")

        With Me.gvPaisesInstrumento
            .DataSource = tbl
            .DataBind()
        End With
    End Sub
    Sub DatosInstrumento(ByVal idInstrumento As Integer)
        Dim tbl As New DataTable
        tbl = objCapaNegocio.DatosInstrumento(idInstrumento).Tables("DatosInstrumento")

        For Each enc As DataRow In tbl.Rows
            txtNombre.Text = enc("nombre_instrumento")
            txtTipo.Text = enc("TipoInstrumento")
            txtSigla.Text = enc("sigla")
            txtSiglaAlterna.Text = enc("sigla_alternativa")
            txtAcuerdo.Text = enc("AcuerdoEntre")
        Next

    End Sub

    Sub Llenar_Paises()
        Dim tbl As New DataTable
        tbl = objCapaNegocio.Paises().Tables("Paises")

        With Me.ddlPaises
            .DataSource = tbl
            .DataValueField = "id_pais"
            .DataTextField = "nombre_pais"
            .DataBind()
        End With

        Llenar_CodigoPais(ddlPaises.SelectedValue, tbl)

    End Sub

    Sub Llenar_CodigoPais(ByVal idPais As Integer, ByVal Datos As DataTable)

        For Each Buscar As DataRow In Datos.Rows
            If idPais = Buscar("id_pais") Then
                txtCodigoPais.Text = Buscar("codigo_alfa")
            End If
        Next

    End Sub

    Sub llenar_TipoSocio()
        Dim tbl As New DataTable
        tbl = objCapaNegocio.TipoSocio().Tables("TipoSocio")

        With Me.ddlTipoSocio
            .DataSource = tbl
            .DataValueField = "id_tipo_socio"
            .DataTextField = "descripcion"
            .DataBind()
        End With
    End Sub

    Sub llenar_RegionPais()
        Dim tbl As New DataTable
        tbl = objCapaNegocio.RegionPais().Tables("RegionPais")

        With Me.ddlRegionPais
            .DataSource = tbl
            .DataValueField = "ID_BLOQUE_PAIS"
            .DataTextField = "DESCRIPCION"
            .DataBind()
        End With
    End Sub

    'Funcion que obtiene del grid el id del instrumento
    Function getIdInstrumentoGridView() As String
        Dim idInstrumento As New Integer
        Dim idPais As New Integer
        Dim idTipoSocio As New Integer

        For i As Integer = 0 To gvPaisesInstrumento.Rows.Count - 1
            Dim rbutton As RadioButton = gvPaisesInstrumento.Rows(i).FindControl("rblPais")
            If rbutton.Checked Then
                idInstrumento = gvPaisesInstrumento.Rows(i).Cells(0).Text
                idPais = gvPaisesInstrumento.Rows(i).Cells(1).Text
                idTipoSocio = gvPaisesInstrumento.Rows(i).Cells(2).Text
            End If
        Next

        Return idInstrumento & "," & idPais & "," & idTipoSocio

    End Function

#End Region

End Class