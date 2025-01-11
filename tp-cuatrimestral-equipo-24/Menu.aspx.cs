using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace tp_cuatrimestral_equipo_24
{
    public partial class Menu : System.Web.UI.Page
    {
        public List<Insumo> ListaInsumos = new List<Insumo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int P = Convert.ToInt32(Session["Puesto"]);
                if (P != 2)
                {
                    btnAgregarInsumo.Visible = false;
                }
                List<Insumo> LimpiarInsumo = new List<Insumo>();
                /*
                InsumosNegocio insumo = new InsumosNegocio();
                Session.Add("ListadoMenu", insumo.ListarConSp());
                idRep.DataSource = (List<Insumo>)Session["ListadoMenu"];
                idRep.DataBind();
                */

                ddlFiltradoTipo.Text = "Mostrar todo";

                InsumosNegocio insumo = new InsumosNegocio();
                ListaInsumos = insumo.ListarConSpInsumo();
                Session["Listado"] = ListaInsumos;
                idRep.DataSource = ListaInsumos;
                idRep.DataBind();

            }
        }
        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            List<Insumo> ListaFiltrada = new List<Insumo>();

            if (Session["Listado"] != null && Session["Listado"] is List<Insumo>)
            {
                if (Filtro.Text == "")
                {
                    ListaFiltrada = (List<Insumo>)Session["Listado"];
                }
                else
                {
                    ListaFiltrada = ((List<Insumo>)Session["Listado"]).FindAll(X => X.Nombre.ToUpper().Contains(Filtro.Text.ToUpper()));
                }
            }
            else
            {
                InsumosNegocio insumo = new InsumosNegocio();
                ListaFiltrada = insumo.ListarConSpInsumo();
                Session["Listado"] = ListaFiltrada;
            }
            idRep.DataSource = ListaFiltrada;
            idRep.DataBind();
        }

        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    InsumosNegocio negocio = new InsumosNegocio();
        //    System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
        //    int id = Convert.ToInt32(btn.CommandArgument);

        //    negocio.eliminar(id);
        //    Response.Redirect(Request.RawUrl);
        //}

        protected void ddlFiltradoTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Tipo = ddlFiltradoTipo.Text;
            List<Insumo> lista = new List<Insumo>();

            if (Session["Listado"] != null)
            {
                lista = (List<Insumo>)Session["Listado"];
            }
            List<Insumo> listaFiltrada = new List<Insumo>();

            foreach(var item in lista)
            {
                if (item.Tipo == Tipo)
                {
                    listaFiltrada.Add(item);
                }
            }

            if (Tipo == "Mostrar todo")
            {
                listaFiltrada = (List<Insumo>)Session["Listado"];
            }

            idRep.DataSource = listaFiltrada;
            idRep.DataBind();

        }

        protected void AgregarInsumo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarInsumo.aspx");
        }
    }
}
