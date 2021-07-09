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
                return this.SinColecciones(_mapper.MapToObject(repository.Get(id)));
            }
        }
        public DTOUsuario Get(string correo)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return this.SinColecciones(_mapper.MapToObject(repository.Get(correo)));
            }
        }
        public List<DTOUsuario> GetAllSeguidores(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return SinColecciones(_mapper.MapToCollectionObject(repository.GetAllSeguidores(idUsuario, context)));
            }
        }
        public List<DTOUsuario> GetAllSiguiendo(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return SinColecciones(_mapper.MapToCollectionObject(repository.GetAllSiguiendo(idUsuario, context)));
            }
        }
        public List<DTOUsuario> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new UsuarioRepository(context);
                return SinColecciones(_mapper.MapToCollectionObject(repository.GetAll()));
            }
        }
        public string HashPassword(string password)
        {
            var hasher = new PasswordHasher();
            return hasher.HashPassword(password);
        }
        public List<DTOUsuario> SinColecciones(List<DTOUsuario> resultado)// devuelve el dto del usuario sin los datos inecesarios como las colecciones
        {
            for (var x = 0; x < resultado.Count; x++)
            {
                resultado.ElementAt(x).Password = null;
                resultado.ElementAt(x).Proyectos = null;
                resultado.ElementAt(x).PValorados = null;
                resultado.ElementAt(x).Seguidores = null;
                //resultado.ElementAt(x).Siguiendo = null;
                resultado.ElementAt(x).MensajesE = null;
                resultado.ElementAt(x).MensajesR = null;
                resultado.ElementAt(x).Comentarios = null;
            }

            return resultado;
        }
        public DTOUsuario SinColecciones(DTOUsuario usuario)
        {
            usuario.Password = null;
            usuario.Proyectos = null;
            usuario.PValorados = null;
            usuario.Seguidores = null;
            //usuario.Siguiendo = null;
            usuario.MensajesE = null;
            usuario.MensajesR = null;
            usuario.Comentarios = null;

            return usuario;
        }
    }
}
