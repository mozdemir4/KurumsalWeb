using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="En fazla 50 karakter girebilirsiniz")]
        [DisplayName("Kategori Adı")]
        public string KategoriAd { get; set; }

        [StringLength(400)]
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}