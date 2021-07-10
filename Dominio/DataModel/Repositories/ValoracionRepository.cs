using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DataModel.Repositories
{
    public class ValoracionRepository
    {
        private readonly DesignProDB _context;

        public ValoracionRepository(DesignProDB context)
        {
            this._context = context;
        }

        public void Create(Valoracion valoracion)
        {
            this._context.Valoracion.Add(valoracion);
        }

        public void Remove(int id)
        {
            this._context.Valoracion.Remove(this.Get(id));
        }

        public Valoracion Get(int id)
        {
            return this._context.Valoracion.FirstOrDefault(a => a.Id == id);
        }

        public Valoracion Get(int idUsuario, int idProyecto)
        {
            return this._context.Valoracion.FirstOrDefault(a => a.IdUsuario == idUsuario && a.IdProyecto == idProyecto);
        }

        public bool LoValoro(int idUsuario, int idProyecto)
        {
            if (this._context.Valoracion.FirstOrDefault(a => a.IdUsuario == idUsuario && a.IdProyecto == idProyecto) == null)
                return false;
            else
                return true;
        }
    }
}
