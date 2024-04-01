using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace SistemaVeterinaria.Views.Clientes
{
    public partial class ClientesForm : System.Web.UI.Page
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
                CargarClientes();
            }
        }

        protected void GridViewClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarCliente")
            {
                // Obtener el ID del cliente a eliminar
                int clienteId = Convert.ToInt32(e.CommandArgument);

                // Realizar la eliminación en la base de datos
                string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
                string query = "DELETE FROM Clientes WHERE ID_Cliente = @ClienteId";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ClienteId", clienteId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Actualizar el GridView después de la eliminación
                    CargarClientes(); // Método para cargar nuevamente los datos en el GridView
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error
                    Response.Write("<script>alert('Error al eliminar el cliente: " + ex.Message + "');</script>");
                }
            }
            else if (e.CommandName == "ActualizarCliente")
            {
                // Obtener el ID del cliente seleccionado
                int clienteId = Convert.ToInt32(e.CommandArgument);

                // Redirigir al usuario a la página de actualización con el ID del cliente en la URL
                Response.Redirect("~/Views/Clientes/ActualizarClienteForm.aspx?ClienteId=" + clienteId);
            }
        }

        private void CargarClientes()
        {
            // Obtener la cadena de conexión desde el archivo de configuración
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;

            // Consulta SQL para seleccionar todos los clientes
            string query = "SELECT ID_Cliente, Nombre_Cliente, Telefono, Direccion FROM Clientes";

            try
            {
                // Establecer la conexión con la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Crear un adaptador de datos para ejecutar la consulta y llenar un DataTable con los resultados
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Asignar el DataTable como el DataSource del GridView
                    GridViewClientes.DataSource = dataTable;

                    // Enlazar los datos al GridView
                    GridViewClientes.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de conexión o consulta
                Response.Write("<script>alert('Error al cargar los clientes: " + ex.Message + "');</script>");
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