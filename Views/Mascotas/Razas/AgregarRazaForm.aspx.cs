using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaVeterinaria.Views.Mascotas
{
    public partial class AgregarRazaForm : System.Web.UI.Page
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreRaza = txtNombre.Text.Trim();

            if (!string.IsNullOrEmpty(nombreRaza))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
                string query = "INSERT INTO Razas (Nombre_Raza) VALUES (@NombreRaza)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@NombreRaza", nombreRaza);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Limpiar el campo de texto después de guardar la raza
                    txtNombre.Text = "";

                    // Opcional: mostrar un mensaje de éxito o redirigir a otra página
                    Response.Write("<script>alert('La raza se ha guardado correctamente.');</script>");
                    Response.Redirect("~/Views/Mascotas/Razas/RazasForm.aspx");

                }
                catch (Exception ex)
                {
                    // Manejar cualquier error de base de datos
                    Response.Write("<script>alert('Error al guardar la raza: " + ex.Message + "');</script>");
                }
            }
            else
            {
                // Mostrar un mensaje si el campo de nombre de raza está vacío
                Response.Write("<script>alert('Por favor ingrese un nombre para la raza.');</script>");
            }
        }
    }
}