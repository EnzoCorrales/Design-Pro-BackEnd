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

        public List<Proyecto> GetBusquedaXTitulo(string busqueda)
        {
            return this._context.Proyecto.Where(a => a.Titulo.Contains(busqueda)).ToList();
        }

        public List<Proyecto> GetBusquedaXAutor(List<int> ids)
        {
            List<Proyecto> resultado = new List<Proyecto>();
            foreach (var id in ids)
            {
                resultado.AddRange(this._context.Proyecto.Where(a => a.IdAutor == id));
            }
            return resultado;
        }

        public List<Proyecto> GetAll()
        {
            return this._context.Proyecto.Select(a => a).ToList();
        }

        public List<Proyecto> GetAllByIds(List<int> ids)
        {
            List<Proyecto> resultado = new List<Proyecto>();
            foreach (var id in ids)
            {
                resultado.Add(this._context.Proyecto.FirstOrDefault(a => a.Id == id));
            }
            return resultado;
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
