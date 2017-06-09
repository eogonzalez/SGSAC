<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmConfigurarMenu.aspx.vb" Inherits="Capa_Presentacion.frmConfigurarMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function SelectSingleRadiobutton(rdbtnid) {
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Panel principal --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Configuracion del Menu</div>
        <br />

        <div class="btn-group pull-right" role="group">
            <asp:LinkButton ID="lkBtt_nuevo" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i> Nueva Opción </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_nuevo_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_nuevo_ModalPopupExtender" PopupControlID="pnlNuevaOpcion"
                DynamicServicePath="" TargetControlID="lkBtt_nuevo">
            </cc1:ModalPopupExtender>


            <asp:LinkButton ID="lkBtt_editar" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-edit"></i> Editar Opción </asp:LinkButton>

            <asp:LinkButton ID="lkBtt_opcionesMenu" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-th-list"></i> Opciones del Menu </asp:LinkButton>

        </div>

        <div>
            <asp:GridView ID="gvOpcionesMenu" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron Opciones del Menu"
                AutoGenerateColumns="false">

                <Columns>

                    <asp:BoundField DataField="id_opcion" SortExpression="id_opcion">
                        <HeaderStyle CssClass="ColumnaOculta" />
                        <ItemStyle CssClass="ColumnaOculta" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_opcion" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="url" HeaderText="URL" />
                    <asp:BoundField DataField="obligatorio" HeaderText="Obligatorio" />
                    <asp:BoundField DataField="visible" HeaderText="Visible" />



                </Columns>

            </asp:GridView>
        </div>

    </div>

    <%-- Panel de mantenimiento --%>
    <div>
        <asp:Panel ID="pnlNuevaOpcion" CssClass="panel panel-primary" runat="server" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="300" Width="35%">
            <div class="panel-heading">Mantenimiento del Menu </div>
            <div class="panel-body form-horizontal">

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label2" CssClass="control-label col-xs-4" runat="server" Text="Nombre: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtNombreOpcion" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label4" CssClass="control-label col-xs-4" runat="server" Text="Descripcion:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtDescripcionOpcion" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label5" CssClass="control-label col-xs-4" runat="server" Text="URL:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtURL" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                    </div>
                </div>

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label7" CssClass="control-label col-xs-4" runat="server" Text="Orden:"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtOrden" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                    </div>

                    <asp:Label ID="Label3" CssClass="control-label col-xs-2" runat="server" Text="Obligatorio:"></asp:Label>
                    <div class="col-xs-1">
                        <asp:CheckBox ID="cb_obligatorio" runat="server" />
                    </div>

                    <asp:Label ID="Label1" CssClass="control-label col-xs-2" runat="server" Text="Visible:"></asp:Label>
                    <div class="col-xs-1">
                        <asp:CheckBox ID="cb_visible" runat="server" />
                    </div>

                </div>

            </div>
            <div class="panel-footer">
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" />
                <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
