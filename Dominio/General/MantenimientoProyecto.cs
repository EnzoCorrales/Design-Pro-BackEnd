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
                return AgregarDatosAutor(_mapper.MapToObject(repository.Get(id)));
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
                var proyectos = _mapper.MapToCollectionObject(repository.GetBusquedaXTitulo(busqueda));
                List<DTOProyecto> resultado = new List<DTOProyecto>();
                foreach (var proyecto in proyectos)
                {
                    resultado.Add(AgregarDatosAutor(proyecto));
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
                var proyectos = _mapper.MapToCollectionObject(P_repository.GetBusquedaXAutor(U_repository.GetIdsByNombre(busqueda)));
                List<DTOProyecto> resultado = new List<DTOProyecto>();
                foreach (var proyecto in proyectos)
                {
                    resultado.Add(AgregarDatosAutor(proyecto));
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
                var proyectos = _mapper.MapToCollectionObject(P_repository.GetAllByIds(T_repository.GetIdsByTag(busqueda)));
                List<DTOProyecto> resultado = new List<DTOProyecto>();
                foreach (var proyecto in proyectos)
                {
                    resultado.Add(AgregarDatosAutor(proyecto));
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
                    resultado.Add(AgregarDatosAutor(p));
                }

                return resultado;
            }
        }

        public List<DTOProyecto> GetProyectosValorados(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var U_repository = new UsuarioRepository(context);
                var valoraciones = U_repository.Get(idUsuario).Valoracion;
                List<DTOProyecto> proyectos = new List<DTOProyecto>();
                for (var x = 0; x < valoraciones.Count; x++)
                {
                    var proyecto = _mapper.MapToObject(repository.Get(valoraciones.ElementAt(x).IdProyecto));
                    proyectos.Add(AgregarDatosAutor(proyecto));
                }

                return proyectos;
            }
        }
        /// <returns>una lista con los proyectos dado un cierto usuario</returns>
        public List<DTOProyecto> GetAll(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                var proyectos = _mapper.MapToCollectionObject(repository.GetAll(idUsuario));
                List<DTOProyecto> resultado = new List<DTOProyecto>();
                foreach (var proyecto in proyectos)
                {
                    resultado.Add(AgregarDatosAutor(proyecto));
                }

                return resultado;
            }
        }

        public void VisitarProyecto(int idProyecto)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ProyectoRepository(context);
                repository.VisitarProyecto(idProyecto);
                context.SaveChanges();
            }
        }

        public DTOProyecto AgregarDatosAutor(DTOProyecto proyecto)
        {
            using (var context = new DesignProDB())
            {
                var u_repository = new UsuarioRepository(context);
                proyecto.NombreAutor = u_repository.Get(proyecto.IdAutor).Nombre + " " + u_repository.Get(proyecto.IdAutor).Apellido;
                proyecto.UbicacionAutor = u_repository.Get(proyecto.IdAutor).Ciudad + ", " + u_repository.Get(proyecto.IdAutor).Pais;
                proyecto.ImgAutor = u_repository.Get(proyecto.IdAutor).ImgPerfil;
                proyecto.Likes = u_repository.Get(proyecto.IdAutor).Proyecto.FirstOrDefault(a => a.Id == proyecto.Id).Valoracion.Count;
                for (var x = 0; x < proyecto.Comentarios.Count; x++)
                {
                    proyecto.Comentarios.ElementAt(x).Nombre = u_repository.Get(proyecto.Comentarios.ElementAt(x).IdUsuario).Nombre + " " + u_repository.Get(proyecto.Comentarios.ElementAt(x).IdUsuario).Apellido;
                }

                return proyecto;
            }
        }
    }
}
