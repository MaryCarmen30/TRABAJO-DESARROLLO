using System.Data.SqlClient;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificacionesController : ControllerBase
    {
        public readonly string con;

        public CertificacionesController(IConfiguration configuration)
        {
            con = configuration.GetConnectionString("conexion");
        } 

        [HttpGet]
        public IEnumerable<Certificacion> Get()
        {
            List<Certificacion> certificaciones = new();
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ObtenerCertificaciones", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Certificacion c = new Certificacion
                            {
                                id_certificacion = Convert.ToInt32(reader["id_certificacion"]),
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                tipo_certificacion = reader["tipo_certificacion"].ToString(),
                                fecha_emision = Convert.ToDateTime(reader["fecha_emision"]),
                                fecha_expiracion = Convert.ToDateTime(reader["fecha_expiracion"]),
                                certificado_url = reader["certificado_url"].ToString(),
                                estado = reader["estado"].ToString()
                            };
                            certificaciones.Add(c);
                        }
                    }
                }
            }
            return certificaciones;
        }

        [HttpPost]
        public void Post([FromBody] Certificacion c)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("InsertarCertificacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_documento", c.id_documento);
                    cmd.Parameters.AddWithValue("@tipo_certificacion", c.tipo_certificacion);
                    cmd.Parameters.AddWithValue("@fecha_emision", c.fecha_emision);
                    cmd.Parameters.AddWithValue("@fecha_expiracion", c.fecha_expiracion);
                    cmd.Parameters.AddWithValue("@certificado_url", c.certificado_url);
                    cmd.Parameters.AddWithValue("@estado", c.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpPut("{id}")]
        public void Put([FromBody] Certificacion c, int id)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ActualizarCertificacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_certificacion", id);
                    cmd.Parameters.AddWithValue("@id_documento", c.id_documento);
                    cmd.Parameters.AddWithValue("@tipo_certificacion", c.tipo_certificacion);
                    cmd.Parameters.AddWithValue("@fecha_emision", c.fecha_emision);
                    cmd.Parameters.AddWithValue("@fecha_expiracion", c.fecha_expiracion);
                    cmd.Parameters.AddWithValue("@certificado_url", c.certificado_url);
                    cmd.Parameters.AddWithValue("@estado", c.estado);
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
                using (SqlCommand cmd = new SqlCommand("EliminarCertificacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_certificacion", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
