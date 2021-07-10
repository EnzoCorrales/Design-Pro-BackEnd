using Common.DataTransferObjects;
using Dominio.Mappers;
using Dominio.DataModel.Repositories;
using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.General
{
    public class MantenimientoValoracion
    {
        private ValoracionMapper _mapper;

        public MantenimientoValoracion()
        {
            this._mapper = new ValoracionMapper();
        }

        public void ValorarProyecto(DTOValoracion valoracion)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ValoracionRepository(context);
                repository.Create(_mapper.MapToEntity(valoracion));
                context.SaveChanges();
            }
        }

        public void QuitarValoracion(DTOValoracion valoracion)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ValoracionRepository(context);
                repository.Remove(this.Get(valoracion.IdUsuario,valoracion.IdProyecto).Id);
                context.SaveChanges();
            }
        }

        public DTOValoracion Get(int idUsuario, int idProyecto)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ValoracionRepository(context);
                return _mapper.MapToObject(repository.Get(idUsuario, idProyecto));
            }
        }

        public bool LoValoro(int idUsuario, int idProyecto)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ValoracionRepository(context);
                return repository.LoValoro(idUsuario,idProyecto);
            }
        }
    }
}
