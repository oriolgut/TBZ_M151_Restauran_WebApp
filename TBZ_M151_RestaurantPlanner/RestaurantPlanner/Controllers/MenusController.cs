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
    public class MenusController : Controller
    {
        private readonly RestaurantPlannerContext _db = new RestaurantPlannerContext();

        // GET: Menus
        public ActionResult Index()
        {
            return View(_db.Menus.ToList());
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
