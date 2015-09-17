$(document).on("ready", inicio);

function inicio() {
    //Aplicar Validacion al texto del formulario 
    //Tipo Instrumento
    //Tipo Relacion Instrumento
    //Tipo Desgravacion
    //Tipo Periodo Corte
    $("#ContentPlaceHolder1_txtDescripcion").keyup(validarCatalogo);
    $("#ContentPlaceHolder1_txtDescripcion").focus(validarCatalogo);

    //Aplicar validacion formulario instrumento comercial
    $("#ContentPlaceHolder1_txtNombreInstrumento").keyup(validarInstrumento);
    $("#ContentPlaceHolder1_txtNombreInstrumento").focus(validarInstrumento);

    $("#ContentPlaceHolder1_txtSigla").keyup(validarInstrumento);

    //Aplicar validacion formulario Categorias
    $("#ContentPlaceHolder1_txtCategoria").keyup(validarCategoria);
    $("#ContentPlaceHolder1_txtCategoria").focus(validarCategoria);
    $("#ContentPlaceHolder1_txtCategoria").focusout(validarCategoria);

    $("#ContentPlaceHolder1_txtCantidadTramos").keyup(validarCategoria);
    
    //Aplicar validacion formulario configuracion de tramos
    $("#ContentPlaceHolder1_txt_cantidad_cortes").keyup(validarTramo);
    $("#ContentPlaceHolder1_txt_cantidad_cortes").focus(validarTramo);
    $("#ContentPlaceHolder1_txt_cantidad_cortes").focusout(validarTramo);

    //Aplicar validacion asigna categoria
    $("#ContentPlaceHolder1_txt_codigo_arancel").keyup(validarAsigna);
    $("#ContentPlaceHolder1_txt_codigo_arancel").focus(validarAsigna);
    
    //$("#ContentPlaceHolder1_btn_seleccionar").click(function () {
    //    $("#ContentPlaceHolder1_btn_asigna_categoria").attr("Class", "btn btn-primary");
    //});
    
    //Aplicar validacion enmienda SAC
    $("#ContentPlaceHolder1_txtAñoVersion").keyup(validarEnmienda);
    $("#ContentPlaceHolder1_txtAñoVersion").focus(validarEnmienda);

    $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").keyup(validarEnmienda);
    $("#ContentPlaceHolder1_txt_Descripcion_Enmienda").focus(validarEnmienda);

    //$("#ContentPlaceHolder1_txtFechaFinVigencia").keyup(validarEnmienda);
    //$("#ContentPlaceHolder1_txtFechaFinVigencia").focus(validarEnmienda);
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
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_txtDescripcion").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtDescripcion").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtDescripcion").parent().append("<span id='iconotexto' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btnGuardar").attr("Class", "btn btn-primary");
    }
}

function validarInstrumento() {
    var nombre_instrumento = document.getElementById("ContentPlaceHolder1_txtNombreInstrumento").value;
    var sigla = document.getElementById("ContentPlaceHolder1_txtSigla").value;

    if (nombre_instrumento == null || nombre_instrumento.length == 0 || /^\s+$/.test(nombre_instrumento)) {
        //Si esta vacio el campo
        $("#iconotexto").remove();
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txtNombreInstrumento").parent().append("<span id='iconotexto' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotexto").remove();
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
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextosigla").remove();
        $("#ContentPlaceHolder1_txtSigla").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtSigla").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtSigla").parent().append("<span id='iconotextosigla' class='glyphicon glyphicon-ok form-control-feedback'>");
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
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocat").remove();
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
        return false;
    }
    else if (cantidad_tramos == 0){
        $("#iconotextocantidadtramos").remove();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().children("span").text("La cantidad de tramos debe ser mayor a cero.").show();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().append("<span id='iconotextocantidadtramos' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if(isNaN(cantidad_tramos)){
        $("#iconotextocantidadtramos").remove();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().parent().attr("class", "form-group has-error has-feedback");
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().children("span").text("Debe ingresar caracteres numericos.").show();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().append("<span id='iconotextocantidadtramos' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocantidadtramos").remove();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().parent().attr("class", "form-group has-success has-feedback");
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txtCantidadTramos").parent().append("<span id='iconotextocantidadtramos' class='glyphicon glyphicon-ok form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_Guardar").attr("Class", "btn btn-primary");
    }
}

function validarTramo() {
    var cantidad_cortes = document.getElementById("ContentPlaceHolder1_txt_cantidad_cortes").value;

    if (cantidad_cortes == null || cantidad_cortes.length == 0 || /^\s+$/.test(cantidad_cortes)) {
        //Si esta vacio el campo
        $("#iconotextocantidadcortes").remove();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().children("span").text("El campo no puede quedar vacio.").show();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().append("<span id='iconotextocantidadcortes' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (cantidad_cortes == 0) {
        //Cantidad de cortes debe de ser mayor a cero
        $("#iconotextocantidadcortes").remove();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().children("span").text("La cantidad de cortes debe ser mayor a cero.").show();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().append("<span id='iconotextocantidadcortes' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else if (isNaN(cantidad_cortes)) {
        //El campo debe de ser numerico
        $("#iconotextocantidadcortes").remove();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().parent().attr("class", "has-error has-feedback");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().children("span").text("Debe ingresar caracteres numericos.").show();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().append("<span id='iconotextocantidadcortes' class='glyphicon glyphicon-remove form-control-feedback'>");
        $("#ContentPlaceHolder1_btn_genera_cortes").attr("Class", "btn btn-primary disabled");
        return false;
    }
    else {
        //Si no esta vacio
        $("#iconotextocantidadcortes").remove();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().parent().attr("class", "has-success has-feedback");
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().children("span").text("").hide();
        $("#ContentPlaceHolder1_txt_cantidad_cortes").parent().append("<span id='iconotextocantidadcortes' class='glyphicon glyphicon-ok form-control-feedback'>");
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