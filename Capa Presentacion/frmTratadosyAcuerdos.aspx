<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/General.Master" CodeBehind="frmTratadosyAcuerdos.aspx.vb" Inherits="Capa_Presentacion.frmTratadosyAcuerdos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style4 {
            text-align: center;
            width: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-primary">

  
    <div class="panel-heading">Instrumentos Comerciales</div>
        <br />

        <div class="btn-group pull-right" role="group" >
            <asp:LinkButton ID="lkBtt_nuevo" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i> Nuevo </asp:LinkButton>
        
            <asp:LinkButton ID="lkBtt_editar" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-edit"></i> Editar </asp:LinkButton>
                
            <asp:LinkButton ID="lkBtt_categorias" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-random"></i>  Categorias </asp:LinkButton>
                
            <asp:LinkButton ID="lkBtt_paises" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-globe"></i> Paises </asp:LinkButton>
        
            <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-primary">
                <i aria-hidden="true" class="glyphicon glyphicon-question-sign"></i> Ayuda </asp:LinkButton>
      
        </div>

        <div>
            <asp:GridView ID="gvInstrumentos" runat="server" 
                CssClass="table table-hover table-striped" 
                GridLines="None"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="id_instrumento" HeaderText="Id Instrumento" SortExpression="id_intrumento" Visible="false" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_sigla" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="sigla" HeaderText="Sigla" />
                    <asp:BoundField DataField="nombre_instrumento" HeaderText="Tratado o Acuerdo Comercial" />
                    <asp:BoundField DataField="fecha_firma" HeaderText="Fecha Firma" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="fecha_ratificada" HeaderText="Fecha Ratificación" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="fecha_vigencia" HeaderText="Fecha Vigencia" DataFormatString="{0:d}" />
                                    
                </Columns>
            </asp:GridView>
        </div>
      </div>
</asp:Content>