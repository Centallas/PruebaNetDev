//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataConexion
{
    using System;
    using System.Collections.Generic;
    
    public partial class VENDEDOR
    {
        public long CODIGO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string NUMERO_IDENTIFICACION { get; set; }
        public Nullable<long> CODIGO_CIUDAD { get; set; }
    
        public virtual CIUDAD CIUDAD { get; set; }
    }
}
