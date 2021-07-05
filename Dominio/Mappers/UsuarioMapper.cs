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
                Profesion = usuario.Profesion,
                Empresa = usuario.Empresa,
                ImgPerfil = usuario.ImgPerfil,
                UrlWeb = usuario.UrlWeb,
                Password = usuario.Password,
                Ciudad = usuario.Ciudad,
                Comentarios = ComentarioMapper.MapToCollectionObject(usuario.Comentario),
                MensajesE = MensajeMapper.MapToCollectionObject(usuario.Mensaje),
                MensajesR = MensajeMapper.MapToCollectionObject(usuario.Mensaje1),
                Proyectos = ProyectoMapper.MapToCollectionObject(usuario.Proyecto),
                Seguidores = SeguimientoMapper.MapToCollectionObject(usuario.Seguimiento1),
                Siguiendo = SeguimientoMapper.MapToCollectionObject(usuario.Seguimiento),
                PValorados = ValoracionMapper.MapToCollectionObject(usuario.Valoracion),
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
                Profesion = usuario.Profesion,
                Empresa = usuario.Empresa,
                ImgPerfil = usuario.ImgPerfil,
                UrlWeb = usuario.UrlWeb,
                Password = usuario.Password,
                Ciudad = usuario.Ciudad,
                Comentario = ComentarioMapper.MapToCollectionEntity(usuario.Comentarios),
                Mensaje = MensajeMapper.MapToCollectionEntity(usuario.MensajesE),
                Mensaje1 = MensajeMapper.MapToCollectionEntity(usuario.MensajesR),
                Proyecto = ProyectoMapper.MapToCollectionEntity(usuario.Proyectos),
                Seguimiento1 = SeguimientoMapper.MapToCollectionEntity(usuario.Seguidores),
                Seguimiento = SeguimientoMapper.MapToCollectionEntity(usuario.Siguiendo),
                Valoracion = ValoracionMapper.MapToCollectionEntity(usuario.PValorados),
            };
        }

        public System.DateTime ParseToDateType(string date)
        {
            string inputFormat = "dd-MM-yyyy";
            return DateTime.ParseExact(date, inputFormat, CultureInfo.InvariantCulture);
        }
        /*
        public static HashSet<Usuario> MapToCollectionEntity(ICollection<DTOUsuario> usuarios)
        {
            if (usuarios == null)
                return null;

            var usuario = new HashSet<Usuario>();
            foreach (var usu in usuarios)
            {
                var u = new Usuario()
                {
                    Id = usu.Id,
                    Nombre = usu.Nombre,
                    Apellido = usu.Apellido,
                    Correo = usu.Correo,
                    FNac = usu.FNac,
                    Pais = usu.Pais,
                    Profesion = usu.Profesion,
                    Empresa = usu.Empresa,
                    ImgPerfil = usu.ImgPerfil,
                    UrlWeb = usu.UrlWeb,
                    Password = usu.Password,
                    Ciudad = usu.Ciudad,
                    Comentario = ComentarioMapper.MapToCollectionEntity(usu.Comentarios),
                    Mensaje = MensajeMapper.MapToCollectionEntity(usu.MensajesE),
                    Mensaje1 = MensajeMapper.MapToCollectionEntity(usu.MensajesR),
                    Proyecto = ProyectoMapper.MapToCollectionEntity(usu.Proyectos),
                    Seguimiento1 = SeguimientoMapper.MapToCollectionEntity(usu.Seguidores),
                    Seguimiento = SeguimientoMapper.MapToCollectionEntity(usu.Siguiendo),
                    Valoracion = ValoracionMapper.MapToCollectionEntity(usu.PValorados),
                };
                usuario.Add(u);
            }
            return usuario;
        }
        
        public static HashSet<DTOUsuario> MapToCollectionObject(ICollection<Usuario> usuarios)
        {
            if (usuarios == null)
                return null;

            var usuario = new HashSet<DTOUsuario>();
            foreach (var usu in usuarios)
            {
                var u = new DTOUsuario()
                {
                    Id = usu.Id,
                    Nombre = usu.Nombre,
                    Apellido = usu.Apellido,
                    Correo = usu.Correo,
                    FNac = usu.FNac,
                    Pais = usu.Pais,
                    Profesion = usu.Profesion,
                    Empresa = usu.Empresa,
                    ImgPerfil = usu.ImgPerfil,
                    UrlWeb = usu.UrlWeb,
                    Password = usu.Password,
                    Ciudad = usu.Ciudad,
                    Comentarios = ComentarioMapper.MapToCollectionObject(usu.Comentario),
                    MensajesE = MensajeMapper.MapToCollectionObject(usu.Mensaje),
                    MensajesR = MensajeMapper.MapToCollectionObject(usu.Mensaje1),
                    Proyectos = ProyectoMapper.MapToCollectionObject(usu.Proyecto),
                    Seguidores = SeguimientoMapper.MapToCollectionObject(usu.Seguimiento1),
                    Siguiendo = SeguimientoMapper.MapToCollectionObject(usu.Seguimiento),
                    PValorados = ValoracionMapper.MapToCollectionObject(usu.Valoracion),
                };
                usuario.Add(u);
            }
            return usuario;
        }*/
    }
}
