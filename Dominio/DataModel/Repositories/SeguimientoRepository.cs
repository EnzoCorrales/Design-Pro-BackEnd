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

        public Seguimiento Get(int id)
        {
            return this._context.Seguimiento.FirstOrDefault(a => a.Id.Equals(id));
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
