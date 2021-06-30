using Common.DataTransferObjects;
using Common.Exceptions;
using Dominio.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InternalServices.Filters;

namespace InternalServices.Controllers
{

    public class UsuarioController : ApiController
    {

        // localhost:{puerto}/api/usuario/register
        // Crea un usuario
        [ValidateUsuarioModel]
        [HttpPost]
        public IHttpActionResult Register(DTOUsuario usuario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                mantenimiento.Create(usuario);
                response.Usuario = mantenimiento.Get(usuario.Correo);
                response.Token = TokenManager.GenerateTokenJwt(usuario.Correo);
                return Ok(response);
            }
            catch (ValidateException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la opración!"));
            }
        }

        // localhost:{puerto}/api/usuario/Login
        // login del usuario
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Login(string correo, string password)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                if (mantenimiento.ValidarUsuario(correo, password))
                {
                    response.Usuario = mantenimiento.Get(correo);
                    response.Token = TokenManager.GenerateTokenJwt(correo);
                    return Ok(response);
                }
                else
                {
                    throw new ValidateException("Las credenciales no son correctas");
                }
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

        // localhost:{puerto}/api/usuario/Update
        // Modifica un usuario
        /// </summary>
        [Authorize]
        [HttpPut]
        public IHttpActionResult Update(DTOUsuario usuario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                mantenimiento.Update(usuario);
                response.Usuario = mantenimiento.Get(usuario.Correo);
                return Ok(response);
            }
            catch (ValidateException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la opración!"));
            }
        }

        // localhost:{puerto}/api/usuario/Remove?id={idUsuario}
        // Elimina un usuario
        // 
        /// </summary>
        [Authorize]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
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

        // localhost:{puerto}/api/usuario/Get?id={idUsuario}
        // Devuelve un usuario dado el id
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            var usuario = mantenimiento.Get(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // localhost:{puerto}/api/usuario/GetAllSeguidores?id={idUsuario}
        // Devuelve una lista con todos los seguidores del usuario dado el id
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<DTOUsuario> GetAllSeguidores(int id)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSeguidores(id);

        }

        // localhost:{puerto}/api/usuario/GetAllSiguiendo?id={idUsuario}
        // Devuelve una lista con todos los siguiendo del usuario dado el id
        /// </summary>
        [Authorize]
        [HttpGet]
        public IEnumerable<DTOUsuario> GetAllSiguiendo(int id)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSiguiendo(id);

        }

        // localhost:{puerto}/api/usuario/GetAll
        // Devuelve una lista con todos los usuarios registrados
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<DTOUsuario> GetAll()
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAll();

        }
    }
}
