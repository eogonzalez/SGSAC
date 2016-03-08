$(document).on("ready", inicio);
//function pageLoad() {
//    inicio();
//}

function inicio() {
    
    //Aplicar Validacion al texto del formulario 
    //Tipo Instrumento
    //Tipo Relacion Instrumento
    //Tipo Desgravacion
    //Tipo Periodo Corte
    $("#ContentPlaceHolder1_txtDescripcion").keyup(validarCatalogo);
    $("#ContentPlaceHolder1_txtDescripcion").focus(validarCatalogo);

    //$("#ContentPlaceHolder1_btnGuardar").click(function () {
    //    if (validarCatalogo() == true) {
    //        $("#ContentPlaceHolder1_btnGuardar").attr("disabled", "disabled");
    //        this.value = "Guardando espere...";
    //        return true;
    //    }
    //    else {
    //        return false;
    //    }
    //});

    var contenidoCatalogo = $("#ContentPlaceHolder1_txtDescripcion").val();
    if (contenidoCatalogo != null){
        validarCatalogo();
    };

    //Aplicar validacion formulario instrumento comercial
    $("#ContentPlaceHolder1_txtNombreInstrumento").keyup(validarInstrumento);
    $("#ContentPlaceHolder1_txtNombreInstrumento").focus(validarInstrumento);

    $("#ContentPlaceHolder1_txtSigla").keyup(validarInstrumento);
    $("#ContentPlaceHolder1_txtSigla").focus(validarInstrumento);

    $("#ContentPlaceHolder1_txtSiglaAlterna").keyup(validarInstrumento);
    $("#ContentPlaceHolder1_txtSiglaAlterna").focus(validarInstrumento);

    $("#ContentPlaceHolder1_txtObservaciones").keyup(validarInstrumento);
    $("#ContentPlaceHolder1_txtObservaciones").focus(validarInstrumento);

    $("#ContentPlaceHolder1_txtFechaFirma").keyup(validarInstrumento);
    $("#ContentPlaceHolder1_txtFechaFirma").focus(validarInstrumento);
    $("#ContentPlaceHolder1_txtFechaFirma").change(validarInstrumento);
    
    $("#ContentPlaceHolder1_txtFechaRatifica").keyup(validarInstrumento);
    $("#ContentPlaceHolder1_txtFechaRatifica").focus(validarInstrumento);
    $("#ContentPlaceHolder1_txtFechaRatifica").change(validarInstrumento);
    
    $("#ContentPlaceHolder1_txtFechaVigencia").keyup(validarInstrumento);
    $("#ContentPlaceHolder1_txtFechaVigencia").focus(validarInstrumento);
    $("#ContentPlaceHolder1_txtFechaVigencia").change(validarInstrumento);

    $("#ContentPlaceHolder1_btn_Guardar").click(validarInstrumento);
    //$("#ContentPlaceHolder1_btn_Guardar").enterKey(validarInstrumento);

    var contenidoInstrumento = $("#ContentPlaceHolder1_txtNombreInstrumento").val();
    if (contenidoInstrumento != null) {
        validarInstrumento();
    }

    //Aplicar validacion formulario Categorias
    $("#ContentPlaceHolder1_txtCategoria").keyup(validarCategoria);
    $("#ContentPlaceHolder1_txtCategoria").focus(validarCategoria);
    $("#ContentPlaceHolder1_txtCategoria").focusout(validarCategoria);

    var contenidoCategorias = $("#ContentPlaceHolder1_txtCategoria").val();
    if (contenidoCategorias != null) {
        validarCategoria();
    }

    $("#ContentPlaceHolder1_txtCantidadTramos").keyup(validarCategoria);
    
    //Aplicar validacion formulario configuracion de tramos
    $("#ContentPlaceHolder1_txt_cantidad_cortes").keyup(validarTramo);
    $("#ContentPlaceHolder1_txt_cantidad_cortes").focus(validarTramo);
    $("#ContentPlaceHolder1_txt_cantidad_cortes").focusout(validarTramo);

    $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").keyup(validarTramo);
    $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").focus(validarTramo);
    $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").focusout(validarTramo);

    $("#ContentPlaceHolder1_txt_porcen_desgrava_final").keyup(validarTramo);
    $("#ContentPlaceHolder1_txt_porcen_desgrava_final").focus(validarTramo);
    $("#ContentPlaceHolder1_txt_porcen_desgrava_final").focusout(validarTramo);

    var contenidoConftramo = $("#ContentPlaceHolder1_txt_cantidad_cortes").val();
    if (contenidoConftramo != null) {
        validarTramo();
    }

    //var contenidoDAnt = $("#ContentPlaceHolder1_txt_porcen_desgrava_anteriors").val();
    //if (contenidoDAnt != null) {
    //    validarTramo();
    //}

    //var contenidoDFinal = $("#ContentPlaceHolder1_txt_porcen_desgrava_final").val();
    //if (contenidoDFinal != null) {
    //    validarTramo();
    //}

    //Aplicar validacion asigna categoria
    //Aplicar validacion asigna precision
    $("#ContentPlaceHolder1_txt_codigo_arancel").keyup(validarAsigna);
    $("#ContentPlaceHolder1_txt_codigo_arancel").focus(validarAsigna);

    var contenidoAsigCat = $("#ContentPlaceHolder1_txt_codigo_arancel").val();
    if (contenidoAsigCat != null) {
        validarAsigna();
    }
    
    //Aplicar validacion asigna precision
    $("#ContentPlaceHolder1_txt_codigo_precision_pnl").keyup(validarAsignaPrecision);
    $("#ContentPlaceHolder1_txt_codigo_precision_pnl").focus(validarAsignaPrecision);

    $("#ContentPlaceHolder1_txt_precision_pnl").keyup(validarAsignaPrecision);
    $("#ContentPlaceHolder1_txt_precision_pnl").focus(validarAsignaPrecision);

    var contenidoAsigPrecision = $("#ContentPlaceHolder1_txt_codigo_precision_pnl").val();
    if (contenidoAsigPrecision != null) {
        validarAsignaPrecision();
    }

    //$("#ContentPlaceHolder1_btn_seleccionar").click(function () {
    //    $("#ContentPlaceHolder1_btn_asigna_categoria").attr("Class", "btn btn-primary");
    //});
    
    //Aplicar validacion enmienda SAC
    $("#ContentPlaceHolder1_txtAñoVersion").keyup(validarEnmienda);
    $("#ContentPlaceHolder1_txtAñoVersion").focus(validarEnmienda);

    var contenidoEnmienda = $("#ContentPlaceHolder1_txtAñoVersion").val();
    if (contenidoEnmienda != null) {
        validarEnmienda();
    }

    $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").keyup(validarEnmienda);
    $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").focus(validarEnmienda);

    //$("#ContentPlaceHolder1_txtFechaFinVigencia").keyup(validarEnmienda);
    //$("#ContentPlaceHolder1_txtFechaFinVigencia").focus(validarEnmienda);

    //Aplicar validacion apertura-correlacion SAC
    $("#ContentPlaceHolder1_txt_inciso_nuevo").keyup(validarApertura);
    $("#ContentPlaceHolder1_txt_inciso_nuevo").focus(validarApertura);

    $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").keyup(validarApertura);
    $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").focus(validarApertura);

    $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").keyup(validarApertura);
    $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").focus(validarApertura);

    var contenidoIncisoNuevo = $("#ContentPlaceHolder1_txt_inciso_nuevo").val();
    if (contenidoIncisoNuevo != null) {
        validarApertura();
    }

    var contenido_partida_apertura = $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").val();
    if (contenido_partida_apertura != null) {
        validarApertura();
    }

    var contenido_subpartida_apertura = $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").val();
    if (contenido_subpartida_apertura != null) {
        validarApertura();
    }

    //Aplicar validacion apertura nueva SAC
    $("#ContentPlaceHolder1_txt_inciso_new").keyup(validarAperturaNew);
    $("#ContentPlaceHolder1_txt_inciso_new").focus(validarAperturaNew);

    $("#ContentPlaceholder1_txt_descripcion_partida_new").keyup(validarAperturaNew);
    $("#ContentPlaceholder1_txt_descripcion_partida_new").focus(validarAperturaNew);

    $("#ContentPlaceholder1_txt_descripcion_SubPartida_new").keyup(validarAperturaNew);
    $("#ContentPlaceholder1_txt_descripcion_SubPartida_new").focus(validarAperturaNew);
    
    var contenidoIncisoNew = $("#ContentPlaceHolder1_txt_inciso_new").val();
    if (contenidoIncisoNew != null) {
        validarAperturaNew();
    }

    var contenido_partida_new = $("#ContentPlaceholder1_txt_descripcion_partida_new").val();
    if (contenido_partida_new != null) {
        validarAperturaNew();
    }

    var contenido_subpartida_new = $("#ContentPlaceHolder1_txt_descripcion_subpartida_new").val();
    if (contenido_subpartida_new != null) {
        validarAperturaNew();
    }

    //Aplicar validacion a Opciones del menu
    $("#ContentPlaceHolder1_txtNombreOpcion").keyup(validarOpcion);
    $("#ContentPlaceHolder1_txtNombreOpcion").focus(validarOpcion);

    $("#ContentPlaceHolder1_txtURL").keyup(validarOpcion);
    $("#ContentPlaceHolder1_txtURL").focus(validarOpcion);

    $("#ContentPlaceHolder1_txtOrden").keyup(validarOpcion);
    $("#ContentPlaceHolder1_txtOrden").focus(validarOpcion);

    var contenidoNombreOpcion = $("#ContentPlaceHolder1_txtNombreOpcion").val();
    if (contenidoNombreOpcion != null) {
        validarOpcion();
    }
    
    var contenidoURL = $("#ContentPlaceHolder1_txtURL").val();
    if (contenidoNombreOpcion != null) {
        validarOpcion();
    }

    var contenidoOrden = $("#ContentPlaceHolder1_txtOrden").val();
    if (contenidoNombreOpcion != null) {
        validarOpcion();
    }

    //Aplicar validacion partidas
    $("#ContentPlaceHolder1_txt_codigo_cap").keyup(validarPartidas);
    $("#ContentPlaceHolder1_txt_codigo_cap").focus(validarPartidas);

    var contenidoCodigoCap = $("#ContentPlaceHolder1_txt_codigo_cap").val();
    if (contenidoCodigoCap != null) {
        validarPartidas();
    }


    $("#ContentPlaceHolder1_txt_codigo_partida").keyup(validarPartidasMant);
    $("#ContentPlaceHolder1_txt_codigo_partida").focus(validarPartidasMant);

    $("#ContentPlaceHolder1_txt_descripcion_partida").keyup(validarPartidasMant);
    $("#ContentPlaceHolder1_txt_descripcion_partida").focus(validarPartidasMant);

    var contenidoPartida = $("#ContentPlaceHolder1_txt_codigo_partida").val();
    if (contenidoPartida != null) {
        validarPartidasMant();
    }

    var contenidoDescripPartida = $("#ContentPlaceHolder1_txt_descripcion_partida").val();
    if (contenidoDescripPartida != null){
        validarPartidasMant();
    }

    //Aplicar validaciones subpartidas
    $("#ContentPlaceHolder1_txt_codigo_subp").keyup(validarSubPartida);
    $("#ContentPlaceHolder1_txt_codigo_subp").focus(validarSubPartida);

    var contenidoCodigoSubp = $("#ContentPlaceHolder1_txt_codigo_subp").val();
    if (contenidoCodigoSubp != null){
        validarSubPartida();
    }

    $("#ContentPlaceHolder1_txt_codigo_subpartida").keyup(validarSubPartidasMant);
    $("#ContentPlaceHolder1_txt_codigo_subpartida").focus(validarSubPartidasMant);

    $("#ContentPlaceHolder1_txt_descripcion_subpartida").keyup(validarSubPartidasMant);
    $("#ContentPlaceHolder1_txt_descripcion_subpartida").focus(validarSubPartidasMant);

    var contenidoSubPartida = $("#ContentPlaceHolder1_txt_codigo_subpartida").val();
    if (contenidoSubPartida != null) {
        validarSubPartidasMant();
    }

    var contenidoDescripSubPartida = $("#ContentPlaceHolder1_txt_descripcion_subpartida").val();
    if (contenidoDescripSubPartida != null) {
        validarSubPartidasMant();
    }

}

