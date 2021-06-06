using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.DataTransferObjects;
using Dominio.General;

namespace InternalServices.Controllers
{
    public class ProyectoController : ApiController
    {

        // localhost:{puerto}/api/proyecto/Create
        // Crea un proyecto
        [HttpPost]
        public IHttpActionResult Create(DTOProyecto proyecto)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                mantenimiento.Create(proyecto);
                response.Success = true;
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
        [HttpPost]
        public IHttpActionResult Remove(int id)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
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

        // localhost:{puerto}/api/proyecto/Remove?correo={correo}
        // Elimina todos los proyecto de cierto usuario dado el correo
        [HttpPost]
        public IHttpActionResult Remove(string correo)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                mantenimiento.Remove(correo);
                response.Success = true;
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
        public IHttpActionResult Get(int id)
        {
            MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
            var proyecto = mantenimiento.Get(id);

            if (proyecto == null)
                return NotFound();

            return Ok(proyecto);
        }

        // localhost:{puerto}/api/proyecto/GetAll
        // Devuelve todos los proyectos que existen en la BD
        public IEnumerable<DTOProyecto> GetAll()
        {
            MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
            return mantenimiento.GetAll();
        }

        // localhost:{puerto}/api/proyecto/GetAll?correo={correo}
        // Devuelve todos los proyectos que tiene un usuario en especifico dado el correo
        public IEnumerable<DTOProyecto> GetAll(string correo)
        {
            MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
            return mantenimiento.GetAll(correo);
        }
    }
}
