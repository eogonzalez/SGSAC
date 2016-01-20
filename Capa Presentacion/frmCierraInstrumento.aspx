<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmCierraInstrumento.aspx.vb" Inherits="Capa_Presentacion.frmCierraInstrumento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <%-- Panel principal --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Configurar Correlaciones </div>

        <%-- Cuerpo del Formulario --%>
        <div class="panel-body form-horizontal">

            <%-- Area de datos generales del la version de la enmienda SAC --%>
            <h5><span class="label label-default">Datos del Instrumento del Vigente</span></h5>

            <div class="form-group">

                <asp:Label ID="lbl_instrumento" CssClass="control-label col-xs-2" Text="Seleccione Instrumento a Cerrar" runat="server"> </asp:Label>
                <div class="col-xs-10">
                    <asp:DropDownList ID="ddl_instrumento" cssclass="form-control" runat="server"></asp:DropDownList>
                </div>

            </div>

            <div class="form-group">

                <asp:Label ID="lbl_fecha_sucripcion" CssClass="control-label col-xs-2" Text="Fecha Suscripción:" runat="server"> </asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txt_fecha_suscripcion" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

                <asp:Label ID="lbl_fecha_vigencia" CssClass="control-label col-xs-2" Text="Fecha Vigencia:" runat="server"> </asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txt_fecha_vigencia" CssClass="form-control" runat="server"> </asp:TextBox>
                </div>

                <asp:Label ID="lbl_anio_vigencia" CssClass="control-label col-xs-2" Text="Año Vigencia:" runat="server"> </asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txt_anio_vigencia" CssClass="form-control" runat="server"> </asp:TextBox>
                </div>

            </div>

            <div class="text-center">
                <div class="btn-group">
                    <asp:LinkButton ID="lkb_cerrar_instrumento" CssClass="btn btn-success" runat="server"><i aria-hidden="true" class="glyphicon glyphicon-check" ></i> Cerrar Instrumento </asp:LinkButton>
                    <asp:LinkButton ID="lkb_cancelar" CssClass="btn btn-danger" runat="server"><i aria-hidden="true" class="glyphicon glyphicon-remove"></i> Cancelar </asp:LinkButton>
                    <%--<asp:LinkButton ID="lkb_salir" CssClass="btn btn-primary" runat="server"><i aria-hidden="true" class="glyphicon glyphicon-do"></i> Salir </asp:LinkButton>                --%>
                </div>
            </div>

            <div>

                <asp:GridView ID="gvInstrumentos" runat="server"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No se encontraron datos de categorias comerciales"
                    AutoGenerateColumns="false">

                    <Columns>
                        <asp:BoundField DataField="id_categoria" SortExpression="id_categoria">
                            <HeaderStyle CssClass="ColumnaOculta"/>
                            <ItemStyle CssClass="ColumnaOculta"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo_categoria" HeaderText="Sigla" />
                        <asp:BoundField DataField="cant_asocia" HeaderText="Incisos Asociados" />
                    </Columns>

                </asp:GridView>
            </div>

        </div>

        <%-- Pie de formulario --%>
        <div class="panel-footer">
            <%--<asp:Button ID="btn_aprobar" CssClass="btn btn-danger" runat="server" Text="aprobar" />--%>
            <asp:Button ID="btn_salir" CssClass="btn btn-default" runat="server" Text="Salir" />
        </div>

    </div>

</asp:Content>
