using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Database;
using Common.DataTransferObjects;

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
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                FNac = usuario.FNac,
                Pais = usuario.Pais,
                Profesion = usuario.Profesion,
                Empresa = usuario.Empresa,
                ImgPerfil = usuario.ImgPerfil,
                UrlWeb = usuario.UrlWeb,
                Descripcion = usuario.Descripcion,
                Comentarios = ComentarioMapper.MapToCollectionObject(usuario.Comentario),
                MensajesE = MensajeMapper.MapToCollectionObject(usuario.Mensaje),
                MensajesR = MensajeMapper.MapToCollectionObject(usuario.Mensaje1),
                Proyectos = ProyectoMapper.MapToCollectionObject(usuario.Proyecto),
                Seguidores = SeguimientoMapper.MapToCollectionObject(usuario.Seguimiento),
                Siguiendo = SeguimientoMapper.MapToCollectionObject(usuario.Seguimiento1),
                PValorados = ValoracionMapper.MapToCollectionObject(usuario.Valoracion),
            };
        }

        public Usuario MapToEntity(DTOUsuario usuario)
        {
            if (usuario == null)
                return null;

            return new Usuario()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                FNac = usuario.FNac,
                Pais = usuario.Pais,
                Profesion = usuario.Profesion,
                Empresa = usuario.Empresa,
                ImgPerfil = usuario.ImgPerfil,
                UrlWeb = usuario.UrlWeb,
                Descripcion = usuario.Descripcion,
                Comentario = ComentarioMapper.MapToCollectionEntity(usuario.Comentarios),
                Mensaje = MensajeMapper.MapToCollectionEntity(usuario.MensajesE),
                Mensaje1 = MensajeMapper.MapToCollectionEntity(usuario.MensajesR),
                Proyecto = ProyectoMapper.MapToCollectionEntity(usuario.Proyectos),
                Seguimiento = SeguimientoMapper.MapToCollectionEntity(usuario.Seguidores),
                Seguimiento1 = SeguimientoMapper.MapToCollectionEntity(usuario.Siguiendo),
                Valoracion = ValoracionMapper.MapToCollectionEntity(usuario.PValorados),
            };
        }

        public static HashSet<Usuario> MapToCollectionEntity(ICollection<DTOUsuario> usuarios)
        {
            if (usuarios == null)
                return null;

            var usuario = new HashSet<Usuario>();
            foreach (var usu in usuarios)
            {
                var u = new Usuario()
                {
                    Nombre = usu.Nombre,
                    Apellido = usu.Apellido,
                    Correo = usu.Correo,
                    FNac = usu.FNac,
                    Pais = usu.Pais,
                    Profesion = usu.Profesion,
                    Empresa = usu.Empresa,
                    ImgPerfil = usu.ImgPerfil,
                    UrlWeb = usu.UrlWeb,
                    Descripcion = usu.Descripcion,
                    Comentario = ComentarioMapper.MapToCollectionEntity(usu.Comentarios),
                    Mensaje = MensajeMapper.MapToCollectionEntity(usu.MensajesE),
                    Mensaje1 = MensajeMapper.MapToCollectionEntity(usu.MensajesR),
                    Proyecto = ProyectoMapper.MapToCollectionEntity(usu.Proyectos),
                    Seguimiento = SeguimientoMapper.MapToCollectionEntity(usu.Seguidores),
                    Seguimiento1 = SeguimientoMapper.MapToCollectionEntity(usu.Siguiendo),
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
                    Nombre = usu.Nombre,
                    Apellido = usu.Apellido,
                    Correo = usu.Correo,
                    FNac = usu.FNac,
                    Pais = usu.Pais,
                    Profesion = usu.Profesion,
                    Empresa = usu.Empresa,
                    ImgPerfil = usu.ImgPerfil,
                    UrlWeb = usu.UrlWeb,
                    Descripcion = usu.Descripcion,
                    Comentarios = ComentarioMapper.MapToCollectionObject(usu.Comentario),
                    MensajesE = MensajeMapper.MapToCollectionObject(usu.Mensaje),
                    MensajesR = MensajeMapper.MapToCollectionObject(usu.Mensaje1),
                    Proyectos = ProyectoMapper.MapToCollectionObject(usu.Proyecto),
                    Seguidores = SeguimientoMapper.MapToCollectionObject(usu.Seguimiento),
                    Siguiendo = SeguimientoMapper.MapToCollectionObject(usu.Seguimiento1),
                    PValorados = ValoracionMapper.MapToCollectionObject(usu.Valoracion),
                };
                usuario.Add(u);
            }
            return usuario;
        }
    }
}
