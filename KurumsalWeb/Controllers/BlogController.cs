using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class BlogController : Controller
    {
        private KurumsalDBContext context = new KurumsalDBContext();
        public ActionResult Index()
        {
            return View(context.Blog.ToList().OrderByDescending(x=>x.BlogID));
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> degerler = (from x in context.Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Value = x.KategoriID.ToString(),
                                                 Text = x.KategoriAd
                                             }).ToList();
            ViewBag.deger = degerler;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Blog model, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo info = new FileInfo(ResimURL.FileName);
                    string resim_ad = Guid.NewGuid().ToString()  + info.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Uploads/Blog/" + resim_ad);

                    model.ResimURL = "/Uploads/Blog/" + resim_ad;
                }
                context.Blog.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //blog guncelleme view sayfası
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            List<SelectListItem> degerler = (from x in context.Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Value = x.KategoriID.ToString(),
                                                 Text = x.KategoriAd
                                             }).ToList();
            ViewBag.deger = degerler;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blog_id = context.Blog.Where(x => x.BlogID == id).FirstOrDefault();
            if (blog_id == null)
            {
                return HttpNotFound();
            }
            return View(blog_id);
        }

        // Blog guncelleme post
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guncelle(int? id,Blog model, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var blog_id = context.Blog.Where(x => x.BlogID == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(blog_id.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(blog_id.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo info = new FileInfo(ResimURL.FileName);
                    string resim_ad = Guid.NewGuid().ToString() + info.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Uploads/Blog/" + resim_ad);

                    blog_id.ResimURL = "/Uploads/Blog/" + resim_ad;
                }
                blog_id.Baslik = model.Baslik;
                blog_id.Icerik = model.Icerik;
                blog_id.Kategoriid = model.Kategoriid;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Sil(int? id)
        {
            var blog_id = context.Blog.Where(x => x.BlogID == id).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }    
            if (blog_id == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(blog_id.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(blog_id.ResimURL));
            }
            context.Blog.Remove(blog_id);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}