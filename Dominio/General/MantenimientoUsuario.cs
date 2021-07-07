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
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);

                repository.Create(_mapper.MapToEntity(dtousuario));

                context.SaveChanges();
            }
        }

        public bool ValidarUsuario(string correo, string password)
        {
              using (var context = new DesignProDB())
              {
                    var repository = new UsuarioRepository(context);
                    if (repository.Get(correo).Password.Equals(password))
                        return true;
                    else
                        return false;
              }
        }

        public void Update(DTOUsuario dtousuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                repository.Update(_mapper.MapToEntity(dtousuario));
                context.SaveChanges();
            }
        }

        public void Remove(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var U_repository = new UsuarioRepository(context);
                var S_repository = new SeguimientoRepository(context);
                var M_repository = new MensajeRepository(context);
                M_repository.RemoveByUsuario(idUsuario);
                S_repository.RemoveByUsuario(idUsuario);
                U_repository.Remove(idUsuario);
                context.SaveChanges();
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
