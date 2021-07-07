using Dominio.Mappers;
using Dominio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataTransferObjects;
using Dominio.DataModel.Repositories;
using Persistencia.Database;

namespace Dominio.General
{
    public class MantenimientoSeguimiento
    {
        private SeguimientoMapper _mapper;

        public MantenimientoSeguimiento()
        {
            this._mapper = new SeguimientoMapper();
        }

        public void Seguir(DTOSeguimiento dtoseguir)
        {
            using (var context = new DesignProDB())
            {
                var repository = new SeguimientoRepository(context);
                repository.Create(_mapper.MapToEntity(dtoseguir));
                context.SaveChanges();
            }
            
        }
        public void DejarDeSeguir(DTOSeguimiento dtoseguir)
        {
            using (var context = new DesignProDB())
            {
                var repository = new SeguimientoRepository(context);
                repository.Remove(this.Get(dtoseguir.IdUsuario,dtoseguir.IdSeguidor).Id);
                context.SaveChanges();
            }
        }

        public DTOSeguimiento Get(int idUsuario, int idSeguidor)
        {
            using (var context = new DesignProDB())
            {
                var repository = new SeguimientoRepository(context);
                return _mapper.MapToObject(repository.Get(idUsuario, idSeguidor));
            }
        }

        public bool LoSigue(DTOSeguimiento dtoseguir)
        {
            using (var context = new DesignProDB())
            {
                var repository = new SeguimientoRepository(context);
                return repository.LoSigue(dtoseguir.IdUsuario, dtoseguir.IdSeguidor);
            }
        }
    }
}
