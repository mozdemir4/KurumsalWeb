using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        
        [Required,StringLength(50,ErrorMessage = "En fazla 50 karakter girebilirsiniz.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="Şifreniz en fazla 50 karakter olabilir.")]
        public string Sifre { get; set; }
        public string Yetki { get; set; }
    }
}