﻿using Persistencia.Database;
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

        public List<Mensaje> GetAllByEmisor(string correo)
        {
            return this._context.Mensaje.Where(a => a.UsuarioE.Equals(correo)).ToList();
        }

        public List<Mensaje> GetAllByReceptor(string correo)
        {
            return this._context.Mensaje.Where(a => a.UsuarioR.Equals(correo)).ToList();
        }

        public List<Mensaje> GetConversacion(string usuario1, string usuario2)
        {
            var lista = this._context.Mensaje.Where(a => a.UsuarioE.Equals(usuario1) && a.UsuarioR.Equals(usuario2)).ToList(); // lista con los mensajes que le envio el usuario 1 al usuario 2
            var lista2 = this._context.Mensaje.Where(a => a.UsuarioE.Equals(usuario2) && a.UsuarioR.Equals(usuario1)).ToList(); // lista con los mensajes que le envio el usuario 2 al usuario 1
            return lista.OrderBy(a => a.Fecha).Union(lista2).ToList(); // uno las dos listas anteriores y las ordeno por fecha, entonces los mensajes me quedan en el orden que se lo mandaron los dos usuarios
        }

        public Mensaje Get(int id)
        {
            return this._context.Mensaje.FirstOrDefault(a => a.Id == id);
        }

        public void Create(Mensaje mensaje)
        {
            this._context.Mensaje.Add(mensaje);
        }

    }
}
