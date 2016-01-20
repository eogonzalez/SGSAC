<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmApruebaSAC.aspx.vb" Inherits="Capa_Presentacion.frmApruebaSAC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>

    <script src="Script/jQuery/jquery-1.11.3.js"></script>
    <script src="Script/jQuery/jquery.validate.js"></script>
    <script src="Script/Aprobaciones/ValidarApruebaSAC.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Panel principal --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Configurar Correlaciones </div>

        <%-- Cuerpo del Formulario --%>
        <div class="panel-body form-horizontal">

            <%-- Area de datos generales del la version de la enmienda SAC --%>
            <h5><span class="label label-default">Datos Generales del SAC Vigente</span></h5>

            <div class="form-group">

                <asp:Label ID="lbl_año_vigencia" CssClass="control-label col-xs-2" Text="Año Vigencia SAC" runat="server"> </asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_año_vigencia" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

                <asp:Label ID="lbl_version_enmienda" CssClass="control-label col-xs-2" Text="Enmienda al SAC" runat="server"></asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txt_version_enmienda" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

                <asp:Label ID="lbl_periodo_enmienda_sac" CssClass="control-label col-xs-2" Text="Periodo Enmienda SAC del:" runat="server"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_periodo_año_inicial" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

                <asp:Label ID="lbl_al" CssClass="control-label col-xs-1" Text=" Al:" runat="server"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_periodo_año_final" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

            </div>

            <div class="form-group">

                <asp:Label ID="Label1" CssClass="control-label col-xs-2" Text="Descripción Versión Base:" runat="server"> </asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txt_descripcion_version_base" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

            </div>

            <%-- Area de datos generales del nuevo SAC --%>
            <h5><span class="label label-success">Datos Generales del Nuevo SAC </span></h5>

            <div class="form-group">

                <asp:Label ID="Label2" CssClass="control-label col-xs-2" Text="Año Vigencia SAC" runat="server"> </asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_nuevo_año_vigencia" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

                <asp:Label ID="Label3" CssClass="control-label col-xs-2" Text="Enmienda al SAC" runat="server"></asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txt_version_nueva_enmienda" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

                <asp:Label ID="Label4" CssClass="control-label col-xs-2" Text="Periodo Enmienda SAC del:" runat="server"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_nuevo_periodo_año_inicial" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

                <asp:Label ID="Label5" CssClass="control-label col-xs-1" Text=" Al:" runat="server"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_nuevo_periodo_año_final" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

            </div>

            <div class="form-group">

                <asp:Label ID="Label6" CssClass="control-label col-xs-2" Text="Descripción Nueva Versión:" runat="server"> </asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txt_descripcion_nueva_version" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

            </div>

            <div class="form-group">

                <asp:Label ID="Label7" CssClass="control-label col-xs-2" Text="Observaciones:" runat="server"> </asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txt_observaciones" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

            </div>

            <div class="text-center">
                <h3>
                    <span class="label label-warning">Firmas de Aprobación </span>
                </h3>
            </div>

            <div class="text-center">
                <div class="btn-group">
                    <asp:LinkButton ID="lkb_firmas" CssClass="btn btn-primary" runat="server"><i aria-hidden="true" class="glyphicon glyphicon-edit"></i> Agregar Firmas </asp:LinkButton>
                    <asp:LinkButton ID="lkb_cancelar" CssClass="btn btn-danger" runat="server"><i aria-hidden="true" class="glyphicon glyphicon-remove"></i> Cancelar </asp:LinkButton>
                    <asp:LinkButton ID="lkb_aprobar" CssClass="btn btn-success" runat="server"><i aria-hidden="true" class="glyphicon glyphicon-check" ></i> Aprobar </asp:LinkButton>
                    <%--<asp:LinkButton ID="lkb_salir" CssClass="btn btn-primary" runat="server"><i aria-hidden="true" class="glyphicon glyphicon-do"></i> Salir </asp:LinkButton>                --%>
                </div>
            </div>

            <h4><span class="label label-primary">Primera firma de aprobación </span></h4>

            <div class="form-group">
                <asp:Label ID="lbl_primer_usuario" CssClass="control-label col-xs-2" Text="Usuario:" runat="server"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txt_primer_usuario" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:Label ID="lbl_primer_contraseña" CssClass="control-label col-xs-2" Text="Contraseña:" runat="server"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txt_primer_contraseña" type="password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <h4><span class="label label-primary">Segunda firma de aprobación </span></h4>

            <div class="form-group">
                <asp:Label ID="Label8" CssClass="control-label col-xs-2" Text="Usuario:" runat="server"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txt_segundo_usuario" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:Label ID="Label9" CssClass="control-label col-xs-2" Text="Contraseña:" runat="server"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txt_segunda_contraseña" type="password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

        </div>

        <%-- Pie de formulario --%>
        <div class="panel-footer">
            <%--<asp:Button ID="btn_aprobar" CssClass="btn btn-danger" runat="server" Text="aprobar" />--%>
            <asp:Button ID="btn_salir" CssClass="btn btn-default" runat="server" Text="Salir" />
        </div>

    </div>

</asp:Content>
