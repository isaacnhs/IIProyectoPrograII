using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaVeterinaria.Views
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConnect_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string clave = txtClave.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
            {
                lblMensaje.Text = "Por favor ingrese un usuario y una contraseña.";
                return;
            }

            // Autenticar al usuario
            bool autenticado = AutenticarUsuario(usuario, clave);

            // Validar las credenciales del usuario
            if (autenticado)
            {
                // Establecer una variable de sesión para indicar que el usuario está autenticado
                Session["UsuarioAutenticado"] = true;

                // Redirigir al usuario a la página principal o a donde sea necesario
                Response.Redirect("~/Views/MenuPrincipalForm.aspx");
            }
            else
            {
                // Mostrar un mensaje de error si las credenciales son incorrectas
                lblMensaje.Text = "Credenciales incorrectas. Por favor, inténtelo de nuevo.";
            }
        }

        // Método para autenticar al usuario
        private bool AutenticarUsuario(string usuario, string clave)
        {
            // Cadena de conexión a la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;

            // Consulta SQL para verificar las credenciales del usuario
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Nombre_Usuario = @Usuario AND Clave_Usuario = @Clave";

            // Variable para almacenar el resultado de la consulta
            int count;

            try
            {
                // Establecer conexión con la base de datos y ejecutar la consulta
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Clave", clave);
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }

                // Si count es mayor que 0, significa que las credenciales son válidas
                return count > 0;
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de conexión o consulta
                lblMensaje.Text = "Error al autenticar al usuario: " + ex.Message;
                return false;
            }
        }
    }
}