using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public DTOUsuario()
        {
            this.Comentarios = new HashSet<DTOComentario>();
            this.MensajesE = new HashSet<DTOMensaje>();
            this.MensajesR = new HashSet<DTOMensaje>();
            this.Proyectos = new HashSet<DTOProyecto>();
            this.Seguidores = new HashSet<DTOSeguimiento>();
            this.Siguiendo = new HashSet<DTOSeguimiento>();
            this.PValorados = new HashSet<DTOValoracion>();
        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public System.DateTime FNac { get; set; }
        public string Pais { get; set; }
        public string Profesion { get; set; }
        public string Empresa { get; set; }
        public string ImgPerfil { get; set; }
        public string UrlWeb { get; set; }
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOComentario> Comentarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOMensaje> MensajesE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOMensaje> MensajesR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOProyecto> Proyectos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOSeguimiento> Seguidores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOSeguimiento> Siguiendo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTOValoracion> PValorados { get; set; }
    }
}
