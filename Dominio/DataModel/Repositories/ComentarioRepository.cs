using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DataModel.Repositories
{
    public class ComentarioRepository
    {
        private readonly DesignProDB _context;

        public ComentarioRepository(DesignProDB context)
        {
            this._context = context;
        }

        public Comentario Get(int id)
        {
            return this._context.Comentario.FirstOrDefault(a => a.Id == id);
        }

        public List<Comentario> GetAll()
        {
            return this._context.Comentario.Select(a => a).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="correo"></param>
        /// <returns>una lista de comentarios dado un usuario</returns>
        public List<Comentario> GetAll(String correo)
        {
            return this._context.Comentario.Where(a => a.Usuario.Equals(correo)).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProyecto"></param>
        /// <returns>una lista de comentarios dado un proyecto</returns>
        public List<Comentario> GetAll(int idProyecto)
        {
            return this._context.Comentario.Where(a => a.IdProyecto == idProyecto).ToList();
        }

        public void Create(Comentario comentario)
        {
            this._context.Comentario.Add(comentario);

        }

        public void Remove(int id)
        {
            var entity = this.Get(id);
            this._context.Comentario.Remove(entity);
        }

        public void Remove(string correo)
        {
            var lista = this.GetAll();
            foreach(var comentario in lista)
            {
                if(comentario.Usuario.Equals(correo))
                    this._context.Comentario.Remove(comentario);
            }
        }

        public void RemoveByProyecto(int idProyecto)
        {
            var lista = this.GetAll();
            foreach (var comentario in lista)
            {
                if (comentario.IdProyecto == idProyecto)
                    this._context.Comentario.Remove(comentario);
            }
        }
    }
}
