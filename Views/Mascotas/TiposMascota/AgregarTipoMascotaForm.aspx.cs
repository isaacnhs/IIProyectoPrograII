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
    public partial class AgregarTipoMascota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está autenticado
            if (Session["UsuarioAutenticado"] == null || !(bool)Session["UsuarioAutenticado"])
            {
                // Si no está autenticado, redirigir al usuario a la página de inicio de sesión
                Response.Redirect("~/Views/LoginForm.aspx");
            }
        }

        protected void btnGuardarTipoMascota_Click(object sender, EventArgs e)
        {
            string nombreTipoMascota = txtNombreTipoMascota.Text;

            if (!string.IsNullOrEmpty(nombreTipoMascota))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
                string query = "INSERT INTO TiposMascotas (Nombre_TipoMascota) VALUES (@NombreTipoMascota)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@NombreTipoMascota", nombreTipoMascota);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            // Éxito al guardar el nuevo tipo de mascota
                            Response.Redirect("~/Views/Mascotas/TiposMascota/TiposMascotaForm.aspx");
                        }
                        else
                        {
                            // Error al guardar el nuevo tipo de mascota
                            Response.Write("<script>alert('Error al guardar el nuevo tipo de mascota');</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error al guardar el nuevo tipo de mascota: {ex.Message}');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Por favor ingrese el nombre del tipo de mascota');</script>");
            }
        }
    }
}