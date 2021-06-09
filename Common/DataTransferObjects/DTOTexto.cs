using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOTexto
    {

        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public string Texto1 { get; set; }
        public int Orden { get; set; }
    }
}
