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
    public class GetraenkeController : Controller
    {
        private RestaurantPlannerContext db = new RestaurantPlannerContext();

        // GET: Getraenke
        public ActionResult Index()
        {
            return View(db.Getraenke.ToList());
        }

        // GET: Getraenke/Details/5
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

        // GET: Getraenke/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Getraenke/Create
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

        // GET: Getraenke/Edit/5
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

        // POST: Getraenke/Edit/5
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

        // GET: Getraenke/Delete/5
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

        // POST: Getraenke/Delete/5
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
