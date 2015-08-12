<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/General.Master"  CodeBehind="frmTipoRelacionInstrumento.aspx.vb" Inherits="Capa_Presentacion.frmTipoRelacionInstrumento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
            .ColumnaOculta {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Panel principal con grid --%>
    <div class="panel panel-primary">
        <div class="panel-heading"> Tipo Relacion Instrumento </div>
        <br />

        <%-- Barra de botones --%>
        <div class="btn-group pull-right">
            <asp:LinkButton ID="lkBtt_Nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>
                Nuevo
            </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_Nuevo_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_Nuevo_ModalPopupExtender" PopupControlID="pnlNuevoTipoRelacionInstrumento"
                DynamicServicePath="" TargetControlID="lkBtt_Nuevo">
            </cc1:ModalPopupExtender>

            <asp:LinkButton ID="lkBtt_Editar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-edit"></i>
                Editar
            </asp:LinkButton>

        </div>

        <%-- Gridview --%>
        <div>
            <asp:GridView ID="gvTipoRelacionInstrumento" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron tipos de instrumentos"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="id_tipo_relacion_instrumento" SortExpression="id_tipo_instrumento">
                        <HeaderStyle CssClass="ColumnaOculta"/>
                        <ItemStyle CssClass="ColumnaOculta"/>
                    </asp:BoundField >

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_tipo_relacion_instrumento" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="descripcion" HeaderText="Tipo Relacion Instrumento" />
                    <asp:BoundField DataField="observaciones" HeaderText="Descripcion" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <%-- Panel del mantenimiento del tipo de relacion instrumento --%>
    <div>
        <asp:Panel ID="pnlNuevoTipoRelacionInstrumento" CssClass="panel panel-primary" runat="server" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="35%">
            <div class="panel-heading">Mantenimiento de Tipo Relacion Instrumentos</div>

            <div class="panel-body form-horizontal" >

                <div class="form-group">
                    <asp:Label ID="Label3" CssClass="control-label col-xs-4" runat="server" Text="Tipo Relacion Instrumento:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtDescripcion" type="text" CssClass="form-control" runat="server" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label4" CssClass="control-label col-xs-4" runat="server" Text="Observaciones:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtObservaciones" type="text" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>

            </div>


            <div class="panel-footer">
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" />
                <asp:Button ID="btnSalir" CssClass="btn btn-default" runat="server" Text="Salir" />
            </div>
        </asp:Panel>
    </div>

    <div>
        <asp:HiddenField ID="hfIdTipoRelacionInstrumento" runat="server" />
    </div>
</asp:Content>
