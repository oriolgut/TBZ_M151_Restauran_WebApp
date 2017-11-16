using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantPlanner.DAL;
using RestaurantPlanner.Models;

namespace RestaurantPlanner
{
    public static class MenuHelper
    {
        private static readonly RestaurantPlannerContext _dbContext = new RestaurantPlannerContext();
        public static List<string> GetMenus()
        {
            return _dbContext.Menus.Select(x => x.MenuName).ToList();
        }
    }
}