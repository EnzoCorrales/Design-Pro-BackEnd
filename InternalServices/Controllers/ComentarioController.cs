using Common.DataTransferObjects;
using Dominio.General;
using InternalServices.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternalServices.Controllers
{
    public class ComentarioController : ApiController
    {
        // localhost:{puerto}/api/comentario/Create
        // Crea un comentario
        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult Create(DTOComentario comentario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            MantenimientoProyecto P_mantenimiento = new MantenimientoProyecto();
            try
            {
                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, comentario.IdUsuario))
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if(!P_mantenimiento.ExisteProyecto(comentario.IdProyecto))
                    throw new ArgumentException("Proyecto no existente");

                MantenimientoComentario mantenimiento = new MantenimientoComentario();
                mantenimiento.Create(comentario);
                response.Success = true;
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));
            }
        }

        // localhost:{puerto}/api/comentario/Remove?id={id}
        // Elimina un comentario dado su id
        [AuthenticateUser]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            MantenimientoComentario mantenimiento = new MantenimientoComentario();
            try
            {
                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, mantenimiento.Get(id).IdUsuario))
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (!mantenimiento.ExisteComentario(id))
                    throw new ArgumentException("Comentario no existente");

                mantenimiento.Remove(id);
                response.Success = true;
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));
            }
        }

        // localhost:{puerto}/api/comentario/RemoveByUsuario?idUsuario={idUsuario}
        // Elimina todos los comentarios de un usuario en especifico dado el correo
        /*[AuthenticateUser]
        [HttpDelete]
        public IHttpActionResult RemoveByUsuario(int idUsuario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoComentario mantenimiento = new MantenimientoComentario();
                mantenimiento.RemoveByUsuario(idUsuario);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }*/

        // localhost:{puerto}/api/comentario/RemoveByProyecto?idProyecto={idProyecto}
        // Elimina todos los comentarios de un proyecto dado la id del proyecto
        /*[AuthenticateUser]
        [HttpDelete]
        public IHttpActionResult RemoveByProyecto(int idProyecto)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoComentario mantenimiento = new MantenimientoComentario();
                mantenimiento.RemoveByProyecto(idProyecto);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }*/

        // localhost:{puerto}/api/comentario/Get?id={id}
        // Devuelve un comentario dado el id
        /*[HttpGet]
        public IHttpActionResult Get(int id)
        {
            MantenimientoComentario mantenimiento = new MantenimientoComentario();
            var proyecto = mantenimiento.Get(id);

            if (proyecto == null)
                return NotFound();

            return Ok(proyecto);
        }*/

        // localhost:{puerto}/api/comentario/GetAllByProyecto?idProyecto={idProyecto}
        // Devuelve todos los comentarios de un proyecto dada la id del proyecto
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAllByProyecto(int idProyecto)
        {
            try
            {
                MantenimientoProyecto p_mantenimiento = new MantenimientoProyecto();
                if(!p_mantenimiento.ExisteProyecto(idProyecto))
                    throw new ArgumentException("Proyecto no existente");

                MantenimientoComentario mantenimiento = new MantenimientoComentario();
                return Ok(mantenimiento.GetAllByProyecto(idProyecto));
            }
            catch (ArgumentException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));
            } 
        }

        // localhost:{puerto}/api/comentario/GetAllByUsuario?idUsuario={idUsuario}
        // Devuelve todos los comentarios de un usuario dado el correo
        /*public IHttpActionResult GetAllByUsuario(int idUsuario)
        {
            MantenimientoComentario mantenimiento = new MantenimientoComentario();
            return Ok(mantenimiento.GetAllByUsuario(idUsuario));
        }*/
    }
}
