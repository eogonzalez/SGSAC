<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/General.Master" CodeBehind="frmAsignaCategoriasSAC.aspx.vb" Inherits="Capa_Presentacion.frmAsignaCategoriasSAC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SelectSingleRadiobutton(rdbtnid) {
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Panel principal --%>
    <div class="panel panel-primary">
        <div class="panel-heading"> Asignar Categorias de Desgravación al SAC </div>

        <%-- Cuerpo del Formulario --%>
        <div class="panel-body form-horizontal">
            <%-- Area de datos generales del SAC --%>
            <h5>
                <span class="label label-primary">Datos Generales del SAC
                </span>
            </h5>
            <div class="form-group">

                <asp:Label ID="lbl_año_vigencia" CssClass="control-label col-xs-2" text="Año Vigencia SAC" runat="server"> </asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_año_vigencia" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>

                <asp:Label ID="lbl_version_enmienda" CssClass="control-label col-xs-2" Text="Enmienda al SAC" runat="server"></asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txt_version_enmienda" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                 </div>

                <asp:Label ID="lbl_periodo_enmienda_sac" CssClass="control-label col-xs-2" Text="Periodo Enmienda SAC del:" runat="server"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_periodo_año_inicial" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>
                <asp:Label ID="lbl_al" CssClass="control-label col-xs-1" Text=" Al:" runat="server"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txt_periodo_año_final" CssClass="form-control" runat="server" disabled> </asp:TextBox>
                </div>
                    
            </div>

            <%-- Area de Datos del Instrumento --%>
            <h5>
                <span class="label label-primary">Datos del Instrumento Comercial
                </span>
            </h5>
            <div class="form-group">
                <asp:Label ID="lbl_nombre_insrumento" CssClass="control-label col-xs-3" Text=" Nombre del Instrumento Comercial: " runat="server"></asp:Label>
                <div class="col-xs-9">
                    <asp:TextBox ID="txt_nombre_instrumento" CssClass="form-control" runat="server" disabled></asp:TextBox>
                </div>
            </div>

            <%-- Area de Categoria y Codigo Arancelario --%>
            <h5>
                <span class="label label-primary">Seleccionar Categoria de Desgravacion y Código Arancelario:
                </span>
            </h5>
            <div class="form-group">
                <asp:Label ID="lbl_categoria_asignar" CssClass="control-label col-xs-2" Text="Categoria para asignar:" runat="server"></asp:Label>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddl_categoria_asignar" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <asp:Label ID="lbl_codigo_arancel" CssClass="control-label col-xs-2" Text="Código Arancelario:" runat="server"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txt_codigo_arancel" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:Button ID="btn_seleccionar" CssClass="btn btn-primary" Text="Seleccionar" runat="server" />
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

            <%-- Grid para asignar categoria --%>
            <h5>
                <span class="label label-primary">Asignar Categoria de Desgravación
                </span>
            </h5>
            <div class="table-responsive">
                <asp:GridView ID="gvAsignarCategorias" runat="server"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No se encontraron incisos arancelarios"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="id_inciso" SortExpression="id_inciso">
                            <HeaderStyle CssClass="ColumnaOculta" />
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="rb_inciso" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="inciso" HeaderText="Inciso Arancelario" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion Inciso Arancelario" />
                        <asp:BoundField DataField="dai" HeaderText="DAI SAC(Base)" />
                        <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="codigo_precision" HeaderText="Código Precision" />
                        <asp:BoundField DataField="descripcion_precision" HeaderText="Descripcion Precision" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <%-- Pie de formulario --%>
        <div class="panel-footer">
            <asp:Button ID="btn_asigna_categoria" CssClass="btn btn-primary" runat="server" Text="Asignar Categoria" />
            <asp:Button ID="btn_Salir" CssClass="btn btn-default" runat="server" Text="Salir" />
        </div>

    </div>

    <div>
        <asp:HiddenField ID="hfIdInstrumento" runat="server" />
    </div>
</asp:Content>
