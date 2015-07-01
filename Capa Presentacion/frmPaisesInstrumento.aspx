<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmPaisesInstrumento.aspx.vb" Inherits="Capa_Presentacion.frmPaisesIntrumento" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
    <div class="panel panel-primary">
        <div class="panel-heading">Ingreso de Países</div>
        <br />        
        <div class="btn-group pull-right">
            <asp:LinkButton ID="lkBtn_Nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-new-window"></i>
                Nuevo
            </asp:LinkButton>

            <asp:LinkButton ID="lkBtn_Hidden_Nuevo" runat="server" Style="display: hidden">
            </asp:LinkButton>
            
            <cc1:ModalPopupExtender ID="mpePaises" runat="server" PopupControlID="pnlAsinarPais" BackgroundCssClass="modalBackground" BehaviorID="hfPopup_ModalPopupExtender" DynamicServicePath="" TargetControlID="lkBtn_Hidden_Nuevo">
            </cc1:ModalPopupExtender>
 
            <asp:LinkButton ID="lkBtt_editar" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-edit"></i> Editar </asp:LinkButton>
            
        </div>
        <div>
            <asp:GridView ID="gvPaisesInstrumento" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No hay países asignados"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ID_INSTRUMENTO" SortExpression="ID_INSTRUMENTO">
                        <HeaderStyle CssClass="ColumnaOculta"/>
                        <ItemStyle CssClass="ColumnaOculta"/>
                    </asp:BoundField>
                     <asp:BoundField DataField="ID_PAIS" SortExpression="ID_PAIS">
                        <HeaderStyle CssClass="ColumnaOculta"/>
                        <ItemStyle CssClass="ColumnaOculta"/>
                    </asp:BoundField>
                     <asp:BoundField DataField="ID_TIPO_SOCIO" SortExpression="ID_TIPO_SOCIO">
                        <HeaderStyle CssClass="ColumnaOculta"/>
                        <ItemStyle CssClass="ColumnaOculta"/>
                    </asp:BoundField>
                     <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rblPais" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)"/>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="nombre_pais" HeaderText="País" SortExpression="País">
                    </asp:BoundField>
                    <asp:BoundField DataField="codigo_alfa" HeaderText="Codigo País" SortExpression="Codigo País">
                    </asp:BoundField>                   
                    <asp:BoundField DataField="FECHA_FIRMA" HeaderText="Fecha Firma" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="FECHA_RATIFICACION" HeaderText="Fecha Ratificación" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="FECHA_VIGENCIA" HeaderText="Fecha Vigencia" DataFormatString="{0:d}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div>
        <asp:Panel ID="pnlAsinarPais" runat="server" CssClass="panel panel-primary" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="55%">
            <div class="panel-heading">Instrumento Comercial</div>
            <div class="panel-body form-horizontal">
                <div class="form-group">
                    <asp:Label ID="lblNombre" CssClass="control-label col-xs-1" runat="server" Text="Nombre: "></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txtNombre" type="text" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblTipo" CssClass="control-label col-xs-1" runat="server" Text="Tipo: "></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txtTipo" type="text" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblSigla" CssClass="control-label col-xs-1" runat="server" Text="Sigla: "></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtSigla" type="text" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>

                    <asp:Label ID="lblSiglaAlterna" CssClass="control-label col-xs-2" runat="server" Text="Sigla alterna: "></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtSiglaAlterna" type="text" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>

                    <asp:Label ID="lblAcuerdo" CssClass="control-label col-xs-2" runat="server" Text="Acuerdo entre: "></asp:Label>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtAcuerdo" type="text" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="panel-heading">Datos del País</div>
            <asp:UpdatePanel ID="datospais" runat="server">
                <ContentTemplate>                    
                    <div class="panel-body form-horizontal">
                        <div class="form-group">
                            <asp:Label ID="lblPaises" CssClass="control-label col-xs-3" runat="server" Text="Nombre del país: "></asp:Label>
                            <div class="col-xs-3">
                                <asp:DropDownList ID="ddlPaises" CssClass="btn btn-default dropdown-toggle" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <asp:Label ID="lblCodigoPais" CssClass="control-label col-xs-3" runat="server" Text="Código del país: "></asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox ID="txtCodigoPais" type="text" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblTipoSocio" CssClass="control-label col-xs-3" runat="server" Text="Tipo de socio: "></asp:Label>
                            <div class="col-xs-3">
                                <asp:DropDownList ID="ddlTipoSocio" CssClass="btn btn-default dropdown-toggle" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <asp:Label ID="lblFechaFirma" CssClass="control-label col-xs-3" runat="server" Text="Fecha de firma: "></asp:Label>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtFechaFirma" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaFirma_CalendarExtender" runat="server" BehaviorID="txtFechaFirma_CalendarExtender" TargetControlID="txtFechaFirma">
                                </cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblRegionPais" CssClass="control-label col-xs-3" runat="server" Text="Región del país: "></asp:Label>
                            <div class="col-xs-3">
                                <asp:DropDownList ID="ddlRegionPais" CssClass="btn btn-default dropdown-toggle" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <asp:Label ID="lblFechaRatificacion" CssClass="control-label col-xs-3" runat="server" Text="Fecha de ratificación: "></asp:Label>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtFechaRatificacion" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaRatificacion_CalendarExtender" runat="server" BehaviorID="txtFechaRatificacion_CalendarExtender" TargetControlID="txtFechaRatificacion">
                                </cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblObservaciones" CssClass="control-label col-xs-2" runat="server" Text="Observaciones: "></asp:Label>
                            <div class="col-xs-4">
                                <asp:TextBox ID="txtObservaciones" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <asp:Label ID="lblFechaVigencia" CssClass="control-label col-xs-3" runat="server" Text="Fecha de vigencia: "></asp:Label>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtFechaVigencia" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaVigencia_CalendarExtender" runat="server" BehaviorID="txtFechaVigencia_CalendarExtender" TargetControlID="txtFechaVigencia">
                                </cc1:CalendarExtender>
                            </div>
                        </div>

                        
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="panel-footer">
                    <asp:Button ID="btnGuardar" runat="server"  CssClass="btn btn-primary" Text="Guardar" />
                    <asp:Button ID="btnSalir" runat="server"  CssClass="btn btn-default" Text="Salir" />
            </div>
        </asp:Panel>
    </div>
    
    <div>
        <asp:HiddenField ID="hfIdInstrumento" runat="server" />
        <asp:HiddenField ID="hfIdPais" runat="server" />
        <asp:HiddenField ID="hfTipoSocio" runat="server" />
        <asp:HiddenField ID="hfBloquePais" runat="server" />
    </div>
</asp:Content>
