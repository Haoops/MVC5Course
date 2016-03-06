using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        // GET: EF
        public ActionResult Index()
        {
            var db = new FabricsEntities();

            db.Product.Add(new Product()
            {
                ProductName = "BMWII",
                Price = 100,
                Stock = 3,
                Active = true
            });

            db.SaveChanges();
            var data = db.Product.ToList();

            return View(data);
        }
    }
}