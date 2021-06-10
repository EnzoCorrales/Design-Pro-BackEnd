using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DataModel.Repositories
{
    public class ProyectoRepository
    {
        private readonly DesignProDB _context;

        public ProyectoRepository(DesignProDB context)
        {
            this._context = context;
        }

        public Proyecto Get(int id)
        {
            return this._context.Proyecto.FirstOrDefault(a => a.Id == id);
        }

        public List<Proyecto> GetAll()
        {
            return this._context.Proyecto.Select(a => a).ToList();
        }

        public List<Proyecto> GetAll(int idUsuario)
        {
            return this._context.Proyecto.Where(a => a.IdAutor == idUsuario).ToList();
        }

        public void Create(Proyecto proyecto)
        {
            this._context.Proyecto.Add(proyecto);
        }

        public void Remove(int id)
        {
            var entity = this.Get(id);
            this._context.Proyecto.Remove(entity);
        }

        public void RemoveByUsuario(int idUsuario)
        {
            var lista = this.GetAll();
            foreach(var proyecto in lista)
            {
                if(proyecto.IdAutor == idUsuario)
                    this._context.Proyecto.Remove(proyecto);
            }
            
        }

    }
}
