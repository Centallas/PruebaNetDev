using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumeSellersApi.Models
{
    public class sellers
    {
        public long CODIGO { get; set; }

        [Required]
        public string NOMBRE { get; set; }
        [Required]
        public string APELLIDO { get; set; }

        [Required]
        public string NUMERO_IDENTIFICACION { get; set; }
        public Nullable<long> CODIGO_CIUDAD { get; set; }        
        public string CIUDAD { get; set; }

        [Required]
        public City _city { get; set; }
    }

    public enum City
    {
        Bogotá = 1,
        Cali = 2,
        Medellín = 3,
        Pasto = 4,
        Popayán = 5,
        Wuham = 6
    }
}