$(document).ready(inicio);

function inicio() {

    $("#ContentPlaceHolder1_lkb_firmas").click(habilitar);
    $("#ContentPlaceHolder1_lkb_cancelar").click(deshabilitar);
    
    var obj_primerUsuario = document.getElementById('ContentPlaceHolder1_txt_primer_usuario');
    var contenido_primerUsuario = $("#ContentPlaceHolder1_txt_primer_usuario").val();

    if (obj_primerUsuario.getAttribute("disabled") != "true" && contenido_primerUsuario == null || contenido_primerUsuario.length == 0) {
        $("#ContentPlaceHolder1_txt_primer_usuario").prop("disabled", true);
        $("#ContentPlaceHolder1_txt_primer_contraseña").prop("disabled", true);
    }

    var obj_segundoUsuario = document.getElementById('ContentPlaceHolder1_txt_segundo_usuario');
    var contenido_segundoUsuario = document.getElementById('ContentPlaceHolder1_txt_segundo_usuario').value;

    if (obj_segundoUsuario.getAttribute("disabled") != "true" && contenido_segundoUsuario == null || contenido_segundoUsuario.length == 0) {
        $("#ContentPlaceHolder1_txt_segundo_usuario").prop("disabled", true);
        $("#ContentPlaceHolder1_txt_segunda_contraseña").prop("disabled", true);
    }
}

function habilitar() {

    //$("#ContentPlaceHolder1_lkb_cancelar").removeProp("disabled");
    //$("#ContentPlaceHolder1_lkb_firmas").prop("disabled", true);

    $("#ContentPlaceHolder1_txt_primer_usuario").removeProp("disabled");
    $("#ContentPlaceHolder1_txt_primer_contraseña").removeProp("disabled");

    $("#ContentPlaceHolder1_txt_segundo_usuario").removeProp("disabled");
    $("#ContentPlaceHolder1_txt_segunda_contraseña").removeProp("disabled");

}

function deshabilitar() {

    
    //document.getElementById('ContentPlaceHolder1_lkb_aprobar').disabled = "true";
    //$("#ContentPlaceHolder1_lkb_aprobar").removeProp("disabled");
    //$("#ContentPlaceHolder1_lkb_firmas").prop("disabled", false);

    $("#ContentPlaceHolder1_txt_primer_usuario").prop("disabled", true);
    document.getElementById('ContentPlaceHolder1_txt_primer_usuario').value = "";


    $("#ContentPlaceHolder1_txt_primer_contraseña").prop("disabled", true);
    document.getElementById('ContentPlaceHolder1_txt_primer_contraseña').value = "";


    $("#ContentPlaceHolder1_txt_segundo_usuario").prop("disabled", true);
    document.getElementById('ContentPlaceHolder1_txt_segundo_usuario').value = "";

    $("#ContentPlaceHolder1_txt_segunda_contraseña").prop("disabled", true);
    document.getElementById('ContentPlaceHolder1_txt_segunda_contraseña').value = "";

}