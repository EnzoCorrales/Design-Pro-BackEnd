using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.DataModel.Repositories
{
    public class UsuarioRepository
    {
        private readonly DesignProDB _context;

        public UsuarioRepository(DesignProDB context)
        {
            this._context = context;
        }

        public Usuario Get(int id)
        {
            return this._context.Usuario.FirstOrDefault(a => a.Id == id);
        }

        public Usuario Get(string correo)
        {
            return this._context.Usuario.FirstOrDefault(a => a.Correo.Equals(correo));
        }

        public bool ValidarUsuario(string correo, string password)
        {
            if ( (this._context.Usuario.FirstOrDefault(a => a.Correo.Equals(correo) && a.Password.Equals(password)) != null ))
                return true;
            else 
                return false;
        }

        public List<Usuario> GetAll()
        {
            return this._context.Usuario.Select(a => a).ToList();
        }

        public List<int> GetIdsByNombre(string nombre)
        {
            var usuarios = this._context.Usuario.Where(a => a.Nombre.Equals(nombre)).ToList();
            List<int> ids = new List<int>();

            foreach (var usuario in usuarios)
            {
                ids.Add(usuario.Id);
            }
            return ids;
        }

        public List<Usuario> GetAllSeguidores(int id, DesignProDB c)
        {
            var repositorySeguimiento = new SeguimientoRepository(c);
            var seguidores = repositorySeguimiento.GetAllSeguidores(id);

            List<Usuario> resultado = new List<Usuario>();
            
            foreach(var seg in seguidores)
            {
                resultado.Add(this.Get(seg.IdSeguidor));
            }

            return resultado;
        }

        public List<Usuario> GetAllSiguiendo(int id, DesignProDB c)
        {
            var repositorySeguimiento = new SeguimientoRepository(c);
            var seguidores = repositorySeguimiento.GetAllSiguiendo(id);

            List<Usuario> resultado = new List<Usuario>();

            foreach (var seg in seguidores)
            {
                resultado.Add(this.Get(seg.IdUsuario));
            }

            return resultado;
        }

        public void Create(Usuario usuario)
        {
            this._context.Usuario.Add(usuario);
        }

        public void Update(Usuario usuario)
        {
            var entity = this.Get(usuario.Id);

            entity.Nombre = usuario.Nombre;
            entity.Apellido = usuario.Apellido;
            entity.Correo = usuario.Correo;
            entity.FNac = usuario.FNac;
            entity.Profesion = usuario.Profesion;
            entity.Empresa = usuario.Empresa;
            entity.Pais = usuario.Pais;
            entity.Ciudad = usuario.Ciudad;
            entity.ImgPerfil = usuario.ImgPerfil;
            entity.UrlWeb = usuario.UrlWeb;
            entity.Password = usuario.Password;
            entity.Descripcion = usuario.Descripcion;
        }

        public void Remove(int id)
        {
            var entity = this.Get(id);
            this._context.Usuario.Remove(entity);
        }
    }
}
