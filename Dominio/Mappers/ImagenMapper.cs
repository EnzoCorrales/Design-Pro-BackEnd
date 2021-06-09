using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataTransferObjects;
using Persistencia.Database;

namespace Dominio.Mappers
{
    public class ImagenMapper
    {

        public DTOImagen MapToObject(Imagen imagen)
        {
            if (imagen == null)
                return null;

            return new DTOImagen()
            {
                Id = imagen.Id,
                IdProyecto = imagen.IdProyecto,
                Nombre = imagen.Nombre,
                Direccion = imagen.Direccion,
                Orden = imagen.Orden,
            };
        }

        public Imagen MapToEntity(DTOImagen imagen)
        {
            if (imagen == null)
                return null;

            return new Imagen()
            {
                Id = imagen.Id,
                IdProyecto = imagen.IdProyecto,
                Nombre = imagen.Nombre,
                Direccion = imagen.Direccion,
                Orden = imagen.Orden,
            };
        }

        public static HashSet<DTOImagen> MapToCollectionObject(ICollection<Imagen> imagenes)
        {
            if (imagenes == null)
                return null;

            var imagen = new HashSet<DTOImagen>();
            foreach (var im in imagenes)
            {
                var i = new DTOImagen()
                {
                    Id = im.Id,
                    IdProyecto = im.IdProyecto,
                    Nombre = im.Nombre,
                    Direccion = im.Direccion,
                    Orden = im.Orden,
                };
                imagen.Add(i);
            }
            return imagen;
        }

        public static HashSet<Imagen> MapToCollectionEntity(ICollection<DTOImagen> imagenes)
        {
            if (imagenes == null)
                return null;

            var imagen = new HashSet<Imagen>();
            foreach (var im in imagenes)
            {
                var i = new Imagen()
                {
                    Id = im.Id,
                    IdProyecto = im.IdProyecto,
                    Nombre = im.Nombre,
                    Direccion = im.Direccion,
                    Orden = im.Orden,
                };
                imagen.Add(i);
            }
            return imagen;
        }
    }
}
