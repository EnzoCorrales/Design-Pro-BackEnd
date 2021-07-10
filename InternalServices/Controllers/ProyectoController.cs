using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
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
        [ValidateProyectoModel]
        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult Create(DTOProyecto proyecto)
        {
            try
            {
                DTOBaseResponse response = new DTOBaseResponse();
                MantenimientoUsuario U_mantenimiento = new MantenimientoUsuario();

                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, proyecto.IdAutor)) // se fija que el proyecto que se vaya a crear sea del usuario loggeado
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (!U_mantenimiento.ExisteUsuario(proyecto.IdAutor))
                    throw new ArgumentException("Usuario no existente");

                var s = new string[] { "dd/MM/yyyy", "dd-MM-yyyy" , "yyyy-MM-dd"};

                if (!DateTime.TryParseExact(proyecto.FechaPub, s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime d))
                    throw new ArgumentException("Fecha incorrecta");

                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                mantenimiento.Create(proyecto);
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

        // localhost:{puerto}/api/proyecto/Remove?id={id}
        // Elimina un poryecto por id de proyecto
        [AuthenticateUser]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            try
            {
                DTOBaseResponse response = new DTOBaseResponse();
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                if (!mantenimiento.ExisteProyecto(id))
                    throw new ArgumentException("Proyecto no existente");

                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, mantenimiento.Get(id).IdAutor)) // se fija que el proyecto que esta intentando eliminar sea del usuario que esta loggeado
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

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

        // localhost:{puerto}/api/proyecto/Get?id={id}
        // Devuelve un proyecto dado el id
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();

                if (!mantenimiento.ExisteProyecto(id))
                    throw new ArgumentException("Proyecto no existente");

                return Ok(mantenimiento.Get(id));
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

        // localhost:{puerto}/api/proyecto/GetAll?idUsuario={idUsuario}
        // Devuelve todos los proyectos que tiene un usuario en especifico dado el id
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAllFromUser(int idUsuario)
        {
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                MantenimientoUsuario U_mantenimiento = new MantenimientoUsuario();

                if (!U_mantenimiento.ExisteUsuario(idUsuario))
                    throw new ArgumentException("Usuario no existente");
                
                return Ok(mantenimiento.GetAll(idUsuario));
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

        // localhost:{puerto}/api/busqueda/GetAll
        // Devuelve todos los proyectos de la BD, lo que vendria a hacer si hace una busqueda con parametros vacios(?
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                return Ok(mantenimiento.GetAll());
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));
            }
        }

        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult ValorarProyecto(DTOValoracion valoracion)// Endpoint para dar like a un proyecto
        {
            try
            {
                MantenimientoProyecto p_mantenimiento = new MantenimientoProyecto();
                MantenimientoValoracion mantenimiento = new MantenimientoValoracion();

                var token = Request.Headers.Authorization.Parameter;

                if (token == null || token == "" || token.Equals(""))
                    throw new ArgumentException("Debe iniciar sesion para dar like a un proyecto");

                if (!TokenManager.VerificarXId(token, valoracion.IdUsuario)) 
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (!p_mantenimiento.ExisteProyecto(valoracion.IdProyecto))
                    throw new ArgumentException("Proyecto no existente");

                if (mantenimiento.LoValoro(valoracion.IdUsuario, valoracion.IdProyecto))
                    throw new ArgumentException("Este usuario ya dio like a este proyecto");

                mantenimiento.ValorarProyecto(valoracion);

                return Ok(true);
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

        [AuthenticateUser]
        [HttpDelete]
        public IHttpActionResult QuitarValoracion(DTOValoracion valoracion)// enpoint para sacarle el like a un proyecto
        {
            try
            {
                MantenimientoProyecto p_mantenimiento = new MantenimientoProyecto();
                MantenimientoValoracion mantenimiento = new MantenimientoValoracion();
                var token = Request.Headers.Authorization.Parameter;

                if (token == null || token == "" || token.Equals(""))
                    throw new ArgumentException("Debe iniciar sesion para quitar like a un proyecto");

                if (!TokenManager.VerificarXId(token, valoracion.IdUsuario))
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (!p_mantenimiento.ExisteProyecto(valoracion.IdProyecto))
                    throw new ArgumentException("Proyecto no existente");

                if (!mantenimiento.LoValoro(valoracion.IdUsuario, valoracion.IdProyecto))
                    throw new ArgumentException("Este usuario no dio like a este proyecto");

                mantenimiento.QuitarValoracion(valoracion);

                return Ok(true);
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

        [AllowAnonymous]
        [HttpPut]
        public IHttpActionResult VisitarProyecto(int idProyecto)// agrega un +1 al contador de visitas de un proyecto
        {
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();

                if(!mantenimiento.ExisteProyecto(idProyecto))
                    throw new ArgumentException("Proyecto no existente");

                mantenimiento.VisitarProyecto(idProyecto);

                return Ok(true);
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


        [AuthenticateUser]
        [HttpGet]
        public IHttpActionResult GetProyectosValorados(int idUsuario) // devuelve una lista con los proyectos valorados dado el id del usuario
        {
            try
            {
                MantenimientoProyecto mantenimiento = new MantenimientoProyecto();
                MantenimientoUsuario U_mantenimiento = new MantenimientoUsuario();

                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, idUsuario))
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (!U_mantenimiento.ExisteUsuario(idUsuario))
                    throw new ArgumentException("Usuario no existente");

                return Ok(mantenimiento.GetProyectosValorados(idUsuario));
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
    }
}
