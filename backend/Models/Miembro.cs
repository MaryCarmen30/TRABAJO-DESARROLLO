using System;

namespace backend.Models
{
    public class Miembro
    {
        public int id_miembro { get; set; }
        public string dni { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string universidad { get; set; }
        public string titulo { get; set; }
        public DateTime fecha_graduacion { get; set; }
        public string foto_url { get; set; }
        public string estado { get; set; }
        public DateTime fecha_registro { get; set; }
    }
}
