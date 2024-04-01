using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SistemaVeterinaria.Views
{
    public partial class CitasForm : System.Web.UI.Page
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
                CargarMascotas();
                CargarMedicos();
            }
        }

        private void CargarMascotas()
        {
            // Lógica para cargar las mascotas desde la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT ID_Mascota, Nombre_Mascota FROM Mascotas";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    ddlMascota.DataSource = table;
                    ddlMascota.DataTextField = "Nombre_Mascota";
                    ddlMascota.DataValueField = "ID_Mascota";
                    ddlMascota.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write($"<script>alert('Error al cargar las mascotas: {ex.Message}');</script>");
            }
        }

        private void CargarMedicos()
        {
            // Lógica para cargar los médicos desde la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT ID_Medico, Nombre_Medico FROM Medicos";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    ddlMedico.DataSource = table;
                    ddlMedico.DataTextField = "Nombre_Medico";
                    ddlMedico.DataValueField = "ID_Medico";
                    ddlMedico.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write($"<script>alert('Error al cargar los médicos: {ex.Message}');</script>");
            }
        }

        protected void btnCrearCita_Click(object sender, EventArgs e)
        {
            int idMascota = Convert.ToInt32(ddlMascota.SelectedValue);
            int idMedico = Convert.ToInt32(ddlMedico.SelectedValue);
            DateTime fechaCita = Convert.ToDateTime(txtFecha.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "INSERT INTO Citas (ID_Mascota, ID_Medico, Proxima_Fecha) VALUES (@IdMascota, @IdMedico, @FechaCita)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdMascota", idMascota);
                    command.Parameters.AddWithValue("@IdMedico", idMedico);
                    command.Parameters.AddWithValue("@FechaCita", fechaCita);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        // Éxito al insertar la cita
                        Response.Redirect("~/Views/Reportes/ReporteCitasForm.aspx");
                    }
                    else
                    {
                        // No se insertó ninguna fila, mostrar mensaje de error
                        Response.Write("<script>alert('Error al crear la cita');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write($"<script>alert('Error al crear la cita: {ex.Message}');</script>");
            }
        }
    }
}