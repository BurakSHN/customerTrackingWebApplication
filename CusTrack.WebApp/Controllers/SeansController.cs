using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CusTrack.Entities;
using CusTrack.WebApp.Models;
using CusTrack.BusinessLayerr;
using CusTrack.WebApp.Models.Home;
using CusTrack.BusinessLayerr.Results;
using CusTrack.WebApp.Filters;

namespace CusTrack.WebApp.Controllers
{
    [Exc]
    [Auth]
    public class SeansController : Controller
    {
        private CustomerManager cm = new CustomerManager();
        private SeansManager sm = new SeansManager();

        // GET: Seans
        public ActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel();
            model.SeansList=sm.ListQueryable().OrderByDescending(x => x.Date).ToList();
            //model.CustomerList = cm.ListQueryable().OrderBy(x => x.Name).ToList();

            return View(model);
        }

        public ActionResult BySeans(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomePageViewModel model = new HomePageViewModel();
            model.SeansList = sm.ListQueryable().Where(x => x.CustomerId == id).OrderByDescending(x => x.Date).ToList();
            model.CustomerList = cm.ListQueryable().OrderBy(x => x.Name).ToList();
            return View("Index", model);
        }

        public ActionResult Details(int? id)
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
            return View(seans);
        }

        public ActionResult Create()
        {
            ViewBag.CustomerId = (from kisi in cm.List()
                                  select new SelectListItem()
                                  {
                                      Text=kisi.Name + " "+kisi.Surname,
                                      Value=kisi.Id.ToString()
                                  }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Seans seans)
        {
            ViewBag.CustomerId = (from kisi in cm.List()
                                  select new SelectListItem()
                                  {
                                      Text = kisi.Name + " " + kisi.Surname,
                                      Value = kisi.Id.ToString()
                                  }).ToList();

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Seans> res = sm.Insert(seans);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(seans);
                }
                return RedirectToAction("Index");
            }

            return View(seans);
        }

        public ActionResult Edit(int? id)
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
            List<SelectListItem> CusList = (from kisi in cm.List()
                                  select new SelectListItem()
                                  {
                                      Text = kisi.Name + " " + kisi.Surname,
                                      Value = kisi.Id.ToString()
                                  }).ToList();
            ViewBag.Customer = CusList;
            return View(seans);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Seans seans)
        {
            List<SelectListItem> CusList = (from kisi in cm.List()
                                            select new SelectListItem()
                                            {
                                                Text = kisi.Name + " " + kisi.Surname,
                                                Value = kisi.Id.ToString()
                                            }).ToList();
            ViewBag.Customer = CusList;

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Seans> res = sm.Update(seans);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(seans);
                }
                return RedirectToAction("Index");
            }
            //ViewBag.CustomerId = new SelectList(cm.List(), "Id", "Name", seans.CustomerId);
            return View(seans);
        }

        public ActionResult Delete(int? id)
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
            return View(seans);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seans seans = sm.Find(x => x.Id == id);
            sm.Delete(seans);
            return RedirectToAction("Index");
        }

        
    }
}
