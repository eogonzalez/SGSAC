<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmTipoRelacionInstrumentoMant.aspx.vb" Inherits="Capa_Presentacion.frmTipoRelacionInstrumentoMant" %>

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
            width: 206px;
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
                    <asp:Label ID="Label1" runat="server" Text="Tipo Relación Instrumento"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Codigo Correlativo:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIdTipoInstrumento" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Tipo de relación del instrumento:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescripcion" runat="server" Height="16px" Width="236px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label4" runat="server" Text="Observaciones:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtObservaciones" runat="server" Height="65px" TextMode="MultiLine" Width="473px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="auto-style1">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnSalir" runat="server" Text="Salir" />
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
