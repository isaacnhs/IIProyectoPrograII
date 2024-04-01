using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SistemaVeterinaria.Views
{
    public partial class MascotasForm : System.Web.UI.Page
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
                    GridViewMascotas.DataSource = table;
                    GridViewMascotas.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar la lista de mascotas: " + ex.Message + "');</script>");
            }
        }

        protected void btnNuevaMascota_Click(object sender, EventArgs e)
        {
            // Redirigir al usuario a la página para agregar una nueva mascota
            Response.Redirect("~/Views/Mascotas/AgregarMascotaForm.aspx");
        }

        protected void GridViewMascotas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarMascota")
            {
                // Obtener el ID del cliente a eliminar
                int mascotaId = Convert.ToInt32(e.CommandArgument);

                // Realizar la eliminación en la base de datos
                string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
                string query = "DELETE FROM Mascotas WHERE ID_Mascota = @MascotaId";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MascotaId", mascotaId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Actualizar el GridView después de la eliminación
                    CargarMascotas(); // Método para cargar nuevamente los datos en el GridView
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error
                    Response.Write("<script>alert('Error al eliminar el cliente: " + ex.Message + "');</script>");
                }
            }
            else if (e.CommandName == "ActualizarMascota")
            {
                // Obtener el ID de la mascota seleccionada
                int mascotaId = Convert.ToInt32(e.CommandArgument);

                // Redirigir al usuario a la página para actualizar la mascota con el ID de la mascota en la URL
                Response.Redirect("~/Views/Mascotas/ActualizarMascotaForm.aspx?MascotaId=" + mascotaId);
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