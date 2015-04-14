<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmTipoInstrumento.aspx.vb" Inherits="Capa_Presentacion.frmTipoInstrumento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
         <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">

</head>
<body>
    <form id="form1" class="panel panel-primary" runat="server">
        <div class="panel-heading">Instrumentos Comerciales</div>
    <div>
    
        
                    <asp:ImageButton ID="ImageButton5" runat="server" Height="44px" ImageUrl="~/Images/nuevo.png" Width="46px" />
                    <asp:ImageButton ID="ImageButton4" runat="server" Height="44px" ImageUrl="~/Images/editar.png" Width="46px" />
                    <asp:ImageButton ID="ImageButton3" runat="server" Height="44px" ImageUrl="~/Images/desgraba.png" Width="46px" />
        <asp:ImageButton ID="ImageButton2" runat="server" Height="44px" ImageUrl="~/Images/paises.png" Width="46px" />
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="44px" ImageUrl="~/Images/ayuda.png" Width="46px" />

         <table class="table table-condensed">
            <tr class="active">
                <td class="active">
                    
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
                    
                </td>
            </tr>
        </table>

    </div>
    </form>
</body>
</html>