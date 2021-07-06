using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataTransferObjects;
using Persistencia.Database;

namespace Dominio.Mappers
{
    public class PortafolioMapper
    {
        public DTOPortafolio MapToObject(Portafolio portafolio)
        {
            if (portafolio == null)
                return null;

            return new DTOPortafolio()
            {
                Id = portafolio.Id,
                IdProyecto = portafolio.IdProyecto,
                Contenido = portafolio.Contenido,
                Orden = portafolio.Orden,
            };
        }

        public Portafolio MapToEntity(DTOPortafolio portafolio)
        {
            if (portafolio == null)
                return null;

            return new Portafolio()
            {
                Id = portafolio.Id,
                IdProyecto = portafolio.IdProyecto,
                Contenido = portafolio.Contenido,
                Orden = portafolio.Orden,
            };
        }

        public static HashSet<DTOPortafolio> MapToCollectionObject(ICollection<Portafolio> portafolios)
        {
            if (portafolios == null)
                return null;

            var portafolio = new HashSet<DTOPortafolio>();
            foreach (var porta in portafolios)
            {
                var p = new DTOPortafolio()
                {
                    Id = porta.Id,
                    IdProyecto = porta.IdProyecto,
                    Contenido = porta.Contenido,
                    Orden = porta.Orden,
                };
                portafolio.Add(p);
            }
            return portafolio;
        }

        public static HashSet<Portafolio> MapToCollectionEntity(ICollection<DTOPortafolio> portafolios)
        {
            if (portafolios == null)
                return null;

            var portafolio = new HashSet<Portafolio>();
            foreach (var porta in portafolios)
            {
                var p = new Portafolio()
                {
                    Id = porta.Id,
                    IdProyecto = porta.IdProyecto,
                    Contenido = porta.Contenido,
                    Orden = porta.Orden,
                };
                portafolio.Add(p);
            }
            return portafolio;
        }
    }
}
