using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Mesajlar")]
    public class Mesajlar
    {
        [Key]
        public int MesajID { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName ="Varchar")]
        public string Gonderen { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "Varchar")]
        public string Konu { get; set; }
        [Required]
        [Column(TypeName ="Varchar")]
        [StringLength(250)]
        public string Mesaj { get; set; }
        [Required]
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Mail { get; set; }
    }
}