<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Capa_Presentacion.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link href="Style/Login.css" rel="stylesheet" type="text/css"/>
    <link href="Style/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="Style/bootstrap/css/bootstrap-theme.css" rel="stylesheet" />


</head>

<body>
    <table style="margin: 0 auto 0 auto; text-align: center; width: 100%; background-color: #FFFFFF">
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/LogoNew.jpg" Height="65px" Width="310px"  />
            </td>
            <%--<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp</td>--%>
            <td style="text-align: center">
                <asp:Label ID="lblTitulo" runat="server" Text="SIGE-SAC <br /> Sistema de Gestión del SAC <br /> Ministerio de Economía" ForeColor="#054795" Font-Names="Verdana" Font-Bold="true" Font-Size="Large"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:Label ID="txtFecha" runat="server" ForeColor="#054795" Font-Names="Verdana" Font-Bold="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: left; width: 100%; background-color: #054795"">
                <br />
            </td>
        </tr>
    </table>

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

<%--    <div class="container">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-md-offset-4">
                <h1 class="text-center login-title">Sign in to continue to Bootsnipp</h1>
                <div class="account-wall">
                    <img class="profile-img" src="https://lh5.googleusercontent.com/-b0-k99FZlyE/AAAAAAAAAAI/AAAAAAAAAAA/eu7opA4byxI/photo.jpg?sz=120"
                        alt=""/>
                    <form class="form-signin">
                        <input type="text" class="form-control" placeholder="Email" required autofocus/>
                        <input type="password" class="form-control" placeholder="Password" required/>
                        <button class="btn btn-lg btn-primary btn-block" type="submit">
                            Sign in</button>
                        <label class="checkbox pull-left">
                            <input type="checkbox" value="remember-me"/>
                            Remember me
                        </label>
                        <a href="#" class="pull-right need-help">Need help? </a><span class="clearfix"></span>
                    </form>
                </div>
                <a href="#" class="text-center new-account">Create an account </a>
            </div>
        </div>
    </div>--%>


    <script src="Script/jQuery/jquery-1.11.3.js"></script>
    <script src="Style/bootstrap/js/bootstrap.js"></script>
</body>
</html>
