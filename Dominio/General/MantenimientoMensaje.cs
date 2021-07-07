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

            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);

                var mensaje = _mapper.MapToEntity(dtomensaje);

                repository.Create(mensaje);

                context.SaveChanges();
            }
        }

        public void Update(DTOMensaje dtoMensaje)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);

                repository.Update(_mapper.MapToEntity(dtoMensaje));

                context.SaveChanges();
            }
        }

        public DTOMensaje Get(int id)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var U_repository = new UsuarioRepository(context);
                var m = _mapper.MapToObject(repository.Get(id));
                m.NombreE = U_repository.Get(m.IdUsuarioE).Nombre;
                m.NombreR = U_repository.Get(m.IdUsuarioR).Nombre;
                return m;
            }
        }

        public List<DTOMensaje> GetAllByEmisor(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetAllByEmisor(idUsuario);

                List<DTOMensaje> resultado = new List<DTOMensaje>();

                foreach (var mensaje in lista)
                {
                    var m = _mapper.MapToObject(mensaje);
                    m.NombreE = U_repository.Get(m.IdUsuarioE).Nombre;
                    m.NombreR = U_repository.Get(m.IdUsuarioR).Nombre;
                    resultado.Add(m);
                }

                return resultado;
            }
        }

        public List<DTOMensaje> GetAllByReceptor(int idUsuario)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetAllByReceptor(idUsuario);

                List<DTOMensaje> resultado = new List<DTOMensaje>();

                foreach (var mensaje in lista)
                {
                    var m = _mapper.MapToObject(mensaje);
                    m.NombreE = U_repository.Get(m.IdUsuarioE).Nombre;
                    m.NombreR = U_repository.Get(m.IdUsuarioR).Nombre;
                    resultado.Add(m);
                }

                return resultado;
            }
        }

        public List<DTOMensaje> GetConversacion(int idUsuario1, int idUsuario2)
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetConversacion(idUsuario1, idUsuario2);

                List<DTOMensaje> resultado = new List<DTOMensaje>();

                foreach (var mensaje in lista)
                {
                    var m = _mapper.MapToObject(mensaje);
                    m.NombreE = U_repository.Get(m.IdUsuarioE).Nombre;
                    m.NombreR = U_repository.Get(m.IdUsuarioR).Nombre;
                    resultado.Add(m);
                }

                return resultado;
            }
        }

        public List<DTOMensaje> GetAll()
        {
            using (var context = new DesignProDB())
            {
                var repository = new MensajeRepository(context);
                var U_repository = new UsuarioRepository(context);
                var lista = repository.GetAll();

                List<DTOMensaje> resultado = new List<DTOMensaje>();

                foreach (var mensaje in lista)
                {
                    var m = _mapper.MapToObject(mensaje);
                    m.NombreE = U_repository.Get(m.IdUsuarioE).Nombre;
                    m.NombreR = U_repository.Get(m.IdUsuarioR).Nombre;
                    resultado.Add(m);
                }

                return resultado;
            }
        }
    }
}
