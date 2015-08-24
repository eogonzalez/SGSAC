<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmEnmiendas.aspx.vb" Inherits="Capa_Presentacion.frmEnmiendas" %>

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

    <%-- Panel Principal --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Enmiendas</div>
        <br />

        <%-- Area de botones --%>
        <div class="btn-group pull-right" role="group">
            <asp:LinkButton ID="lkBtt_nuevo" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i> Nuevo </asp:LinkButton>

            <cc1:modalpopupextender id="lkBtt_nuevo_ModalPopupExtender" backgroundcssclass="modalBackground"
                runat="server" behaviorid="lkBtt_nuevo_ModalPopupExtender" popupcontrolid="pnlNuevoSAC"
                dynamicservicepath="" targetcontrolid="lkBtt_nuevo">
            </cc1:modalpopupextender>

            <asp:LinkButton ID="lkBtt_editar" runat="server" CssClass="btn btn-primary"> <i aria-hidden="true" class="glyphicon glyphicon-edit"></i> Editar </asp:LinkButton>

            <asp:LinkButton ID="lkBtt_categorias" runat="server" CssClass="btn btn-primary"> 
                        <i aria-hidden="true" class="glyphicon glyphicon-random"></i> 
                         Correlacion 
            </asp:LinkButton>



            <asp:LinkButton ID="lkBtn_asignar_categorias" runat="server" CssClass="btn btn-primary"> 
                        <i aria-hidden="true" class="glyphicon glyphicon-check"></i> 
                        Aprobar Version
            </asp:LinkButton>

        </div>

        <%-- Gridview --%>

        <div>
            <asp:GridView ID="gv_Versiones_SAC" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron instrumentos comerciales"
                AutoGenerateColumns="false">

                <Columns>
                    <asp:BoundField DataField="id_version" SortExpression="id_version">
                        <HeaderStyle CssClass="ColumnaOculta" />
                        <ItemStyle CssClass="ColumnaOculta" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_version" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="anio_version" HeaderText="Versión" />
                    <asp:BoundField DataField="enmienda" HeaderText="Descripción" />
                    <asp:BoundField DataField="fecha_inicia_vigencia" HeaderText="Fecha Inicio Vigencia" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="fecha_fin_vigencia" HeaderText="Fecha Fin Vigencia" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="observaciones" HeaderText="Base Normativa" />

                </Columns>

            </asp:GridView>
        </div>

    </div>

    <div>
        <asp:Panel ID="pnlNuevoSAC" CssClass="panel panel-primary" runat="server" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="35%">
            <div class="panel-heading">Datos Generales de Versiones SAC </div>
            <div class="panel-body form-horizontal">

                <div class="form-group">
                    <asp:Label ID="Label2" CssClass="control-label col-xs-3" runat="server" Text="Año Version: "></asp:Label>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txtAñoVersion" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label3" CssClass="control-label col-xs-3" runat="server" Text="Descripcion:"></asp:Label>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txtDescripcion" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label9" CssClass="control-label col-xs-3" runat="server" Text="Fecha Inicio Vigencia:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtFechaInicioVigencia" CssClass="form-control" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFechaInicioVigencia_CalendarExtender" runat="server" BehaviorID="txtFechaFirma_CalendarExtender" TargetControlID="txtFechaInicioVigencia" />
                    </div>

                    <asp:Label ID="Label10" CssClass="control-label col-xs-3" runat="server" Text="Fecha Fin Vigencia:"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtFechaFinVigencia" CssClass="form-control" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFechaFinVigencia_CalendarExtender" runat="server" BehaviorID="txtFechaRatifica_CalendarExtender" TargetControlID="txtFechaFinVigencia" />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label7" CssClass="control-label col-xs-3" runat="server" Text="Base Normativa:"></asp:Label>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txtObservaciones" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
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
        <asp:HiddenField ID="hfIdVersionSAC" runat="server" />
        <asp:HiddenField ID="hfAnioVersion" runat="server" />
    </div>
</asp:Content>
