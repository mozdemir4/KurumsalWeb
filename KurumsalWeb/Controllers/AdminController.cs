
using KurumsalWeb.Models;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace KurumsalWeb.Controllers
{

    public class AdminController : Controller
    {
        KurumsalDBContext context = new KurumsalDBContext();
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.BlogCount = context.Blog.Count();
            ViewBag.JobCount = context.Hizmet.Count();
            ViewBag.CommentCount = context.Yorums.Count();
            ViewBag.CategoryCount = context.Kategori.Count();

            ViewBag.Yorum = context.Yorums.Where(x => x.Onay == false).Count();
            return View();
        }
        [Route("yonetimpaneli/giris/")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin model)
        {
            var admin_id = context.Admin.Where(x => x.Email == model.Email).SingleOrDefault();
            if (admin_id.Email == model.Email && admin_id.Sifre == Crypto.Hash(model.Sifre,"MD5"))
            {
              
                Session["adminid"] = admin_id.AdminID;
                Session["email"] = admin_id.Email;
                Session["yetki"] = admin_id.Yetki;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.uyari = "Kullanıcı adınız veya şifreniz yanlış!!!";
                return View(model);
            }

        }

        /* ------------------ */

        //admin listeleme
        public ActionResult Profiles()
        {
            return View(context.Admin.ToList());
        }

        public ActionResult Create(Admin model, string Sifre)
        {
            if (ModelState.IsValid)
            {
                model.Sifre = Crypto.Hash(Sifre, "MD5");
                context.Admin.Add(model);
                context.SaveChanges();
                return RedirectToAction("Profiles");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var id_ = context.Admin.Where(x => x.AdminID == id).SingleOrDefault();

            return View(id_);
        }
        [HttpPost]
        public ActionResult Edit(int id, Admin model, string sifre)
        {
            if (ModelState.IsValid)
            {
                var id_ = context.Admin.Where(x => x.AdminID == id).SingleOrDefault();
                id_.Sifre = Crypto.Hash(sifre, "MD5");
                id_.Email = model.Email;
                id_.Yetki = model.Yetki;
                context.SaveChanges();
                return RedirectToAction("Profiles");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var admin_id = context.Admin.Where(x => x.AdminID == id).FirstOrDefault();
            if(admin_id != null)
            {
                context.Admin.Remove(admin_id);
                context.SaveChanges();
                return RedirectToAction("Profiles");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string eposta)
        {
            var eposta_id = context.Admin.Where(x => x.Email == eposta).FirstOrDefault();
            if (eposta_id!=null)
            {
                Random rnd = new Random();
                string yeni_sifre = rnd.Next().ToString();

                eposta_id.Sifre = Crypto.Hash(yeni_sifre,"MD5");
                context.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "krmsal2@gmail.com";
                WebMail.Password = "leopar23.";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta,"Admin Panel Şifre İşlemi","Şifreniz: "+yeni_sifre);
                ViewBag.uyari = "İstek başarıyla yollanmıştır.";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.uyari = "Mail gönderilmedi!!!";
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

    }
}