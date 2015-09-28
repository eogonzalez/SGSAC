Imports Capa_Datos
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

    'Funcion para calcular el DAI 
    Public Function CalcularDAI(ByVal id_instrumento As Integer) As Boolean
        Return objCDInstrumentos.CalcularDAI(id_instrumento)
    End Function

    'Funcion para obtener datos para el formulario de calculo de DAI
    Public Function SelectInstrumentoCalculoDAI(ByVal id_instrumento As Integer) As DataSet
        Return objCDInstrumentos.SelectInstrumentoCalculoDAI(id_instrumento)
    End Function

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

    'Funcion para verificar si categoria ya esta asociada
    Public Function VerificaCategoriaAsocia(ByVal objCategoriaDesgrava As CECategoriaDesgravacion) As Boolean
        Return objCDInstrumentos.VerificaCategoriaAsocia(objCategoriaDesgrava)
    End Function

    'Funcion para eliminar Categoria
    Public Function DeleteCategoria(ByVal objCategoriaDesgrava As CECategoriaDesgravacion) As Boolean
        Return objCDInstrumentos.DeleteCategoria(objCategoriaDesgrava)
    End Function

    'Funcion para verificar si las categorias han sido aprobadas
    Public Function VerificaCategoriasEstado(ByVal id_instrumento As Integer) As Boolean
        Return objCDInstrumentos.VerificaCategoriasEstado(id_instrumento)
    End Function

    'Funcion para Aprobar Categoria
    Public Function ApruebaCategoria(ByVal id_instrumento As Integer) As Boolean
        Return objCDInstrumentos.ApruebaCategoria(id_instrumento)
    End Function

    'Funcion para obtener el nombre del instrumento y cantidad de categorias
    Public Function SelectInstrumentoCategoria(ByVal id_instrumento As Integer) As DataTable
        Return objCDInstrumentos.SelectInstrumentoCategoria(id_instrumento)
    End Function

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

#Region "Funciones y procedimientos para el Mantenimiento de Asignacion Categorias"

    'Funcion para Insertar las Asociaciones de categoria
    Public Function InsertAsignaCategoria(ByVal id_instrumento As Integer, ByVal id_categoria As Integer, ByVal dt_asocia As DataTable) As Boolean
        Return objCDInstrumentos.InsertAsignaCategoria(id_instrumento, id_categoria, dt_asocia)
    End Function

    'Funcion para obtener los datos para los codigos seleccionados
    Public Function SelectDatosCodigoInciso(ByVal id_instrumento As Integer, ByVal str_codigo As String) As DataSet
        Return objCDInstrumentos.SelectDatosCodigoInciso(id_instrumento, str_codigo)
    End Function

    'Funcion para obtener los datos del Mantenimiento de Asignacion de Categoria
    Public Function SelectDatosAsignaCategoriaMant(ByVal id_instrumento As Integer) As DataSet
        Return objCDInstrumentos.SelectDatosAsignaCategoriaMant(id_instrumento)
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

#Region "Funciones y procedimientos para el Mantenimiento de Enmiendas del SAC"

    'Funcion para obtener los datos para los codigos seleccionados
    Public Function SelectDatosCodigoIncisoCorrelacion(ByVal str_codigo As String) As DataSet
        Return objCDInstrumentos.SelectDatosCodigoIncisoCorrelacion(str_codigo)
    End Function

    'Funcion para obtener los datos del Mantenimiento de Correlacion 
    Public Function SelectCorrelacionMant() As DataSet
        Return objCDInstrumentos.SelectCorrelacionMant()
    End Function

    'Metodo para Insertar nueva Version SAC
    Public Function InsertVersionSAC(ByVal objVersionSAC As CEEnmiendas) As Boolean
        Return objCDInstrumentos.InsertVersionSAC(objVersionSAC)
    End Function

    'Metodo para Actualizar version SAC
    Public Function UpdateVersionSAC(ByVal objVersionSAC As CEEnmiendas) As Boolean
        Return objCDInstrumentos.UpdateVersionSAC(objVersionSAC)
    End Function

    'Funcion para seleccionar el SAC segun el id_version_sac
    Public Function SelectVersionSACMant(ByVal id_version_sac As Integer, ByVal anio_version As Integer) As DataTable
        Return objCDInstrumentos.SelectVersionSACMant(id_version_sac, anio_version)
    End Function

    'Funcion para llenar el GridView de Enmiendas del SAC
    Public Function SelectEnmiendas() As DataSet
        Return objCDInstrumentos.SelectEnmiendas()
    End Function

    'Funcion que verifica si existe version pendiente de aprobar
    Public Function ExisteVersionSACPendiente() As Boolean
        Return objCDInstrumentos.ExisteVersionSACPendiente()
    End Function

    'Funcion que verifica cuantas versiones de sac pendiente existen
    Public Function CantidadVersionesSACPendientes() As Integer
        Return objCDInstrumentos.CantidadVersionesSACPendientes()
    End Function

    'Funcion que obtiene los datos para el mantenimiento de apertura arancelaria
    Public Function SelectIncisoApertura(ByVal codigo_inciso As String) As DataTable
        Return objCDInstrumentos.SelectIncisoApertura(codigo_inciso)
    End Function

    'Funcion que almacena la correlacion del inciso seleccionado
    Public Function InsertApertura(ByVal objCorrelacion As CEEnmiendas) As Boolean
        Return objCDInstrumentos.InsertApertura(objCorrelacion)
    End Function

    'Funcion para almacenar la supresion del iniciso seleccionado
    Public Function InsertSupresion(ByVal objCorrelacion As CEEnmiendas) As Boolean
        Return objCDInstrumentos.InsertSupresion(objCorrelacion)
    End Function

#End Region

#Region "Funciones y procedimientos para el Mantenimiento de Asignacion de Precision"

    'Funcion para obtener los datos del Mantenimiento de Asigna Precision
    Public Function SelectDatosAsignaPrecisionMant(ByVal id_instrumento As Integer) As DataSet
        'Se reutiliza ws del mantenimiento Asignacion de categoria 
        Return objCDInstrumentos.SelectDatosAsignaCategoriaMant(id_instrumento)
    End Function

    'Funcion para obtener los datos para el mantenimiento de precision
    Public Function SelectDatosEncabezadoPrecisionMant(ByVal id_instrumento As Integer) As DataSet
        'Se reutiliza ws del mantenimiento de asignacion de categoria
        Return objCDInstrumentos.SelectDatosAsignaCategoriaMant(id_instrumento)
    End Function

#End Region

End Class
