using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOMensaje
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdUsuarioE { get; set; }
        public int IdUsuarioR { get; set; }
        public int Leído { get; set; }

    }
}
