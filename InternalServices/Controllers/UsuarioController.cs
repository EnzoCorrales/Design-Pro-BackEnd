using Common.DataTransferObjects;
using Dominio.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Web.Http;
using InternalServices.Models;

namespace InternalServices.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = (login.Password == "123456");
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Correo);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

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

        // localhost:{puerto}/api/usuario/Update
        // Modifica un usuario
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
        public IHttpActionResult Get(int idUsuario)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            var usuario = mantenimiento.Get(idUsuario);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // localhost:{puerto}/api/usuario/GetByCorreo?correoUsuario={correoUsuario}
        // Devuelve un usuario dado el id
        public IHttpActionResult GetByCorreo(string correoUsuario)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            var usuario = mantenimiento.GetByCorreo(correoUsuario);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // localhost:{puerto}/api/usuario/GetAllSeguidores?idUsuario={idUsuario}
        // Devuelve una lista con todos los seguidores del usuario dado el id
        public IEnumerable<DTOUsuario> GetAllSeguidores(int idUsuario)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSeguidores(idUsuario);

        }

        // localhost:{puerto}/api/usuario/GetAllSiguiendo?idUsuario={idUsuario}
        // Devuelve una lista con todos los siguiendo del usuario dado el id
        public IEnumerable<DTOUsuario> GetAllSiguiendo(int idUsuario)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSiguiendo(idUsuario);

        }

        // localhost:{puerto}/api/usuario/GetAll
        // Devuelve una lista con todos los usuarios registrados
        public IEnumerable<DTOUsuario> GetAll()
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAll();

        }
    }
}
