using Common.DataTransferObjects;
using Dominio.General;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InternalServices.Filters;
using System.Globalization;


namespace InternalServices.Controllers
{
    public class UsuarioController : ApiController
    {
        // localhost:{puerto}/api/usuario/register
        // Crea un usuario
        [ValidateUsuarioModel]
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Register([FromBody] DTOUsuario usuario)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            try
            {
                if(usuario.Password == null || usuario.Password.Equals("") || usuario.Password == "")
                    throw new ArgumentException("Contraseña vacía");

                if (!DateTime.TryParseExact(usuario.FNac, "dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime d))
                    throw new ArgumentException("Debe ingresar la fecha con formato dd-MM-yyyy");

                mantenimiento.Create(usuario);
                response.Usuario = mantenimiento.Get(usuario.Correo);
                response.Token = TokenManager.GenerateTokenJwt(usuario.Correo, mantenimiento.Get(usuario.Correo).Id);
                response.Success = true;
            }
            catch (ArgumentException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));
            }

            return Ok(response);
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

                if (!mantenimiento.ValidarUsuario(correo, password))
                    throw new ArgumentException("Credenciales no válidas");

                response.Success = true;
                response.Usuario = mantenimiento.Get(correo);
                response.Token = TokenManager.GenerateTokenJwt(correo, mantenimiento.Get(correo).Id);

                return Ok(response);
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

        // localhost:{puerto}/api/usuario/Update
        // Modifica un usuario
        [ValidateUsuarioModel]
        [AuthenticateUser]
        [HttpPut]
        public IHttpActionResult Update(DTOUsuario usuario)

        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                if (! (TokenManager.VerificarXCorreo(Request.Headers.Authorization.Parameter,usuario.Correo) && TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, usuario.Id))) // se fija que el usuario que esta intentando modificar sea el que esta loggeado
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (!DateTime.TryParseExact(usuario.FNac, "dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime d))
                    throw new ArgumentException("Debe ingresar la fecha con formato dd-MM-yyyy");

                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                mantenimiento.Update(usuario);
                response.Usuario = mantenimiento.Get(usuario.Correo);
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

        // localhost:{puerto}/api/usuario/Remove?id={idUsuario}
        // Elimina un usuario
        [AuthenticateUser]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();

                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, id)) // se fija que el usuario que esta intentando eliminar sea el que esta loggeado
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");
                
                if (mantenimiento.Get(id) == null) // se fija si existe el usuario que esta intentando eliminar
                    throw new ArgumentException("Usuario no existe");

                mantenimiento.Remove(id);
                response.Success = true;

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));
            } 
        }

        // localhost:{puerto}/api/usuario/Get?id={idUsuario}
        // Devuelve un usuario dado el id
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                if (mantenimiento.Get(id) == null)
                    throw new ArgumentException("Usuario no existe");

                var usuario = mantenimiento.Get(id);
                usuario.Password = null;

                return Ok(usuario);
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

        // localhost:{puerto}/api/usuario/Seguir
        // Comienza a seguir a otro usuario
        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult Seguir(DTOSeguimiento seguir)
        {
            MantenimientoSeguimiento mantenimiento = new MantenimientoSeguimiento();
            MantenimientoUsuario mantenimiento_U = new MantenimientoUsuario();
            try
            {
                if (seguir.IdSeguidor.ToString() == "" || seguir.IdUsuario.ToString() == "" || mantenimiento_U.Get(seguir.IdSeguidor) == null || mantenimiento_U.Get(seguir.IdUsuario) == null)
                    throw new ArgumentException("Usuario no existente");

                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, seguir.IdSeguidor))
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (mantenimiento.LoSigue(seguir))
                    throw new ArgumentException("Este usuario ya sigue a " + mantenimiento_U.Get(seguir.IdUsuario).Nombre);

                mantenimiento.Seguir(seguir);

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

        // localhost:{puerto}/api/usuario/DejarDeSeguir
        // Deja de seguir al usuario
        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult DejarDeSeguir(DTOSeguimiento seguir)
        {
            MantenimientoSeguimiento mantenimiento = new MantenimientoSeguimiento();
            MantenimientoUsuario mantenimiento_U = new MantenimientoUsuario();
            try
            {
                if (seguir.IdSeguidor.ToString() == "" || seguir.IdUsuario.ToString() == "" || mantenimiento_U.Get(seguir.IdSeguidor) == null || mantenimiento_U.Get(seguir.IdUsuario) == null)
                    throw new ArgumentNullException("Usuario no existente");

                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, seguir.IdSeguidor))
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (!mantenimiento.LoSigue(seguir))
                    throw new ArgumentException("Este usuario no sigue a " + mantenimiento_U.Get(seguir.IdUsuario).Nombre);

                mantenimiento.DejarDeSeguir(seguir);

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

        // localhost:{puerto}/api/usuario/GetAllSeguidores?id={idUsuario}
        // Devuelve una lista con todos los seguidores del usuario dado el id
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<DTOUsuario> GetAllSeguidores(int id)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSeguidores(id);
        }

        // localhost:{puerto}/api/usuario/GetAllSiguiendo?id={idUsuario}
        // Devuelve una lista con todos los siguiendo del usuario dado el id
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<DTOUsuario> GetAllSiguiendo(int id)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSiguiendo(id);
        }

        // localhost:{puerto}/api/usuario/GetAll
        // Devuelve una lista con todos los usuarios registrados
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<DTOUsuario> GetAll()
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAll();
        }
    }
}
