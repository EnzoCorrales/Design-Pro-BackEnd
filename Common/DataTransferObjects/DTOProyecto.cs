using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOProyecto
    {

        public DTOProyecto()
        {

        }

        public int Id { get; set; }
        [Required(ErrorMessage = "El título es requerido"), MaxLength(100)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "La portada es requerida")]
        public string Portada { get; set; }
        public int IdAutor { get; set; }
        [Required(ErrorMessage = "Las visitas son requeridas")]
        public int Visitas { get; set; }
        [Required(ErrorMessage = "Una categoría es requerida"), MaxLength(50)]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "La descripción es requerida"), MaxLength(200)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage ="La fecha de publicación es requerida, formato dd-mm-yyyy")]
        public string FechaPub { get; set; }
        public string NombreAutor { get; set; }
        public string UbicacionAutor { get; set; }


        public virtual ICollection<DTOComentario> Comentarios { get; set; }

        public virtual ICollection<DTOTag> Tags { get; set; }

        public virtual ICollection<DTOValoracion> Valoraciones { get; set; }

        public virtual ICollection<DTOPortafolio> Portafolios { get; set; }

    }
}
