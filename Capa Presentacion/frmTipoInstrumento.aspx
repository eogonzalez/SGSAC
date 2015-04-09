<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmTipoInstrumento.aspx.vb" Inherits="Capa_Presentacion.frmTipoInstrumento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="Tipo de Instrumento"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Código correlativo:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Tipo de instrumento:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Height="16px" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label4" runat="server" Text="Observaciones:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Height="61px" TextMode="MultiLine" Width="544px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="2" style="text-align: center">
                    <table class="auto-style1">
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Guardar" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Salir" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
