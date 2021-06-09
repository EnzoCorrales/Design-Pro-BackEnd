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
                response.Error = ex.ToString();
            }

            return Ok(response);
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

        // localhost:{puerto}/api/usuario/Remove?correo={correo}
        // Elimina un usuario
        // No funciona porque hay que eiminar todas las tablas donde tiene foreign key primero antes de eliminar al usuario, hay que implementar mas codigo que antes de eliminarlo
        // elimine sus proyectos si tiene, sus mensajes, sus comentarios, etc.
        [HttpPost]
        public IHttpActionResult Remove(string correo)
        {
            DTOBaseResponse response = new DTOBaseResponse();
            try
            {
                MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
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

        // localhost:{puerto}/api/usuario/Get?correo={correo}
        // Devuelve un usuario dado un correo
        public IHttpActionResult Get(string correo)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            var usuario = mantenimiento.Get(correo);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // localhost:{puerto}/api/usuario/GetAllSeguidores?correo={correo}
        // Devuelve una lista con todos los seguidores del usuario dado el correo
        public IEnumerable<DTOUsuario> GetAllSeguidores(string correo)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSeguidores(correo);

        }

        // localhost:{puerto}/api/usuario/GetAllSiguiendo?correo={correo}
        // Devuelve una lista con todos los siguiendo del usuario dado el correo
        public IEnumerable<DTOUsuario> GetAllSiguiendo(string correo)
        {
            MantenimientoUsuario mantenimiento = new MantenimientoUsuario();
            return mantenimiento.GetAllSiguiendo(correo);

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
