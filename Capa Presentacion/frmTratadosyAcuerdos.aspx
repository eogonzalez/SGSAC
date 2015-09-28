<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" Culture="auto" UICulture="auto" CodeBehind="frmTratadosyAcuerdos.aspx.vb" Inherits="Capa_Presentacion.frmTratadosyAcuerdos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            height: 26px;
        }

        .auto-style4 {
            text-align: center;
            width: 50px;
        }

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

    <div class="panel panel-primary">
        <div class="panel-heading">Instrumentos Comerciales</div>
        <br />

        <div class="btn-group pull-right" role="group">
            <asp:LinkButton ID="lkBtt_nuevo" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i> Nuevo </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_nuevo_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_nuevo_ModalPopupExtender" PopupControlID="pnlNuevoInstrumento"
                DynamicServicePath="" TargetControlID="lkBtt_nuevo">
            </cc1:ModalPopupExtender>

            <asp:LinkButton ID="lkBtt_editar" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-edit"></i> Editar </asp:LinkButton>

            <asp:LinkButton ID="lkBtt_categorias" runat="server" CssClass="btn btn-primary"> 
                        <i aria-hidden="true" class="glyphicon glyphicon-random"></i> 
                         Categorias 
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtt_paises" runat="server" CssClass="btn btn-primary">
                        <i aria-hidden="true" class="glyphicon glyphicon-globe"></i> 
                        Paises 
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_asignar_precision" runat="server" CssClass="btn btn-primary"> 
                        <i aria-hidden="true" class="glyphicon glyphicon-th-list"></i> 
                        Asignar Precisión
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_asignar_categorias" runat="server" CssClass="btn btn-primary"> 
                        <i aria-hidden="true" class="glyphicon glyphicon-indent-left"></i> 
                        Asignar Categorias
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtb_calcula_dai" runat="server" CssClass="btn btn-primary">
                        <i aria-hidden="true" class="glyphicon glyphicon-certificate"></i>
                        Calcula DAI
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_Hidden_Calcula_Dai" runat="server" Style="display: hidden">
            </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_Calcula_Dai_ModalPopupExtender" BackgroundCssClass="modalBackground"
                runat="server" BehaviorID="lkBtt_Calcula_Dai_ModalPopupExtender" PopupControlID="pnl_Calcula_Dai"
                DynamicServicePath="" TargetControlID="lkBtn_Hidden_Calcula_Dai">
            </cc1:ModalPopupExtender>

        </div>

        <div>
            <asp:GridView ID="gvInstrumentos" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron instrumentos comerciales"
                AutoGenerateColumns="false">

                <Columns>
                    <asp:BoundField DataField="id_instrumento" SortExpression="id_intrumento">
                        <HeaderStyle CssClass="ColumnaOculta" />
                        <ItemStyle CssClass="ColumnaOculta" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_sigla" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="sigla" HeaderText="Sigla" />
                    <asp:BoundField DataField="nombre_instrumento" HeaderText="Tratado o Acuerdo Comercial" />
                    <asp:BoundField DataField="fecha_firma" HeaderText="Fecha Firma" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="fecha_ratificada" HeaderText="Fecha Ratificación" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="fecha_vigencia" HeaderText="Fecha Vigencia" DataFormatString="{0:d}" />

                </Columns>

            </asp:GridView>
        </div>

    </div>

    <div>
        <asp:Panel ID="pnlNuevoInstrumento" CssClass="panel panel-primary" runat="server" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="35%">
            <div class="panel-heading">Mantenimiento de Instrumentos Comerciales</div>
            <div class="panel-body form-horizontal">

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label2" CssClass="control-label col-xs-4" runat="server" Text="Nombre: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtNombreInstrumento" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label3" CssClass="control-label col-xs-4" runat="server" Text="Tipo Instrumento:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:DropDownList ID="ddlstTipoInstrumento" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label4" CssClass="control-label col-xs-4" runat="server" Text="Sigla:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtSigla" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                    </div>
                </div>

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label5" CssClass="control-label col-xs-4" runat="server" Text="Sigla alterna:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtSiglaAlterna" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label6" CssClass="control-label col-xs-4" runat="server" Text="Acuerdo entre:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:DropDownList ID="ddlstTipoRelacion" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label7" CssClass="control-label col-xs-4" runat="server" Text="Observaciones:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtObservaciones" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                    </div>
                </div>

                <h5>
                    <span class="label label-primary">Registro de fechas para Guatemala
                    </span>
                </h5>

                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label9" CssClass="control-label col-xs-4" runat="server" Text="Fecha Firma:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtFechaFirma" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                        <cc1:CalendarExtender ID="txtFechaFirma_CalendarExtender" runat="server" BehaviorID="txtFechaFirma_CalendarExtender" TargetControlID="txtFechaFirma" />
                    </div>
                </div>
                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label10" CssClass="control-label col-xs-4" runat="server" Text="Fecha Ratificación:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtFechaRatifica" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                        <cc1:CalendarExtender ID="txtFechaRatifica_CalendarExtender" runat="server" BehaviorID="txtFechaRatifica_CalendarExtender" TargetControlID="txtFechaRatifica" />
                    </div>
                </div>
                <div class="form-group has-error has-feedback">
                    <asp:Label ID="Label11" CssClass="control-label col-xs-4" runat="server" Text="Fecha Vigencia:"></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtFechaVigencia" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="help-block">El campo no puede quedar vacio.</span>
                        <cc1:CalendarExtender ID="txtFechaVigencia_CalendarExtender" runat="server" BehaviorID="txtFechaVigencia_CalendarExtender" TargetControlID="txtFechaVigencia" />
                    </div>
                </div>
            </div>
            <div class="panel-footer">
                <asp:Button ID="btn_Guardar" CssClass="btn btn-primary" runat="server" Text="Guardar" disabled="disabled"/>
                <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
            </div>
        </asp:Panel>
    </div>

    <%-- Panel para calculo del DAI por instrumento --%>
    <div>
        <asp:Panel ID="pnl_Calcula_Dai" CssClass="panel panel-primary" runat="server"
            BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" Height="375" Width="50%">
            <div class="panel-heading">Calcula DAI para Instrumento Comercial</div>
            <div class="panel-title">Datos del Instrumento Comercial</div>

            <div class="panel-body form-horizontal">
                <div class="form-group">
                    <asp:Label ID="lbl_nombre" CssClass="control-label col-xs-1" Enabled="false" runat="server" Text="Nombre:"></asp:Label>
                    <div class="col-xs-11">
                        <asp:TextBox ID="txt_nombre" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_tipo" CssClass="control-label col-xs-1" Enabled="false" runat="server" Text="Tipo:"></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txt_tipo" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_cantidad_incisos_calcular" CssClass="control-label col-xs-4" Enabled="false" runat="server" Text="Cantidad Incisos a calcular:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_cantidad_incisos_calcular" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_sigla" CssClass="control-label col-xs-1" Enabled="false" runat="server" Text="Sigla:"></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txt_sigla" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_Inicio_Vigencia" CssClass="control-label col-xs-4" Enabled="false" runat="server" Text="Inicio Vigencia:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txt_inicio_vigencia" CssClass="form-control" runat="server" disabled></asp:TextBox>
                        <cc1:CalendarExtender ID="cal_inicio_vigencia" runat="server" BehaviorID="txt_inicio_vigencia" TargetControlID="txt_inicio_vigencia" />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_cantidad_cortes_ejecutados" CssClass="control-label col-xs-1" Enabled="false" runat="server" Text="Cantidad Cortes Ejecutados:"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_cantidad_cortes_ejecutados" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_ultimo_corte_ejecutado" CssClass="control-label col-xs-3" Enabled="false" runat="server" Text="Ultimo corte ejecutado:"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_ultimo_corte_ejecutado" type="text" CssClass="form-control" runat="server" disabled></asp:TextBox>
                    </div>

                    <asp:Label ID="lbl_corte_ejecutar" CssClass="control-label col-xs-2" Enabled="false" runat="server" Text="Corte a Ejecutar:"></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txt_corte_ejecutar" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="panel-footer">
                <asp:Button id="btn_Calcular" CssClass="btn btn-primary" Text="Calcular" runat="server"/>
                <asp:Button ID="btn_Cancelar" CssClass="btn btn-default" Text="Cancelar" runat="server" />
            </div>

        </asp:Panel>
    </div>

    <div>
        <asp:HiddenField ID="hfIdInstrumento" runat="server" />
    </div>

</asp:Content>
