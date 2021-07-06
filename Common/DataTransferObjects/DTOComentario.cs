using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOComentario
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public int IdProyecto { get; set; }

        public string Nombre { get; set; }

    }
}
