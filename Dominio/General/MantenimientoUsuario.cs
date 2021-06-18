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
using System.Data.Entity.Validation;

namespace Dominio.General
{
    public class MantenimientoUsuario
    {
        private UsuarioMapper _mapper;

        public MantenimientoUsuario()
        {
            this._mapper = new UsuarioMapper();
        }

        public void Create(DTOUsuario dtousuario)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new UsuarioRepository(context);
                    var current = repository.Get(dtousuario.Correo);

                    if (current != null)
                        throw new Exception("Correo en uso");

                    var usuario = _mapper.MapToEntity(dtousuario);

                    repository.Create(usuario);

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void Update(DTOUsuario dtousuario)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new UsuarioRepository(context);
                    repository.Update(_mapper.MapToEntity(dtousuario));

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(int idUsuario)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new UsuarioRepository(context);
                    repository.Remove(idUsuario);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DTOUsuario Get(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return _mapper.MapToObject(repository.Get(idUsuario));
            }
        }

        public DTOUsuario Get(string correo)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return _mapper.MapToObject(repository.Get(correo));
            }
        }

        public List<DTOUsuario> GetAllSeguidores(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                var lista = repository.GetAllSeguidores(idUsuario, context);

                List<DTOUsuario> resultado = new List<DTOUsuario>();

                foreach (var usuario in lista)
                {
                    resultado.Add(_mapper.MapToObject(usuario));
                }

                return resultado;
            }
        }

        public List<DTOUsuario> GetAllSiguiendo(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                var lista = repository.GetAllSiguiendo(idUsuario, context);

                List<DTOUsuario> resultado = new List<DTOUsuario>();

                foreach (var usuario in lista)
                {
                    resultado.Add(_mapper.MapToObject(usuario));
                }

                return resultado;
            }
        }

        public List<DTOUsuario> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                var lista = repository.GetAll();

                List<DTOUsuario> resultado = new List<DTOUsuario>();

                foreach (var usuario in lista)
                {
                    resultado.Add(_mapper.MapToObject(usuario));
                }

                return resultado;
            }
        }

    }
}
