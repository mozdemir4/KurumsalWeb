using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HakkimizdaController : Controller
    {
        KurumsalDBContext context = new KurumsalDBContext();
        public ActionResult Index()
        {
            var listele = context.Hakkimizda.ToList();
            return View("Index", listele);
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            var about_id = context.Hakkimizda.Find(id);
            return View("Guncelle", about_id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Guncelle(int id, Hakkimizda hakkimizda)
        {
            if (ModelState.IsValid)
            {
                var hk_id = context.Hakkimizda.Where(x => x.HakkimizdaID == id).FirstOrDefault();
                hk_id.Aciklama = hakkimizda.Aciklama;
                context.SaveChanges();
                return RedirectToAction("Index", "Hakkimizda");
            }
            return View(hakkimizda);
        }

    }
}