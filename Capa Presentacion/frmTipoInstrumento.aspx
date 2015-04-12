<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmTipoInstrumento.aspx.vb" Inherits="Capa_Presentacion.frmTipoInstrumento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
         <table class="auto-style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Strikeout="False" ForeColor="#000099" Text="Datos Tipo Instrumentos Comerciales"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:ImageButton ID="ImageButton5" runat="server" Height="44px" ImageUrl="~/Images/nuevo.png" Width="46px" />
                </td>
                <td class="auto-style4">
                    <asp:ImageButton ID="ImageButton4" runat="server" Height="44px" ImageUrl="~/Images/editar.png" Width="46px" />
                </td>
                <td class="auto-style4">
                    <asp:ImageButton ID="ImageButton3" runat="server" Height="44px" ImageUrl="~/Images/desgraba.png" Width="46px" />
                </td>
                <td class="auto-style4">
                    <asp:ImageButton ID="ImageButton2" runat="server" Height="44px" ImageUrl="~/Images/paises.png" Width="46px" />
                </td>
                <td class="auto-style4">
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="44px" ImageUrl="~/Images/ayuda.png" Width="46px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="6">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvTipoInstrumento" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="id_tipo_instrumento" HeaderText="Id Tipo Instrumento" SortExpression="id_tipo_instrumento" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rb_tipo_instrumento" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Tipo Instrumento" />
                                    
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>

    </div>
    </form>
</body>
</html>