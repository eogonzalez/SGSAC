<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmCorrelacionSAC.aspx.vb" Inherits="Capa_Presentacion.frmCorrelacionSAC" %>

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
        <div class="panel-heading"> Configurar Correlaciones </div>

        <%-- Cuerpo del Formulario --%>
        <div class="panel-body form-horizontal">
            <%-- Area de datos generales del la version de la enmienda SAC --%>
            <h5>
                <span class="label label-default">Datos Generales del SAC Vigente
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

            <h5>
                <span class="label label-success"> Datos Generales del Nuevo SAC </span>
            </h5>

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


            <%-- Area de Categoria y Codigo Arancelario --%>
            <h5>
                <span class="label label-primary">Seleccionar Código Arancelario:
                </span>
            </h5>

            <div class="form-group">

                <div>
                    <asp:Label ID="lbl_codigo_arancel" CssClass="control-label col-xs-2" Text="Código Arancelario:" runat="server"></asp:Label>
                    <div class="col-xs-5">
                        <asp:TextBox ID="txt_codigo_arancel" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block"></span>
                    </div>
                </div>

                <div class="col-xs-5">
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




            <%-- Grid para asignar categoria --%>
            <h5>
                <span class="label label-primary">Correlación de Incisos
                </span>
            </h5>

            <div class="form-group">
                <div class="btn-group pull-right">

                    <asp:LinkButton ID="lkBtn_Suprimir" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-remove"></i>
                Suprimir
                    </asp:LinkButton>

                    <asp:LinkButton ID="lkBtn_Nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-th-list"></i>
                Apertura
                    </asp:LinkButton>

                    <asp:LinkButton id="lkBtn_Hidden_Nuevo" Style="display:hidden" runat="server">
                    </asp:LinkButton>

                </div>

                <cc1:ModalPopupExtender ID="lkBtt_Nuevo_ModalPopupExtender"
                    BackgroundCssClass="modalBackground" BehaviorID="lkBtt_Nuevo_ModalPopupExtender"
                    PopupControlID="pnlApertura" DynamicServicePath="" TargetControlID="lkBtn_Hidden_Nuevo"
                    runat="server">
                </cc1:ModalPopupExtender>

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
            <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
        </div>

    </div>

    <%-- Panel para las aperturas --%>
    <div>
        <asp:Panel ID="pnlApertura" CssClass="panel panel-primary" 
            BorderColor="Black" BackColor="White" BorderStyle="Inset" BorderWidth="1px"
            Height="635" Width="30%" runat="server">
            <div class="panel-heading">Datos de Apertura Arancelaria SAC </div>
            <div class="panel-body form-horizontal">

                <div class="form-group">
                    <asp:Label ID="lbl_actual" CssClass="control-label col-xs-3" Text="Actual Version: " runat="server"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_anio_actual" type="text" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txt_descripcion_actual" type="text" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_nueva" CssClass="control-label col-xs-3" Text="Nueva Version: " runat="server"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_anio_nueva" type="text" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txt_descripcion_nueva" type="text" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_inciso_actual" CssClass="control-label col-xs-3" Text="Inciso Actual: " runat="server"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_inciso_actual" type="text" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_dai_actual" CssClass="control-label col-xs-3" Text="DAI Actual:" runat="server"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_dai_actual" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_descripcion_inciso" CssClass="control-label col-xs-3" Text="Descripcion Inciso:" runat="server"></asp:Label>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txt_descripcion_inciso" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_inciso_nuevo" CssClass="control-label col-xs-3" Text="Inciso Nuevo: " runat="server"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_inciso_nuevo" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_dai_nuevo" CssClass="control-label col-xs-3" Text="DAI Nuevo:" runat="server"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_dai_nuevo" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_descripcion_inciso_nuevo" CssClass="control-label col-xs-3" Text="Descripcion Nuevo Inciso:" runat="server"></asp:Label>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txt_descripcion_inciso_nuevo" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>


                <div class="form-group">
                    <asp:Label ID="lbl_fecha_inicio_vigencia" CssClass="control-label col-xs-3" runat="server" Text="Fecha Inicio Vigencia:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_Fecha_Inicio_Vigencia" CssClass="form-control" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txt_Fecha_Inicio_Vigencia_CalendarExtender" runat="server" BehaviorID="txt_Fecha_Inicio_Vigencia_CalendarExtender" TargetControlID="txt_Fecha_Inicio_Vigencia" />
                    </div>

                    <asp:Label ID="lbl_fecha_fin_vigencia" CssClass="control-label col-xs-3" runat="server" Text="Fecha Fin de Vigencia:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_Fecha_Fin_Vigencia" CssClass="form-control" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txt_Fecha_Fin_Vigencia_CalendarExtender" runat="server" BehaviorID="txt_Fecha_Fin_Vigencia_CalendarExtender" TargetControlID="txt_Fecha_Fin_Vigencia" />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_base_normativa" CssClass="control-label col-xs-3" Text="Base Normativa:" runat="server"></asp:Label>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txt_base_normativa" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_observaciones" CssClass="control-label col-xs-3" Text="Observaciones:" runat="server"></asp:Label>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txt_observaciones" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
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
