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
    public class TageskarteController : Controller
    {
        private RestaurantPlannerContext db = new RestaurantPlannerContext();

        // GET: Tageskarte
        public ActionResult Index()
        {
            return View(db.Tageskarten.Where(x => x.Ablaufdatum > DateTime.Now).ToList());
        }

        // GET: Tageskarte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tageskarte tageskarte = db.Tageskarten.Find(id);
            if (tageskarte == null)
            {
                return HttpNotFound();
            }
            tageskarte.Gerichte = tageskarte.MenuZugehoerigkeit.Gerichte;
            tageskarte.Getraenke = tageskarte.MenuZugehoerigkeit.Getraenke;
            return View(tageskarte);
        }

        // GET: Tageskarte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tageskarte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TageskarteDataTransferObject tageskarteDataTransferObject)
        {
            if (!ModelState.IsValid) return View(tageskarteDataTransferObject);
            var menu = db.Menus.FirstOrDefault(x => x.MenuName.Equals(tageskarteDataTransferObject.MenuZugehoerigkeit));
            if (menu == null)
            {
                throw new WebException("The choosen Menu doesn't exists. Contact the Systemadministrator");
            }
            var tageskarte = new Tageskarte
            {
                TageskarteId = tageskarteDataTransferObject.TageskarteId,
                MenuZugehoerigkeit = menu,
                Gerichte = menu.Gerichte,
                Getraenke = menu.Getraenke,
                Ablaufdatum = DateTime.Now + TimeSpan.FromDays(1)
            };
            db.Tageskarten.Add(tageskarte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tageskarte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tageskarte tageskarte = db.Tageskarten.Find(id);
            if (tageskarte == null)
            {
                return HttpNotFound();
            }
            var tageskarteDataTransferObject = new TageskarteDataTransferObject
            {
                TageskarteId = tageskarte.TageskarteId,
                MenuZugehoerigkeit = tageskarte.MenuZugehoerigkeit.MenuName,
                Getraenke = tageskarte.Getraenke,
                Gerichte = tageskarte.Gerichte,
                Ablaufdatum = tageskarte.Ablaufdatum
            };
            return View(tageskarteDataTransferObject);
        }

        // POST: Tageskarte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TageskarteDataTransferObject tageskarteDataTransferObject)
        {
            if (!ModelState.IsValid) return View(tageskarteDataTransferObject);
            var tageskarte =
                db.Tageskarten.FirstOrDefault(x => x.TageskarteId.Equals(tageskarteDataTransferObject.TageskarteId));
            var menu = db.Menus.FirstOrDefault(x => x.MenuName.Equals(tageskarteDataTransferObject.MenuZugehoerigkeit));
            if (tageskarte == null || menu == null)
            {
                throw new WebException("The choosen Menu doesn't exists. Contact the Systemadministrator");
            }
            tageskarte.MenuZugehoerigkeit = menu;
            tageskarte.Getraenke = tageskarteDataTransferObject.Getraenke;
            tageskarte.Gerichte = tageskarteDataTransferObject.Gerichte;
            db.Entry(tageskarte).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tageskarte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tageskarte tageskarte = db.Tageskarten.Find(id);
            if (tageskarte == null)
            {
                return HttpNotFound();
            }
            return View(tageskarte);
        }

        // POST: Tageskarte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tageskarte tageskarte = db.Tageskarten.Find(id);
            db.Tageskarten.Remove(tageskarte);
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
