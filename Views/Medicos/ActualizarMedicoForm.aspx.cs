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
    public partial class ActualizarMedicoForm : System.Web.UI.Page
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
                // Verificar si se proporciona el ID del médico en la URL
                if (Request.QueryString["MedicoId"] != null)
                {
                    int medicoId = Convert.ToInt32(Request.QueryString["MedicoId"]);

                    // Cargar los datos del médico seleccionado en los controles de entrada
                    CargarDatosMedico(medicoId);
                }
                else
                {
                    // Si no se proporciona el ID del médico, mostrar un mensaje de error o redirigir a otra página
                    Response.Redirect("~/Error.aspx");
                }
            }
        }

        private void CargarDatosMedico(int medicoId)
        {
            // Obtener los datos del médico de la base de datos usando su ID
            // Lógica para obtener los datos del médico desde la base de datos y cargarlos en los controles de entrada
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT Nombre_Medico, Especialidad FROM Medicos WHERE ID_Medico = @MedicoId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MedicoId", medicoId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Asignar los valores del médico a los controles de entrada
                        txtNombre.Text = reader["Nombre_Medico"].ToString();
                        txtEspecialidad.Text = reader["Especialidad"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar los datos del médico: " + ex.Message + "');</script>");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del médico de la URL
            int medicoId = Convert.ToInt32(Request.QueryString["MedicoId"]);

            // Obtener los nuevos datos del médico desde los controles de entrada
            string nombre = txtNombre.Text;
            string especialidad = txtEspecialidad.Text;

            // Actualizar los datos del médico en la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "UPDATE Medicos SET Nombre_Medico = @Nombre, Especialidad = @Especialidad WHERE ID_Medico = @MedicoId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Especialidad", especialidad);
                    command.Parameters.AddWithValue("@MedicoId", medicoId);
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Redirigir al usuario de regreso a la página de listado de médicos
                    Response.Redirect("~/Views/Medicos/MedicosForm.aspx");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al actualizar el médico: " + ex.Message + "');</script>");
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