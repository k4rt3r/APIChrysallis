//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIChrysallis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class valoracions
    {
        public int id_soci { get; set; }
        public int id_esdeveniment { get; set; }
        public int valoracio { get; set; }
    
        public virtual socis socis { get; set; }
        public virtual esdeveniments esdeveniments { get; set; }
    }
}