function validarCatalogo() {
    //Validar Formulario
    //Tipo Instrumento
    //Tipo Relacion Instrumento
    //Tipo Desgravacion
    //Tipo Periodo Corte
    var valor = document.getElementById("ContentPlaceHolder1_txtDescripcion").value;
    
    
    if (valor == null || valor.length == 0 || /^\s+$/.test(valor)) {
        //Si esta vacio el campo
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_txtDescripcion").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtDescripcion").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtDescripcion").parent().append("<span id='iconotexto' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btnGuardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_btnGuardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtDescripcion").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtDescripcion").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtDescripcion").parent().append("<span id='iconotexto' class='glyphicon glyphicon-ok form-control-feedback'>");
        //$("#ContentPlaceHolder1_btnGuardar").prop("disabled", false);
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
        return true
    }
}

function validarInstrumento() {
    var nombre_instrumento = document.getElementById("ContentPlaceHolder1_txtNombreInstrumento").value;
    var sigla = document.getElementById("ContentPlaceHolder1_txtSigla").value;
    var sigla_alterna = document.getElementById("ContentPlaceHolder1_txtSiglaAlterna").value;
    var observaciones = document.getElementById("ContentPlaceHolder1_txtObservaciones").value;
    var fecha_firma = document.getElementById("ContentPlaceHolder1_txtFechaFirma").value;
    var fecha_ratifica = document.getElementById("ContentPlaceHolder1_txtFechaRatifica").value;
    var fecha_vigencia = document.getElementById("ContentPlaceHolder1_txtFechaVigencia").value;

    if (nombre_instrumento == null || nombre_instrumento.length == 0 || /^\s+$/.test(nombre_instrumento)) {
        //Si esta vacio el campo
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().append("<span id='iconotexto' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().append("<span id='iconotexto' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    if (sigla == null || sigla.length == 0 || /^\s+$/.test(sigla)) {
        //Si esta vacio el campo
        $("#iconotextosigla").remove();
        $("#ContentPlaceHolder1_txtSigla").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtSigla").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtSigla").parent().append("<span id='iconotextosigla' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextosigla").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtSigla").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtSigla").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtSigla").parent().append("<span id='iconotextosigla' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    if (sigla_alterna == null || sigla_alterna.length == 0 || /^\s+$/.test(sigla_alterna)) {
        //Si esta vacio el campo
        $("#iconotextosigla_alt").remove();
        $("#ContentPlaceHolder1_txtSiglaAlterna").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtSiglaAlterna").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtSiglaAlterna").parent().append("<span id='iconotextosigla_alt' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextosigla_alt").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtSiglaAlterna").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtSiglaAlterna").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtSiglaAlterna").parent().append("<span id='iconotextosigla_alt' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    if (observaciones == null || observaciones.length == 0 || /^\s+$/.test(observaciones)) {
        //Si esta vacio el campo
        $("#iconotextoobs").remove();
        $("#ContentPlaceHolder1_txtObservaciones").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtObservaciones").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtObservaciones").parent().append("<span id='iconotextoobs' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextoobs").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtObservaciones").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtObservaciones").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtObservaciones").parent().append("<span id='iconotextoobs' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    if (fecha_firma == null || fecha_firma.length == 0 || /^\s+$/.test(fecha_firma)) {
        //Si esta vacio el campo
        $("#iconotextofechafirma").remove();
        $("#ContentPlaceHolder1_txtFechaFirma").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtFechaFirma").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtFechaFirma").parent().append("<span id='iconotextofechafirma' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextofechafirma").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtFechaFirma").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtFechaFirma").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtFechaFirma").parent().append("<span id='iconotextofechafirma' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    if (fecha_ratifica == null || fecha_ratifica.length == 0 || /^\s+$/.test(fecha_ratifica)) {
        //Si esta vacio el campo
        $("#iconotextofecharatifica").remove();
        $("#ContentPlaceHolder1_txtFechaRatifica").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtFechaRatifica").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtFechaRatifica").parent().append("<span id='iconotextofecharatifica' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextofecharatifica").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtFechaRatifica").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtFechaRatifica").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtFechaRatifica").parent().append("<span id='iconotextofecharatifica' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    if (fecha_vigencia == null || fecha_vigencia.length == 0 || /^\s+$/.test(fecha_vigencia)) {
        //Si esta vacio el campo
        $("#iconotextofechavigencia").remove();
        $("#ContentPlaceHolder1_txtFechaVigencia").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtFechaVigencia").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtFechaVigencia").parent().append("<span id='iconotextofechavigencia' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextofechavigencia").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtFechaVigencia").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtFechaVigencia").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtFechaVigencia").parent().append("<span id='iconotextofechavigencia' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }
}

