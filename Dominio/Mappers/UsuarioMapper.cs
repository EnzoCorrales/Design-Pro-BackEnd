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
                Ciudad = usuario.Ciudad,
                Profesion = usuario.Profesion,
                Empresa = usuario.Empresa,
                ImgPerfil = usuario.ImgPerfil,
                UrlWeb = usuario.UrlWeb,
                Password = usuario.Password,
                Descripcion = usuario.Descripcion,
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
                Comentario = null,
                Mensaje = null,
                Mensaje1 = null,
                Proyecto = null,
                Seguimiento1 = null,
                Seguimiento = null,
                Valoracion = null,
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
    }
}
