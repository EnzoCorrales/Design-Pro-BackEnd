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

        public List<DTOTag> MapToCollectionObject(ICollection<Tag> tags)
        {
            if (tags == null)
                return null;

            var tag = new List<DTOTag>();
            foreach (var ta in tags)
            {
                var t = new DTOTag()
                {
                    Id = ta.Id,
                    IdProyecto = ta.IdProyecto,
                    Tag = ta.Tag1,
                };
                tag.Add(t);
            }
            return tag;
        }

        public List<Tag> MapToCollectionEntity(ICollection<DTOTag> tags)
        {
            if (tags == null)
                return null;

            var tag = new List<Tag>();
            foreach (var ta in tags)
            {
                var t = new Tag()
                {
                    Id = ta.Id,
                    IdProyecto = ta.IdProyecto,
                    Tag1 = ta.Tag,
                };
                tag.Add(t);
            }
            return tag;
        }

    }
}
