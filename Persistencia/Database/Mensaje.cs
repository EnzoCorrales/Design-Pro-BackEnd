//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Persistencia.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mensaje
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdUsuarioE { get; set; }
        public int IdUsuarioR { get; set; }
        public int Leído { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
    }
}
