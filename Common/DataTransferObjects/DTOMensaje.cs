using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.DataTransferObjects
{
    public class DTOMensaje
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El asunto es requerido"), MaxLength(100)]
        public string Asunto { get; set; }
        [Required(ErrorMessage = "El contenido es requerido"), MaxLength(5000)]
        public string Contenido { get; set; }
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El remitente es requerido")]
        public int IdUsuarioE { get; set; }
        [Required(ErrorMessage = "El receptor es requerido")]
        public int IdUsuarioR { get; set; }
        public bool Visto { get; set; }
        public string NombreE { get; set; }
        public string NombreR { get; set; }
    }
}
