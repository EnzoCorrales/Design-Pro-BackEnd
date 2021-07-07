using Common.DataTransferObjects;
using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mappers
{
    class ValoracionMapper
    {
        public DTOValoracion MapToObject(Valoracion valoracion)
        {
            if (valoracion == null)
                return null;

            return new DTOValoracion()
            {
                Id = valoracion.Id,
                IdUsuario = valoracion.IdUsuario,
                IdProyecto = valoracion.IdProyecto,
            };
        }

        public Valoracion MapToEntity(DTOValoracion valoracion)
        {
            if (valoracion == null)
                return null;

            return new Valoracion()
            {
                Id = valoracion.Id,
                IdUsuario = valoracion.IdUsuario,
                IdProyecto = valoracion.IdProyecto,
            };
        }

        public List<DTOValoracion> MapToCollectionObject(ICollection<Valoracion> valoraciones)
        {
            if (valoraciones == null)
                return null;

            var valoracion = new List<DTOValoracion>();
            foreach (var val in valoraciones)
            {
                var v = new DTOValoracion()
                {
                    Id = val.Id,
                    IdUsuario = val.IdUsuario,
                    IdProyecto = val.IdProyecto,
                };
                valoracion.Add(v);
            }
            return valoracion;
        }

        public List<Valoracion> MapToCollectionEntity(ICollection<DTOValoracion> valoraciones)
        {
            if (valoraciones == null)
                return null;

            var valoracion = new List<Valoracion>();
            foreach (var val in valoraciones)
            {
                var v = new Valoracion()
                {
                    Id = val.Id,
                    IdUsuario = val.IdUsuario,
                    IdProyecto = val.IdProyecto,
                };
                valoracion.Add(v);
            }
            return valoracion;
        }
    }
}
