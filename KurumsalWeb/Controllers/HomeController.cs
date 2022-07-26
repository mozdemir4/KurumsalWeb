using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();

        [Route("")]
        [Route("AnaSayfa")]
        public ActionResult Index()
        {
            ViewBag.hizmet = db.Hizmet.ToList().OrderByDescending(k => k.HizmetID);
            ViewBag.kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }
        public ActionResult SliderPartial()
        {
            return PartialView(db.Sliders.ToList().OrderByDescending(k => k.SliderID));
        }
        public ActionResult HizmetPartial()
        {
            
            return PartialView(db.Hizmet.ToList().OrderByDescending(x => x.HizmetID));
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            ViewBag.kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hakkimizda.ToList());
        }
        [Route("Hizmetler")]
        public ActionResult Hizmetler()
        {
            ViewBag.kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hizmet.ToList().OrderByDescending(x => x.HizmetID));
        }
        [Route("BlogPost")]
        public ActionResult Blog(int sayfa = 1)
        {
            ViewBag.kimlik = db.Kimlik.SingleOrDefault();
            var liste = db.Blog.OrderByDescending(x=>x.BlogID).ToPagedList(sayfa, 1);
            return View("Blog",liste);
        }

      [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay (int? id)
        {
            ViewBag.kimlik = db.Kimlik.SingleOrDefault();
            var blog = db.Blog.Include("Yorums").Where(x => x.BlogID == id).FirstOrDefault();
            return View(blog);
        }
        
        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x=>x.KategoriAd));
        }

        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult BlogKategoriListele(int? id,int sayfa=1)
        {
            ViewBag.kimlik = db.Kimlik.SingleOrDefault();
            var b_id = db.Blog.Include("Kategori").OrderByDescending(u=>u.BlogID).Where(x => x.Kategori.KategoriID == id).ToPagedList(sayfa, 2);
            return View(b_id);
        }

        public ActionResult BlogKayitPartial()
        {
            ViewBag.kimlik = db.Kimlik.SingleOrDefault();
            return PartialView(db.Blog.ToList().OrderByDescending(x => x.BlogID));
        }
        [Route("Iletisim")]
        [HttpGet]
        public ActionResult Iletisim()
        {
            ViewBag.kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Iletisim(Mesajlar m)
        {
            if (ModelState.IsValid)
            {
                db.Mesajlars.Add(m);
                db.SaveChanges();
                ViewBag.uyari = "Mesajınız başarıyla gönderilmiştir.";
                return RedirectToAction("Iletisim");

            }
            else
            {
                ViewBag.uyari = "Mesajınızı gönderirken hata meydana geldi!!!";
            }
            return View();
        }
     
        public JsonResult YorumYap(string adsoyad,string email,string icerik, int blogid)
        {
            if(adsoyad == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorums.Add(new Yorum { AdSoyad = adsoyad, Email = email, Icerik = icerik, Blogid = blogid ,Onay = false});
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FooterPartial()
        {
            ViewBag.hizmet = db.Hizmet.ToList().OrderByDescending(k => k.HizmetID);
            ViewBag.iletisim = db.Iletisim.ToList().FirstOrDefault();
            ViewBag.blog = db.Blog.ToList().OrderByDescending(x => x.BlogID);
            return PartialView();
        }




    }
}