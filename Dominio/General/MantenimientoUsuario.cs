using Dominio.Mappers;
using Dominio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DataTransferObjects;
using Dominio.DataModel.Repositories;
using Persistencia.Database;
using Common.Exceptions;

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
                        throw new ValidateException("Correo en uso");

                    var usuario = _mapper.MapToEntity(dtousuario);

                    repository.Create(usuario);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ValidarUsuario(string correo, string password)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new UsuarioRepository(context);
                    return repository.ValidarUsuario(correo, password);
                }
            }
            catch (Exception)
            {
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
                    var current = repository.Get(dtousuario.Id);

                    //System.Diagnostics.Debug.WriteLine(current.Password + " curr pass || curr cor " + current.Correo + " curr cor || dto cor " + dtousuario.Correo + " dto cor || dto pass  " + dtousuario.Password);

                    if (dtousuario.Correo != "")
                    {
                        if(current.Correo != dtousuario.Correo && repository.Get(dtousuario.Correo) != null)
                        {
                            throw new Exception("Correo en uso");
                        }
                    }

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

        public DTOUsuario Get(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return _mapper.MapToObject(repository.Get(id));
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
