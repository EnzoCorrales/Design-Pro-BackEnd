using Common.DataTransferObjects;
using Dominio.DataModel.Repositories;
using Dominio.Mappers;
using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.General
{
    public class MantenimientoComentario
    {
        private ComentarioMapper _mapper;

        public MantenimientoComentario()
        {
            this._mapper = new ComentarioMapper();
        }

        public void Create(DTOComentario dtocomentario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);

                var comentario = _mapper.MapToEntity(dtocomentario);

                repository.Create(comentario);

                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                repository.Remove(id);

                context.SaveChanges();
            }
        }

        public void RemoveByUsuario(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                repository.RemoveByUsuario(idUsuario);

                context.SaveChanges();
            }
        }

        public void RemoveByProyecto(int idProyecto)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                repository.RemoveByProyecto(idProyecto);

                context.SaveChanges();
            }
        }

        public DTOComentario Get(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                var U_repository = new UsuarioRepository(context);
                var c = _mapper.MapToObject(repository.Get(id));
                c.Nombre = U_repository.Get(c.IdUsuario).Nombre;
                return c;
            }
        }

        public bool ExisteComentario(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                if (repository.Get(id) == null)
                    return false;
                else
                    return true;
            }
        }

        public List<DTOComentario> GetAllByUsuario(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetAllByUsuario(idUsuario);

                List<DTOComentario> resultado = new List<DTOComentario>();

                foreach (var comentario in lista)
                {
                    var c = _mapper.MapToObject(comentario);
                    c.Nombre = U_repository.Get(idUsuario).Nombre;
                    resultado.Add(c);
                }

                return resultado;
            }
        }

        public List<DTOComentario> GetAllByProyecto(int idProyecto)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetAllByProyecto(idProyecto);

                List<DTOComentario> resultado = new List<DTOComentario>();

                foreach (var comentario in lista)
                {
                    var c = _mapper.MapToObject(comentario);
                    c.Nombre = U_repository.Get(c.IdUsuario).Nombre;
                    resultado.Add(c);
                }

                return resultado;
            }
        }

        public List<DTOComentario> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetAll();

                List<DTOComentario> resultado = new List<DTOComentario>();

                foreach (var comentario in lista)
                {
                    var c = _mapper.MapToObject(comentario);
                    c.Nombre = U_repository.Get(c.IdUsuario).Nombre;
                    resultado.Add(c);
                }

                return resultado;
            }
        }
    }
}
