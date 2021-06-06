using Common.DataTransferObjects;
using Dominio.DataModel.Repositories;
using Dominio.Mappers;
using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.General
{
    public class MantenimientoMensaje
    {

        private MensajeMapper _mapper;

        public MantenimientoMensaje()
        {
            this._mapper = new MensajeMapper();
        }

        public void Create(DTOMensaje dtomensaje)
        {
            try
            {
                using (var context = new DesignProDB())
                {
                    var repository = new MensajeRepository(context);

                    var mensaje = _mapper.MapToEntity(dtomensaje);

                    repository.Create(mensaje);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DTOMensaje> GetAllByEmisor(string correo)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var lista = repository.GetAllByEmisor(correo);

                List<DTOMensaje> resultado = new List<DTOMensaje>();

                foreach (var mensaje in lista)
                {
                    resultado.Add(_mapper.MapToObject(mensaje));
                }

                return resultado;
            }
        }

        public List<DTOMensaje> GetAllByReceptor(string correo)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var lista = repository.GetAllByReceptor(correo);

                List<DTOMensaje> resultado = new List<DTOMensaje>();

                foreach (var mensaje in lista)
                {
                    resultado.Add(_mapper.MapToObject(mensaje));
                }

                return resultado;
            }
        }

        public List<DTOMensaje> GetConversacion(string usuario1, string usuario2)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var lista = repository.GetConversacion(usuario1, usuario2);

                List<DTOMensaje> resultado = new List<DTOMensaje>();

                foreach (var mensaje in lista)
                {
                    resultado.Add(_mapper.MapToObject(mensaje));
                }

                return resultado;
            }
        }

        public DTOMensaje Get(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                return _mapper.MapToObject(repository.Get(id));
            }
        }

        public List<DTOMensaje> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var lista = repository.GetAll();

                List<DTOMensaje> resultado = new List<DTOMensaje>();

                foreach (var mensaje in lista)
                {
                    resultado.Add(_mapper.MapToObject(mensaje));
                }

                return resultado;
            }
        }
    }
}
