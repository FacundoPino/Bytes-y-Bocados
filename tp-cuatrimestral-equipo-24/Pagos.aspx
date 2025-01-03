<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="tp_cuatrimestral_equipo_24.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="EstiloPagos.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body-recibo">
        <!-- Asegúrate de que esta clase esté aplicada -->
        <div class="recibo-container">
            <h4>Formulario Pago</h4>
            <div class="recibo-group">
                <asp:Label ID="lblpagos" runat="server" Text="Pagos:" CssClass="recibo-label"></asp:Label>
                <asp:DropDownList ID="ddlPagos" runat="server" CssClass="recibo-select">
                    <asp:ListItem Text="Efectivo" />
                    <asp:ListItem Text="Credito" />
                    <asp:ListItem Text="Debito" />
                    <asp:ListItem Text="Tranferencia" />
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="recibo-button" OnClick="btnConfirmar_Click" />
            <a href="Menu.aspx" class="recibo-back">Volver</a>
        </div>

        <div class="recibo-container" id="divtext" visible="false" runat="server">
            <div class="receipt">
                <h1 id="htext" runat="server" visible="false">Recibo de Pago</h1>
                <p>***************************************</p>
                <p>RECIBO DE CAJA</p>
                <p>***************************************</p>
                <p id="lblMesa" runat="server" visible="false">Num de Mesa: <span id="txtMesa" runat="server"></span></p>
                <p id="lblmesero" runat="server" visible="false">Mesero: <span id="txtMesero" runat="server"></span></p>
                <p id="lblFecha" runat="server" visible="false">Fecha: <span id="Txtfecha" runat="server"></span></p>
                <p id="lbltotal" runat="server" visible="false">Total: <span id="txtTotal" runat="server"></span></p>
                <p id="lblTipo" runat="server" visible="false">Tipo de Pago: <span id="txtTipo" runat="server"></span></p>
                <p id="lblconsumicion" runat="server" visible="false">Consumición: <span id="txtConsumicion" runat="server"></span></p>
                <p>***************************************</p>
                <p>Note: <span id="note"></span>ESPERO QUE LE HAYA GUSTADO</p>
                <p>***************************************</p>
                <p>GRACIAS!</p>
            </div>
        </div>


        <asp:Button ID="btnPdf" runat="server" Text="Descargar" CssClass="recibo-button" Visible="true" OnClick="btnPdf_Click" />

    </div>
</asp:Content>
