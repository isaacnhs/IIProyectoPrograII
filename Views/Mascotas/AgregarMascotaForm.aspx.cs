using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SistemaVeterinaria.Views.Mascotas
{
    public partial class AgregarMascotaForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está autenticado
            if (Session["UsuarioAutenticado"] == null || !(bool)Session["UsuarioAutenticado"])
            {
                // Si no está autenticado, redirigir al usuario a la página de inicio de sesión
                Response.Redirect("~/Views/LoginForm.aspx");
            }

            CargarRazas();
            CargarTiposMascotas();
            CargarClientes();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener los datos de la nueva mascota desde los controles de entrada
            string nombre = txtNombre.Text;
            int idRaza = Convert.ToInt32(ddlRaza.SelectedValue);
            int idTipoMascota = Convert.ToInt32(ddlTipoMascota.SelectedValue);
            int idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
            string comidaFavorita = txtComidaFavorita.Text;

            // Insertar los datos de la nueva mascota en la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "INSERT INTO Mascotas (Nombre_Mascota, ID_Raza, ID_TipoMascota, ID_Cliente, Comida_Favorita) VALUES (@Nombre, @IdRaza, @IdTipoMascota, @IdCliente, @ComidaFavorita)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@IdRaza", idRaza);
                    command.Parameters.AddWithValue("@IdTipoMascota", idTipoMascota);
                    command.Parameters.AddWithValue("@IdCliente", idCliente);
                    command.Parameters.AddWithValue("@ComidaFavorita", comidaFavorita);
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Redirigir al usuario de regreso a la página de lista de mascotas
                    Response.Redirect("~/Views/Mascotas/MascotasForm.aspx");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al guardar la mascota: " + ex.Message + "');</script>");
            }
        }

        private void CargarRazas()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT ID_Raza, Nombre_Raza FROM Razas";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    ddlRaza.DataSource = reader;
                    ddlRaza.DataTextField = "Nombre_Raza";
                    ddlRaza.DataValueField = "ID_Raza";
                    ddlRaza.DataBind();

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos al cargar las razas
                Response.Write("<script>alert('Error al cargar las razas: " + ex.Message + "');</script>");
            }
        }

        private void CargarTiposMascotas()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT ID_TipoMascota, Nombre_TipoMascota FROM TiposMascotas";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    ddlTipoMascota.DataSource = reader;
                    ddlTipoMascota.DataTextField = "Nombre_TipoMascota";
                    ddlTipoMascota.DataValueField = "ID_TipoMascota";
                    ddlTipoMascota.DataBind();

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos al cargar los tipos de mascotas
                Response.Write("<script>alert('Error al cargar los tipos de mascotas: " + ex.Message + "');</script>");
            }
        }

        private void CargarClientes()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT ID_Cliente, Nombre_Cliente FROM Clientes";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    ddlCliente.DataSource = reader;
                    ddlCliente.DataTextField = "Nombre_Cliente";
                    ddlCliente.DataValueField = "ID_Cliente";
                    ddlCliente.DataBind();

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos al cargar los clientes
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