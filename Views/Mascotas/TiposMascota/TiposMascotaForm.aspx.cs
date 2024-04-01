using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaVeterinaria.Views.Mascotas.TiposMascota
{
    public partial class TiposMascota : System.Web.UI.Page
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
                CargarTiposMascotas();
            }
        }

        private void CargarTiposMascotas()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT ID_TipoMascota, Nombre_TipoMascota FROM TiposMascotas";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    GridViewTiposMascotas.DataSource = table;
                    GridViewTiposMascotas.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar la lista de tipos de mascotas: {ex.Message}');</script>");
            }
        }

        protected void btnNuevoTipoMascota_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Mascotas/TiposMascota/AgregarTipoMascotaForm.aspx");
        }

        protected void GridViewTiposMascotas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarTipoMascota")
            {
                // Obtener el ID del cliente a eliminar
                int tipoId = Convert.ToInt32(e.CommandArgument);

                // Realizar la eliminación en la base de datos
                string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
                string query = "DELETE FROM TiposMascota WHERE ID_TipoMascota = @tipoId";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@tipoId", tipoId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Actualizar el GridView después de la eliminación
                    CargarTiposMascotas(); // Método para cargar nuevamente los datos en el GridView
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error
                    Response.Write("<script>alert('Error al eliminar el cliente: " + ex.Message + "');</script>");
                }
            }
            else if (e.CommandName == "ActualizarTipoMascota")
            {
                int tipoMascotaId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/Views/Mascotas/TiposMascota/ActualizarTipoMascotaForm.aspx?TipoMascotaId=" + tipoMascotaId);
            }
        }
    }
}