using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOImagen
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
    }
}
