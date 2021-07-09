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
        public List<DTOPortafolio> MapToCollectionObject(ICollection<Portafolio> portafolios)
        {
            if (portafolios == null)
                return null;

            var portafolio = new List<DTOPortafolio>();
            foreach (var porta in portafolios)
            {
                var p = new DTOPortafolio()
                {
                    Id = porta.Id,
                    IdProyecto = porta.IdProyecto,
                    Contenido = porta.Contenido,
                };
                portafolio.Add(p);
            }
            return portafolio;
        }

        public List<Portafolio> MapToCollectionEntity(ICollection<DTOPortafolio> portafolios)
        {
            if (portafolios == null)
                return null;

            var portafolio = new List<Portafolio>();
            foreach (var porta in portafolios)
            {
                var p = new Portafolio()
                {
                    Id = porta.Id,
                    IdProyecto = porta.IdProyecto,
                    Contenido = porta.Contenido,
                };
                portafolio.Add(p);
            }
            return portafolio;
        }
    }
}
