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

        public static HashSet<DTOSeguimiento> MapToCollectionObject(ICollection<Seguimiento> seguimientos)
        {
            if (seguimientos == null)
                return null;

            var seguimiento = new HashSet<DTOSeguimiento>();
            foreach (var seg in seguimientos)
            {
                var s = new DTOSeguimiento()
                {
                    Id = seg.Id,
                    IdUsuario = seg.IdUsuario,
                    IdSeguidor = seg.IdSeguidor,
                };
                seguimiento.Add(s);
            }
            return seguimiento;
        }

        public static HashSet<Seguimiento> MapToCollectionEntity(ICollection<DTOSeguimiento> seguimientos)
        {
            if (seguimientos == null)
                return null;

            var seguimiento = new HashSet<Seguimiento>();
            foreach (var seg in seguimientos)
            {
                var s = new Seguimiento()
                {
                    Id = seg.Id,
                    IdUsuario = seg.IdUsuario,
                    IdSeguidor = seg.IdSeguidor,
                };
                seguimiento.Add(s);
            }
            return seguimiento;
        }
    }
}
