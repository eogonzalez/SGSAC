<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmInstrumentosMant.aspx.vb" Inherits="Capa_Presentacion.frmInstrumentosMant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td colspan="6">
                    <asp:Label ID="Label1" runat="server" Text="Datos Generales"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table class="auto-style1">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Nombre: "></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtNombreInstrumento" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Tipo Instrumento:"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Sigla:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSigla" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Sigla alterna:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSiglaAlterna" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Acuerdo entre:"></asp:Label>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox2" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Observaciones:"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtObservaciones" runat="server" Height="56px" TextMode="MultiLine" Width="503px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="Label8" runat="server" Text="Registro de Fechas para Guatemala:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Fecha Firma:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaFirma" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Fecha Ratificación:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaRatifica" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Fecha Vigencia:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaVigencia" runat="server" TextMode="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="Guardar" />
                </td>
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="Button2" runat="server" Text="Salir" />
                </td>
            </tr>
        </table>
    <div>
    
    </div>
    </form>
</body>
</html>
