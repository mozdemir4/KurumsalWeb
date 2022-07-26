using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Iletisim")]
    public class Iletisim
    {
        [Key]
        public int IletisimID { get; set; }

        [StringLength(200)]
        public string Adres { get; set; }
        [StringLength(11)]
        public string Telefon { get; set; }
        [StringLength(11)]
        public string Fax { get; set; }
        [StringLength(200)]
        public string WhatsApp { get; set; }
        [StringLength(200)]
        public string Facebook { get; set; }
        [StringLength(200)]
        public string Twitter { get; set; }
        [StringLength(200)]
        public string Instagram { get; set; }
    
    }
}