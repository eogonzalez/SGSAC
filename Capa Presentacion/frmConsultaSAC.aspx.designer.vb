'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class frmConsultaSAC

    '''<summary>
    '''Control lbl_nombre_insrumento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_nombre_insrumento As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control ddl_instrumento_comercial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddl_instrumento_comercial As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control lbl_categoria_asignar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_categoria_asignar As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control ddl_categoria_asignar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddl_categoria_asignar As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control lbl_codigo_arancel.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_codigo_arancel As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txt_codigo_arancel.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_codigo_arancel As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btn_seleccionar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_seleccionar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Datos_SAC.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Datos_SAC As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control lbl_descripcion_capitulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_descripcion_capitulo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txt_descripcion_capitulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_descripcion_capitulo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lbl_descripcion_partida.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_descripcion_partida As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txt_descripcion_partida.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_descripcion_partida As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lbl_descripcion_sub_partida.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_descripcion_sub_partida As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txt_descripcion_sub_partida.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_descripcion_sub_partida As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control Datos_GridView.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Datos_GridView As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control gv_incisos_sac.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents gv_incisos_sac As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control btn_genera.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_genera As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btn_Salir.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_Salir As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hfIdInstrumento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hfIdInstrumento As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hfCheckInciso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hfCheckInciso As Global.System.Web.UI.WebControls.HiddenField
End Class
