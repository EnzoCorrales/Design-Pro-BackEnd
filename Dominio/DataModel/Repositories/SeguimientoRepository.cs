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

        public List<Seguimiento> GetAllSeguidores(string correo)
        {
            return this._context.Seguimiento.Where(a => a.Usuario.Equals(correo)).ToList();
        }

        public List<Seguimiento> GetAllSiguiendo(string correo)
        {
            return this._context.Seguimiento.Where(a => a.Seguidor.Equals(correo)).ToList();
        }
    }
}
