<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmConsultaSACAsocia.aspx.vb" Inherits="Capa_Presentacion.frmConsultaSACAsocia" %>

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
                    <h5>
                        <span class="label label-primary">1. Seleccione Instrumento Comercial
                        </span>
                    </h5>
                    <div class="form-group">
                        <asp:Label ID="lbl_nombre_insrumento" CssClass="control-label col-xs-3" Text=" Nombre del Instrumento Comercial: " runat="server"></asp:Label>
                        <div class="col-xs-9">
                            <asp:DropDownList ID="ddl_instrumento_comercial" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <%-- Area de Categoria y Codigo Arancelario --%>
                    <h5>
                        <span class="label label-primary">2. Seleccionar Categoria de Desgravacion y Código Arancelario:
                        </span>
                    </h5>
                    <div class="form-group">

                        <asp:Label ID="lbl_todas_categorias" CssClass="control-label col-xs-2" Text="Todas las categorias:" runat="server"></asp:Label>
                        <div class="col-xs-2">
                            <asp:CheckBox ID="cb_categorias" runat="server" AutoPostBack="True" />
                        </div>

                        <asp:Label ID="lbl_categoria_asignar" CssClass="control-label col-xs-2" Text="Categoria para asignar:" runat="server"></asp:Label>
                        <div class="col-xs-5">
                            <asp:DropDownList ID="ddl_categoria_asignar" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">

                        <asp:Label ID="lbl_todos_incisos" CssClass="control-label col-xs-2" Text="Todos los incisos:" runat="server"></asp:Label>
                        <div class="col-xs-2">
                            <asp:CheckBox ID="cb_incisos" runat="server" AutoPostBack="True" />
                        </div>

                        <div>
                            <asp:Label ID="lbl_codigo_arancel" CssClass="control-label col-xs-2" Text="Código Arancelario:" runat="server"></asp:Label>
                            <div class="col-xs-4">
                                <asp:TextBox ID="txt_codigo_inciso_rep" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-xs-2">
                                <asp:Button ID="btn_seleccionar" CssClass="btn btn-primary" Text="Consultar" runat="server" />
                            </div>
                        </div>

                    </div>

                    <%-- Descripcion Capitulo-Partida y Subpartida Seleccionada --%>
                    <h5>
                        <span class="label label-primary">Descripción Capitulo-Partida y Subpartida Seleccionada
                        </span>
                    </h5>

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
                                <asp:BoundField DataField="codigo_inciso" HeaderText="Inciso Arancelario" />
                                <asp:BoundField DataField="texto_inciso" HeaderText="Descripcion Inciso Arancelario" />
                                <asp:BoundField DataField="dai_base" HeaderText="DAI SAC(Base)" />
                                <asp:BoundField DataField="codigo_categoria" HeaderText="Categoria" />
                                <asp:BoundField DataField="inciso_presicion" HeaderText="Código Precision" />
                                <asp:BoundField DataField="texto_precision" HeaderText="Descripcion Precision" />
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
