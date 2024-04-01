using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaVeterinaria.Views.Medicos
{
    public partial class AgregarMedicoForm : System.Web.UI.Page
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
            // Obtener los datos del nuevo médico desde los controles de entrada
            string nombre = txtNombre.Text;
            string especialidad = txtEspecialidad.Text;

            // Insertar los datos del nuevo médico en la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "INSERT INTO Medicos (Nombre_Medico, Especialidad) VALUES (@Nombre, @Especialidad)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Especialidad", especialidad);
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Redirigir al usuario de regreso a la página de listado de médicos
                    Response.Redirect("~/Views/Medicos/MedicosForm.aspx");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al guardar el médico: " + ex.Message + "');</script>");
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