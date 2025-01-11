<%@ Page Title="Menú" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="tp_cuatrimestral_equipo_24.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="EstilosTarjetas.css" rel="stylesheet" />
    <link href="EstilosFiltrar.css" rel="stylesheet" />
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background: linear-gradient(rgba(0, 0, 0, .7), rgba(0, 0, 0, .7)), url("https://previews.123rf.com/images/yupiramos/yupiramos1307/yupiramos130700939/20982775-restaurante-de-piel-sobre-fondo-de-color-rosa-ilustraci%C3%B3n-vectorial.jpg");
            background-size: cover;
            background-color: #f5f0f0b6;   
            color: #721c24;  
            min-height: 100vh;  
            position: relative;   
        }

        #wrapper {
            min-height: 100vh; 
            display: flex;
            flex-direction: column;
        }


        .label-filtrar {
            display: flex;
            align-items: center;
            font-size: 1.25rem;
            font-weight: bold;
            margin-right: 10px;
            color: #ffffff;
            font-family: 'Roboto', sans-serif;
        }

        .btn-container {
            display: flex;
            justify-content: center;
            margin: 20px 0;
        }

        .btn-custom {
            display: inline-block;
            padding: 10px 20px;
            font-size: 1rem;
            font-weight: bold;
            color: white;
            background-color: #dc3545; /* Color rojo para el botón */
            border: none;
            border-radius: 5px;
            text-align: center;
            text-decoration: none;
            transition: background-color 0.3s, transform 0.3s;
        }

            .btn-custom:hover {
                background-color: #c82333; /* Color rojo más oscuro al pasar el mouse */
                transform: scale(1.05); /* Efecto de agrandar el botón al pasar el mouse */
            }

            .btn-custom:active {
                background-color: #bd2130; /* Color rojo aún más oscuro al hacer clic */
                transform: scale(1); /* Regresa el botón a su tamaño original al hacer clic */
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p class="title">Menú</p>

    <div class="filter-container">
        <%--<i class="fas fa-search">Buscar</i>--%>
        <asp:Label ID="LabelFiltrar" runat="server" Text="Buscar" CssClass="label-filtrar"></asp:Label>
        <asp:TextBox ID="Filtro" AutoPostBack="true" OnTextChanged="filtro_TextChanged" runat="server" CssClass="input-filtrar" />
    </div>

    <div class="filter-container">
        <asp:Label ID="LblTipo" runat="server" Text="Tipo:" CssClass="label-filtrar"></asp:Label>
        <asp:DropDownList runat="server" ID="ddlFiltradoTipo" class="form-select" aria-label="Default select example"
            OnSelectedIndexChanged="ddlFiltradoTipo_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="Desayuno/Merienda"></asp:ListItem>
            <asp:ListItem Text="Almuerzo/Cena"></asp:ListItem>
            <asp:ListItem Text="Postres"></asp:ListItem>
            <asp:ListItem Text="Bebidas"></asp:ListItem>
            <asp:ListItem Text="Mostrar todo"></asp:ListItem>
        </asp:DropDownList>
    </div>

    <section class="body-def">
        <div class="contenedor-card">
            <asp:Repeater runat="server" ID="idRep">
                <ItemTemplate>
                    <div class="card h-100 tarjeta-rest" style="background: url('<%# Eval("UrlImagen") %>') center; background-size: cover;">
                        <div class="wrap-text_tarjeta-rest">
                            <h3><%# Eval("Nombre") %></h3>
                            <p><%# Eval("Tipo") %></p>
                            <%int p = Convert.ToInt32(Session["Puesto"]);
                                if (p == 2 || p == 1)
                                {
                            %>
                            <p>Cant. <%# Eval("Stock") %></p>
                            <asp:CheckBox ID="CheckBoxActivo" runat="server" Enabled="false" Text="Activo" Checked='<%# Eval("Activo") %>' />
                            <% }

%>
                            <p><%# Eval("descripcion") %></p>
                            <h3>$ <%# Eval("Precio") %></h3>

                            <% if (p == 2 || p == 1)
                                { %>
                            <div class="cta_tarjeta-rest">
                                <a href='<%# "DetalleInsumo.aspx?IdInsumo=" + Eval("IdInsumo") %>'>Detalles</a>
                            </div>
                            <%
                                }
                            %>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </section>

    <div class="btn-container">
        <asp:Button ID="btnAgregarInsumo" runat="server" Text="AGREGAR INSUMO" class="btn-custom" OnClick="AgregarInsumo_Click" ></asp:Button>
    </div>


    <asp:GridView ID="dgvArticulos" runat="server"></asp:GridView>
</asp:Content>
