using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Hizmetler")]
    public class Hizmetler
    {
        [Key]
        public int HizmetID { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Başlık")]
        public string Baslik { get; set; }

        [StringLength(250)]
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Resim")]
        public string ResimURL { get; set; }
    }
}