using Common.DataTransferObjects;
using Dominio.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Exceptions;
using InternalServices.Filters;

namespace InternalServices.Controllers
{
    public class MensajeController : ApiController
    {
        // localhost:{puerto}/api/mensaje/Create
        // Crea un mensaje
        [ValidateMensajeModel]
        [HttpPost]
        public IHttpActionResult Create(DTOMensaje mensaje)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
                mantenimiento.Create(mensaje);
                return Ok(true);
            }
            catch (ValidateException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e));
            }
        }

        // localhost:{puerto}/api/mensaje/GetAllByReceptor?id={idUsuario}
        // Devuelve todos los mensajes recibidos por un usuario en especifico dado un id
        public IEnumerable<DTOMensaje> GetAllByReceptor(int id)
        {
            MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
            return mantenimiento.GetAllByReceptor(id);
        }

        // localhost:{puerto}/api/mensaje/Visto
        // Actualiza el mensaje a visto
        [HttpPut]
        public IHttpActionResult Visto(DTOMensaje mensaje)
        {
            try
            {
                MantenimientoMensaje mantenimiento = new MantenimientoMensaje();
                mantenimiento.Update(mensaje);
                return Ok(true);
            }
             catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la opración!"));
            }
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
