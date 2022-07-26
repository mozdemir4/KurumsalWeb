using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Kimlik")]
    public class Kimlik
    {
        [Key]
        public int KimlikID { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Site Başlığı")]
        public string Title { get; set; }

        [Required]
        [StringLength(400)]
        [DisplayName("Anahtar Kelimeler")]
        public string Keywords { get; set; }

        [Required]
        [StringLength(300)]
        [DisplayName("Site Açıklama")]
        public string Description { get; set; }

        [DisplayName("Site Logo")]
        [StringLength(300)]
        public string LogoURL { get; set; }

        [DisplayName("Site Ünvan")]
        [StringLength(50)]
        public string Unvan { get; set; }
    }
}