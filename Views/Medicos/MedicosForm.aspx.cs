using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SistemaVeterinaria.Views
{
    public partial class MedicosForm : System.Web.UI.Page
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
                // Cargar la lista de médicos
                CargarMedicos();
            }
        }

        private void CargarMedicos()
        {
            // Lógica para cargar la lista de médicos en el GridView
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT ID_Medico, Nombre_Medico, Especialidad FROM Medicos";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    GridViewMedicos.DataSource = table;
                    GridViewMedicos.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar la lista de médicos: " + ex.Message + "');</script>");
            }
        }

        protected void GridViewMedicos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarMedico")
            {
                // Obtener el ID del cliente a eliminar
                int medicoId = Convert.ToInt32(e.CommandArgument);

                // Realizar la eliminación en la base de datos
                string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
                string query = "DELETE FROM Medicos WHERE ID_Medico = @MedicoId";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MedicoId", medicoId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Actualizar el GridView después de la eliminación
                    CargarMedicos(); // Método para cargar nuevamente los datos en el GridView
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error
                    Response.Write("<script>alert('Error al eliminar el medico: " + ex.Message + "');</script>");
                }
            }
            else if (e.CommandName == "ActualizarMedico")
            {
                // Obtener el ID del cliente seleccionado
                int clienteId = Convert.ToInt32(e.CommandArgument);

                // Redirigir al usuario a la página de actualización con el ID del cliente en la URL
                Response.Redirect("~/Views/Medicos/ActualizarMedicoForm.aspx?MedicoId=" + clienteId);
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