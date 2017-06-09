<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmAsignaPrecision.aspx.vb" Inherits="Capa_Presentacion.frmAsignaPrecisionTLC" %>

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
        <div class="panel-heading"> Asignar Precisión al Instrumento </div>

        <%-- Cuerpo del Formulario --%>
        <div class="panel-body form-horizontal">
            <%-- Area de datos generales del SAC --%>
            <h5>
                <span class="label label-primary">Datos Generales del SAC
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

            <%-- Area de Datos del Instrumento --%>
            <h5>
                <span class="label label-primary">Datos del Instrumento Comercial
                </span>
            </h5>
            <div class="form-group">
                <asp:Label ID="lbl_nombre_insrumento" CssClass="control-label col-xs-3" Text=" Nombre del Instrumento Comercial: " runat="server"></asp:Label>
                <div class="col-xs-9">
                    <asp:TextBox ID="txt_nombre_instrumento" CssClass="form-control" runat="server" disabled></asp:TextBox>
                </div>
            </div>

            <%-- Area de Categoria y Codigo Arancelario --%>
            <h5>
                <span class="label label-primary">Seleccionar Código Arancelario:
                </span>
            </h5>

            <div class="form-group">
               
                <div>
                    <asp:Label ID="lbl_codigo_arancel" CssClass="control-label col-xs-2" Text="Código Arancelario:" runat="server"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_codigo_arancel" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block"></span>
                    </div>
                </div>

                <div class="col-xs-3">
                    <asp:Button ID="btn_seleccionar" CssClass="btn btn-primary disabled" Text="Seleccionar" runat="server" />
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

            <%-- Area del grid para asignar precision --%>
            <h5>
                <span class="label label-primary">Asignar Precisión
                </span>
            </h5>

            <%-- Boton para agregar precision --%>
            <div class="form-group">
                <div class="btn-group pull-right">
                    <asp:LinkButton ID="lkBtn_Agregar_Precision" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-ok"></i>Agregar Precisión </asp:LinkButton>

                    <asp:LinkButton ID="lkBtn_precision" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>Editar Precisión </asp:LinkButton>

                    <asp:LinkButton ID="lkBtn_Hidden_precision" Style="display: hidden" runat="server"> </asp:LinkButton>

                    <cc1:ModalPopupExtender ID="lkBtn_Precision_ModalPopupExtender"
                        BackgroundCssClass="modalBackground" BehaviorID="lkBtn_Precision_ModalPopupExtender"
                        PopupControlID="pnlPrecision" DynamicServicePath="" TargetControlID="lkBtn_Hidden_precision"
                        runat="server">
                    </cc1:ModalPopupExtender>

                    
                    <asp:LinkButton ID="lkBtn_Eliminar_Precision" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-remove"></i>Eliminar Precisión </asp:LinkButton>

                </div>
            </div>

            <%-- Grid view asignar precision --%>
            <div class="table-responsive">
                <asp:UpdatePanel ID="Datos_GridView" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvAsignarPrecision" runat="server"
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
                                <asp:BoundField DataField="codigo_inciso" HeaderText="Inciso Arancelario" />
                                <asp:BoundField DataField="texto_inciso" HeaderText="Descripcion Inciso Arancelario" />
                                <asp:BoundField DataField="dai_base" HeaderText="DAI SAC(Base)" />
                                <asp:BoundField DataField="codigo_categoria" HeaderText="Categoria" />
                                <asp:BoundField DataField="inciso_presicion" HeaderText="Inciso Precision" />
                                <asp:BoundField DataField="texto_precision" HeaderText="Descripcion Precision" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <%-- Pie de formulario --%>
        <div class="panel-footer">
            <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
        </div>

    </div>

        <%-- Panel para las aperturas --%>
    <div>
        <asp:Panel ID="pnlPrecision" CssClass="panel panel-primary" 
            BorderColor="Black" BackColor="White" BorderStyle="Inset" BorderWidth="1px"
            Height="675" Width="50%" runat="server">
            <div class="panel-heading">Datos de la precision del Inciso </div>
            <div class="panel-body form-horizontal">

                <%-- Area de datos generales del SAC --%>
                <h5>
                    <span class="label label-primary">Datos Generales del SAC</span>
                </h5>
                

                <div class="form-group">


                    <asp:Label ID="Label3" CssClass="control-label col-xs-2" Text="Enmienda al SAC" runat="server"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_version_enmienda_pnl" CssClass="form-control" runat="server" disabled="true"> </asp:TextBox>
                    </div>

                    
                    <asp:Label ID="Label4" CssClass="control-label col-xs-2" Text="Periodo Enmienda SAC del:" runat="server"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_periodo_año_inicial_pnl" CssClass="form-control" runat="server" disabled="true"> </asp:TextBox>
                    </div>

                    <asp:Label ID="Label5" CssClass="control-label col-xs-1" Text=" Al:" runat="server"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_periodo_año_final_pnl" CssClass="form-control" runat="server" disabled="true"> </asp:TextBox>
                    </div>

                </div>

                
                <%-- Area de datos del instrumento comecial --%>
                <h5>
                    <span class="label label-primary">Datos del Instrumento Comercial</span>
                </h5>

                <div class="form-group">
                    <asp:Label ID="Label1" CssClass="control-label col-xs-2" Text=" Nombre del Instrumento Comercial: " runat="server"></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txt_nombre_instrumento_pnl" CssClass="form-control" runat="server" disabled="true"></asp:TextBox>
                    </div>
                </div>

                <%-- Area de datos del inciso seleccionado --%>
                <h5>
                    <span class="label label-primary">Datos del inciso seleccionado</span>
                </h5>

                <div class="form-group">
                    <asp:Label ID="lbl_inciso_actual" CssClass="control-label col-xs-2" Text="Inciso Actual: " runat="server"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_inciso_actual_pnl" type="text" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_dai_actual" CssClass="control-label col-xs-2" Text="DAI Actual:" runat="server"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_dai_actual_pnl" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_descripcion_inciso" CssClass="control-label col-xs-2" Text="Descripción Inciso:" runat="server"></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txt_descripcion_inciso_pnl" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>
                </div>

                <%-- Area de para agregar precision --%>
                <h5>
                    <span class="label label-primary">Datos de la Categoria y Precisión</span>
                </h5>

                <div class="form-group">
                    <asp:Label ID="lbl_categoria_asignar" CssClass="control-label col-xs-2" Text="Categoria para asignar:" runat="server"></asp:Label>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddl_categoria_asignar_pnl" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                    <div>
                        <asp:Label ID="lbl_codigo_precision" CssClass="control-label col-xs-2" Text="Código Precisión:" runat="server"></asp:Label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_codigo_precision_pnl" CssClass="form-control" runat="server"></asp:TextBox>
                            <span class="help-block"></span>
                        </div>
                    </div>

                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_precision" CssClass="control-label col-xs-2" Text="Texto Precisión:" runat="server"></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txt_precision_pnl" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block"></span>
                    </div>
                </div>

<%--                <div class="form-group">
                    <asp:Label ID="lbl_observaciones" CssClass="control-label col-xs-2" Text="Observaciones:" runat="server"></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txt_observaciones_pnl" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                </div>--%>

            </div>
            
            <div class="panel-footer">
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" disabled="true"/>
                <asp:Button ID="btnSalir" CssClass="btn btn-default" runat="server" Text="Salir" />
            </div>

        </asp:Panel>
    </div>

</asp:Content>
