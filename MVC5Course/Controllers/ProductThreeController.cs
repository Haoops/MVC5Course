using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ProductThreeController : BaseController    
    {
        // GET: ProductThree
        public ActionResult Index()
        {
            var data = repo.All().Take(10);

            return View(data);
        }


        [HttpPost]
        public ActionResult Index(IList<Product> products)
        {

            foreach (var item in products)
            {
                var product = repo.Find(item.ProductId);
                product.ProductName = item.ProductName;
                product.Price = item.Price;
                product.Stock = item.Stock;
            }

            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }
    }
}