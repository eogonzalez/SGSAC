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

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">

    <link href="CSS/Login.css" rel="stylesheet" />

</head>

<body>
    <div class="container">
      <div class =" card card-container">
           <form id="form1" runat="server" class="form-signin">
                    <asp:TextBox ID="txt_usuario" type="usuario" CssClass="form-control" runat="server" placeholder="Usuario" required autofocus></asp:TextBox>
                    <br />

                    <asp:TextBox ID="txt_contraseña" type="password" CssClass="form-control" runat="server" TextMode="Password" placeholder="Contraseña" required></asp:TextBox>
                    <asp:Button ID="btn_ingresar" CssClass="btn btn-lg btn-primary btn-block btn-signin" runat="server" Text="Ingresar" />
                    <asp:Label ID="lbl_mensaje_login" runat="server"></asp:Label>
          </form>
        </div>
    </div>
</body>
</html>
