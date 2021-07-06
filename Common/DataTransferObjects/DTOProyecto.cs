using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOProyecto
    {

        public DTOProyecto()
        {
            this.Comentarios = new HashSet<DTOComentario>();
            this.Tags = new HashSet<DTOTag>();
            this.Valoraciones = new HashSet<DTOValoracion>();
            this.Portafolios = new HashSet<DTOPortafolio>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Portada { get; set; }
        public int IdAutor { get; set; }
        public int Visitas { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaPub { get; set; }


        public virtual ICollection<DTOComentario> Comentarios { get; set; }

        public virtual ICollection<DTOTag> Tags { get; set; }

        public virtual ICollection<DTOValoracion> Valoraciones { get; set; }

        public virtual ICollection<DTOPortafolio> Portafolios { get; set; }

    }
}
