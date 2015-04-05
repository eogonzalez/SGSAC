<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Capa_Presentacion.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 99%;
        }
    </style>
</head>
<body style="width: 322px">
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text="Login"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="Usuario" Width="100px"></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="txt_usuario" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label2" runat="server" Text="Contraseña" Width="100px"></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="txt_contraseña" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btn_ingresar" runat="server" Text="Ingresar" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
