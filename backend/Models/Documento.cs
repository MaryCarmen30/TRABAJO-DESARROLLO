using System;

namespace backend.Models
{
    public class Documento
    {
        public int id_documento { get; set; }
        public int id_miembro { get; set; }
        public string tipo_documento { get; set; }
        public string documento_url { get; set; }
        public DateTime fecha_carga { get; set; }
        public string estado { get; set; }
    }
}
