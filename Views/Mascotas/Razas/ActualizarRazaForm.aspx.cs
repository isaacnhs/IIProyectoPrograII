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
    public partial class ActualizarRazaForm : System.Web.UI.Page
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
                // Verificar si se ha proporcionado el ID de la raza en la URL
                if (Request.QueryString["RazaId"] != null)
                {
                    int razaId = Convert.ToInt32(Request.QueryString["RazaId"]);

                    // Obtener los detalles de la raza desde la base de datos
                    CargarDetallesRaza(razaId);
                }
                else
                {
                    // Si no se proporciona un ID de raza, redirigir a la página de lista de razas
                    Response.Redirect("~/Views/Razas/RazasForm.aspx");
                }
            }
        }

        private void CargarDetallesRaza(int razaId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT Nombre_Raza FROM Razas WHERE ID_Raza = @RazaId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RazaId", razaId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Mostrar el nombre de la raza en el campo de texto para actualizar
                        txtNombre.Text = reader["Nombre_Raza"].ToString();
                    }
                    else
                    {
                        // Si no se encuentra la raza con el ID proporcionado, redirigir a la página de lista de razas
                        Response.Redirect("~/Views/Razas/RazasForm.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar los detalles de la raza: " + ex.Message + "');</script>");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int razaId = Convert.ToInt32(Request.QueryString["RazaId"]);
            string nuevoNombreRaza = txtNombre.Text.Trim();

            if (!string.IsNullOrEmpty(nuevoNombreRaza))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
                string query = "UPDATE Razas SET Nombre_Raza = @NuevoNombreRaza WHERE ID_Raza = @RazaId";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@NuevoNombreRaza", nuevoNombreRaza);
                        command.Parameters.AddWithValue("@RazaId", razaId);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Opcional: mostrar un mensaje de éxito o redirigir a otra página
                    Response.Write("<script>alert('La raza se ha actualizado correctamente.');</script>");
                    Response.Redirect("~/Views/Mascotas/Razas/RazasForm.aspx");

                }
                catch (Exception ex)
                {
                    // Manejar cualquier error de base de datos
                    Response.Write("<script>alert('Error al actualizar la raza: " + ex.Message + "');</script>");
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