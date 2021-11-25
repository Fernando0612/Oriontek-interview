using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrionTek.Models
{
    public class Address
    {

        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

      
    }
}