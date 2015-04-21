<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmTratadosyAcuerdos.aspx.vb" Inherits="Capa_Presentacion.frmTratadosyAcuerdos" %>

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
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>        
        <asp:Panel ID="pnlNuevoInstrumento" runat="server" BorderColor="Black" BackColor="White"
                BorderStyle="Inset" BorderWidth="1px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Tipo de Instrumento"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Código correlativo:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIdTipoInstrumento" CssClass=" form form-control"  runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="text-info" Text="Tipo de instrumento:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" placeholder="Tipo de instrumento" CssClass="form-control" MaxLength="100" Width="324px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtDescripcion_FilteredTextBoxExtender" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz ÁÉÍÓÚáéíóú" runat="server" BehaviorID="txtDescripcion_FilteredTextBoxExtender" TargetControlID="txtDescripcion" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Observaciones:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtObservaciones" CssClass="form-control" runat="server" Height="61px" TextMode="MultiLine" Width="544px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtObservaciones_FilteredTextBoxExtender" runat="server" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz ÁÉÍÓÚáéíóú1234567890/-.,:;" BehaviorID="txtObservaciones_FilteredTextBoxExtender" FilterMode="ValidChars" FilterType="Custom" TargetControlID="txtObservaciones" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                            <asp:Button ID="Button1" runat="server" Text="Guardar" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                            <asp:Button ID="Button2" runat="server" Text="Salir" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
    </div>

    <div class="panel panel-primary">


        <div class="panel-heading">Instrumentos Comerciales</div>
        <br />

        <div class="btn-group pull-right" role="group">
            <asp:LinkButton ID="lkBtt_nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i> Nuevo </asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkBtt_nuevo_ModalPopupExtender"  BackgroundCssClass="modalBackground" runat="server" BehaviorID="lkBtt_nuevo_ModalPopupExtender" PopupControlID="pnlNuevoInstrumento" DynamicServicePath="" TargetControlID="lkBtt_nuevo">
            </cc1:ModalPopupExtender>

            <asp:LinkButton ID="lkBtt_editar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-edit"></i> Editar </asp:LinkButton>

            <asp:LinkButton ID="lkBtt_categorias" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-random"></i>  Categorias </asp:LinkButton>

            <asp:LinkButton ID="lkBtt_paises" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-globe"></i> Paises </asp:LinkButton>

            <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-question-sign"></i> Ayuda </asp:LinkButton>

        </div>

        <div>
            <asp:GridView ID="gvInstrumentos" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron instrumentos comerciales"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="id_instrumento" HeaderText="Id Instrumento" SortExpression="id_intrumento" Visible="false" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButtonList ID="rb_sigla" runat="server"></asp:RadioButtonList>
                            <%--<asp:RadioButton ID="rb_sigla" runat="server" />--%>
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
        <div>
            
        </div>
    </div>
</asp:Content>
