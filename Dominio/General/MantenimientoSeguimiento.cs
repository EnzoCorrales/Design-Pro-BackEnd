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
            try
            {
                using(var context = new DesignProDB())
                {
                    var repository = new SeguimientoRepository(context);
                    var seguir = _mapper.MapToEntity(dtoseguir);
                    repository.Create(seguir);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DejarDeSeguir(DTOSeguimiento dtoseguir)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new SeguimientoRepository(context);
                    var seguir = _mapper.MapToEntity(dtoseguir);
                    repository.Remove(seguir.Id);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