function validarCategoria() {
    var codigo_categoria = document.getElementById("ContentPlaceHolder1_txtCategoria").value;
    var cantidad_tramos = document.getElementById("ContentPlaceHolder1_txtCantidadTramos").value;

    if (codigo_categoria == null || codigo_categoria.length == 0 || /^\s+$/.test(codigo_categoria)) {
        //Si esta vacio el campo
        $("#iconotextocat").remove();
        $("#ContentPlaceHolder1_txtCategoria").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtCategoria").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtCategoria").parent().append("<span id='iconotextocat' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocat").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtCategoria").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtCategoria").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtCategoria").parent().append("<span id='iconotextocat' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    if (cantidad_tramos == null || cantidad_tramos.length == 0 || /^\s+$/.test(cantidad_tramos)) {
        //Si esta vacio el campo
        $("#iconotextocantidadtramos").remove();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().append("<span id='iconotextocantidadtramos' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else if (cantidad_tramos == 0){
        $("#iconotextocantidadtramos").remove();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().children("span").text("La cantidad de tramos debe ser mayor a cero.").show();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().append("<span id='iconotextocantidadtramos' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else if(isNaN(cantidad_tramos)){
        $("#iconotextocantidadtramos").remove();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().children("span").text("Debe ingresar caracteres numericos.").show();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().append("<span id='iconotextocantidadtramos' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_Guardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocantidadtramos").remove();
        $("#ContentPlaceHolder1_btn_Guardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().append("<span id='iconotextocantidadtramos' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }
}

function validarTramo() {
    var cantidad_cortes = document.getElementById("ContentPlaceHolder1_txt_cantidad_cortes").value;
    var periodo_anterior = document.getElementById("ContentPlaceHolder1_txt_porcen_desgrava_anterior").value;
    var periodo_final = document.getElementById("ContentPlaceHolder1_txt_porcen_desgrava_final").value;

    if (cantidad_cortes == null || cantidad_cortes.length == 0 || /^\s+$/.test(cantidad_cortes)) {
        //Si esta vacio el campo
        $("#iconotextocantidadcortes").remove();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().append("<span id='iconotextocantidadcortes' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_genera_cortes").prop("disabled", true);
        return false;
    }
    else if (cantidad_cortes == 0) {
        //Cantidad de cortes debe de ser mayor a cero
        $("#iconotextocantidadcortes").remove();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().children("span").text("La cantidad de cortes debe ser mayor a cero.").show();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().append("<span id='iconotextocantidadcortes' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_genera_cortes").prop("disabled", true);
        return false;
    }
    else if (isNaN(cantidad_cortes)) {
        //El campo debe de ser numerico
        $("#iconotextocantidadcortes").remove();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().children("span").text("Debe ingresar caracteres numericos.").show();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().append("<span id='iconotextocantidadcortes' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_genera_cortes").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocantidadcortes").remove();
        $("#ContentPlaceHolder1_btn_genera_cortes").removeProp("disabled");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().append("<span id='iconotextocantidadcortes' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary");
    }

    if (periodo_anterior == null || periodo_anterior.length == 0 || /^\s+$/.test(periodo_anterior)) {
        //Si esta vacio el campo
        $("#iconotexto_periodo_anterior").remove();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().append("<span id='iconotexto_periodo_anterior' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_genera_cortes").prop("disabled", true);
        return false;
    }
    else if (isNaN(periodo_anterior)) {
        //El campo debe de ser numerico
        $("#iconotexto_periodo_anterior").remove();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().children("span").text("Debe ingresar caracteres numericos.").show();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().append("<span id='iconotexto_periodo_anterior' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_genera_cortes").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto_periodo_anterior").remove();
        $("#ContentPlaceHolder1_btn_genera_cortes").removeProp("disabled");
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_anterior").parent().append("<span id='iconotexto_periodo_anterior' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary");
    }

    if (periodo_final == null || periodo_final.length == 0 || /^\s+$/.test(periodo_final)) {
        //Si esta vacio el campo
        $("#iconotexto_periodo_final").remove();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().append("<span id='iconotexto_periodo_final' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_genera_cortes").prop("disabled", true);
        return false;
    }
    else if (isNaN(periodo_final)) {
        //El campo debe de ser numerico
        $("#iconotexto_periodo_final").remove();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().children("span").text("Debe ingresar caracteres numericos.").show();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().append("<span id='iconotexto_periodo_final' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btn_genera_cortes").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto_periodo_final").remove();
        $("#ContentPlaceHolder1_btn_genera_cortes").removeProp("disabled");
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_porcen_desgrava_final").parent().append("<span id='iconotexto_periodo_final' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary");
    }
}

function validarAsigna() {
    var codigo = document.getElementById("ContentPlaceHolder1_txt_codigo_arancel").value;

    if (codigo == null || codigo.length == 0 || /^\s+$/.test(codigo)) {
        //Si esta vacio el campo
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().children("span").text("Para seleccionar, el campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary disabled");
        //$("#ContentPlaceHolder1_btn_asigna_categoria").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (codigo.length < 2) {
        //Cantidad de cortes debe de ser mayor a cero
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().children("span").text("Para seleccionar, la cantidad de digitos debe ser mayor a uno.").show();
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary disabled");
        //$("#ContentPlaceHolder1_btn_asigna_categoria").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_codigo_arancel").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary");
    }
}

function validarAsignaPrecision() {
    var codigo = document.getElementById("ContentPlaceHolder1_txt_codigo_precision_pnl").value;
    var texto = document.getElementById("ContentPlaceHolder1_txt_precision_pnl").value;

    if (codigo == null || codigo.length == 0 || /^\s+$/.test(codigo)) {
        //Si esta vacio el campo
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_precision_pnl").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_precision_pnl").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_codigo_precision_pnl").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btnGuardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_btnGuardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txt_codigo_precision_pnl").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_precision_pnl").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_codigo_precision_pnl").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }

    if (texto == null || texto.length == 0 || /^\s+$/.test(texto)) {
        //Si esta vacio el campo
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_txt_precision_pnl").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_precision_pnl").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_precision_pnl").parent().append("<span id='iconotexto' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
        $("#ContentPlaceHolder1_btnGuardar").prop("disabled", true);
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_btnGuardar").removeProp("disabled");
        $("#ContentPlaceHolder1_txt_precision_pnl").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txt_precision_pnl").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_precision_pnl").parent().append("<span id='iconotexto' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }
}

