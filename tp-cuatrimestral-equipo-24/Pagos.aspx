<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="tp_cuatrimestral_equipo_24.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="EstiloPagos.css" rel="stylesheet" />
    <style>
        .recibo-button {
            display: inline-block;
            width: 25%;
            padding: 10px;
            background: linear-gradient(90deg, #34d058, #28a745);
            color: #ffffff;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            text-align: center;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            transition: all 0.4s ease;
            box-shadow: 0 5px 15px rgba(40, 167, 69, 0.3);
        }

            .recibo-button:hover {
                background: linear-gradient(90deg, #28a745, #34d058);
                box-shadow: 0 8px 20px rgba(40, 167, 69, 0.4);
                transform: translateY(-2px);
            }

            .recibo-button:active {
                transform: translateY(1px);
                box-shadow: 0 4px 10px rgba(40, 167, 69, 0.2);
            }

            .recibo-button:focus {
                outline: none;
                box-shadow: 0 0 0 3px rgba(40, 167, 69, 0.5);
            }
    </style>

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


        <asp:Button ID="btnPdf" runat="server" Text="Descargar" CssClass="recibo-button" Visible="false" OnClick="btnPdf_Click" />

    </div>
</asp:Content>
