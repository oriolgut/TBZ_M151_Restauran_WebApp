using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RestaurantPlanner.DAL;
using RestaurantPlanner.Models;

namespace RestaurantPlanner.Controllers
{
    public class GerichteController : Controller
    {
        private readonly RestaurantPlannerContext _db = new RestaurantPlannerContext();

        // GET: Gerichte
        public ActionResult Index()
        {
            return View(_db.Gerichte.ToList());
        }

        // GET: Gerichte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gericht = _db.Gerichte.Find(id);
            if (gericht == null)
            {
                return HttpNotFound();
            }
            return View(gericht);
        }

        // GET: Gerichte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gerichte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gericht gericht)
        {
            if (!ModelState.IsValid) return View(gericht);
            var menu = _db.Menus.FirstOrDefault(x =>
                x.MenuName.Equals(gericht.MenuZugehoerigkeit.MenuName,
                    StringComparison.InvariantCultureIgnoreCase));
            if (menu != null)
            {
                gericht.MenuZugehoerigkeit = menu;
            }
            _db.Gerichte.Add(gericht);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Gerichte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gericht = _db.Gerichte.Find(id);
            if (gericht == null)
            {
                return HttpNotFound();
            }
            return View(gericht);
        }

        // POST: Gerichte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gericht gericht)
        {
            if (!ModelState.IsValid) return View(gericht);
            var menu = _db.Menus.FirstOrDefault(x => x.MenuName.Equals(gericht.MenuZugehoerigkeit.MenuName));
            if (menu == null)
            {
                _db.Menus.Add(gericht.MenuZugehoerigkeit);
                _db.SaveChanges();
                menu = _db.Menus.First(x => x.MenuName.Equals(gericht.MenuZugehoerigkeit.MenuName));
            }
            gericht.MenuId = menu.MenuId;
            _db.Entry(gericht).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Gerichte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gericht = _db.Gerichte.Find(id);
            if (gericht == null)
            {
                return HttpNotFound($"Das Gericht mit der id: {id} wurde nicht gefunden.");
            }
            return View(gericht);
        }

        // POST: Gerichte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gericht = _db.Gerichte.Find(id);
            if (gericht == null)
            {
                return HttpNotFound($"Das Gericht mit der id: {id} wurde nicht gefunden.");
            }
            _db.Gerichte.Remove(gericht);
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
