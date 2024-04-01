using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SistemaVeterinaria.Views.Clientes
{
    public partial class AgregarClienteForm : System.Web.UI.Page
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
            // Obtener los valores ingresados por el usuario
            string nombre = txtNombre.Text;
            string telefono = txtTelefono.Text;
            string direccion = txtDireccion.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(direccion))
            {
                // Mostrar un mensaje de error si algún campo está vacío
                Response.Write("<script>alert('Todos los campos son requeridos');</script>");
                return; // Salir del método sin realizar la inserción
            }

            // Cadena de conexión a la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;

            // Consulta SQL para insertar un nuevo cliente
            string query = "INSERT INTO Clientes (Nombre_Cliente, Telefono, Direccion) VALUES (@Nombre, @Telefono, @Direccion)";

            try
            {
                // Establecer conexión con la base de datos y ejecutar la consulta de inserción
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Cliente guardado correctamente');</script>");
                Response.Redirect("~/Views/Clientes/ClientesForm.aspx");

            }
            catch (Exception ex)
            {
                // Manejar cualquier error de conexión o consulta
                Response.Write("<script>alert('Error al guardar el cliente: " + ex.Message + "');</script>");
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