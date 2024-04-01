using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SistemaVeterinaria.Views.Mascotas
{
    public partial class RazasForm : System.Web.UI.Page
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
                // Cargar la lista de razas
                CargarRazas();
            }
        }

        private void CargarRazas()
        {
            // Lógica para cargar la lista de razas en el GridView
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT ID_Raza, Nombre_Raza FROM Razas";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    GridViewRazas.DataSource = table;
                    GridViewRazas.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar la lista de razas: " + ex.Message + "');</script>");
            }
        }

        protected void btnAgregarRaza_Click(object sender, EventArgs e)
        {
            // Redirigir al usuario a la página para agregar una nueva raza
            Response.Redirect("~/Views/Mascotas/Razas/AgregarRazaForm.aspx");
        }

        protected void GridViewRazas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarRaza")
            {
                // Obtener el ID del cliente a eliminar
                int razaId = Convert.ToInt32(e.CommandArgument);

                // Realizar la eliminación en la base de datos
                string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
                string query = "DELETE FROM Razas WHERE ID_Raza = @RazaId";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@RazaId", razaId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Actualizar el GridView después de la eliminación
                    CargarRazas(); // Método para cargar nuevamente los datos en el GridView
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error
                    Response.Write("<script>alert('Error al eliminar el cliente: " + ex.Message + "');</script>");
                }
            }
            else if (e.CommandName == "ActualizarRaza")
            {
                // Obtener el ID de la raza seleccionada
                int razaId = Convert.ToInt32(e.CommandArgument);

                // Redirigir al usuario a la página para actualizar la raza con el ID de la raza en la URL
                Response.Redirect("~/Views/Mascotas/Razas/ActualizarRazaForm.aspx?RazaId=" + razaId);
            }
        }

        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            // Limpiar la variable de sesión para indicar que el usuario ha cerrado sesión
            Session["UsuarioAutenticado"] = false;

            // Redirigir al usuario a la página de inicio de sesión
            Response.Redirect("~/Views/LoginForm.aspx");
        }
    }
}