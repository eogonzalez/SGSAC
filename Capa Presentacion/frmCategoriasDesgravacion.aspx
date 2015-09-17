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

            <asp:LinkButton id="lkBtn_Hidden_Nuevo" runat="server" style="display:hidden">
            </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_Nuevo_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_nuevo_ModalPopupExtender" PopupControlID="pnlNuevaCategoria"
                DynamicServicePath="" TargetControlID="lkBtn_Hidden_Nuevo">
            </cc1:ModalPopupExtender>

            <asp:LinkButton ID="lkBtn_Editar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-edit"></i>
                Editar
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_Borrar" runat="server" CssClass="btn btn-primary"
                OnClientClick="return confirm(&quot;¿Esta seguro que desea eliminar la categoria Seleccionada?&quot;)">
                <i aria-hidden="true" class="glyphicon glyphicon-erase"></i>
                Eliminar
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_Config" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-cog"></i>
                Configurar Tramos
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_Aprueba" runat="server" CssClass="btn btn-primary" 
                OnClientClick="return confirm(&quot;¿Esta seguro que desea Aprobar todas las Categorias de Desgravación del Instrumento Comercial Seleccionado?&quot;)">
                <i aria-hidden="true" class="glyphicon glyphicon-check"></i>
                Aprobar Categorias
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_Hidden_Aprueba" runat="server" Style="display: hidden">
            </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_Aprueba_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_Aprueba_ModalPopupExtender" PopupControlID="pnlApruebaCategoria"
                DynamicServicePath="" TargetControlID="lkBtn_Hidden_Aprueba">
            </cc1:ModalPopupExtender>
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
            <div class="panel-heading">Mantenimiento Categorias Desgravación </div>
            <div class="panel-title">Titulo del tratado </div>

            <div class="panel-body form-horizontal">

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="lbl_codidgo_Categoria" CssClass="control-label col-xs-4" Enabled="false" runat="server" Text="Categoria: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtCategoria" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_tipo_desgravacion" CssClass="control-label col-xs-4" Enabled="false" runat="server" Text="Tipo de Desgravación:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:DropDownList ID="ddl_tipo_desgravacion" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="lbl_cantidad_tramos" CssClass="control-label col-xs-4" Enabled="false" runat="server" Text="Cantidad de Tramos:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtCantidadTramos" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
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
                <asp:Button ID="btn_Guardar" CssClass="btn btn-primary disabled" Text="Guardar" runat="server" />
                <asp:Button ID="btn_Salir" CssClass="btn btn-default" Text="Salir" runat="server" />
            </div>


        </asp:Panel>
    </div>
    
    <%-- Panel para aprobar categorias de desgravacion --%>
    <div>
        <asp:Panel ID="pnlApruebaCategoria" CssClass="panel panel-primary" runat="server"
            BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" Height="375" Width="40%">
            <div class="panel-heading">Aprobar Categorias de Desgravación</div>
            <div class="panel-title">Aprobar Categorias de Desgravación Instrumento Comercial</div>

            <div class="panel-body form-horizontal">
                <div class="form-group">
                    <asp:Label ID="lbl_nombre" CssClass="control-label col-xs-2" Enabled="false" runat="server" Text="Nombre:"></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txt_Nombre" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_sigla" CssClass="control-label col-xs-2" Enabled="false" runat="server" Text="Sigla:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_sigla" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_cantidad_cetegorias" CssClass="control-label col-xs-5" runat="server" Text="Cantidad ingresada de Categorias:"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_cantidad_categorias" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>
                </div>

                <div class="alert alert-warning" role="alert">
                    ADVERTENCIA: Si usted efectua este proceso de "Aprobación" NO podrá posteriormente modificar 
                    ninguno de los datos correspondientes a estas Categorias de Desgravación. 

                </div>
            </div>
            <div class="panel-footer">
                <asp:Button ID="btn_Aprobar" CssClass="btn btn-primary" Text="Aprobar" runat="server" />
                <asp:Button ID="btn_Cancelar" CssClass="btn btn-default" Text="Cancelar" runat="server" />
            </div>
        </asp:Panel>
    </div>

    <div>
        <asp:HiddenField ID="hfIdInstrumento" runat="server" />
        <asp:HiddenField ID="hfIdCategoria" runat="server" />
    </div>
</asp:Content>
