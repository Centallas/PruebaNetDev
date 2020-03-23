using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
   
    public class seller
    {
        public long CODIGO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string NUMERO_IDENTIFICACION { get; set; }
        public Nullable<long> CODIGO_CIUDAD { get; set; }
        public string CIUDAD { get; set; }
    }
}