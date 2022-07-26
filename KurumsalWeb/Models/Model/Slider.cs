using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderID { get; set; }

        [StringLength(50)]
        [Column(TypeName ="Varchar")]
        [DisplayName("Başlık")]
        public string Baslik { get; set; }

        [StringLength(150)]
        [Column(TypeName = "Varchar")]
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Varchar")] 
        public string ResimURL { get; set; }
    }
}