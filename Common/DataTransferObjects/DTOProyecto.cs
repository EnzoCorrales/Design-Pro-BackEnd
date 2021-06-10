using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOProyecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DTOProyecto()
        {
            this.Comentarios = new HashSet<DTOComentario>();
            this.Imagenes = new HashSet<DTOImagen>();
            this.Tags = new HashSet<DTOTag>();
            this.Textos = new HashSet<DTOTexto>();
            this.Videos = new HashSet<DTOVideo>();
            this.Valoraciones = new HashSet<DTOValoracion>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Portada { get; set; }
        public int IdAutor { get; set; }
        public int Visitas { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaPub { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOComentario> Comentarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOImagen> Imagenes { get; set; }
        public virtual ICollection<DTOTag> Tags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOTexto> Textos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOValoracion> Valoraciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOVideo> Videos { get; set; }
    }
}
