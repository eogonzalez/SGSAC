<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmCorrelacionSAC.aspx.vb" Inherits="Capa_Presentacion.frmCorrelacionSAC" %>
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
        <div class="panel-heading"> Configurar Correlaciones </div>

        <%-- Cuerpo del Formulario --%>
        <div class="panel-body form-horizontal">
            <%-- Area de datos generales del la version de la enmienda SAC --%>
            <h5>
                <span class="label label-primary">Datos Generales del SAC Vigente
                </span>
            </h5>

            <div class="form-group">

                <asp:Label ID="lbl_año_vigencia" CssClass="control-label col-xs-2" text="Año Vigencia SAC" runat="server"> </asp:Label>
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

            <div class="form-group">
                <asp:Label ID="Label2" CssClass="control-label col-xs-2" Text="Descripcion Nueva Versión:" runat="server"> </asp:Label>
                <div class="col-xs-10">
                    <asp:DropDownList ID="ddl_version_nueva" CssClass="form-control" runat="server"> </asp:DropDownList>
                </div>
            </div>


            <%-- Area de Categoria y Codigo Arancelario --%>
            <h5>
                <span class="label label-primary">Seleccionar Código Arancelario:
                </span>
            </h5>
            <div class="form-group">

                <asp:Label ID="lbl_codigo_arancel" CssClass="control-label col-xs-2" Text="Código Arancelario:" runat="server"></asp:Label>
                <div class="col-xs-5">
                    <asp:TextBox ID="txt_codigo_arancel" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-5">
                    <asp:Button ID="btn_seleccionar" CssClass="btn btn-primary" Text="Seleccionar" runat="server" />
                </div>
                
            </div>

            <%-- Descripcion Capitulo-Partida y Subpartida Seleccionada --%>
            <h5>
                <span class="label label-primary">Descripción Capitulo-Partida y Subpartida Seleccionada
                </span>
            </h5>
            <asp:UpdatePanel ID="Datos_SAC" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <asp:Label ID="lbl_descripcion_capitulo" CssClass="control-label col-xs-2" Text="Capitulo:" runat="server"></asp:Label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txt_descripcion_capitulo" CssClass="form-control" runat="server" disabled></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">

                        <asp:Label ID="lbl_descripcion_partida" CssClass="control-label col-xs-2" Text="Partida:" runat="server"></asp:Label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txt_descripcion_partida" CssClass="form-control" runat="server" disabled></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lbl_descripcion_sub_partida" CssClass="control-label col-xs-2" Text="Subpartida:" runat="server"></asp:Label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txt_descripcion_sub_partida" CssClass="form-control" runat="server" disabled></asp:TextBox>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Grid para asignar categoria --%>
            <h5>
                <span class="label label-primary">Correlación de Incisos
                </span>
            </h5>
            <div class="btn-group pull-right">

                <asp:LinkButton ID="lkBtn_Suprimir" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-remove"></i>
                Suprimir
                </asp:LinkButton>

                <asp:LinkButton ID="lkBtn_Nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-th-list"></i>
                Apertura
                </asp:LinkButton>

            </div>

            <div class="table-responsive">
                <asp:UpdatePanel ID="Datos_GridView" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvAsignarCategorias" runat="server"
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
                                        <asp:RadioButton ID="rb_inciso" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:BoundField DataField="codigo_inciso" HeaderText="Inciso Arancelario" />
                                <asp:BoundField DataField="texto_inciso" HeaderText="Descripcion Inciso Arancelario" />
                                <asp:BoundField DataField="dai_base" HeaderText="DAI SAC(Base)" />
                                <asp:BoundField DataField="codigo_inciso_corr" HeaderText="Inciso Correlación" />
                                <asp:BoundField DataField="texto_inciso_corr" HeaderText="Descripción Inciso Arancelario" />
                                <asp:BoundField DataField="dai_corr" HeaderText="DAI" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <%-- Pie de formulario --%>
        <div class="panel-footer">
            <asp:Button ID="btn_guardar_cambios" CssClass="btn btn-primary" runat="server" Text="Guardar Cambios" />
            <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
        </div>

    </div>

    <div>
        <asp:HiddenField ID="hfIdVersionSAC" runat="server" />
    </div>

</asp:Content>
