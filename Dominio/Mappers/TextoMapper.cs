using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataTransferObjects;
using Persistencia.Database;

namespace Dominio.Mappers
{
    public class TextoMapper
    {

        public DTOTexto MapToObject(Texto texto)
        {
            if (texto == null)
                return null;

            return new DTOTexto()
            {
                Id = texto.Id,
                IdProyecto = texto.IdProyecto,
                Texto1 = texto.Texto1,
                Orden = texto.Orden,
            };
        }

        public Texto MapToEntity(DTOTexto texto)
        {
            if (texto == null)
                return null;

            return new Texto()
            {
                Id = texto.Id,
                IdProyecto = texto.IdProyecto,
                Texto1 = texto.Texto1,
                Orden = texto.Orden,
            };
        }

        public static HashSet<DTOTexto> MapToCollectionObject(ICollection<Texto> textos)
        {
            if (textos == null)
                return null;

            var texto = new HashSet<DTOTexto>();
            foreach (var tex in textos)
            {
                var t = new DTOTexto()
                {
                    Id = tex.Id,
                    IdProyecto = tex.IdProyecto,
                    Texto1 = tex.Texto1,
                    Orden = tex.Orden,
                };
                texto.Add(t);
            }
            return texto;
        }

        public static HashSet<Texto> MapToCollectionEntity(ICollection<DTOTexto> textos)
        {
            if (textos == null)
                return null;

            var texto = new HashSet<Texto>();
            foreach (var tex in textos)
            {
                var t = new Texto()
                {
                    Id = tex.Id,
                    IdProyecto = tex.IdProyecto,
                    Texto1 = tex.Texto1,
                    Orden = tex.Orden,
                };
                texto.Add(t);
            }
            return texto;
        }
    }
}
