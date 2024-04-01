using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaVeterinaria.Views.Mascotas.TiposMascota
{
    public partial class ActualizarTipoMascota : System.Web.UI.Page
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
                // Obtener el ID del tipo de mascota a actualizar desde la URL
                if (Request.QueryString["TipoMascotaId"] != null)
                {
                    int tipoMascotaId = Convert.ToInt32(Request.QueryString["TipoMascotaId"]);
                    CargarDatosTipoMascota(tipoMascotaId);
                }
                else
                {
                    // Si no se proporciona un ID en la URL, redirigir a la página de lista de tipos de mascotas
                    Response.Redirect("~/Views/Mascotas/TiposMascota/TiposMascotaForm.aspx");
                }
            }
        }

        private void CargarDatosTipoMascota(int tipoMascotaId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT Nombre_TipoMascota FROM TiposMascotas WHERE ID_TipoMascota = @TipoMascotaId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TipoMascotaId", tipoMascotaId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombreTipoMascota.Text = reader["Nombre_TipoMascota"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar los datos del tipo de mascota: {ex.Message}');</script>");
            }
        }

        protected void btnActualizarTipoMascota_Click(object sender, EventArgs e)
        {
            int tipoMascotaId = Convert.ToInt32(Request.QueryString["TipoMascotaId"]);
            string nuevoNombreTipoMascota = txtNombreTipoMascota.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "UPDATE TiposMascotas SET Nombre_TipoMascota = @NuevoNombre WHERE ID_TipoMascota = @TipoMascotaId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NuevoNombre", nuevoNombreTipoMascota);
                    command.Parameters.AddWithValue("@TipoMascotaId", tipoMascotaId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        // Éxito al actualizar el tipo de mascota
                        Response.Redirect("~/Views/Mascotas/TiposMascota/TiposMascotaForm.aspx");
                    }
                    else
                    {
                        // Error al actualizar el tipo de mascota
                        Response.Write("<script>alert('Error al actualizar el tipo de mascota');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al actualizar el tipo de mascota: {ex.Message}');</script>");
            }


        }
    }
}