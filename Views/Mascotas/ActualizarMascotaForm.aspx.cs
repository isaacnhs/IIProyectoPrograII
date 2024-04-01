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
    public partial class ActualizarMascotaForm : System.Web.UI.Page
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
                // Obtener el ID de la mascota a actualizar desde la URL
                if (Request.QueryString["MascotaId"] != null)
                {
                    CargarRazas();
                    CargarTiposMascotas();
                    CargarClientes();

                    int mascotaId = Convert.ToInt32(Request.QueryString["MascotaId"]);
                    CargarDatosMascota(mascotaId);
                }
                else
                {
                    // Si no se proporciona un ID en la URL, redirigir a la página de lista de mascotas
                    Response.Redirect("~/Views/Mascota/MascotasForm.aspx");
                }
            }
        }

        private void CargarDatosMascota(int mascotaId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "SELECT Nombre_Mascota, ID_Raza, ID_TipoMascota, ID_Cliente, Comida_Favorita FROM Mascotas WHERE ID_Mascota = @MascotaId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MascotaId", mascotaId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombreMascota.Text = reader["Nombre_Mascota"].ToString();
                        ddlRaza.SelectedValue = reader["ID_Raza"].ToString();
                        ddlTipoMascota.SelectedValue = reader["ID_TipoMascota"].ToString();
                        ddlCliente.SelectedValue = reader["ID_Cliente"].ToString();
                        txtComidaFavorita.Text = reader["Comida_Favorita"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar los datos de la mascota: {ex.Message}');</script>");
            }
        }


        protected void btnActualizarMascota_Click(object sender, EventArgs e)
        {
            int mascotaId = Convert.ToInt32(Request.QueryString["MascotaId"]);
            string nuevoNombreMascota = txtNombreMascota.Text;
            int nuevaIdRaza = Convert.ToInt32(ddlRaza.SelectedValue);
            int nuevaIdTipoMascota = Convert.ToInt32(ddlTipoMascota.SelectedValue);
            int nuevoIdCliente = Convert.ToInt32(ddlCliente.SelectedValue);
            string nuevaComidaFavorita = txtComidaFavorita.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = "UPDATE Mascotas SET Nombre_Mascota = @NuevoNombre, ID_Raza = @NuevaRaza, ID_TipoMascota = @NuevoTipoMascota, ID_Cliente = @NuevoCliente, Comida_Favorita = @NuevaComidaFavorita WHERE ID_Mascota = @MascotaId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NuevoNombre", nuevoNombreMascota);
                    command.Parameters.AddWithValue("@NuevaRaza", nuevaIdRaza);
                    command.Parameters.AddWithValue("@NuevoTipoMascota", nuevaIdTipoMascota);
                    command.Parameters.AddWithValue("@NuevoCliente", nuevoIdCliente);
                    command.Parameters.AddWithValue("@NuevaComidaFavorita", nuevaComidaFavorita);
                    command.Parameters.AddWithValue("@MascotaId", mascotaId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        Response.Redirect("~/Views/Mascota/MascotasForm.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al actualizar la mascota');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al actualizar la mascota: {ex.Message}');</script>");
            }
        }

        private void CargarRazas()
        {
            // Lógica para cargar las opciones de la lista desplegable de razas desde la base de datos
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
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar las razas: " + ex.Message + "');</script>");
            }
        }

        private void CargarTiposMascotas()
        {
            // Lógica para cargar las opciones de la lista desplegable de tipos de mascotas desde la base de datos
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
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar los tipos de mascotas: " + ex.Message + "');</script>");
            }
        }

        private void CargarClientes()
        {
            // Lógica para cargar las opciones de la lista desplegable de clientes desde la base de datos
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
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar los clientes: " + ex.Message + "');</script>");
            }
        }

    }
}