﻿Imports Capa_Datos
Imports Capa_Entidad
Public Class CNInstrumentosComerciales
    Dim objCDInstrumentos As New CDInstrumentosComerciales
    Dim objCDGeneral As New General

#Region "Funciones y procedimientos para el Mantenimiento de Tipo de Desgravacion"

    'Metodo para insertar tipo de desgravacion
    Public Function InsertTipoDesgravacion(ByVal objTipoDesgravacion As CeTipoDesgravacion) As Boolean
        Return objCDInstrumentos.InsertTipoDesgravacion(objTipoDesgravacion)
    End Function

    'Funcion para seleccionar tipo de desgravacion segun el id_tipoDesgravacion
    Public Function SelectTipoDesgravacionMant(ByVal id_tipoDesgravacion As Integer) As DataTable
        Return objCDInstrumentos.SelectTipoDesgravacionMant(id_tipoDesgravacion)
    End Function

    'Metodo para actualizar tipo de desgravacion
    Public Function UpdateTipoDesgravacion(ByVal objTipoDesgravacion As CeTipoDesgravacion) As Boolean
        Return objCDInstrumentos.UpdateTipoDesgravacion(objTipoDesgravacion)
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Instrumentos"

    'Funcion para llenar el GridView de Instrumentos
    Public Function SelectInstrumentos() As DataSet
        Return objCDInstrumentos.SelectInstrumentos
    End Function

    'Funcion para seleccionar listado del combo tipo de instrumetos
    Public Function SelectTipoInstrumento() As DataSet
        Return objCDInstrumentos.SelectTipoInstrumento
    End Function

    'Funcion para seleccionar listado del combo tipo de relaciones de instrumentos
    Public Function SelectTipoRelacionInstrumento() As DataSet
        Return objCDInstrumentos.SelectTipoRelacionInstrumento
    End Function

    'Funcion para seleccionar el instrumento segun el id_instrumento
    Public Function SelectInstrumentoMant(ByVal id_instrumento As Integer) As DataTable
        Return objCDInstrumentos.SelectInstrumentosMant(id_instrumento)
    End Function

    'Metodo para Insertar nuevo instrumento comercial
    Public Function InsertInstrumento(ByVal objIns As CEInstrumentosMant) As Boolean
        Return objCDInstrumentos.InsertInstrumento(objIns)
    End Function

    'Metodo para Actualizar instrumento comercial
    Public Function UpdateInstrumento(ByVal objIns As CEInstrumentosMant) As Boolean
        Return objCDInstrumentos.UpdateInstrumento(objIns)
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Tipo de Instrumentos"

    'Metodo para insertar tipo de instrumento
    Public Function InsertTipoInstrumento(ByVal objTipoIns As CETipoInstrumento) As Boolean
        Return objCDInstrumentos.InsertTipoInstrumento(objTipoIns)
    End Function

    'Funcion para seleccionar tipo instrumento segun el id_tipoInstrumento
    Public Function SelectTipoInstrumentoMant(ByVal id_tipoInstrumento As Integer) As DataTable
        Return objCDInstrumentos.SelectTipoInstrumentoMant(id_tipoInstrumento)
    End Function

    'Metodo para actualizar tipo de instrumento
    Public Function UpdateTipoInstrumento(ByVal objTipoIns As CETipoInstrumento) As Boolean
        Return objCDInstrumentos.UpdateTipoInstrumento(objTipoIns)
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Tipo Relacion Instrumentos"

    'Metodo para insertar tipo relacion de instrumento
    Public Function InsertTipoRelacionInstrumento(ByVal objTipoRelIns As CETipoRelacionInstrumento) As Boolean
        Return objCDInstrumentos.InsertTipoRelacionInstrumento(objTipoRelIns)
    End Function

    'Funcion para obtener el tipo de relacion segun el id_tipoRelacionInstrumento
    Public Function SelectTipoRelacionInstrumentoMant(ByVal id_tipoRelacionInstrumento As Integer) As DataTable
        Return objCDInstrumentos.SelectTipoRelacionInstrumentoMant(id_tipoRelacionInstrumento)
    End Function

    'Metodo para actualizar tipo relacion de instrumento
    Public Function UpdateTipoRelacionInstrumento(ByRef objTipoRelIns As CETipoRelacionInstrumento) As Boolean
        Return objCDInstrumentos.UpdateTipoRelacionInstrumento(objTipoRelIns)
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Categorias de Desgravacion "
    'Funcion para actualizar categorias
    Public Function UpdateCategoriaDesgrava(ByVal objCeCategoria As CECategoriaDesgravacion) As Boolean
        Return objCDInstrumentos.UpdateCategoriaDesgrava(objCeCategoria)
    End Function

    'Funcion para seleccionar categoria segun id_categoria
    Public Function SelectCategoriaDesgravaMant(ByVal id_categoria As Integer, ByVal id_instrumento As Integer) As DataTable
        Return objCDInstrumentos.SelectCategoriaDesgravaMant(id_categoria, id_instrumento)
    End Function

    'Funcion para insertar nueva categoria y tramos 
    Public Function InsertCategoriaDesgrava(ByVal objCECategoria As CECategoriaDesgravacion) As Boolean
        Return objCDInstrumentos.InsertCategoriaDesgrava(objCECategoria)
    End Function

    'Funcion que selecciona tipo desgravacion
    Public Function SelectTipoDesgravacion() As DataSet
        Return objCDInstrumentos.SelectTipoDesgravacion()
    End Function

    'Funcion para seleccionar categorias segun el id_instrumento para llenar gvCategorias
    Public Function SelectCategoriasDesgrava(ByVal id_instrumento As Integer) As DataTable
        Return objCDInstrumentos.SelectCategoriasDesgrava(id_instrumento)
    End Function
