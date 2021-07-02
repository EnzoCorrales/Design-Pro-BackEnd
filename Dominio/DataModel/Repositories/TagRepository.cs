using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DataModel.Repositories
{
    public class TagRepository
    {
        private readonly DesignProDB _context;

        public TagRepository(DesignProDB context)
        {
            this._context = context;
        }

        public List<int> GetIdsByTag(string busqueda)
        {
            List<int> resultado = new List<int>();
            var lista = this._context.Tag.Where(a => a.Tag1.Equals(busqueda)).ToList();

            foreach (var tag in lista)
            {
                resultado.Add(tag.IdProyecto);
            }
            return resultado;
        }
    }
}
