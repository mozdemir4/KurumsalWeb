using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Yorum")]
    public class Yorum
    {
        [Key]
        public int YorumID { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="En fazla 50 karakter girebilirsiniz!!!")]
        [Column(TypeName ="Varchar")]

        public string AdSoyad { get; set; }
        [Required]
        [Column(TypeName ="Varchar")]
        [StringLength(250)]
        [Display(Name ="Yorum")]
        public string Icerik { get; set; }
        [StringLength(100)]
        [Column(TypeName ="Varchar")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public bool Onay { get; set; }

        public int? Blogid { get; set; }
        public virtual Blog Blog { get; set; }

    }
}