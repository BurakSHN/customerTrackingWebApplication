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
using System.Web.Helpers;

namespace CusTrack.WebApp.Controllers
{
    [Exc]
    [Auth]
    public class UserController : Controller
    {
        private UserManager um = new UserManager();

        public ActionResult Index()
        {
            return View(um.List());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Crypto.HashPassword(user.Password);
                BusinessLayerResult<User> res = um.Insert(user);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x=>ModelState.AddModelError("",x.Message));
                    return View(user);
                }
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = um.Find(x => x.Id == id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                User db_user = um.Find(x => x.Id == user.Id);
                db_user.Username = user.Username;
                db_user.Email = user.Email;
                db_user.Password = Crypto.HashPassword(user.Password);
                um.Update(db_user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = um.Find(x => x.Id == id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = um.Find(x => x.Id == id);
            um.Delete(user);
            return RedirectToAction("Index");
        }
    }
}
