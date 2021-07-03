using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DataModel.Repositories
{
    public class SeguimientoRepository
    {
        private readonly DesignProDB _context;

        public SeguimientoRepository(DesignProDB context)
        {
            this._context = context;
        }

        public void Create(Seguimiento seguimiento)
        {
            this._context.Seguimiento.Add(seguimiento);
        }

        public void Remove(int id)
        {
            var entity = this.Get(id);
            this._context.Seguimiento.Remove(entity);
        }

        public void RemoveByUsuario(int idUsuario)
        {
            var lista = this.GetAll();
            foreach (var usuario in lista)
            {
                if((usuario.IdUsuario == idUsuario) || (usuario.IdSeguidor == idUsuario))
                {
                    this._context.Seguimiento.Remove(usuario);
                }
            }
        }

        public bool LoSigue(int idUsuario, int idSeguidor)
        {
            if (this._context.Seguimiento.FirstOrDefault(a => a.IdUsuario == idUsuario && a.IdSeguidor == idSeguidor) == null)
                return false;
            else
                return true;
        }

        public List<Seguimiento> GetAll()
        {
            return this._context.Seguimiento.Select(a => a).ToList();
        }
        public Seguimiento Get(int id)
        {
            return this._context.Seguimiento.FirstOrDefault(a => a.Id.Equals(id));
        }

        public Seguimiento Get(int idUsuario, int idSeguidor)
        {
            return this._context.Seguimiento.FirstOrDefault(a => a.IdUsuario == idUsuario && a.IdSeguidor == idSeguidor);
        }

        public List<Seguimiento> GetAllSeguidores(int idUsuario)
        {
            return this._context.Seguimiento.Where(a => a.IdUsuario == idUsuario).ToList();
        }

        public List<Seguimiento> GetAllSiguiendo(int idUsuario)
        {
            return this._context.Seguimiento.Where(a => a.IdSeguidor == idUsuario).ToList();
        }
    }
}
