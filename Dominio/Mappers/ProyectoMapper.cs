using Common.DataTransferObjects;
using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
// una clase que mappea los DTO a Entidades y viceversa; también mappea colecciones de Entidades a colecciones de DTO y viceversa
/// </summary>

namespace Dominio.Mappers
{
    public class ProyectoMapper
    {
        private ComentarioMapper _comentario = new ComentarioMapper();
        private PortafolioMapper _portafolio = new PortafolioMapper();
        private TagMapper _tag = new TagMapper(); 
        public DTOProyecto MapToObject(Proyecto proyecto)
        {
            if (proyecto == null)
                return null;

            return new DTOProyecto()
            {
                Id = proyecto.Id,
                Titulo = proyecto.Titulo,
                Portada = proyecto.Portada,
                IdAutor = proyecto.IdAutor,
                Visitas = proyecto.Visitas,
                Categoria = proyecto.Categoria,
                Descripcion = proyecto.Descripcion,
                FechaPub = proyecto.FechaPub.ToShortDateString(),
                Comentarios = _comentario.MapToCollectionObject(proyecto.Comentario),
                Portafolios = _portafolio.MapToCollectionObject(proyecto.Portafolio),
                Tags = _tag.MapToCollectionObject(proyecto.Tag),
            };
        }

        public Proyecto MapToEntity(DTOProyecto proyecto)
        {
            if (proyecto == null)
                return null;

            return new Proyecto()
            {
                Id = proyecto.Id,
                Titulo = proyecto.Titulo,
                Portada = proyecto.Portada,
                IdAutor = proyecto.IdAutor,
                Visitas = proyecto.Visitas,
                Categoria = proyecto.Categoria,
                Descripcion = proyecto.Descripcion,
                FechaPub = ParseToDateType(proyecto.FechaPub),
                Comentario = _comentario.MapToCollectionEntity(proyecto.Comentarios),
                Portafolio = _portafolio.MapToCollectionEntity(proyecto.Portafolios),
                Tag = _tag.MapToCollectionEntity(proyecto.Tags),
                Valoracion = null,
            };
        }

        public List<DTOProyecto> MapToCollectionObject(ICollection<Proyecto> proyectos)
        {
            if (proyectos == null)
                return null;

            var proyecto = new List<DTOProyecto>();
            foreach (var pro in proyectos)
            {
                var p = new DTOProyecto()
                {
                    Id = pro.Id,
                    Titulo = pro.Titulo,
                    Portada = pro.Portada,
                    IdAutor = pro.IdAutor,
                    Visitas = pro.Visitas,
                    Categoria = pro.Categoria,
                    Descripcion = pro.Descripcion,
                    FechaPub = pro.FechaPub.ToShortDateString(),
                    Comentarios = _comentario.MapToCollectionObject(pro.Comentario),
                    Portafolios = _portafolio.MapToCollectionObject(pro.Portafolio),
                    Tags = _tag.MapToCollectionObject(pro.Tag),
                };
                proyecto.Add(p);
            }
            return proyecto;
        }

        public static System.DateTime ParseToDateType(string date)
        {
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime d))
                return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (DateTime.TryParseExact(date, "dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime di))
                return DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            if (DateTime.TryParseExact(date, "yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime die))
                return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return DateTime.Now;
        }
    }
}
