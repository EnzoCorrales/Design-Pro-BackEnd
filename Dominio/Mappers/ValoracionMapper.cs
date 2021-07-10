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
    }
}
