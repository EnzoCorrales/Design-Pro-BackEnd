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
    public class ComentarioMapper
    {
        public DTOComentario MapToObject(Comentario comentario)
        {
            if (comentario == null)
                return null;

            return new DTOComentario()
            {
                Id = comentario.Id,
                Contenido = comentario.Contenido,
                Fecha = comentario.Fecha,
                IdUsuario = comentario.IdUsuario,
                IdProyecto = comentario.IdProyecto,
            };
        }

        public Comentario MapToEntity(DTOComentario comentario)
        {
            if (comentario == null)
                return null;

            return new Comentario()
            {
                Id = comentario.Id,
                Contenido = comentario.Contenido,
                Fecha = comentario.Fecha,
                IdUsuario = comentario.IdUsuario,
                IdProyecto = comentario.IdProyecto,
            };
        }

        public List<DTOComentario> MapToCollectionObject(ICollection<Comentario> comentarios)
        {
            if (comentarios == null)
                return null;

            var comentario = new List<DTOComentario>();
            foreach (var com in comentarios)
            {
                var c = new DTOComentario()
                {
                    Id = com.Id,
                    Contenido = com.Contenido,
                    Fecha = com.Fecha,
                    IdUsuario = com.IdUsuario,
                    IdProyecto = com.IdProyecto,
                };
                comentario.Add(c);
            }
            return comentario;
        }

        public List<Comentario> MapToCollectionEntity(ICollection<DTOComentario> comentarios)
        {
            if (comentarios == null)
                return null;

            var comentario = new List<Comentario>();
            foreach (var com in comentarios)
            {
                var c = new Comentario()
                {
                    Id = com.Id,
                    Contenido = com.Contenido,
                    Fecha = com.Fecha,
                    IdUsuario = com.IdUsuario,
                    IdProyecto = com.IdProyecto,
                };
                comentario.Add(c);
            }
            return comentario;
        }
    }
}