function validarEnmienda() {
    var anio_version = document.getElementById("ContentPlaceHolder1_txtAñoVersion").value;
    var descripcion = document.getElementById("ContentPlaceHolder1_txt_Descripcion_Enmienda").value;

    //var fecha_inicio = document.getElementById("ContentPlaceHolder1_txtFechaInicioVigencia").value;
    //var fecha_fin = document.getElementById("ContentPlaceHolder1_txtFechaFinVigencia").value;

    if (anio_version == null || anio_version.length == 0 || /^\s+$/.test(anio_version)) {
        //Si esta vacio el campo
        $("#iconotextoanio").remove();
        $("#ContentPlaceHolder1_txtAñoVersion").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtAñoVersion").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtAñoVersion").parent().append("<span id='iconotextoanio' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (anio_version.length <= 3) {
        //Cantidad de cortes debe de ser de 4 digitos
        $("#iconotextoanio").remove();
        $("#ContentPlaceHolder1_txtAñoVersion").parent().parent().attr("class", "form-group has-error has-feedback");
        //$("#ContentPlaceHolder1_txtAñoVersion").parent().children("span").text("La cantidad de cortes debe ser mayor a cero.").show();
        $("#ContentPlaceHolder1_txtAñoVersion").parent().append("<span id='iconotextoanio' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (isNaN(anio_version)) {
        //El campo debe de ser numerico
        $("#iconotextoanio").remove();
        $("#ContentPlaceHolder1_txtAñoVersion").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtAñoVersion").parent().children("span").text("Debe ingresar caracteres numericos.").show();
        $("#ContentPlaceHolder1_txtAñoVersion").parent().append("<span id='iconotextoanio' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextoanio").remove();
        $("#ContentPlaceHolder1_txtAñoVersion").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtAñoVersion").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtAñoVersion").parent().append("<span id='iconotextoanio' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    if (descripcion == null || descripcion.length == 0 || /^\s+$/.test(descripcion)) {
        //Si esta vacio el campo
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").parent().append("<span id='iconotexto' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").parent().append("<span id='iconotexto' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }

    //if (fecha_fin.getYear() < fecha_inicio.getYear()){
    //    $("#iconotexto").remove();
    //    $("#ContentPlaceHolder1_txtFechaFinVigencia").parent().parent().attr("class", "form-group has-error has-feedback");
    //    $("#ContentPlaceHolder1_txtFechaFinVigencia").parent().children("span").text("La fecha final no puede ser menor que la inicial.").show();
    //    $("#ContentPlaceHolder1_txtFechaFinVigencia").parent().append("<span id='iconotexto' class='glyphicon glyphicon-remove form-control-feedback'>");
    //    $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
    //    return false;
    //}
    //else {
    //    $("#iconotexto").remove();
    //    $("#ContentPlaceHolder1_txtFechaFinVigencia").parent().parent().attr("class", "form-group has-success has-feedback");
    //    $("#ContentPlaceHolder1_txtFechaFinVigencia").parent().children("span").text("").hide();
    //    $("#ContentPlaceHolder1_txtFechaFinVigencia").parent().append("<span id='iconotexto' class='glyphicon glyphicon-ok form-control-feedback'>");
    //    $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    //}
}

function validarApertura(){
    var inciso_nuevo = document.getElementById("ContentPlaceHolder1_txt_inciso_nuevo").value;
    var descripcion_partida = document.getElementById("ContentPlaceHolder1_txt_descripcion_partida_apertura").value;
    var descripcion_subpartida = document.getElementById("ContentPlaceHolder1_txt_descripcion_subpartida_apertura").value;


    if (inciso_nuevo == null || inciso_nuevo.length == 0 || /^\s+$/.test(inciso_nuevo) ) {
        //Si esta vacio el campo
        $("#iconotextoinciso").remove();
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().append("<span id='iconotextoinciso' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (inciso_nuevo.length != 8) {
        //Cantidad de cortes debe de ser de 4 digitos
        $("#iconotextoinciso").remove();
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().children("span").text("El inciso debe ser de 8 digitos.").show();
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().append("<span id='iconotextoinciso' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
    }
    else {
        //Si no esta vacio
        $("#iconotextoinciso").remove();
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_inciso_nuevo").parent().append("<span id='iconotextoinciso' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }

    if (descripcion_partida == null || descripcion_partida.length == 0 || /^\s+$/.test(descripcion_partida)) {
        //Si esta vacio el campo
        $("#iconotexto_partida").remove();
        $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").parent().children("span").text("Ingrese Partida primero.").show();
        $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").parent().append("<span id='iconotexto_partida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto_partida").remove();
        $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_descripcion_partida_apertura").parent().append("<span id='iconotexto_partida' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }

    if (descripcion_subpartida == null || descripcion_subpartida.length == 0 || /^\s+$/.test(descripcion_subpartida)) {
        //Si esta vacio el campo
        $("#iconotexto_subpartida").remove();
        $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").parent().children("span").text("Verifique si existe Subpartida.").show();
        $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").parent().append("<span id='iconotexto_subpartida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto_subpartida").remove();
        $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_descripcion_subpartida_apertura").parent().append("<span id='iconotexto_subpartida' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }
}

function validarAperturaNew() {
    var inciso_nuevo = document.getElementById("ContentPlaceHolder1_txt_inciso_new").value;
    var descripcion_partida = document.getElementById("ContentPlaceHolder1_txt_descripcion_partida_new").value;
    var descripcion_subpartida = document.getElementById("ContentPlaceHolder1_txt_descripcion_SubPartida_new").value;


    if (inciso_nuevo == null || inciso_nuevo.length == 0 || /^\s+$/.test(inciso_nuevo)) {
        //Si esta vacio el campo
        $("#iconotextoinciso").remove();
        $("#ContentPlaceHolder1_txt_inciso_new").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_inciso_new").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_inciso_new").parent().append("<span id='iconotextoinciso' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar_new").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (inciso_nuevo.length != 8) {
        //Cantidad de cortes debe de ser de 4 digitos
        $("#iconotextoinciso").remove();
        $("#ContentPlaceHolder1_txt_inciso_new").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_inciso_new").parent().children("span").text("El inciso debe ser de 8 digitos.").show();
        $("#ContentPlaceHolder1_txt_inciso_new").parent().append("<span id='iconotextoinciso' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar_new").attr("Class", "btn btn-primary disabled");
    }
    else {
        //Si no esta vacio
        $("#iconotextoinciso").remove();
        $("#ContentPlaceHolder1_txt_inciso_new").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txt_inciso_new").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_inciso_new").parent().append("<span id='iconotextoinciso' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar_new").attr("Class", "btn btn-primary");
    }

    if (descripcion_partida == null || descripcion_partida.length == 0 || /^\s+$/.test(descripcion_partida)) {
        //Si esta vacio el campo
        $("#iconotexto_partida").remove();
        $("#ContentPlaceHolder1_txt_descripcion_partida_new").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_partida_new").parent().children("span").text("Ingrese Partida primero.").show();
        $("#ContentPlaceHolder1_txt_descripcion_partida_new").parent().append("<span id='iconotexto_partida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar_new").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto_partida").remove();
        $("#ContentPlaceHolder1_txt_descripcion_partida_new").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_partida_new").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_descripcion_partida_new").parent().append("<span id='iconotexto_partida' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar_new").attr("Class", "btn btn-primary");
    }

    if (descripcion_subpartida == null || descripcion_subpartida.length == 0 || /^\s+$/.test(descripcion_subpartida)) {
        //Si esta vacio el campo
        $("#iconotexto_subpartida").remove();
        $("#ContentPlaceHolder1_txt_descripcion_SubPartida_new").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_SubPartida_new").parent().children("span").text("Verifique si existe Subpartida.").show();
        $("#ContentPlaceHolder1_txt_descripcion_SubPartida_new").parent().append("<span id='iconotexto_subpartida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar_new").attr("Class", "btn btn-primary");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto_subpartida").remove();
        $("#ContentPlaceHolder1_txt_descripcion_SubPartida_new").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_SubPartida_new").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_descripcion_SubPartida_new").parent().append("<span id='iconotexto_subpartida' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar_new").attr("Class", "btn btn-primary");
    }
}


