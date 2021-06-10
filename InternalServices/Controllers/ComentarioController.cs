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
    public class ComentarioController : ApiController
    {
        // localhost:{puerto}/api/comentario/Create
        // Crea un comentario
        [HttpPost]
        public IHttpActionResult Create(DTOComentario comentario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoComentario mantenimiento = new MantenimientoComentario();
                mantenimiento.Create(comentario);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }

        // localhost:{puerto}/api/comentario/Remove?id={id}
        // Elimina un comentario dado su id
        [HttpPost]
        public IHttpActionResult Remove(int id)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoComentario mantenimiento = new MantenimientoComentario();
                mantenimiento.Remove(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }

        // localhost:{puerto}/api/comentario/RemoveByUsuario?idUsuario={idUsuario}
        // Elimina todos los comentarios de un usuario en especifico dado el correo
        [HttpPost]
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
        }

        // localhost:{puerto}/api/comentario/RemoveByProyecto?idProyecto={idProyecto}
        // Elimina todos los comentarios de un proyecto dado la id del proyecto
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
        }

        // localhost:{puerto}/api/comentario/Get?id={id}
        // Devuelve un comentario dado el id
        public IHttpActionResult Get(int id)
        {
            MantenimientoComentario mantenimiento = new MantenimientoComentario();
            var proyecto = mantenimiento.Get(id);

            if (proyecto == null)
                return NotFound();

            return Ok(proyecto);
        }

        // localhost:{puerto}/api/comentario/GetAll
        // Devuelve todos los comentarios que existen en la BD
        public IEnumerable<DTOComentario> GetAll()
        {
            MantenimientoComentario mantenimiento = new MantenimientoComentario();
            return mantenimiento.GetAll();
        }

        // localhost:{puerto}/api/comentario/GetAllByProyecto?idProyecto={idProyecto}
        // Devuelve todos los comentarios de un proyecto dada la id del proyecto
        public IEnumerable<DTOComentario> GetAllByProyecto(int idProyecto)
        {
            MantenimientoComentario mantenimiento = new MantenimientoComentario();
            return mantenimiento.GetAllByProyecto(idProyecto);
        }

        // localhost:{puerto}/api/comentario/GetAllByUsuario?idUsuario={idUsuario}
        // Devuelve todos los comentarios de un usuario dado el correo
        public IEnumerable<DTOComentario> GetAllByUsuario(int idUsuario)
        {
            MantenimientoComentario mantenimiento = new MantenimientoComentario();
            return mantenimiento.GetAllByUsuario(idUsuario);
        }
    }
}
