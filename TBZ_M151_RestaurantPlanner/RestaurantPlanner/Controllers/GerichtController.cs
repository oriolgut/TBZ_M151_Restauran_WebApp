using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantPlanner.DataTransferObjects;
using RestaurantPlanner.DAL;
using RestaurantPlanner.Models;

namespace RestaurantPlanner.Controllers
{
    public class GerichtController : Controller
    {
        private RestaurantPlannerContext db = new RestaurantPlannerContext();

        // GET: Gericht
        public ActionResult Index()
        {
            return View(db.Gerichte.ToList());
        }

        // GET: Gericht/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gericht gericht = db.Gerichte.Find(id);
            if (gericht == null)
            {
                return HttpNotFound();
            }
            return View(gericht);
        }

        // GET: Gericht/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gericht/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GerichtDataTransferObject gerichtDataTransferObject)
        {
            if (!ModelState.IsValid) return View(gerichtDataTransferObject);
            var menu = db.Menus.FirstOrDefault(x => x.MenuName.Equals(gerichtDataTransferObject.MenuZugehoerigkeit));
            if (menu == null)
            {
                throw new WebException("The choosen Menu doesn't exists. Contact the Systemadministrator");
            }
            var gericht = new Gericht
            {
                GerichtId = gerichtDataTransferObject.GerichtId,
                GerichtName = gerichtDataTransferObject.GerichtName,
                GerichtPreis = gerichtDataTransferObject.GerichtPreis,
                MenuZugehoerigkeit = new List<Menu>
                {
                    menu
                }
            };
            db.Gerichte.Add(gericht);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Gericht/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gericht gericht = db.Gerichte.Find(id);
            if (gericht == null)
            {
                return HttpNotFound();
            }
            var gerichtDataTransferObject = new GerichtDataTransferObject
            {
                GerichtId = gericht.GerichtId,
                GerichtName = gericht.GerichtName,
                GerichtPreis = gericht.GerichtPreis,
            };
            return View(gerichtDataTransferObject);
        }

        // POST: Gericht/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GerichtDataTransferObject gerichtDataTransferObject)
        {
            if (!ModelState.IsValid) return View(gerichtDataTransferObject);

            var menu = db.Menus.FirstOrDefault(x => x.MenuName.Equals(gerichtDataTransferObject.MenuZugehoerigkeit));
            if (menu == null)
            {
                throw new WebException("The choosen Menu doesn't exists. Contact the Systemadministrator");
            }

            var gericht = db.Gerichte.First(x => x.GerichtId.Equals(gerichtDataTransferObject.GerichtId));
            gericht.MenuZugehoerigkeit.Add(menu);

            db.Entry(gericht).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Gericht/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gericht gericht = db.Gerichte.Find(id);
            if (gericht == null)
            {
                return HttpNotFound();
            }
            return View(gericht);
        }

        // POST: Gericht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gericht gericht = db.Gerichte.Find(id);
            db.Gerichte.Remove(gericht);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
