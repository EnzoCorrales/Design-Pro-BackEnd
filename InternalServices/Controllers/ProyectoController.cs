using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.DataTransferObjects;
using Dominio.General;
using InternalServices.Filters;

namespace InternalServices.Controllers
{
    public class ProyectoController : ApiController
    {

        // localhost:{puerto}/api/proyecto/Create
        // Crea un proyecto
        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult Create(DTOProyecto proyecto)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                if (TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, proyecto.IdAutor)) // se fija que el proyecto que se vaya a crear sea del usuario loggeado
                {
                    MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                    mantenimiento.Create(proyecto);
                    response.Success = true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }

        // localhost:{puerto}/api/proyecto/Remove?id={id}
        // Elimina un poryecto por id de proyecto
        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult Remove(int id)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                if (TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, mantenimiento.GetIdAutor(id))) // se fija que el proyecto que esta intentando eliminar sea del usuario que esta loggeado
                {
                    mantenimiento.Remove(id);
                    response.Success = true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }

        // localhost:{puerto}/api/proyecto/Get?id={id}
        // Devuelve un proyecto dado el id
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
            var proyecto = mantenimiento.Get(id);

            if (proyecto == null)
                return NotFound();

            return Ok(proyecto);
        }

        // localhost:{puerto}/api/proyecto/GetAll?idUsuario={idUsuario}
        // Devuelve todos los proyectos que tiene un usuario en especifico dado el id
        [AllowAnonymous]
        public IEnumerable<DTOProyecto> GetAll(int idUsuario)
        {
            MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
            return mantenimiento.GetAll(idUsuario);
        }
    }
}
