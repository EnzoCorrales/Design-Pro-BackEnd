using Common.DataTransferObjects;
using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

/// <summary>
// una clase que mappea los DTO a Entidades y viceversa; también mappea colecciones de Entidades a colecciones de DTO y viceversa
/// </summary>

namespace Dominio.Mappers
{
    public class MensajeMapper
    {

        public DTOMensaje MapToObject(Mensaje mensaje)
        {
            if (mensaje == null)
                return null;

            return new DTOMensaje()
            {
                Id = mensaje.Id,
                Asunto = mensaje.Asunto,
                Contenido = mensaje.Contenido,
                Fecha = mensaje.Fecha.ToShortDateString(),
                IdUsuarioE = mensaje.IdUsuarioE,
                IdUsuarioR = mensaje.IdUsuarioR,
                Visto = mensaje.Visto,
            };
        }

        public Mensaje MapToEntity(DTOMensaje mensaje)
        {
            if (mensaje == null)
                return null;

            return new Mensaje()
            {
                Id = mensaje.Id,
                Asunto = mensaje.Asunto,
                Contenido = mensaje.Contenido,
                Fecha = this.ParseToDateType(mensaje.Fecha),
                IdUsuarioE = mensaje.IdUsuarioE,
                IdUsuarioR = mensaje.IdUsuarioR,
                Visto = mensaje.Visto,
            };
        }

        public List<DTOMensaje> MapToCollectionObject(ICollection<Mensaje> mensajes)
        {
            if (mensajes == null)
                return null;

            var mensaje = new List<DTOMensaje>();
            foreach (var men in mensajes)
            {
                var m = new DTOMensaje()
                {
                    Id = men.Id,
                    Asunto = men.Asunto,
                    Contenido = men.Contenido,
                    Fecha = men.Fecha.ToShortDateString(),
                    IdUsuarioE = men.IdUsuarioE,
                    IdUsuarioR = men.IdUsuarioR,
                    Visto = men.Visto,
                };
                mensaje.Add(m);
            }
            return mensaje;
        }

        public List<Mensaje> MapToCollectionEntity(ICollection<DTOMensaje> mensajes)
        {
            if (mensajes == null)
                return null;

            var mensaje = new List<Mensaje>();
            foreach (var men in mensajes)
            {
                var m = new Mensaje()
                {
                    Id = men.Id,
                    Asunto = men.Asunto,
                    Contenido = men.Contenido,
                    Fecha = DateTime.ParseExact(men.Fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    IdUsuarioE = men.IdUsuarioE,
                    IdUsuarioR = men.IdUsuarioR,
                    Visto = men.Visto,
                };
                mensaje.Add(m);
            }
            return mensaje;
        }

        public System.DateTime ParseToDateType(string date)
        {
            string inputFormat = "yyyy-MM-dd";
            return DateTime.ParseExact(date, inputFormat, CultureInfo.InvariantCulture);
        }
    }
}
