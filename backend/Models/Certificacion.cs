using System;

namespace backend.Models
{
    public class Certificacion
    {
        public int id_certificacion { get; set; }
        public int id_documento { get; set; }
        public string tipo_certificacion { get; set; }
        public DateTime fecha_emision { get; set; }
        public DateTime fecha_expiracion { get; set; }
        public string certificado_url { get; set; }
        public string estado { get; set; }
    }
}
