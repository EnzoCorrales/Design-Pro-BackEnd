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

    public class UsuarioController : ApiController
    {

        // localhost:{puerto}/api/usuario/register
        // Crea un usuario
        [HttpPost]
        public IHttpActionResult Register(DTOUsuario usuario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                mantenimiento.Create(usuario);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message; // Obtiene el mensaje plano
            }

            if (response.Success)
                return Ok(response.Success);
            else
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.Error));
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
                    var token = TokenManager.GenerateTokenJwt(correo);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }

        // localhost:{puerto}/api/usuario/Update
        // Modifica un usuario
        /// </summary>
        [Authorize]
        [HttpPost]
        public IHttpActionResult Update(DTOUsuario usuario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                mantenimiento.Update(usuario);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }

        // localhost:{puerto}/api/usuario/Remove?idUsuario={idUsuario}
        // Elimina un usuario
        // 
        /// </summary>
        [Authorize]
        [HttpPost]
        public IHttpActionResult Remove(int idUsuario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                mantenimiento.Remove(idUsuario);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            return Ok(response);
        }

        // localhost:{puerto}/api/usuario/Get?idUsuario={idUsuario}
        // Devuelve un usuario dado el id
        /// </summary>
        [Authorize]
        public IHttpActionResult Get(int idUsuario)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            var usuario = mantenimiento.Get(idUsuario);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // localhost:{puerto}/api/usuario/GetAllSeguidores?idUsuario={idUsuario}
        // Devuelve una lista con todos los seguidores del usuario dado el id
        /// </summary>
        [Authorize]
        public IEnumerable<DTOUsuario> GetAllSeguidores(int idUsuario)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSeguidores(idUsuario);

        }

        // localhost:{puerto}/api/usuario/GetAllSiguiendo?idUsuario={idUsuario}
        // Devuelve una lista con todos los siguiendo del usuario dado el id
        /// </summary>
        [Authorize]
        public IEnumerable<DTOUsuario> GetAllSiguiendo(int idUsuario)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSiguiendo(idUsuario);

        }

        // localhost:{puerto}/api/usuario/GetAll
        // Devuelve una lista con todos los usuarios registrados
        /// </summary>
        [Authorize]
        public IEnumerable<DTOUsuario> GetAll()
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAll();

        }
    }
}
