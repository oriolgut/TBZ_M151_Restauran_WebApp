using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantPlanner.DAL;
using RestaurantPlanner.Models;

namespace RestaurantPlanner.Controllers
{
    public class GetraenkController : Controller
    {
        private RestaurantPlannerContext db = new RestaurantPlannerContext();

        // GET: Getraenk
        public ActionResult Index()
        {
            return View(db.Getraenke.ToList());
        }

        // GET: Getraenk/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Getraenk getraenk = db.Getraenke.Find(id);
            if (getraenk == null)
            {
                return HttpNotFound();
            }
            return View(getraenk);
        }

        // GET: Getraenk/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Getraenk/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GetraenkId,GetraenkName,GetraenkPreis,HeissesGetraenk,AlkoholischesGetraenk,GetraenkMenge")] Getraenk getraenk)
        {
            if (ModelState.IsValid)
            {
                db.Getraenke.Add(getraenk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(getraenk);
        }

        // GET: Getraenk/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Getraenk getraenk = db.Getraenke.Find(id);
            if (getraenk == null)
            {
                return HttpNotFound();
            }
            return View(getraenk);
        }

        // POST: Getraenk/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GetraenkId,GetraenkName,GetraenkPreis,HeissesGetraenk,AlkoholischesGetraenk,GetraenkMenge")] Getraenk getraenk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(getraenk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(getraenk);
        }

        // GET: Getraenk/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Getraenk getraenk = db.Getraenke.Find(id);
            if (getraenk == null)
            {
                return HttpNotFound();
            }
            return View(getraenk);
        }

        // POST: Getraenk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Getraenk getraenk = db.Getraenke.Find(id);
            db.Getraenke.Remove(getraenk);
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
