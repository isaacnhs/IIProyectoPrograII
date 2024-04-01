using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaVeterinaria.Views.Reportes
{
    public partial class ReporteMascotasForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está autenticado
            if (Session["UsuarioAutenticado"] == null || !(bool)Session["UsuarioAutenticado"])
            {
                // Si no está autenticado, redirigir al usuario a la página de inicio de sesión
                Response.Redirect("~/Views/LoginForm.aspx");
            }

            if (!IsPostBack)
            {
                // Cargar la lista de mascotas
                CargarMascotas();
            }
        }

        private void CargarMascotas()
        {
            // Lógica para cargar la lista de mascotas en el GridView, incluyendo las tablas relacionadas
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = @"SELECT M.ID_Mascota, M.Nombre_Mascota, R.Nombre_Raza AS Raza, TM.Nombre_TipoMascota AS TipoMascota, C.Nombre_Cliente, M.Comida_Favorita
                             FROM Mascotas M
                             INNER JOIN Razas R ON M.ID_Raza = R.ID_Raza
                             INNER JOIN TiposMascotas TM ON M.ID_TipoMascota = TM.ID_TipoMascota
                             INNER JOIN Clientes C ON M.ID_Cliente = C.ID_Cliente";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    GridViewReporteMascotas.DataSource = table;
                    GridViewReporteMascotas.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar la lista de mascotas: " + ex.Message + "');</script>");
            }
        }
    }
}