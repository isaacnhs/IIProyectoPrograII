using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SistemaVeterinaria.Views.Reportes
{
    public partial class ReporteUsuariosForm : System.Web.UI.Page
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
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT Login_Usuario, Nombre_Usuario, Clave_Usuario FROM Usuarios";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    GridViewUsuarios.DataSource = table;
                    GridViewUsuarios.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write($"<script>alert('Error al cargar los usuarios: {ex.Message}');</script>");
            }
        }
    }
}