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

            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);

                var proyecto = _mapper.MapToEntity(dtoproyecto);

                repository.Create(proyecto);

                context.SaveChanges();
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

            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                repository.RemoveByUsuario(idUsuario);

                context.SaveChanges();
            }
        }

        public DTOProyecto Get(int id)
        {
            using(var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var U_repository = new UsuarioRepository(context);
                var p = _mapper.MapToObject(repository.Get(id));
                p.NombreAutor = U_repository.Get(p.IdAutor).Nombre;
                return p;
            }
        }

        public bool ExisteProyecto(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                if (repository.Get(id) == null)
                    return false;
                else
                    return true;
            }
        }

        public List<DTOProyecto> GetBusquedaXTitulo(string busqueda)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetBusquedaXTitulo(busqueda);

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    var p = _mapper.MapToObject(proyecto);
                    p.NombreAutor = U_repository.Get(p.IdAutor).Nombre;
                    resultado.Add(p);
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
                    var p = _mapper.MapToObject(proyecto);
                    p.NombreAutor = U_repository.Get(p.IdAutor).Nombre;
                    resultado.Add(p);
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
                var U_repository = new UsuarioRepository(context);
                var lista = P_repository.GetAllByIds(T_repository.GetIdsByTag(busqueda));

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    var p = _mapper.MapToObject(proyecto);
                    p.NombreAutor = U_repository.Get(p.IdAutor).Nombre;
                    resultado.Add(p);
                }
                return resultado;
            }
        }

        /// <returns>todos los proyectos</returns>
        public List<DTOProyecto> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetAll();

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    var p = _mapper.MapToObject(proyecto);
                    p.NombreAutor = U_repository.Get(p.IdAutor).Nombre;
                    resultado.Add(p);
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
                var u_repository = new UsuarioRepository(context);
                var lista = repository.GetAll(idUsuario);

                List<DTOProyecto> resultado = new List<DTOProyecto>();

                foreach (var proyecto in lista)
                {
                    var p = _mapper.MapToObject(proyecto);
                    p.NombreAutor = u_repository.Get(idUsuario).Nombre;
                    resultado.Add(p);
                }

                return resultado;
            }
        }

    }
}
