<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmTipoPeriodo.aspx.vb" Inherits="Capa_Presentacion.frmTipoPeriodo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .ColumnaOculta {
            display: none;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Panel Principal con Grid --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Tipo Periodo de Corte</div>
        <br />

        <%--Barra de botones--%>
        <div class="btn-group pull-right">

            <asp:LinkButton ID="lkBtt_Nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>
                Nuevo
            </asp:LinkButton>
            
            <cc1:ModalPopupExtender ID="lkBtt_Nuevo_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_nuevo_ModalPopupExtender" PopupControlID="pnlNuevoTipoPeriodoCorte"
                DynamicServicePath="" TargetControlID="lkBtt_Nuevo">
            </cc1:ModalPopupExtender>

            <asp:LinkButton ID="lkBtt_Editar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-edit"></i>
                Editar
            </asp:LinkButton>
        </div>

        <%-- Gridview --%>
        <div>
            <asp:GridView ID="gvTipoPeriodoCorte" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron tipos de periodos de corte"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="id_tipo_periodo" SortExpression="id_tipo_periodo">
                        <HeaderStyle CssClass="ColumnaOculta"/>
                        <ItemStyle CssClass="ColumnaOculta"/>
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_tipo_periodo" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="descripcion" HeaderText="Tipo Periodo de Corte" />
                    <asp:BoundField DataField="observaciones" HeaderText="Descripcion" />

                </Columns>
            </asp:GridView>
        </div>

    </div>

    <%-- Panel del mantenimiento de tipo de instrumento --%>
    <div>
        <asp:Panel ID="pnlNuevoTipoPeriodoCorte" CssClass="panel panel-primary" runat="server" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="35%">
            <div class="panel-heading">Mantenimiento de Tipo de Periodo de Corte</div>

            <div class="panel-body form-horizontal">

                <div class="form-group">
                    <asp:Label ID="lbl_Descripcion" CssClass="control-label col-xs-4" runat="server" Text="Tipo Instrumento:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtDescripcion" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block"></span>
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
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" disabled="true"/>
                <asp:Button ID="btnSalir" CssClass="btn btn-default" runat="server" Text="Salir" />
            </div>
        </asp:Panel>
    </div>
    
    <div>
        <asp:HiddenField ID="hfIdTipoPeriodoCorte" runat="server" />
    </div>

</asp:Content>
