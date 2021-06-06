using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DataModel.Repositories
{
    public class UsuarioRepository
    {
        private readonly DesignProDB _context;

        public UsuarioRepository(DesignProDB context)
        {
            this._context = context;
        }

        public Usuario Get(string correo)
        {
            return this._context.Usuario.FirstOrDefault(a => a.Correo == correo);
        }

        public List<Usuario> GetAll()
        {
            return this._context.Usuario.Select(a => a).ToList();
        }

        public List<Usuario> GetAllSeguidores(string correo, DesignProDB c)
        {
            var repositorySeguimiento = new SeguimientoRepository(c);
            var seguidores = repositorySeguimiento.GetAllSeguidores(correo);

            List<Usuario> resultado = new List<Usuario>();
            
            foreach(var seg in seguidores)
            {
                resultado.Add(this.Get(seg.Seguidor));
            }

            return resultado;
        }

        public List<Usuario> GetAllSiguiendo(string correo, DesignProDB c)
        {
            var repositorySeguimiento = new SeguimientoRepository(c);
            var seguidores = repositorySeguimiento.GetAllSiguiendo(correo);

            List<Usuario> resultado = new List<Usuario>();

            foreach (var seg in seguidores)
            {
                resultado.Add(this.Get(seg.Usuario));
            }

            return resultado;
        }

        public void Create(Usuario usuario)
        {
            this._context.Usuario.Add(usuario);
        }

        public void Update(Usuario usuario)
        {
            var entity = this.Get(usuario.Correo);

            entity.Nombre = usuario.Nombre;
            entity.Apellido = usuario.Apellido;
            entity.Profesion = usuario.Profesion;
            entity.Empresa = usuario.Empresa;
            entity.Pais = usuario.Pais;
            entity.ImgPerfil = usuario.ImgPerfil;
            entity.UrlWeb = usuario.UrlWeb;
        }

        public void Remove(string correo)
        {
            var entity = this.Get(correo);
            this._context.Usuario.Remove(entity);
        }
    }
}
