using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Database;
using Common.DataTransferObjects;
using System.Globalization;

/// <summary>
// una clase que mappea los DTO a Entidades y viceversa; también mappea colecciones de Entidades a colecciones de DTO y viceversa
/// </summary>

namespace Dominio.Mappers
{
    public class UsuarioMapper
    {
        private ComentarioMapper _comentario = new ComentarioMapper();
        private MensajeMapper _mensaje = new MensajeMapper();
        private ProyectoMapper _proyecto = new ProyectoMapper();
        private SeguimientoMapper _seguimiento = new SeguimientoMapper();
        private ValoracionMapper _valoracion = new ValoracionMapper();
        public DTOUsuario MapToObject(Usuario usuario)
        {
            if (usuario == null)
                return null;



            return new DTOUsuario()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                FNac = usuario.FNac.ToShortDateString(),
                Pais = usuario.Pais,
                Ciudad = usuario.Ciudad,
                Profesion = usuario.Profesion,
                Empresa = usuario.Empresa,
                ImgPerfil = usuario.ImgPerfil,
                UrlWeb = usuario.UrlWeb,
                Password = usuario.Password,
                Descripcion = usuario.Descripcion,
                Comentarios = _comentario.MapToCollectionObject(usuario.Comentario),
                MensajesE = _mensaje.MapToCollectionObject(usuario.Mensaje),
                MensajesR = _mensaje.MapToCollectionObject(usuario.Mensaje1),
                Proyectos = _proyecto.MapToCollectionObject(usuario.Proyecto),
                Seguidores = _seguimiento.MapToCollectionObject(usuario.Seguimiento1),
                Siguiendo = _seguimiento.MapToCollectionObject(usuario.Seguimiento),
                PValorados = _valoracion.MapToCollectionObject(usuario.Valoracion),
            };
        }

        public Usuario MapToEntity(DTOUsuario usuario)
        {
            if (usuario == null)
                return null;

            return new Usuario()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                FNac = this.ParseToDateType(usuario.FNac),
                Pais = usuario.Pais,
                Ciudad = usuario.Ciudad,
                Profesion = usuario.Profesion,
                Empresa = usuario.Empresa,
                ImgPerfil = usuario.ImgPerfil,
                UrlWeb = usuario.UrlWeb,
                Password = usuario.Password,
                Descripcion = usuario.Descripcion,
                Comentario = _comentario.MapToCollectionEntity(usuario.Comentarios),
                Mensaje = _mensaje.MapToCollectionEntity(usuario.MensajesE),
                Mensaje1 = _mensaje.MapToCollectionEntity(usuario.MensajesR),
                Proyecto = _proyecto.MapToCollectionEntity(usuario.Proyectos),
                Seguimiento1 = _seguimiento.MapToCollectionEntity(usuario.Seguidores),
                Seguimiento = _seguimiento.MapToCollectionEntity(usuario.Siguiendo),
                Valoracion = _valoracion.MapToCollectionEntity(usuario.PValorados),
            };
        }

        public System.DateTime ParseToDateType(string date)
        {
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime d))
                return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (DateTime.TryParseExact(date, "dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime di))
                return DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            if (DateTime.TryParseExact(date, "yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime die))
                return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return DateTime.Now;
        }
        
        public List<Usuario> MapToCollectionEntity(ICollection<DTOUsuario> usuarios)
        {
            if (usuarios == null)
                return null;

            var usuario = new List<Usuario>();
            foreach (var usu in usuarios)
            {
                var u = new Usuario()
                {
                    Id = usu.Id,
                    Nombre = usu.Nombre,
                    Apellido = usu.Apellido,
                    Correo = usu.Correo,
                    FNac = this.ParseToDateType(usu.FNac),
                    Pais = usu.Pais,
                    Profesion = usu.Profesion,
                    Empresa = usu.Empresa,
                    ImgPerfil = usu.ImgPerfil,
                    UrlWeb = usu.UrlWeb,
                    Password = usu.Password,
                    Comentario = _comentario.MapToCollectionEntity(usu.Comentarios),
                    Mensaje = _mensaje.MapToCollectionEntity(usu.MensajesE),
                    Mensaje1 = _mensaje.MapToCollectionEntity(usu.MensajesR),
                    Proyecto = _proyecto.MapToCollectionEntity(usu.Proyectos),
                    Seguimiento1 = _seguimiento.MapToCollectionEntity(usu.Seguidores),
                    Seguimiento = _seguimiento.MapToCollectionEntity(usu.Siguiendo),
                    Valoracion = _valoracion.MapToCollectionEntity(usu.PValorados),
                };
                usuario.Add(u);
            }
            return usuario;
        }
        
        public List<DTOUsuario> MapToCollectionObject(ICollection<Usuario> usuarios)
        {
            if (usuarios == null)
                return null;

            var usuario = new List<DTOUsuario>();
            foreach (var usu in usuarios)
            {
                var u = new DTOUsuario()
                {
                    Id = usu.Id,
                    Nombre = usu.Nombre,
                    Apellido = usu.Apellido,
                    Correo = usu.Correo,
                    FNac = usu.FNac.ToShortDateString(),
                    Pais = usu.Pais,
                    Profesion = usu.Profesion,
                    Empresa = usu.Empresa,
                    ImgPerfil = usu.ImgPerfil,
                    UrlWeb = usu.UrlWeb,
                    Password = usu.Password,
                    Comentarios = _comentario.MapToCollectionObject(usu.Comentario),
                    MensajesE = _mensaje.MapToCollectionObject(usu.Mensaje),
                    MensajesR = _mensaje.MapToCollectionObject(usu.Mensaje1),
                    Proyectos = _proyecto.MapToCollectionObject(usu.Proyecto),
                    Seguidores = _seguimiento.MapToCollectionObject(usu.Seguimiento1),
                    Siguiendo = _seguimiento.MapToCollectionObject(usu.Seguimiento),
                    PValorados = _valoracion.MapToCollectionObject(usu.Valoracion),
                };
                usuario.Add(u);
            }
            return usuario;
        }
    }
}
