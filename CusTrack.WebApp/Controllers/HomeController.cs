using CusTrack.BusinessLayerr;
using CusTrack.BusinessLayerr.Results;
using CusTrack.Entities;
using CusTrack.Entities.ValueObjects;
using CusTrack.WebApp.Filters;
using CusTrack.WebApp.Models.Calendar;
using CusTrack.WebApp.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CusTrack.WebApp.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
        private SeansManager sm = new SeansManager();
        private CustomerManager cm = new CustomerManager();
        private UserManager um = new UserManager();

        [Auth]
        public ActionResult Index()
        {
            //Test asamasında MSSQL de Database'i oluşturuyor ve Uptadate, Insert, Delete gibi işlemlerini kontrol ediyoruz !!!
            //Test test = new Test();
            //test.InsertTest();
            //-----------------------------------------------

            //return View();
            DateTime digerGun = DateTime.Today.AddDays(5);
            HomePageViewModel model = new HomePageViewModel();
            model.CustomerList = cm.ListQueryable().Where(x=>x.Birthday==DateTime.Today).ToList();
            model.SeansList = sm.ListQueryable().Where(x => x.IsDraft == false && (x.Date==DateTime.Today || x.Date==digerGun)).OrderBy(x=>x.Date).ToList();

            return View(model);
        }

        //Seans günü geçenler
        [Auth]
        public ActionResult PastDay()
        {
            HomePageViewModel model = new HomePageViewModel();
            model.CustomerList = cm.ListQueryable().Where(x => x.Birthday == DateTime.Today).ToList();
            model.SeansList = sm.ListQueryable().Where(x => x.IsDraft == false && x.Date < DateTime.Today).OrderByDescending(x => x.Date).ToList();

            ViewBag.Baslik = "<span class='fa fa-exclamation-circle' style='color:#ff0000'></span>&nbsp;Seans Günü Geçenler<br />";
            return View("Index",model);
        }

        //Kasa Ciro İşlemleri
        [Auth]
        public ActionResult Safe()
        {
            return View();
        }

        [Auth]
        [HttpPost]
        public JsonResult SafeMoney(DateTime ilkData,DateTime sonData)
        {
            List<int> total = sm.List(x=>x.Date >= ilkData && x.Date <= sonData && x.IsDraft==true).Select(x => x.Money).ToList();
            int result = 0;
            foreach (int item in total)
            {
                result += item;
            }
            System.Threading.Thread.Sleep(1500);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        //Takvim Ajanda
        [Auth]
        public ActionResult Calendar()
        {
            return View();
        }

        [Auth]
        public JsonResult GetCalendarEvents()
        {
            List<CalendarEventViewModel> eventItems = new List<CalendarEventViewModel>();


            DateTime takvim = DateTime.Today;
            foreach (Seans seans in sm.List().Where(x=>(x.IsDraft==false && x.Date >= takvim) || (x.IsDraft==true && x.Date<=takvim) || (x.IsDraft==false && x.Date<takvim) ).ToList())
            {
                CalendarEventViewModel item = new CalendarEventViewModel();
                item.id = seans.Id;
                item.start = seans.Date.ToString("s");
                item.end = seans.Date.ToString("s");
                item.allDay = true;
                if (seans.IsDraft==false && seans.Date>=takvim)
                {
                    item.color = "red";
                }
                else if (seans.IsDraft==true && seans.Date<=takvim)
                {
                    item.color = "green";
                }
                else if (seans.IsDraft==false && seans.Date<takvim)
                {
                    item.color = "blue";
                }              
                item.title = (seans.Owner.Name +" "+ seans.Owner.Surname);

                item.TelNo = seans.Owner.Phone.ToString();
                item.Tarih = seans.Date.ToString("dd-MM-yyyy");
                item.Baslik = seans.Title;
                item.Aciklama = seans.Text;     
                item.Ucret = seans.Money;

                eventItems.Add(item);

            }

            return Json(eventItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //LoginViewModel de ki şartlar sağlandı mı ?
            if (ModelState.IsValid)
            {
                BusinessLayerResult<User> user = um.LoginUser(model);

                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                Session["login"] = user.Result;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Auth]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [Auth]
        public ActionResult GetSeansText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seans seans = sm.Find(x => x.Id == id.Value);
            if (seans == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialSeansText", seans);
        }

        public ActionResult HassError()
        {
            return View();
        }

        public ActionResult EMailSend()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EMailSend(EmailSendViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<User> user = um.EmailSend(model);
                return View(model);
            }
            return View(model);
        }

        public ActionResult PassReset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PassReset(int id,PassResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = um.Find(x => x.Id == id);
                if (user !=null)
                {
                    user.Password = Crypto.HashPassword(model.Password);
                    um.Update(user);
                    return RedirectToAction("Login");
                }
            }
            
            return View(model);
        }

    }
}