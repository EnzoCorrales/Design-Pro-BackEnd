using Common.DataTransferObjects;
using Dominio.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternalServices.Controllers
{
    public class MensajeController : ApiController
    {
        // localhost:{puerto}/api/mensaje/Create
        // Crea un mensaje
        [HttpPost]
        public IHttpActionResult Create(DTOMensaje mensaje)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
                mantenimiento.Create(mensaje);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }

        // localhost:{puerto}/api/mensaje/GetAll
        // Devuelve todos los mensajes que existen en la BD
        public IEnumerable<DTOMensaje> GetAll()
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            return mantenimiento.GetAll();
        }

        // localhost:{puerto}/api/mensaje/GetAllByEmisor?idUsuario={idUsuario}
        // Devuelve todos los mensajes enviados por un usuario en especifico dado un id
        public IEnumerable<DTOMensaje> GetAllByEmisor(int idUsuario)
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            return mantenimiento.GetAllByEmisor(idUsuario);
        }

        // localhost:{puerto}/api/mensaje/GetAllByReceptor?idUsuario={idUsuario}
        // Devuelve todos los mensajes recibidos por un usuario en especifico dado un id
        public IEnumerable<DTOMensaje> GetAllByReceptor(int idUsuario)
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            return mantenimiento.GetAllByReceptor(idUsuario);
        }

        // localhost:{puerto}/api/mensaje/GetConversacion?idUsuario1={idUsuario1}&idUsuario2={idUsuario2}
        // Devuelve la conversacion ordenada por fecha entre dos usuarios dado sus id respectivamente
        public IEnumerable<DTOMensaje> GetConversacion(int idUsuario1, int idUsuario2)
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            return mantenimiento.GetConversacion(idUsuario1, idUsuario2);
        }

        // localhost:{puerto}/api/mensaje/Get?id={id}
        // Devuelve un mensaje dado su id
        public IHttpActionResult Get(int id)
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            var mensaje = mantenimiento.Get(id);

            if (mensaje == null)
                return NotFound();

            return Ok(mensaje);
        }
    }
}