function validarOpcion() {
    

    var nombre = document.getElementById("ContentPlaceHolder1_txtNombreOpcion").value;
    var URL= document.getElementById("ContentPlaceHolder1_txtURL").value;
    var orden = document.getElementById("ContentPlaceHolder1_txtOrden").value;


    if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
        //Si esta vacio el campo
        $("#iconotextonombrem").remove();
        $("#ContentPlaceHolder1_txtNombreOpcion").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtNombreOpcion").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtNombreOpcion").parent().append("<span id='iconotextonombrem' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
        
    }
    else {
        //Si no esta vacio
        $("#iconotextonombrem").remove();
        $("#ContentPlaceHolder1_txtNombreOpcion").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtNombreOpcion").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtNombreOpcion").parent().append("<span id='iconotextonombrem' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
        
    }

    if (URL == null || URL.length == 0 || /^\s+$/.test(URL)) {
        //Si esta vacio el campo
        $("#iconotextoURL").remove();
        $("#ContentPlaceHolder1_txtURL").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtURL").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtURL").parent().append("<span id='iconotextoURL' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
        
    }
    else {
        //Si no esta vacio
        $("#iconotextoURL").remove();
        $("#ContentPlaceHolder1_txtURL").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtURL").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtURL").parent().append("<span id='iconotextoURL' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
        
    }

    if (orden == null || orden.length == 0 || /^\s+$/.test(orden)) {
        //Si esta vacio el campo
        $("#iconotextoOrden").remove();
        $("#ContentPlaceHolder1_txtOrden").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtOrden").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtOrden").parent().append("<span id='iconotextoOrden' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
        
    }
    else {
        //Si no esta vacio
        $("#iconotextoOrden").remove();
        $("#ContentPlaceHolder1_txtOrden").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtOrden").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtOrden").parent().append("<span id='iconotextoOrden' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
        
    }
    return true;
}

