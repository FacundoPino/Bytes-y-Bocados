<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="tp_cuatrimestral_equipo_24.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.7/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link href="EstilosFiltrar.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="filter-container">
        <%--<i class="fas fa-search">Buscar</i>--%>
        <asp:Label ID="LabelFiltrar" runat="server" Text="Filtrar Insumos:" CssClass="label-filtrar"></asp:Label>
        <asp:TextBox ID="Filtro" AutoPostBack="true" OnTextChanged="filtro_TextChanged" runat="server" CssClass="input-filtrar" />
    </div>

    <div class="container mt-5">
        <h1 class="mb-4">Pedido para mesa <span id="numeroMesa" class="numero-mesa">
            <asp:Label ID="numeroMesaLabel" runat="server"></asp:Label></span></h1>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="IdInsumo" HeaderText="ID Insumo" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:ButtonField ButtonType="Button" CommandName="Agregar" Text="Agregar" />
            </Columns>
        </asp:GridView>

        <div class="Pedido-label">
            <h2 class="mt-5">Pedidos</h2>
        </div>
        <asp:GridView ID="GridViewPedidos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowCommand="GridViewPedidos_RowCommand">
            <Columns>
                <asp:BoundField DataField="Insumo.Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <%# (Convert.ToInt32(Eval("Cantidad")) * Convert.ToDecimal(Eval("PrecioUnitario"))).ToString("C") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnAumentar" runat="server" CommandName="Aumentar" CommandArgument='<%# Container.DataItemIndex %>' Text="+" CssClass="btn btn-success btn-sm" />
                        <asp:Button ID="btnDisminuir" runat="server" CommandName="Disminuir" CommandArgument='<%# Container.DataItemIndex %>' Text="-" CssClass="btn btn-warning btn-sm" />
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="total-label">
            <h3 class="mt-5">Total:
            <asp:Label ID="TotalLabel" runat="server" Text=""></asp:Label>
            </h3>
        </div>

        <div class="row g-3">
            <div class="col-auto">
                <asp:Button ID="BtnCerrarPedido" runat="server" CssClass="btn btn-outline-danger" Text="Cerrar pedido" OnClick="BtnCerrarPedido_Click" />
            </div>
        </div>

        <asp:Label ID="ErrorMessage" runat="server" CssClass="error-message"></asp:Label>
    </div>
</asp:Content>
