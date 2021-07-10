using Common.DataTransferObjects;
using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mappers
{
    public class SeguimientoMapper
    {
        public DTOSeguimiento MapToObject(Seguimiento seguimiento)
        {
            if (seguimiento == null)
                return null;

            return new DTOSeguimiento()
            {
                Id = seguimiento.Id,
                IdUsuario = seguimiento.IdUsuario,
                IdSeguidor = seguimiento.IdSeguidor,
            };
        }

        public Seguimiento MapToEntity(DTOSeguimiento seguimiento)
        {
            if (seguimiento == null)
                return null;

            return new Seguimiento()
            {
                Id = seguimiento.Id,
                IdUsuario = seguimiento.IdUsuario,
                IdSeguidor = seguimiento.IdSeguidor,
            };
        }
    }
}
