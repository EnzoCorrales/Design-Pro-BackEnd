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
            try
            {
                DTOBaseResponse response = new DTOBaseResponse();
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();

                if (usuario.Password == null || usuario.Password.Equals("") || usuario.Password == "")
                    throw new ArgumentException("Contraseña vacía");

                var s = new string[] { "dd/MM/yyyy", "dd-MM-yyyy" , "yyyy-MM-dd"};

                if (!DateTime.TryParseExact(usuario.FNac, s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime d))
                    throw new ArgumentException("Fecha incorrecta");

                if (mantenimiento.ExisteUsuario(usuario.Correo))
                    throw new ArgumentException("Correo en uso");

                mantenimiento.Create(usuario);
                response.Usuario = mantenimiento.Get(usuario.Correo);
                response.Token = TokenManager.GenerateTokenJwt(usuario.Correo, mantenimiento.Get(usuario.Correo).Id);
                response.Success = true;
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
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
            try
            {
                DTOBaseResponse response = new DTOBaseResponse();
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();

                if (!mantenimiento.ExisteUsuario(correo))
                    throw new ArgumentException("Usuario no existente");

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
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la opración!")); // normalmente son problemas con la base de datos
            }
        }

        // localhost:{puerto}/api/usuario/Update
        // Modifica un usuario
        [ValidateUsuarioModel]
        [AuthenticateUser]
        [HttpPut]
        public IHttpActionResult Update(DTOUsuario usuario)
        {
            try
            {
                DTOBaseResponse response = new DTOBaseResponse();

                if (! (TokenManager.VerificarXCorreo(Request.Headers.Authorization.Parameter,usuario.Correo) && TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, usuario.Id))) // se fija que el usuario que esta intentando modificar sea el que esta loggeado
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                var s = new string[] { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd" };

                if (!DateTime.TryParseExact(usuario.FNac, s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime d))
                    throw new ArgumentException("Fecha incorrecta");

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
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!")); // normalmente son problemas con la base de datos
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
                
                if (!mantenimiento.ExisteUsuario(id)) // se fija si existe el usuario que esta intentando eliminar
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
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));// normalmente son problemas con la base de datos
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

                if (!mantenimiento.ExisteUsuario(id))
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
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));// normalmente son problemas con la base de datos
            }
        }

        // localhost:{puerto}/api/usuario/Seguir
        // Comienza a seguir a otro usuario
        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult Seguir(DTOSeguimiento seguir)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoSeguimiento mantenimiento = new MantenimientoSeguimiento();
                MantenimientoUsuario mantenimiento_U = new MantenimientoUsuario();

                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, seguir.IdSeguidor))
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (mantenimiento.LoSigue(seguir))
                    throw new ArgumentException("Este usuario ya sigue a " + mantenimiento_U.Get(seguir.IdUsuario).Nombre);

                mantenimiento.Seguir(seguir);
                response.Usuario = mantenimiento_U.Get(seguir.IdSeguidor);

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
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));// normalmente son problemas con la base de datos
            }
        }

        // localhost:{puerto}/api/usuario/DejarDeSeguir
        // Deja de seguir al usuario
        [AuthenticateUser]
        [HttpPost]
        public IHttpActionResult DejarDeSeguir(DTOSeguimiento seguir)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoSeguimiento mantenimiento = new MantenimientoSeguimiento();
                MantenimientoUsuario mantenimiento_U = new MantenimientoUsuario();

                if (!TokenManager.VerificarXId(Request.Headers.Authorization.Parameter, seguir.IdSeguidor))
                    throw new UnauthorizedAccessException("Se ha denegado la autorización para esta solicitud");

                if (!mantenimiento.LoSigue(seguir))
                    throw new ArgumentException("Este usuario no sigue a " + mantenimiento_U.Get(seguir.IdUsuario).Nombre);

                mantenimiento.DejarDeSeguir(seguir);
                response.Usuario = mantenimiento_U.Get(seguir.IdSeguidor);

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
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));// normalmente son problemas con la base de datos
            }
        }

        // localhost:{puerto}/api/usuario/GetAllSeguidores?id={idUsuario}
        // Devuelve una lista con todos los seguidores del usuario dado el id
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAllSeguidores(int id)
        {
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                if (mantenimiento.Get(id) == null)
                    throw new ArgumentException("Usuario no existente");

                return Ok(mantenimiento.GetAllSeguidores(id));
            }
            catch (ArgumentException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));// normalmente son problemas con la base de datos
            }
        }

        // localhost:{puerto}/api/usuario/GetAllSiguiendo?id={idUsuario}
        // Devuelve una lista con todos los siguiendo del usuario dado el id
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAllSiguiendo(int id)
        {
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();

                if(mantenimiento.Get(id) == null)
                    throw new ArgumentException("Usuario no existente");

                return Ok(mantenimiento.GetAllSiguiendo(id));
            }
            catch (ArgumentException ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));// normalmente son problemas con la base de datos
            }
        }

        // localhost:{puerto}/api/usuario/GetAll
        // Devuelve una lista con todos los usuarios registrados
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
                return Ok(mantenimiento.GetAll());
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Fallo al procesar la operación!"));// normalmente son problemas con la base de datos
            }
        }
    }
}
