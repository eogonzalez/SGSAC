<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmCategoriasDesgravacion.aspx.vb" Inherits="Capa_Presentacion.frmCategoriasDesgravacion" %>

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

    <%-- Panel principal con Grid --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Categorias y Tramos de Desgravacion </div>
        <br />

        <%-- Barra de botones --%>
        <div class="btn-group pull-right">
            <asp:LinkButton ID="lkBtn_Nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>
                Nuevo
            </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_Nuevo_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_nuevo_ModalPopupExtender" PopupControlID="pnlNuevaCategoria"
                DynamicServicePath="" TargetControlID="lkBtn_Nuevo">
            </cc1:ModalPopupExtender>

            <asp:LinkButton ID="lkBtn_Editar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-edit"></i>
                Editar
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_Config" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-cog"></i>
                Configurar Tramos
            </asp:LinkButton>
        </div>

        <%-- Gridview --%>
        <div>
            <asp:GridView ID="gvCategorias" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron categorias"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="id_categoria" SortExpression="id_categoria"> 
                        <HeaderStyle CssClass="ColumnaOculta"/>
                        <ItemStyle CssClass="ColumnaOculta"/>
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_categoria" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="sigla" HeaderText="Sigla" />
                    <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                    <asp:BoundField DataField="tipo_desgravacion" HeaderText="Tipo Desgravacion" />
                    <%--<asp:BoundField DataField="periodo" HeaderText="Periodo" />--%>
                    <asp:BoundField DataField="activo" HeaderText="Activo" />
                    <asp:BoundField DataField="cantidad_tramos" HeaderText="Cantidad Tramos" />
                    <asp:BoundField DataField="cantidad_cortes" HeaderText=" Cantidad de Cortes" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <%-- Panel de mantenimiento de categorias de desgravacion --%>
    <div>
        <asp:Panel ID="pnlNuevaCategoria" CssClass="panel panel-primary" runat="server"
            BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="35%">
            <div class="panel-heading">Mantenimiento Categorias Desgravacion </div>
            <div class="panel-title">Titulo del tratado </div>

            <div class="panel-body form-horizontal">

                <div class="form-group">
                    <asp:Label ID="lbl_codidgo_Categoria" CssClass="control-label col-xs-4" Enabled="false" runat="server" Text="Categoria: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtCategoria" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_tipo_desgravacion" CssClass="control-label col-xs-4" Enabled="false" runat="server" Text="Tipo de Desgravación:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:DropDownList ID="ddl_tipo_desgravacion" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_cantidad_tramos" CssClass="control-label col-xs-4" Enabled="false" runat="server" Text="Cantidad de Tramos:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtCantidadTramos" type="text" CssClass="form-control" runat="server"></asp:TextBox>
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
                <asp:Button ID="btn_Guardar" CssClass="btn btn-primary" Text="Guardar" runat="server" />
                <asp:Button ID="btn_Salir" CssClass="btn btn-default" Text="Salir" runat="server" />
            </div>


        </asp:Panel>
    </div>

    <div>
        <asp:HiddenField ID="hfIdInstrumento" runat="server" />
        <asp:HiddenField ID="hfIdCategoria" runat="server" />
    </div>
</asp:Content>
