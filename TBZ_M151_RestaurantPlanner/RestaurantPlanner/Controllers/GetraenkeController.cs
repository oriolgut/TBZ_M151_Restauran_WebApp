using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RestaurantPlanner.DAL;
using RestaurantPlanner.Models;

namespace RestaurantPlanner.Controllers
{
    public class GetraenkeController : Controller
    {
        private readonly RestaurantPlannerContext _db = new RestaurantPlannerContext();

        // GET: Getraenke
        public ActionResult Index()
        {
            return View(_db.Getraenke.ToList());
        }

        // GET: Getraenke/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var getraenk = _db.Getraenke.Find(id);
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
        public ActionResult Create(Getraenk getraenk)
        {
            if (!ModelState.IsValid) return View(getraenk);
            var menu = _db.Menus.FirstOrDefault(x =>
                x.MenuName.Equals(getraenk.MenuZugehoerigkeit.MenuName,
                    StringComparison.InvariantCultureIgnoreCase));
            if (menu != null)
            {
                getraenk.MenuZugehoerigkeit = menu;
            }
            _db.Getraenke.Add(getraenk);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Getraenke/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var getraenk = _db.Getraenke.Find(id);
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
        public ActionResult Edit(Getraenk getraenk)
        {
            if (!ModelState.IsValid) return View(getraenk);

            var menu = _db.Menus.FirstOrDefault(x => x.MenuName.Equals(getraenk.MenuZugehoerigkeit.MenuName));
            if (menu == null)
            {
                _db.Menus.Add(getraenk.MenuZugehoerigkeit);
                _db.SaveChanges();
                menu = _db.Menus.First(x => x.MenuName.Equals(getraenk.MenuZugehoerigkeit.MenuName));
            }
            getraenk.MenuId = menu.MenuId;
            _db.Entry(getraenk).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Getraenke/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var getraenk = _db.Getraenke.Find(id);
            if (getraenk == null)
            {
                return HttpNotFound();
            }
            return View(getraenk);
        }

        // POST: Getraenke/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var getraenk = _db.Getraenke.Find(id);
            if (getraenk == null)
            {
                return HttpNotFound();
            }
            _db.Getraenke.Remove(getraenk);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
