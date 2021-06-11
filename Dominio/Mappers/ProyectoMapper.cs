using Common.DataTransferObjects;
using Persistencia.Database;
using System;
using System.Collections.Generic;
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
                FechaPub = proyecto.FechaPub,
                Comentarios = ComentarioMapper.MapToCollectionObject(proyecto.Comentario),
                Imagenes = ImagenMapper.MapToCollectionObject(proyecto.Imagen),
                Videos = VideoMapper.MapToCollectionObject(proyecto.Video),
                Textos = TextoMapper.MapToCollectionObject(proyecto.Texto),
                Tags = TagMapper.MapToCollectionObject(proyecto.Tag),
                Valoraciones = ValoracionMapper.MapToCollectionObject(proyecto.Valoracion),
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
                FechaPub = proyecto.FechaPub,
                Comentario = ComentarioMapper.MapToCollectionEntity(proyecto.Comentarios),
                Imagen = ImagenMapper.MapToCollectionEntity(proyecto.Imagenes),
                Video = VideoMapper.MapToCollectionEntity(proyecto.Videos),
                Texto = TextoMapper.MapToCollectionEntity(proyecto.Textos),
                Tag = TagMapper.MapToCollectionEntity(proyecto.Tags),
                Valoracion = ValoracionMapper.MapToCollectionEntity(proyecto.Valoraciones),
            };
        }

        public static HashSet<Proyecto> MapToCollectionEntity(ICollection<DTOProyecto> proyectos)
        {
            if (proyectos == null)
                return null;

            var proyecto = new HashSet<Proyecto>();
            foreach (var pro in proyectos)
            {
                var p = new Proyecto()
                {
                    Id = pro.Id,
                    Titulo = pro.Titulo,
                    Portada = pro.Portada,
                    IdAutor = pro.IdAutor,
                    Visitas = pro.Visitas,
                    Categoria = pro.Categoria,
                    Descripcion = pro.Descripcion,
                    FechaPub = pro.FechaPub,
                    Comentario = ComentarioMapper.MapToCollectionEntity(pro.Comentarios),
                    Imagen = ImagenMapper.MapToCollectionEntity(pro.Imagenes),
                    Video = VideoMapper.MapToCollectionEntity(pro.Videos),
                    Texto = TextoMapper.MapToCollectionEntity(pro.Textos),
                    Tag = TagMapper.MapToCollectionEntity(pro.Tags),
                    Valoracion = ValoracionMapper.MapToCollectionEntity(pro.Valoraciones),
                };
                proyecto.Add(p);
            }
            return proyecto;
        }

        public static HashSet<DTOProyecto> MapToCollectionObject(ICollection<Proyecto> proyectos)
        {
            if (proyectos == null)
                return null;

            var proyecto = new HashSet<DTOProyecto>();
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
                    FechaPub = pro.FechaPub,
                    Comentarios = ComentarioMapper.MapToCollectionObject(pro.Comentario),
                    Imagenes = ImagenMapper.MapToCollectionObject(pro.Imagen),
                    Videos = VideoMapper.MapToCollectionObject(pro.Video),
                    Textos = TextoMapper.MapToCollectionObject(pro.Texto),
                    Tags = TagMapper.MapToCollectionObject(pro.Tag),
                    Valoraciones = ValoracionMapper.MapToCollectionObject(pro.Valoracion),
                };
                proyecto.Add(p);
            }
            return proyecto;
        }
    }
}
