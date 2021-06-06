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
    public class TagMapper
    {
        public DTOTag MapToObject(Tag tags)
        {
            if (tags == null)
                return null;

            return new DTOTag()
            {
                IdProyecto = tags.IdProyecto,
                Tag = tags.Tag1,
            };
        }

        public Tag MapToEntity(DTOTag tags)
        {
            if (tags == null)
                return null;

            return new Tag()
            {
                IdProyecto = tags.IdProyecto,
                Tag1 = tags.Tag,
            };
        }

        public static HashSet<DTOTag> MapToCollectionObject(ICollection<Tag> tags)
        {
            if (tags == null)
                return null;

            var tag = new HashSet<DTOTag>();
            foreach (var ta in tags)
            {
                var t = new DTOTag()
                {
                    IdProyecto = ta.IdProyecto,
                    Tag = ta.Tag1,
                };
                tag.Add(t);
            }
            return tag;
        }

        public static HashSet<Tag> MapToCollectionEntity(ICollection<DTOTag> tags)
        {
            if (tags == null)
                return null;

            var tag = new HashSet<Tag>();
            foreach (var ta in tags)
            {
                var t = new Tag()
                {
                    IdProyecto = ta.IdProyecto,
                    Tag1 = ta.Tag,
                };
                tag.Add(t);
            }
            return tag;
        }

    }
}
