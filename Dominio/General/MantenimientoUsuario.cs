using Dominio.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.DataTransferObjects;
using Dominio.DataModel.Repositories;
using Persistencia.Database;
using Microsoft.AspNet.Identity;

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
                dtousuario.Password = this.HashPassword(dtousuario.Password);
                repository.Create(_mapper.MapToEntity(dtousuario));

                context.SaveChanges();
            }
        }
        public bool ValidarUsuario(string correo, string password)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                var hasher = new PasswordHasher();

                if (hasher.VerifyHashedPassword(repository.Get(correo).Password, password) != PasswordVerificationResult.Failed)
                    return true;
                else
                    return false;
            }
        }
        public bool ExisteUsuario(string correo)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                if (repository.Get(correo) == null)
                    return false;
                else
                    return true;
            }
        }
        public bool ExisteUsuario(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                if (repository.Get(id) == null)
                    return false;
                else
                    return true;
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
                return this.AgregarDatos(_mapper.MapToObject(repository.Get(id)));
            }
        }
        public DTOUsuario Get(string correo)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return this.AgregarDatos(_mapper.MapToObject(repository.Get(correo)));
            }
        }
        public List<DTOUsuario> GetAllSeguidores(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                List<DTOUsuario> resultado = new List<DTOUsuario>();
                var lista = repository.GetAllSeguidores(idUsuario, context);
                foreach (var usuario in lista)
                {
                    var u = _mapper.MapToObject(usuario);
                    resultado.Add(AgregarDatos(u));
                }

                return resultado;
            }
        }
        public List<DTOUsuario> GetAllSiguiendo(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                List<DTOUsuario> resultado = new List<DTOUsuario>();
                var lista = repository.GetAllSiguiendo(idUsuario, context);
                foreach (var usuario in lista)
                {
                    var u = _mapper.MapToObject(usuario);
                    resultado.Add(AgregarDatos(u));
                }

                return resultado;
            }
        }
        public List<DTOUsuario> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                List<DTOUsuario> resultado = new List<DTOUsuario>();
                var lista = repository.GetAll();
                foreach(var usuario in lista)
                {
                    var u = _mapper.MapToObject(usuario);
                    resultado.Add(AgregarDatos(u));
                }

                return resultado;
            }
        }
        public string HashPassword(string password)
        {
            var hasher = new PasswordHasher();
            return hasher.HashPassword(password);
        }

        public int GetVisitasTotales(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                var proyectos = repository.Get(idUsuario).Proyecto;
                var visitas = 0;
                foreach (var proyecto in proyectos)
                {
                    visitas = visitas + proyecto.Visitas;
                }

                return visitas;
            }
        }

        public int GetLikesTotales(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                var proyectos = repository.Get(idUsuario).Proyecto;
                var likes = 0;
                foreach(var proyecto in proyectos)
                {
                    likes = likes + proyecto.Valoracion.Count;
                }

                return likes;
            }
        }

        public int GetSeguidores(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return repository.Get(idUsuario).Seguimiento1.Count;
            }
        }

        public DTOUsuario AgregarDatos(DTOUsuario usuario)
        {
            usuario.Likes = this.GetLikesTotales(usuario.Id);
            usuario.Visitas = this.GetVisitasTotales(usuario.Id);
            usuario.Seguidores = this.GetSeguidores(usuario.Id);
            usuario.Password = null;

            return usuario;
        }
    }
}
