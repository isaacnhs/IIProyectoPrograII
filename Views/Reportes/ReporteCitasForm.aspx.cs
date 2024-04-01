using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaVeterinaria.Views.Reportes
{
    public partial class ReporteCitasForm : System.Web.UI.Page
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
                // Cargar el reporte de control de citas
                CargarReporteControlCitas();
            }
        }

        private void CargarReporteControlCitas()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinariaConnectionString"].ConnectionString;
            string query = @"SELECT Mascotas.Nombre_Mascota AS NombreMascota, Citas.Proxima_fecha AS ProximaFecha, Medicos.Nombre_Medico AS MedicoAsignado
                             FROM Citas
                             INNER JOIN Mascotas ON Citas.ID_Mascota = Mascotas.ID_Mascota
                             INNER JOIN Medicos ON Citas.ID_Medico = Medicos.ID_Medico
            		         WHERE CAST(Citas.Proxima_fecha AS DATE) >= CAST(GETDATE() AS DATE)                 
                             ORDER BY Citas.Proxima_fecha ASC";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    GridViewCitas.DataSource = table;
                    GridViewCitas.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de base de datos
                Response.Write("<script>alert('Error al cargar el reporte de control de citas: " + ex.Message + "');</script>");
            }
        }
    }
}