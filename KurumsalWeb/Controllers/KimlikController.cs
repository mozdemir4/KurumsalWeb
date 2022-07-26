using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class KimlikController : Controller
    {
        KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index()
        {
            var listele = db.Kimlik.ToList();
            return View("Index",listele);
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            var kimlik_id = db.Kimlik.Where(x => x.KimlikID == id).SingleOrDefault();

            return View("Guncelle", kimlik_id); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Guncelle(int id,Kimlik k, HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid)
            {
                var kimlik = db.Kimlik.Where(x => x.KimlikID == id).SingleOrDefault();

                if(LogoURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(kimlik.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(kimlik.LogoURL));
                    }
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imgInfo = new FileInfo(LogoURL.FileName);
                   
                    string logoname = LogoURL.FileName + imgInfo.Extension;
                    img.Resize(200, 300);
                    img.Save("~/Uploads/Kimlik/" + logoname);

                    kimlik.LogoURL = "/Uploads/Kimlik/" + logoname;
                }
                kimlik.Title = k.Title;
                kimlik.Keywords = k.Keywords;
                kimlik.Unvan = k.Unvan;
                kimlik.Description = k.Description;
               
                db.SaveChanges();

                return RedirectToAction("Index", "Kimlik");
            }
            return View(k);
        }
    }
}