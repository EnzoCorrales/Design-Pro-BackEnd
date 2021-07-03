using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DataModel.Repositories
{
    public class MensajeRepository
    {
        private readonly DesignProDB _context;

        public MensajeRepository(DesignProDB context)
        {
            this._context = context;
        }

        public List<Mensaje> GetAll()
        {
            return this._context.Mensaje.Select(a => a).ToList();
        }

        public List<Mensaje> GetAllByEmisor(int idUsuario)
        {
            return this._context.Mensaje.Where(a => a.IdUsuarioE == idUsuario).ToList();
        }

        public List<Mensaje> GetAllByReceptor(int idUsuario)
        {
            return this._context.Mensaje.Where(a => a.IdUsuarioR == idUsuario).ToList();
        }

        public List<Mensaje> GetConversacion(int idUsuario1, int idUsuario2)
        {
            var lista = this._context.Mensaje.Where(a => a.IdUsuarioE == idUsuario1 && a.IdUsuarioR == idUsuario2).ToList(); // lista con los mensajes que le envio el usuario 1 al usuario 2
            var lista2 = this._context.Mensaje.Where(a => a.IdUsuarioE == idUsuario2 && a.IdUsuarioR == idUsuario1).ToList(); // lista con los mensajes que le envio el usuario 2 al usuario 1
            return lista.OrderBy(a => a.Fecha).Union(lista2).ToList(); // uno las dos listas anteriores y las ordeno por fecha, entonces los mensajes me quedan en el orden que se lo mandaron los dos usuarios
        }

        public void RemoveByUsuario(int idUsuario)
        {
            var lista = this.GetAll();
            foreach (var usuario in lista)
            {
                if ((usuario.IdUsuarioE == idUsuario) || (usuario.IdUsuarioR == idUsuario))
                {
                    this._context.Mensaje.Remove(usuario);
                }
            }
        }

        public Mensaje Get(int id)
        {
            return this._context.Mensaje.FirstOrDefault(a => a.Id == id);
        }

        public void Create(Mensaje mensaje)
        {
            this._context.Mensaje.Add(mensaje);
        }

        public void Update(Mensaje mensaje)
        {
            var entity = this.Get(mensaje.Id);

            entity.Visto = 1;
        }

    }
}
