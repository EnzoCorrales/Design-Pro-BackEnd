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

        public void RemoveByUsuario(int idUsuario)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new ProyectoRepository(context);
                    repository.RemoveByUsuario(idUsuario);

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

        public List<DTOProyecto> GetBusquedaXTitulo(string busqueda)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var lista = repository.GetBusquedaXTitulo(busqueda);

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    resultado.Add(_mapper.MapToObject(proyecto));
                }
                return resultado;
            }
        }

        public List<DTOProyecto> GetBusquedaXAutor(string busqueda)
        {
            using (var context = new DesignProDB())
            {
                var P_repository = new ProyectoRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = P_repository.GetBusquedaXAutor(U_repository.GetIdsByNombre(busqueda));

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    resultado.Add(_mapper.MapToObject(proyecto));
                }
                return resultado;
            }
        }

        public List<DTOProyecto> GetBusquedaXTag(string busqueda)
        {
            using (var context = new DesignProDB())
            {
                var P_repository = new ProyectoRepository(context);
                var T_repository = new TagRepository(context);
                var lista = P_repository.GetAllByIds(T_repository.GetIdsByTag(busqueda));

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    resultado.Add(_mapper.MapToObject(proyecto));
                }
                return resultado;
            }
        }

        public int GetIdAutor(int idProyecto)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                return this.Get(idProyecto).IdAutor;
            }
        }

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
        public List<DTOProyecto> GetAll(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var lista = repository.GetAll(idUsuario);

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
