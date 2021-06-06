using Dominio.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataTransferObjects;
using Persistencia.Database;
using Dominio.DataModel.Repositories;

namespace Dominio.General
{
    public class MantenimientoProyecto
    {
        private ProyectoMapper _mapper;

        public MantenimientoProyecto()
        {
            this._mapper = new ProyectoMapper();
        }

        public void Create(DTOProyecto dtoproyecto)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new ProyectoRepository(context);

                    var proyecto = _mapper.MapToEntity(dtoproyecto);

                    repository.Create(proyecto);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new ProyectoRepository(context);
                    repository.Remove(id);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(string correo)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new ProyectoRepository(context);
                    repository.Remove(correo);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DTOProyecto Get(int id)
        {
            using(var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                return _mapper.MapToObject(repository.Get(id));
            }
        }
        /// <summary>
        /// u
        /// </summary>
        /// <returns>todos los proyectos</returns>
        public List<DTOProyecto> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var lista = repository.GetAll();

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    resultado.Add(_mapper.MapToObject(proyecto));
                }

                return resultado;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="correo"></param>
        /// <returns>una lista con los proyectos dado un cierto usuario</returns>
        public List<DTOProyecto> GetAll(string correo)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var lista = repository.GetAll(correo);

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    resultado.Add(_mapper.MapToObject(proyecto));
                }

                return resultado;
            }
        }

    }
}
