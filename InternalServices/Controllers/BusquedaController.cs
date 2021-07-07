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
    public class BusquedaController : ApiController
    {
        // localhost:{puerto}/api/busqueda/GetAll
        // Devuelve todos los proyectos de la BD, lo que vendria a hacer si hace una busqueda con parametros vacios(?
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
            return Ok(mantenimiento.GetAll());
        }

        // localhost:{puerto}/api/busqueda/Busqueda?busqueda={busqueda}
        // Devuelve una lista con los proyectos que coinciden con el resultado de busqueda
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Busqueda(string busqueda)
        {
            try
            {
                List<DTOProyecto> resultado = new List<DTOProyecto>();
                MantenimientoProyecto mProyecto = new MantenimientoProyecto();

                // Primero agrega en la lista resultado las busquedas que coincidieron por el titulo, este resultado es parcial, es decir, si existe un proyecto con el titulo "Animales exoticos"
                // y el usuario realiza la busqueda "animales", el proyecto "Animales exoticos" le saldra en la busqueda.

                var XTitulo = mProyecto.GetBusquedaXTitulo(busqueda);

                foreach (var proyecto in XTitulo)
                {
                    resultado.Add(proyecto);
                }

                // Segundo agrega en la lista resultado las busquedas que coincidieron por el nombre del autor, este resultado es exacto, es decir, solo devolvera proyectos siempre y cuando el nombre
                // ingresado del autor sea exacto.

                var XAutor = mProyecto.GetBusquedaXAutor(busqueda);

                foreach (var proyecto in XAutor)
                {
                    resultado.Add(proyecto);
                }

                // Tercero agrega en la lista resultado las busquedas que coincidieron por tag, este resultado es exacto de igual manera que la busqueda por nombre de autor anteriormente explicada

                var XTag = mProyecto.GetBusquedaXTag(busqueda);

                foreach (var proyecto in XTag)
                {
                    resultado.Add(proyecto);
                }

                return Ok(resultado);
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));
            }
        }
    }
}
