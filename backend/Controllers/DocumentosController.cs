using System.Data.SqlClient;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentosController : ControllerBase
    {
        public readonly string con;

        public DocumentosController(IConfiguration configuration)
        {
            con = configuration.GetConnectionString("conexion");
        } 

        [HttpGet]
        public IEnumerable<Documento> Get()
        {
            List<Documento> documentos = new();
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ObtenerDocumentos", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Documento d = new Documento
                            {
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                id_miembro = Convert.ToInt32(reader["id_miembro"]),
                                tipo_documento = reader["tipo_documento"].ToString(),
                                documento_url = reader["documento_url"].ToString(),
                                fecha_carga = Convert.ToDateTime(reader["fecha_carga"]),
                                estado = reader["estado"].ToString()
                            };
                            documentos.Add(d);
                        }
                    }
                }
            }
            return documentos;
        }

        [HttpPost]
        public void Post([FromBody] Documento d)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("InsertarDocumento", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_miembro", d.id_miembro);
                    cmd.Parameters.AddWithValue("@tipo_documento", d.tipo_documento);
                    cmd.Parameters.AddWithValue("@documento_url", d.documento_url);
                    cmd.Parameters.AddWithValue("@fecha_carga", d.fecha_carga);
                    cmd.Parameters.AddWithValue("@estado", d.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpPut("{id}")]
        public void Put([FromBody] Documento d, int id)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ActualizarDocumento", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_documento", id);
                    cmd.Parameters.AddWithValue("@id_miembro", d.id_miembro);
                    cmd.Parameters.AddWithValue("@tipo_documento", d.tipo_documento);
                    cmd.Parameters.AddWithValue("@documento_url", d.documento_url);
                    cmd.Parameters.AddWithValue("@fecha_carga", d.fecha_carga);
                    cmd.Parameters.AddWithValue("@estado", d.estado);
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
                using (SqlCommand cmd = new SqlCommand("EliminarDocumento", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_documento", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
