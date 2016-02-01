<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="Inicio.aspx.vb" Inherits="Capa_Presentacion.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 44%;
        }
        .auto-style3 {
            text-align: right;
            width: 129px;
        }
        .auto-style4 {
            width: 12px;
        }
        .auto-style5 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="margin: 0 auto 0 auto">
            <tr>
                <td style="text-align:left">
<%--                    <asp:Menu ID="MenuPrincipal" runat="server" disappearafter="500" Orientation="Horizontal" staticdisplaylevels="2" font-names="Trebuchet MS, Arial" >
                        <staticmenuitemstyle backcolor="#054795" forecolor="WhiteSmoke" horizontalpadding="35" verticalpadding="10" />
                        <statichoverstyle backcolor="#0066cc" forecolor="White" borderstyle="Solid" borderwidth="1px" />
                        <dynamicmenuitemstyle backcolor="RoyalBlue" forecolor="WhiteSmoke" horizontalpadding="5" verticalpadding="2" />
                        <dynamichoverstyle backcolor="CornflowerBlue" forecolor="White" borderstyle="Solid" borderwidth="1px" />  
                                     
                    </asp:Menu>--%>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <table style="margin: 0 auto 0 auto">
            <tr>
                <%--<td>
                    <asp:Image ID="imgLogo" ImageUrl="~/Images/ministerioeco.jpg" runat="server" Height="95px" Width="264px" />
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp</td>
                <td style="text-align:center">
                    <asp:Label ID="lblTitulo" runat="server" Text="Sistema de Gestión del SAC <br /> Ministerio de Economía" ForeColor="#054795" Font-Names="Verdana" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                </td>--%>
            </tr>
        </table>
    </div>   
</asp:Content>
