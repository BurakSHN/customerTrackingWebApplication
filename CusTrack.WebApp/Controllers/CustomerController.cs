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
using CusTrack.BusinessLayerr.Results;
using CusTrack.WebApp.Filters;

namespace CusTrack.WebApp.Controllers
{
    [Exc]
    [Auth]
    public class CustomerController : Controller
    {
        private CustomerManager cm = new CustomerManager();

        public ActionResult Index()
        {
            return View(cm.ListQueryable().OrderBy(x=>x.Name).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = cm.Find(x => x.Id == id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Customer> res = cm.Insert(customer);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("",x.Message));
                    return View(customer);
                }
                CacheHelper.RemoveCustomersFromCache();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = cm.Find(x => x.Id == id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Customer> res = cm.Update(customer);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(customer);
                }
                CacheHelper.RemoveCustomersFromCache();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = cm.Find(x => x.Id == id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = cm.Find(x => x.Id == id);
            cm.Delete(customer);
            CacheHelper.RemoveCustomersFromCache();
            return RedirectToAction("Index");
        }

    }
}
