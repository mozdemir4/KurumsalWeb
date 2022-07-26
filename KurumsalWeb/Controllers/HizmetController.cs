using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HizmetController : Controller
    {
        private KurumsalDBContext context = new KurumsalDBContext();
        public ActionResult Index()
        {
            return View(context.Hizmet.ToList());
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(Hizmetler hizmetler, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo info = new FileInfo(ResimURL.FileName);
                    string resim_ad = ResimURL.FileName + info.Extension;
                    img.Resize(300, 250);
                    img.Save("~/Uploads/Hizmetler/" + resim_ad);

                    hizmetler.ResimURL = "/Uploads/Hizmetler/" + resim_ad;

                }
                context.Hizmet.Add(hizmetler);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hizmetler);
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var iletisim = context.Hizmet.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View("Guncelle", iletisim);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Guncelle(int? id, Hizmetler hizmetler, HttpPostedFileBase ResimURL)
        {

            if (ModelState.IsValid)
            {
                var hizmet_id = context.Hizmet.Find(id);
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(hizmet_id.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(hizmet_id.ResimURL));
                    }
                    WebImage webImage = new WebImage(ResimURL.InputStream);
                    FileInfo fileInfo = new FileInfo(ResimURL.FileName);
                    string imgname = ResimURL.FileName + fileInfo.Extension;
                    webImage.Resize(300, 250);
                    webImage.Save("~/Uploads/Hizmetler/" + imgname);

                    hizmet_id.ResimURL = "/Uploads/Hizmetler/" + imgname;
                }
                hizmet_id.Baslik = hizmetler.Baslik;
                hizmet_id.Aciklama = hizmetler.Aciklama;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(hizmetler);

        }
        public ActionResult Sil(int? id)
        {
            var h_id = context.Hizmet.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (h_id == null)
            {
                return HttpNotFound();
            }
            context.Hizmet.Remove(h_id);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}