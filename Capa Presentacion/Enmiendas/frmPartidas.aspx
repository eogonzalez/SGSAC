<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmPartidas.aspx.vb" Inherits="Capa_Presentacion.frmPartidas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
        <div class="panel-heading">Configurar Partidas </div>

        <%-- Cuerpo del Formulario --%>
        <div class="panel-body form-horizontal">
            <%-- Area de datos generales del la version de la enmienda SAC --%>
            <h5>
                <span class="label label-default">Datos Generales del SAC Vigente
                </span>
            </h5>

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



            <%-- Area de Categoria y Codigo Arancelario --%>
            <h5>
                <span class="label label-primary">Seleccionar Partida Arancelaria:
                </span>
            </h5>

            <div class="form-group">

                <div>
                    <asp:Label ID="lbl_codigo_arancel" CssClass="control-label col-xs-2" Text="Código Partida:" runat="server"></asp:Label>
                    <div class="col-xs-5">
                        <asp:TextBox ID="txt_codigo_cap" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block"></span>
                    </div>
                </div>

                <div class="col-xs-5">
                    <asp:Button ID="btn_seleccionar" CssClass="btn btn-primary disabled" Text="Seleccionar" runat="server" />
                </div>

            </div>


            <%-- Botones de nuevo y edicion --%>
            <div class="form-group">
                <div class="btn-group pull-right">

                    <asp:LinkButton ID="lkBtn_nuevo" runat="server" CssClass="btn btn-primary">
                        <i aria-hidden="true" class="glyphicon glyphicon-remove"></i>
                        Nuevo
                    </asp:LinkButton>

                    <asp:LinkButton ID="lkBtn_Hidden_Nuevo" Style="display: hidden" runat="server">
                    </asp:LinkButton>

                    <cc1:ModalPopupExtender ID="lkBtn_Nuevo_ModalPopupExtender" runat="server"
                        BackgroundCssClass="modalBackground" BehaviorID="lkBtn_Nuevo_ModalPopupExtender"
                        PopupControlID="pnlPartidas" DynamicServicePath="" TargetControlID="lkBtn_Hidden_Nuevo">
                    </cc1:ModalPopupExtender>

                    <asp:LinkButton ID="lkBtn_editar" runat="server" CssClass="btn btn-primary">
                        <i aria-hidden="true" class="glyphicon glyphicon-th-list"></i>
                        Editar
                    </asp:LinkButton>


                </div>

            </div>


            <%-- Grid de correlacion del SAC --%>
            <div class="table-responsive">

                <asp:GridView ID="gridviewPartidas" runat="server"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No se encontraron incisos arancelarios"
                    AutoGenerateColumns="false" AllowPaging="True">

                    <PagerSettings Mode="Numeric"
                        Position="Bottom"
                        PageButtonCount="10" />

                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="rb_partida" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="capitulo" HeaderText="Capitulo" />
                        <asp:BoundField DataField="partida" HeaderText="Partida" />
                        <asp:BoundField DataField="descripcion_partida" HeaderText="Descripcion" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>

        <%-- Pie de formulario --%>
        <div class="panel-footer">
            <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
        </div>

    </div>

    <%-- Panel para agregar/modificar partidas --%>
    <div>
        <asp:Panel ID="pnlPartidas" CssClass="panel panel-primary"
            BorderColor="Black" BackColor="White" BorderStyle="Inset" BorderWidth="1px"
            Height="300" Width="30%" runat="server">
            <div class="panel-heading">Datos de Partida</div>
            <div class="panel-body form-horizontal">

                <%--<div class="form-group">
                    <asp:Label ID="lbl_capitulo" CssClass="control-label col-xs-3" Text="Capitulo: " runat="server"></asp:Label>
                    <div class="col-xs-9">
                        <asp:DropDownList ID="ddl_capitulo" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>--%>

                <div class="form-group">
                    <div>
                        <asp:Label ID="lbl_codigo_partida" CssClass="control-label col-xs-3" Text="Código Partida: " runat="server"></asp:Label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_codigo_partida" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                            <span class="help-block"></span>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div>
                        <asp:Label ID="lbl_descripcion_partida" CssClass="control-label col-xs-3" Text="Descripción Partida:" runat="server"></asp:Label>
                        <div class="col-xs-9">
                            <asp:TextBox ID="txt_descripcion_partida" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                            <span class="help-block"></span>
                        </div>
                    </div>
                </div>

            </div>

            <div class="panel-footer">
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" />
                <asp:Button ID="btnSalir" CssClass="btn btn-default" runat="server" Text="Salir" />
            </div>

        </asp:Panel>
    </div>

</asp:Content>
