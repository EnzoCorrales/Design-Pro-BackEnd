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
                var token = TokenManager.GenerateTokenJwt(usuario.Correo);
                response.Usuario = mantenimiento.Get(usuario.Correo);
                response.Success = true;
                response.Token = token;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message; // Obtiene el mensaje plano
            }

            if (response.Success)
                return Ok(response);
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
                    response.Usuario = mantenimiento.Get(correo);
                    response.Success = true;
                    response.Token = token;
                }
                else
                {
                    response.Success = false;
                    response.Error = "Las credenciales no son correctas";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
            }

            if (response.Success)
                return Ok(response);
            else
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.Error));

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
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
            }

            if (response.Success)
                return Ok(response);
            else
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.Error));
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
