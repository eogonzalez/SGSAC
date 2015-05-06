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
    <asp:UpdatePanel ID="updpnlGeneral" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">Instrumentos Comerciales</div>
                <br />

                <div class="btn-group pull-right" role="group">
                    <asp:LinkButton ID="lkBtt_nuevo" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>Nuevo </asp:LinkButton>

                    <cc1:ModalPopupExtender ID="lkBtt_nuevo_ModalPopupExtender" BackgroundCssClass="modalBackground"
                        runat="server" BehaviorID="lkBtt_nuevo_ModalPopupExtender" PopupControlID="pnlNuevoInstrumento"
                        DynamicServicePath="" TargetControlID="lkBtt_nuevo">
                    </cc1:ModalPopupExtender>

                    <asp:LinkButton ID="lkBtt_editar" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-edit"></i>Editar </asp:LinkButton>

                    <asp:LinkButton ID="lkBtt_categorias" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-random"></i>Categorias </asp:LinkButton>

                    <asp:LinkButton ID="lkBtt_paises" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-globe"></i>Paises </asp:LinkButton>

                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-question-sign"></i>Ayuda </asp:LinkButton>

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

                        <div class="form-group">
                            <asp:Label ID="Label2" CssClass="control-label col-xs-4" runat="server" Text="Nombre: "></asp:Label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtNombreInstrumento" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label3" CssClass="control-label col-xs-4" runat="server" Text="Tipo Instrumento:"></asp:Label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlstTipoInstrumento" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label4" CssClass="control-label col-xs-4" runat="server" Text="Sigla:"></asp:Label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtSigla" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label5" CssClass="control-label col-xs-4" runat="server" Text="Sigla alterna:"></asp:Label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtSiglaAlterna" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label6" CssClass="control-label col-xs-4" runat="server" Text="Acuerdo entre:"></asp:Label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlstTipoRelacion" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label7" CssClass="control-label col-xs-4" runat="server" Text="Observaciones:"></asp:Label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtObservaciones" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <h5>
                            <span class="label label-primary">Registro de fechas para Guatemala
                            </span>
                        </h5>

                        <div class="form-group">
                            <asp:Label ID="Label9" CssClass="control-label col-xs-4" runat="server" Text="Fecha Firma:"></asp:Label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtFechaFirma" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaFirma_CalendarExtender" runat="server" BehaviorID="txtFechaFirma_CalendarExtender" TargetControlID="txtFechaFirma" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label10" CssClass="control-label col-xs-4" runat="server" Text="Fecha Ratificación:"></asp:Label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtFechaRatifica" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaRatifica_CalendarExtender" runat="server" BehaviorID="txtFechaRatifica_CalendarExtender" TargetControlID="txtFechaRatifica" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label11" CssClass="control-label col-xs-4" runat="server" Text="Fecha Vigencia:"></asp:Label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtFechaVigencia" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaVigencia_CalendarExtender" runat="server" BehaviorID="txtFechaVigencia_CalendarExtender" TargetControlID="txtFechaVigencia" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="btn_Guardar" CssClass="btn btn-primary" runat="server" Text="Guardar" />
                        <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
                    </div>
                </asp:Panel>
            </div>
            <div>
                <asp:HiddenField ID="hfIdInstrumento" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
