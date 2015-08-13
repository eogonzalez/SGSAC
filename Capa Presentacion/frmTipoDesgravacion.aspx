<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmTipoDesgravacion.aspx.vb" Inherits="Capa_Presentacion.frmTipoDesgravacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .ColumnaOculta {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        //$(document).ready(function () {

        //    $('#GlobalForm').formValidation({
        //        framework: 'bootstrap',
        //        submitButtons: $('#Add'),
        //        feedbackIcons: {
        //            valid: 'glyphicon glyphicon-ok',
        //            invalid: 'glyphicon glyphicon-remove',
        //            validating: 'glyphicon glyphicon-refresh'
        //        },
        //        live: 'enabled',
        //        message: 'This value is not valid',
        //        submitButtons: 'button[type="submit"]',
        //        trigger: null,
        //        fields: {
        //            txtDescripcion: {
        //                selector: '#txtDescripcion',
        //                message: 'The username is not valid',
        //                validators: {
        //                    notEmpty: {
        //                        message: 'The user name is required.'
        //                    }
        //                }
        //            },
        //            Name: {
        //                selector: '#Name',
        //                message: 'The name is not valid',
        //                validators: {
        //                    notEmpty: {
        //                        message: 'Name is required.'
        //                    }
        //                }
        //            },
        //            LastName: {
        //                selector: '#LastName',
        //                message: 'The last name is not valid.',
        //                validators: {
        //                    notEmpty: {
        //                        message: 'Last name is required.'
        //                    }
        //                }
        //            }
        //        }
        //    });
        //});

        // This method is called by the Add button when it's clicked
        //function isValidForm() {
        //    var formValidation = $('#GlobalForm').data('formValidation');

        //    // Checks if the form is valid
        //    if (formValidation.isValid()) {
        //        var User = ({ txtDescripcion: $('#txtDescripcion').val()});
        //        $.ajax({
        //            type: "POST",
        //            data: JSON.stringify(User),
        //            url: "../api/Users/PostUser",
        //            contentType: "application/json",
        //            success: function (response) {
        //                // Success code goes here
        //            },
        //            error: function (response) {
        //                // Error code goes here
        //            }
        //        });
        //    }
        //    else {
        //        formValidation.validate();
        //    }
        //};

        //$(document).ready(function () {
        //    var form = $('#userForm')
        //        , formData = $.data(form[0])
        //        , settings = formData.validator.settings
        //        // Store existing event handlers in local variables
        //        , oldErrorPlacement = settings.errorPlacement
        //        , oldSuccess = settings.success;

        //    settings.errorPlacement = function (label, element) {

        //        // Call old handler so it can update the HTML
        //        oldErrorPlacement(label, element);

        //        // Add Bootstrap classes to newly added elements
        //        label.parents('.form-group').addClass('has-error');
        //        label.addClass('text-danger');
        //    };

        //    settings.success = function (label) {
        //        // Remove error class from <div class="form-group">, but don't worry about
        //        // validation error messages as the plugin is going to remove it anyway
        //        label.parents('.form-group').removeClass('has-error');

        //        // Call old handler to do rest of the work
        //        oldSuccess(label);
        //    };
        //});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%-- Panel Principal con Grid --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Tipo de Desgravación</div>
        <br />

        <%--Barra de botones--%>
        <div class="btn-group pull-right">

            <asp:LinkButton ID="lkBtt_Nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>
                Nuevo
            </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_Nuevo_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_nuevo_ModalPopupExtender" PopupControlID="pnlNuevoTipoDesgravacion"
                DynamicServicePath="" TargetControlID="lkBtt_Nuevo">
            </cc1:ModalPopupExtender>

            <asp:LinkButton ID="lkBtt_Editar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-edit"></i>
                Editar
            </asp:LinkButton>
        </div>

        <%-- Gridview --%>
        <div>
            <asp:GridView ID="gvTipoDesgravacion" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron tipos de desgravacion"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="id_tipo_desgrava" SortExpression="id_tipo_desgrava">
                        <HeaderStyle CssClass="ColumnaOculta"/>
                        <ItemStyle CssClass="ColumnaOculta"/>
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_tipo_desgravacion" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="descripcion" HeaderText="Tipo de Desgravacion" />
                    <asp:BoundField DataField="observaciones" HeaderText="Descripcion" />

                </Columns>
            </asp:GridView>
        </div>

    </div>

    <%-- Panel del mantenimiento de tipo de instrumento --%>
    <div>
        <asp:Panel ID="pnlNuevoTipoDesgravacion" CssClass="panel panel-primary" runat="server" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="35%">
            
            <div class="panel-heading">Mantenimiento de Tipo de Desgravacion</div>

            <div class="panel-body form-horizontal">

                <div class="form-group">
                    <asp:Label ID="lbl_Descripcion" CssClass="control-label col-xs-4" runat="server" Text="Tipo de Desgravacion:" ></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtDescripcion" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    

                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_Observaciones" CssClass="control-label col-xs-4" runat="server" Text="Observaciones:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtObservaciones" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>

            </div>


            <div class="panel-footer">

                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" type="submit"/>
                <asp:Button ID="btnSalir" CssClass="btn btn-default" runat="server" Text="Salir" />
            </div>
                
        </asp:Panel>
    </div>
    
    <div>
        <asp:HiddenField ID="hfIdTipoDesgravacion" runat="server" />
    </div>
</asp:Content>
