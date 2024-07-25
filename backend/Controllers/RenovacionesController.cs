using System.Data.SqlClient;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenovacionesController : ControllerBase
    {
        public readonly string con;

        public RenovacionesController(IConfiguration configuration)
        {
            con = configuration.GetConnectionString("conexion");
        } 

        [HttpGet]
        public IEnumerable<Renovacion> Get()
        {
            List<Renovacion> renovaciones = new();
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ObtenerRenovaciones", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Renovacion r = new Renovacion
                            {
                                id_renovacion = Convert.ToInt32(reader["id_renovacion"]),
                                id_miembro = Convert.ToInt32(reader["id_miembro"]),
                                id_pago = Convert.ToInt32(reader["id_pago"]),
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                fecha_solicitud = Convert.ToDateTime(reader["fecha_solicitud"]),
                                fecha_aprobacion = Convert.ToDateTime(reader["fecha_aprobacion"]),
                                estado = reader["estado"].ToString()
                            };
                            renovaciones.Add(r);
                        }
                    }
                }
            }
            return renovaciones;
        }

        [HttpPost]
        public void Post([FromBody] Renovacion r)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("InsertarRenovacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_miembro", r.id_miembro);
                    cmd.Parameters.AddWithValue("@id_pago", r.id_pago);
                    cmd.Parameters.AddWithValue("@id_documento", r.id_documento);
                    cmd.Parameters.AddWithValue("@fecha_solicitud", r.fecha_solicitud);
                    cmd.Parameters.AddWithValue("@fecha_aprobacion", r.fecha_aprobacion);
                    cmd.Parameters.AddWithValue("@estado", r.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpPut("{id}")]
        public void Put([FromBody] Renovacion r, int id)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ActualizarRenovacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_renovacion", id);
                    cmd.Parameters.AddWithValue("@id_miembro", r.id_miembro);
                    cmd.Parameters.AddWithValue("@id_pago", r.id_pago);
                    cmd.Parameters.AddWithValue("@id_documento", r.id_documento);
                    cmd.Parameters.AddWithValue("@fecha_solicitud", r.fecha_solicitud);
                    cmd.Parameters.AddWithValue("@fecha_aprobacion", r.fecha_aprobacion);
                    cmd.Parameters.AddWithValue("@estado", r.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("EliminarRenovacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_renovacion", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
