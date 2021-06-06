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

        // localhost:{puerto}/api/mensaje/GetAllByEmisor?correo={correo}
        // Devuelve todos los mensajes enviados por un usuario en especifico dado un correo
        public IEnumerable<DTOMensaje> GetAllByEmisor(string correo)
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            return mantenimiento.GetAllByEmisor(correo);
        }

        // localhost:{puerto}/api/mensaje/GetAllByReceptor?correo={correo}
        // Devuelve todos los mensajes recibidos por un usuario en especifico dado un correo
        public IEnumerable<DTOMensaje> GetAllByReceptor(string correo)
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            return mantenimiento.GetAllByReceptor(correo);
        }

        // localhost:{puerto}/api/mensaje/GetConversacion?usuario1={usuario1}&usuario2={usuario2}
        // Devuelve la conversacion ordenada por fecha entre dos usuarios dado sus correos respectivamente
        public IEnumerable<DTOMensaje> GetConversacion(string usuario1, string usuario2)
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            return mantenimiento.GetConversacion(usuario1,usuario2);
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