#End Region

#Region "Funciones y procedimientos para el Mantenimientos de tramos de categorias"

    Public Function UpdateTramoCategoriaMant(ByVal ObjCETramo As CECorteDesgravacion) As Boolean
        Return objCDInstrumentos.UpdateTramoCategoriaMant(ObjCETramo)
    End Function

    'Funcion para seleccionar el tramo segun el id_instrumento, id_catetoria y id_tramo
    Public Function SelectTramoCategoriaMant(ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal id_tramo As Integer) As DataTable
        Return objCDInstrumentos.SelectTramoCategoriaMant(id_instrumento, id_categoria, id_tramo)
    End Function

    'Funciones que selecciona los tramos de la categoria
    Public Function SelectTramoCategoria(ByVal id_instrumento As Integer, ByVal id_categoria As Integer) As DataTable
        Return objCDInstrumentos.SelectTramoCategoria(id_instrumento, id_categoria)
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenieto de Tipo de Periodo"

    'Funcion para seleccionar listado del combo tipo de periodo
    Public Function SelectTipoPeriodo() As DataSet
        Return objCDInstrumentos.SelectTipoPeriodo()
    End Function

    'Metodo para insertar tipo de peiodo
    Public Function InsertTipoPeriodo(ByVal objTipoPeriodo As CETipoPeriodo) As Boolean
        Return objCDInstrumentos.InsertTipoPeriodo(objTipoPeriodo)
    End Function

    'Funcion para seleccionar tipo periodo segun el id_tipoPeriodo
    Public Function SelectTipoPeriodoMant(ByVal id_tipoPeriodo As Integer) As DataTable
        Return objCDInstrumentos.SelectTipoPeriodoMant(id_tipoPeriodo)
    End Function

    'Metodo para actualizar tipo de periodo
    Public Function UpdateTipoPeriodo(ByVal objTipoPeriodo As CETipoPeriodo) As Boolean
        Return objCDInstrumentos.UpdateTipoPeriodo(objTipoPeriodo)
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Paises Instrumentos"
    'Función para traer los paises instrumento
    Public Function PaisesInstrumento(ByVal IdInstrumento As Integer) As DataSet
        Return objCDInstrumentos.PaisesInstrumento(IdInstrumento)
    End Function

    'Función para mostrar los datos del intrumento para editarlo
    Public Function PaisesInstrumentoMant(ByVal idInstrumento As Integer, ByVal idPais As Integer, ByVal idTipoSocio As Integer) As DataSet
        Return objCDInstrumentos.PaisesInstrumentoMant(idInstrumento, idPais, idTipoSocio)
    End Function

    'Funcion para mostrar los datos del instrumento seleccionado
    Public Function DatosInstrumento(ByVal IdInstrumento As Integer) As DataSet
        Return objCDInstrumentos.DatosInstrumento(IdInstrumento)
    End Function

    'Función para listar el tipo de socio
    Public Function TipoSocio() As DataSet
        Return objCDInstrumentos.TipoSocios
    End Function

    'Función para listar los paises
    Public Function Paises() As DataSet
        Return objCDInstrumentos.Paises
    End Function

    'Función para listar el tipo de socio
    Public Function RegionPais() As DataSet
        Return objCDInstrumentos.RegionPais
    End Function

    'Función para guardar los paises instrumento
    Public Function GuardarInstrumentoPais(ByVal obj As CeInstrumentoPais) As Boolean
        Return objCDInstrumentos.GuardarInstrumentoPais(obj)
    End Function

    'Función para actualizar los paises instrumento
    Public Function ActualizarInstrumentoPais(ByVal obj As CeInstrumentoPais, ByVal objMant As CeInstrumentoPaisMant) As Boolean
        Return objCDInstrumentos.ActualizarInstrumentoPais(obj, objMant)
    End Function
#End Region
End Class
