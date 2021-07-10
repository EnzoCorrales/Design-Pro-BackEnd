using Common.DataTransferObjects;
using Persistencia.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Fecha = mensaje.Fecha,
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
                Fecha = mensaje.Fecha,
                IdUsuarioE = mensaje.IdUsuarioE,
                IdUsuarioR = mensaje.IdUsuarioR,
                Visto = mensaje.Visto,
            };
        }
    }
}
