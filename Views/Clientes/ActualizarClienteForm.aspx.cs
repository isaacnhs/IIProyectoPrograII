using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SistemaVeterinaria.Views.Clientes
{
    public partial class ActualizarClienteForm : System.Web.UI.Page
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
                // Verificar si se proporciona el ID del cliente en la URL
                if (Request.QueryString["ClienteId"] != null)
                {
                    int clienteId = Convert.ToInt32(Request.QueryString["ClienteId"]);

                    // Cargar los datos del cliente seleccionado en los controles de entrada
                    CargarDatosCliente(clienteId);
                }
                else
                {
                    // Si no se proporciona el ID del cliente, mostrar un mensaje de error o redirigir a otra página
                    Response.Redirect("~/Error.aspx");
                }
            }
        }

        private void CargarDatosCliente(int clienteId)
        {
            // Obtener los datos del cliente de la base de datos usando su ID
            // Lógica para obtener los datos del cliente desde la base de datos y cargarlos en los controles de entrada
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT Nombre_Cliente, Telefono, Direccion FROM Clientes WHERE ID_Cliente = @ClienteId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ClienteId", clienteId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Asignar los valores del cliente a los controles de entrada
                        txtNombre.Text = reader["Nombre_Cliente"].ToString();
                        txtTelefono.Text = reader["Telefono"].ToString();
                        txtDireccion.Text = reader["Direccion"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar los datos del cliente: " + ex.Message + "');</script>");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del cliente de la URL
            int clienteId = Convert.ToInt32(Request.QueryString["ClienteId"]);

            // Obtener los nuevos datos del cliente desde los controles de entrada
            string nombre = txtNombre.Text;
            string telefono = txtTelefono.Text;
            string direccion = txtDireccion.Text;

            // Actualizar los datos del cliente en la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "UPDATE Clientes SET Nombre_Cliente = @Nombre, Telefono = @Telefono, Direccion = @Direccion WHERE ID_Cliente = @ClienteId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@ClienteId", clienteId);
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Redirigir al usuario de regreso a la página de listado de clientes
                    Response.Redirect("~/Views/Clientes/ClientesForm.aspx");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al actualizar el cliente: " + ex.Message + "');</script>");
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