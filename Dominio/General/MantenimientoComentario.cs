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
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new ComentarioRepository(context);

                    var comentario = _mapper.MapToEntity(dtocomentario);

                    repository.Create(comentario);

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
                    var repository = new ComentarioRepository(context);
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
                    var repository = new ComentarioRepository(context);
                    repository.RemoveByUsuario(idUsuario);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveByProyecto(int idProyecto)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new ComentarioRepository(context);
                    repository.RemoveByProyecto(idProyecto);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DTOComentario Get(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                return _mapper.MapToObject(repository.Get(id));
            }
        }

        public List<DTOComentario> GetAllByUsuario(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                var lista = repository.GetAllByUsuario(idUsuario);

                List<DTOComentario> resultado = new List<DTOComentario>();

                foreach (var comentario in lista)
                {
                    resultado.Add(_mapper.MapToObject(comentario));
                }

                return resultado;
            }
        }

        public List<DTOComentario> GetAllByProyecto(int idProyecto)
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                var lista = repository.GetAllByProyecto(idProyecto);

                List<DTOComentario> resultado = new List<DTOComentario>();

                foreach (var comentario in lista)
                {
                    resultado.Add(_mapper.MapToObject(comentario));
                }

                return resultado;
            }
        }

        public List<DTOComentario> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new ComentarioRepository(context);
                var lista = repository.GetAll();

                List<DTOComentario> resultado = new List<DTOComentario>();

                foreach (var comentario in lista)
                {
                    resultado.Add(_mapper.MapToObject(comentario));
                }

                return resultado;
            }
        }
    }
}
