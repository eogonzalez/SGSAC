<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmConsultaVerSAC.aspx.vb" Inherits="Capa_Presentacion.frmConsultaVerSAC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Panel principal --%>
    <div class="panel panel-primary">
        <div class="panel-heading">Consulta del  SAC </div>

        <%-- Cuerpo del Formulario --%>
        <div class="panel-body form-horizontal">


            <asp:UpdatePanel ID="Datos_SAC" runat="server">
                <ContentTemplate>

                    <%-- Area de Datos del Instrumento --%>
                    <h4>
                        <span class="label label-success">1. Seleccione versión del SAC
                        </span>
                    </h4>
                    <div class="form-group">
                        <asp:Label ID="lbl_nombre_version" CssClass="control-label col-xs-2" Text=" Version del SAC: " runat="server"></asp:Label>
                        <div class="col-xs-10">
                            <asp:DropDownList ID="ddl_version_SAC" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <%-- Area de Categoria y Codigo Arancelario --%>
                    <h4>
                        <span class="label label-success">2. Seleccionar el detalle a generar:
                        </span>
                    </h4>
                    <div class="form-group">

                        <asp:Label ID="lbl_todas_capitulos" CssClass="control-label col-xs-2" Text="Todas las capitulos:" runat="server"></asp:Label>
                        <div class="col-xs-1">
                            <asp:CheckBox ID="cb_capitulo" runat="server" AutoPostBack="True" />
                        </div>

                        <asp:Label ID="lbl_capitulo" CssClass="control-label col-xs-2" Text="Capitulo:" runat="server"></asp:Label>
                        <div class="col-xs-5">
                            <asp:DropDownList ID="ddl_capitulo" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </div>

                        <div class="col-xs-2">
                            <asp:Button ID="btn_seleccionar" CssClass="btn btn-primary" Text="Consultar" runat="server" />
                        </div>

                    </div>

<%--                    <div class="form-group">

                        <asp:Label ID="lbl_todas_partidas" CssClass="control-label col-xs-2" Text="Todas las partidas:" runat="server"></asp:Label>
                        <div class="col-xs-1">
                            <asp:CheckBox ID="cb_partidas" runat="server" AutoPostBack="True" />
                        </div>

                        <div>
                            <asp:Label ID="lbl_partida" CssClass="control-label col-xs-2" Text="Partida:" runat="server"></asp:Label>
                            <div class="col-xs-5">
                                <asp:DropDownList ID="ddl_partida" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>


                        </div>

                    </div>--%>

                    <%-- Descripcion Capitulo-Partida y Subpartida Seleccionada --%>
                    <%--                    <h4>
                        <span class="label label-success">Descripción Capitulo-Partida y Subpartida Seleccionada
                        </span>
                    </h4>

                    <div class="form-group">
                        <asp:Label ID="lbl_descripcion_capitulo" CssClass="control-label col-xs-2" Text="Capitulo:" runat="server"></asp:Label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txt_descripcion_capitulo" CssClass="form-control" runat="server" disabled></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">

                        <asp:Label ID="lbl_descripcion_partida" CssClass="control-label col-xs-2" Text="Partida:" runat="server"></asp:Label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txt_descripcion_partida" CssClass="form-control" runat="server" disabled></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lbl_descripcion_sub_partida" CssClass="control-label col-xs-2" Text="Subpartida:" runat="server"></asp:Label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txt_descripcion_sub_partida" CssClass="form-control" runat="server" disabled></asp:TextBox>
                        </div>
                    </div>--%>

                    <div class="form-group">
                        <span class="badge">Cantidad de incisos:
                            <asp:Label ID="lbl_cantidad" runat="server"></asp:Label>
                        </span>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>


            <%-- Grid que muestra incisos del SAC --%>

            <div class="table-responsive">
                <asp:UpdatePanel ID="Datos_GridView" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gv_incisos_sac" runat="server"
                            CssClass="table table-hover table-striped"
                            GridLines="None"
                            EmptyDataText="No se encontraron incisos arancelarios"
                            AutoGenerateColumns="false" AllowPaging="True">

                            <PagerSettings Mode="Numeric"
                                Position="Bottom"
                                PageButtonCount="10" />

                            <Columns>
                                <asp:BoundField DataField="codigo" HeaderText="Inciso Arancelario" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion Inciso Arancelario" />
                                <asp:BoundField DataField="SAC" HeaderText="DAI SAC(Base)" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <%-- Pie de formulario --%>
        <div class="panel-footer">
            <asp:Button ID="btn_genera" CssClass="btn btn-primary" runat="server" Text="Generar a Excel" />
            <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
        </div>

    </div>

</asp:Content>
