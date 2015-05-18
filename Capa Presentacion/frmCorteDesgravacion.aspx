<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmCorteDesgravacion.aspx.vb" Inherits="Capa_Presentacion.frmCorteDesgravacion" %>

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
    <%-- Panel con grid --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Configuracion de tramos de Desgravación</div>
        <br />

        <%-- Barra de botones --%>
        <div class="btn-group pull-right">
            <asp:LinkButton ID="lkBtn_Configurar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-cog"></i>
                 Configurar
            </asp:LinkButton> 
            <asp:LinkButton ID="lkBtn_Hidden_Config" runat="server" style="display:hidden">

            </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_Configurar_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_Configurar_ModalPopupExtender" PopupControlID="pnlTramos"
                DynamicServicePath="" TargetControlID="lkBtn_Hidden_Config">
            </cc1:ModalPopupExtender>

            <asp:LinkButton ID="lkbtn_regresar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-arrow-left"></i>
                 Regresar
            </asp:LinkButton>

        </div>

        <%-- Gridview --%>
        <div>
            <asp:GridView ID="gvTramos" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron Tramos"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="id_tramo" SortExpression="id_tramo">
                        <HeaderStyle CssClass="ColumnaOculta" />
                        <ItemStyle CssClass="ColumnaOculta" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_tramo" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="sigla" HeaderText="Sigla" />
                    <asp:BoundField DataField="codigo_categoria" HeaderText="Categoria" />
                    <asp:BoundField DataField="id_tramo" HeaderText="Orden de Ejecutar" />
                    <asp:BoundField DataField="descripcion" HeaderText="Tipo Desgravacion" />
                    <asp:BoundField DataField="periodo_corte" HeaderText="Periodo" />
                    <asp:BoundField DataField="activo" HeaderText="Activo" />
                    <asp:BoundField DataField="factor_desgrava" HeaderText="Cantidad Tramos" />
                    <asp:BoundField DataField="cantidad_cortes" HeaderText="Cantidad de Cortes" />
                </Columns>
            </asp:GridView>
        </div>

    </div>

    <%-- Formulario del matenimiento --%>
    <div>
        <asp:Panel ID="pnlTramos" CssClass="panel panel-primary" runat="server"
            BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" Height="570px" Width="50%">

            <div class="panel-heading">Configuracion de Tramos </div>
            <div class="panel-title">Titulo del tratado </div>

            <div class="panel-body form-horizontal">
                <h5>
                    <span class="label label-primary">Datos Categoria: Nombre Instrumento
                    </span>
                </h5>

                <div class="form-group">
                    <asp:Label ID="lbl_codidgo_Categoria" CssClass="control-label col-xs-3" Enabled="false" runat="server" Text="Categoria: "></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtCategoria" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_id_etapa" CssClass="control-label col-xs-3" Enabled="false" runat="server" Text="Orden de Etapa: "></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtIdEtapa" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_tipo_desgravacion" CssClass="control-label col-xs-3 " Enabled="false" runat="server" Text="Tipo de Desgravación:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddl_tipo_desgravacion" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>



                <h5>
                    <span class="label label-primary">Ingresar Datos del Corte para esta Etapa:
                    </span>
                </h5>


                <div class="form-group">
                    <asp:Label ID="lbl_cantidad_cortes" CssClass="control-label col-xs-3" Enabled="false" runat="server" Text="Cantidad de cortes:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_cantidad_cortes" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>


                    <asp:Label ID="lbl_periodo_corte" CssClass="control-label col-xs-3" Enabled="false" runat="server" Text="Periodo del Corte:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddl_tipo_periodo_corte" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <h5>
                    <span class="label label-primary">Porcentaje Desgravacion:
                    </span>
                </h5>

                <section class="well">

                    <div class="form-group">
                        <asp:Label ID="lbl_porcen_desgravacion_anterior" CssClass="control-label col-xs-3" Enabled="false" runat="server" Text="Periodo Anterior:"></asp:Label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_porcen_desgrava_anterior" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <asp:Label ID="lbl_porcen_desgravacion_final" CssClass="control-label col-xs-3" Enabled="false" runat="server" Text="Periodo Final:"></asp:Label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_porcen_desgrava_final" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lbl_factor_desgravacion" CssClass="control-label col-xs-3" Enabled="false" runat="server" Text="Factor de Desgravación"></asp:Label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_factor_desgravacion" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>


                </section>



            </div>

            <div class="panel-footer">
                <asp:Button ID="btn_genera_cortes" CssClass="btn btn-primary" runat="server" Text="Generar Cortes" />
                <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
            </div>

        </asp:Panel>
    </div>

    <%-- Hidden Fields para valores globales --%>
    <div>
        <asp:HiddenField ID="hfIdInstrumento" runat="server" />
        <asp:HiddenField ID="hfIdCategoria" runat="server" />
        <asp:HiddenField ID="hfIdTramo" runat="server" />
    </div>
</asp:Content>