function validarPartidas() {
    var codigo = document.getElementById("ContentPlaceHolder1_txt_codigo_cap").value;

    if (codigo == null || codigo.length == 0 || /^\s+$/.test(codigo)) {
        //Si esta vacio el campo
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().children("span").text("Para seleccionar, el campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary disabled");
        
        return false;
    }
    else if (codigo.length < 2) {
        //Cantidad de cortes debe de ser mayor a cero
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().children("span").text("Para seleccionar, la cantidad de digitos debe ser mayor a uno.").show();
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary disabled");
        //$("#ContentPlaceHolder1_btn_asigna_categoria").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (codigo.length > 4) {
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().children("span").text("Para seleccionar, la cantidad de digitos debe ser menor a cuatro.").show();
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary disabled");
        //$("#ContentPlaceHolder1_btn_asigna_categoria").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_codigo_cap").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary");
    }
}

function validarPartidasMant() {
    var partida = document.getElementById("ContentPlaceHolder1_txt_codigo_partida").value;
    var descripcion = document.getElementById("ContentPlaceHolder1_txt_descripcion_partida").value;

    if (partida == null || partida.length == 0 || /^\s+$/.test(partida)) {
        //Si esta vacio el campo
        $("#iconopartida").remove();
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().append("<span id='iconopartida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");

        
    }
    else if (isNaN(partida)){
        $("#iconopartida").remove();
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().children("span").text("El debe ser numerico.").show();
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().append("<span id='iconopartida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
    }
    else if (partida.length != 4){
        $("#iconopartida").remove();
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().children("span").text("El campo no puede ser menor o mayor de 4 digitos.").show();
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().append("<span id='iconopartida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
    }
    else {
        //Si no esta vacio
        $("#iconopartida").remove();
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_codigo_partida").parent().append("<span id='iconopartida' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }

    if (descripcion == null || descripcion.length == 0 || /^\s+$/.test(descripcion)) {
        //Si esta vacio el campo
        $("#iconodescripcion").remove();
        $("#ContentPlaceHolder1_txt_descripcion_partida").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_partida").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_descripcion_partida").parent().append("<span id='iconodescripcion' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");

        
    }
    else {
        //Si no esta vacio
        $("#iconodescripcion").remove();
        $("#ContentPlaceHolder1_txt_descripcion_partida").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_partida").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_descripcion_partida").parent().append("<span id='iconodescripcion' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }
}

function validarSubPartida() {
    var codigo = document.getElementById("ContentPlaceHolder1_txt_codigo_subp").value;

    if (codigo == null || codigo.length == 0 || /^\s+$/.test(codigo)) {
        //Si esta vacio el campo
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().children("span").text("Para seleccionar, el campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary disabled");

        return false;
    }
    else if (codigo.length < 2) {
        //Cantidad de cortes debe de ser mayor a cero
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().children("span").text("Para seleccionar, la cantidad de digitos debe ser mayor a uno.").show();
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary disabled");
        //$("#ContentPlaceHolder1_btn_asigna_categoria").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (codigo.length > 6) {
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().children("span").text("Para seleccionar, la cantidad de digitos debe ser menor a seis.").show();
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary disabled");
        //$("#ContentPlaceHolder1_btn_asigna_categoria").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocodigo").remove();
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_codigo_subp").parent().append("<span id='iconotextocodigo' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_seleccionar").attr("Class", "btn btn-primary");
    }
}

function validarSubPartidasMant() {
    var subpartida = document.getElementById("ContentPlaceHolder1_txt_codigo_subpartida").value;
    var descripcion = document.getElementById("ContentPlaceHolder1_txt_descripcion_subpartida").value;

    if (subpartida == null || subpartida.length == 0 || /^\s+$/.test(subpartida)) {
        //Si esta vacio el campo
        $("#iconosubpartida").remove();
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().append("<span id='iconosubpartida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");


    }
    else if (isNaN(subpartida)) {
        $("#iconosubpartida").remove();
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().children("span").text("El debe ser numerico.").show();
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().append("<span id='iconosubpartida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
    }
    else if (subpartida.length > 6) {
        $("#iconosubpartida").remove();
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().children("span").text("El campo no puede ser menor de 6 digitos.").show();
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().append("<span id='iconosubpartida' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");
    }
    else {
        //Si no esta vacio
        $("#iconosubpartida").remove();
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_codigo_subpartida").parent().append("<span id='iconosubpartida' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }

    if (descripcion == null || descripcion.length == 0 || /^\s+$/.test(descripcion)) {
        //Si esta vacio el campo
        $("#iconodescripcion").remove();
        $("#ContentPlaceHolder1_txt_descripcion_subpartida").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_subpartida").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_descripcion_subpartida").parent().append("<span id='iconodescripcion' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary disabled");


    }
    else {
        //Si no esta vacio
        $("#iconodescripcion").remove();
        $("#ContentPlaceHolder1_txt_descripcion_subpartida").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_descripcion_subpartida").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_descripcion_subpartida").parent().append("<span id='iconodescripcion' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }
}