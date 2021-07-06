﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOUsuario
    {
        public DTOUsuario()
        {
            this.Comentarios = new HashSet<DTOComentario>();
            this.MensajesE = new HashSet<DTOMensaje>();
            this.MensajesR = new HashSet<DTOMensaje>();
            this.Proyectos = new HashSet<DTOProyecto>();
            this.Seguidores = new HashSet<DTOSeguimiento>();
            this.Siguiendo = new HashSet<DTOSeguimiento>();
            this.PValorados = new HashSet<DTOValoracion>();
        }        
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido"), MaxLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido"), MaxLength(50)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El correo es requerido"), MaxLength(100)]
        [EmailAddress(ErrorMessage = "El correo es inválido")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public string FNac { get; set; }
        [Required(ErrorMessage = "El país es requerido")]
        public string Pais { get; set; }
        [MaxLength(50)]
        public string Profesion { get; set; }
        [MaxLength(50)]
        public string Empresa { get; set; }
        public string ImgPerfil { get; set; }
        public string UrlWeb { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string Ciudad { get; set; }
        [MaxLength(500)]
        public string Descripcion { get; set; }

        public virtual ICollection<DTOComentario> Comentarios { get; set; }
        public virtual ICollection<DTOMensaje> MensajesE { get; set; }
        public virtual ICollection<DTOMensaje> MensajesR { get; set; }
        public virtual ICollection<DTOProyecto> Proyectos { get; set; }
        public virtual ICollection<DTOSeguimiento> Seguidores { get; set; }
        public virtual ICollection<DTOSeguimiento> Siguiendo { get; set; }
        public virtual ICollection<DTOValoracion> PValorados { get; set; }
    }
}
