<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmInstrumentosMant.aspx.vb" Inherits="Capa_Presentacion.frmInstrumentosMant" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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

     <!-- Latest compiled and minified CSS -->
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">--%>

    <!-- Optional theme -->
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">--%>

</head>
<body>

    <form id="form1" class="panel panel-primary" runat="server">
        <div class="panel-heading">Mantenimiento de Instrumentos Comerciales</div>
        
        <div class="panel-body">
           
            <div class="form-group">    
                <asp:Label ID="Label2" class="control-label col-xs-2" runat="server" Text="Nombre: "></asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtNombreInstrumento" type="text" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label3" class="control-label col-xs-2" runat="server" Text="Tipo Instrumento:"></asp:Label>
                <div class="col-sm-10">
                <asp:DropDownList ID="ddlstTipoInstrumento" class="form-control" runat="server">
                                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label4" class="control-label col-xs-2"  runat="server" Text="Sigla:"></asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtSigla" type="text" class="form-control" runat="server" ></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label5" class="control-label col-xs-2" runat="server" Text="Sigla alterna:"></asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtSiglaAlterna" type="text" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label6"  class="control-label col-xs-2" runat="server" Text="Acuerdo entre:"></asp:Label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlstTipoRelacion" class="form-control" runat="server">
                                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label7" class="control-label col-xs-2" runat="server" Text="Observaciones:"></asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtObservaciones" type="text" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </div>
            

        <div class="panel-body">
            <div class="panel-heading">
                <h4>
                     Registro de fechas para Guatemala 
                </h4>

            </div>
          
            <div class="form-group">
                <asp:Label ID="Label9" class="control-label col-sm-2" runat="server" Text="Fecha Firma:"></asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtFechaFirma"  class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label10" class="control-label col-sm-2" runat="server" Text="Fecha Ratificación:"></asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtFechaRatifica" class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>
   
            <div class="form-group">        
                <asp:Label ID="Label11" class="control-label col-sm-2" runat="server" Text="Fecha Vigencia:"></asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtFechaVigencia" class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>

        </div>

        <div class="panel-footer">
            <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Guardar" />
            <asp:Button ID="Button2" class="btn btn-default" runat="server" Text="Salir" />
        </div>
  
    </form>
</body>
</html>
