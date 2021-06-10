using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataTransferObjects;
using Persistencia.Database;

namespace Dominio.Mappers
{
    public class VideoMapper
    {

        public DTOVideo MapToObject(Video video)
        {
            if (video == null)
                return null;

            return new DTOVideo()
            {
                Id = video.Id,
                IdProyecto = video.IdProyecto,
                Nombre = video.Nombre,
                Direccion = video.Direccion,
                Orden = video.Orden,
            };
        }

        public Video MapToEntity(DTOVideo video)
        {
            if (video == null)
                return null;

            return new Video()
            {
                Id = video.Id,
                IdProyecto = video.IdProyecto,
                Nombre = video.Nombre,
                Direccion = video.Direccion,
                Orden = video.Orden,
            };
        }

        public static HashSet<DTOVideo> MapToCollectionObject(ICollection<Video> videos)
        {
            if (videos == null)
                return null;

            var video = new HashSet<DTOVideo>();
            foreach (var vid in videos)
            {
                var v = new DTOVideo()
                {
                    Id = vid.Id,
                    IdProyecto = vid.IdProyecto,
                    Nombre = vid.Nombre,
                    Direccion = vid.Direccion,
                    Orden = vid.Orden,
                };
                video.Add(v);
            }
            return video;
        }

        public static HashSet<Video> MapToCollectionEntity(ICollection<DTOVideo> videos)
        {
            if (videos == null)
                return null;

            var video = new HashSet<Video>();
            foreach (var vid in videos)
            {
                var v = new Video()
                {
                    Id = vid.Id,
                    IdProyecto = vid.IdProyecto,
                    Nombre = vid.Nombre,
                    Direccion = vid.Direccion,
                    Orden = vid.Orden,
                };
                video.Add(v);
            }
            return video;
        }
    }
}
