﻿using Common.DataTransferObjects;
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
                Usuario = valoracion.Usuario,
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
                Usuario = valoracion.Usuario,
                IdProyecto = valoracion.IdProyecto,
            };
        }

        public static HashSet<DTOValoracion> MapToCollectionObject(ICollection<Valoracion> valoraciones)
        {
            if (valoraciones == null)
                return null;

            var valoracion = new HashSet<DTOValoracion>();
            foreach (var val in valoraciones)
            {
                var v = new DTOValoracion()
                {
                    Id = val.Id,
                    Usuario = val.Usuario,
                    IdProyecto = val.IdProyecto,
                };
                valoracion.Add(v);
            }
            return valoracion;
        }

        public static HashSet<Valoracion> MapToCollectionEntity(ICollection<DTOValoracion> valoraciones)
        {
            if (valoraciones == null)
                return null;

            var valoracion = new HashSet<Valoracion>();
            foreach (var val in valoraciones)
            {
                var v = new Valoracion()
                {
                    Id = val.Id,
                    Usuario = val.Usuario,
                    IdProyecto = val.IdProyecto,
                };
                valoracion.Add(v);
            }
            return valoracion;
        }
    }
}