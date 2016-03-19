using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        // private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index(int? ProductId, string type, bool? isActive, int? minPrice, int? maxPrice, string keyWord)
        {

            // var data = db.Product.Where(p => !p.IsDelete).ToString();
            // var data = repo.All().Take(5);
            
            // Repo 取得全部的資料
            var data = repo.All(true);

            // 判斷 isActive 是否有傳值進來
            if (isActive.HasValue)
            {
                data = data.Where(p => p.Active.HasValue && p.Active == isActive.Value);
            }

            // 判斷 minPrice 是否有傳值進來
            if (minPrice.HasValue)
            {
                data = data.Where(p => p.Price.HasValue && p.Price >= minPrice.Value);
            }

            // 判斷 maxPrice 是否有傳值進來
            if (maxPrice.HasValue)
            {
                data = data.Where(p => p.Price.HasValue && p.Price <= maxPrice.Value);
            }

            // 判斷 keyWord 是否有傳值進來
            if (!string.IsNullOrEmpty(keyWord))
            {
                data = data.Where(p => p.ProductName.Contains(keyWord));
            }

            // 設定 View上的下拉式選單的選項
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Value = "true", Text = "有效" });
            items.Add(new SelectListItem() { Value = "false", Text = "無效" });
            ViewData["isActive"] = new SelectList(items, "Value", "Text");
            
            ViewBag.type = type;

            if (ProductId.HasValue)
            {
                ViewBag.SelectedProductId = ProductId.Value;
            }

            return View(data.Take(5));
        }

        [HttpPost]
        public ActionResult Index(IList<Products批次更新ViewModel> products)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in products)
                {
                    var product = repo.Find(item.ProductId);

                    product.Stock = item.Stock;
                    product.Price = item.Price;

                }

                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(repo.All().Take(5));
            }

        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Product product = db.Product.Find(id);
            Product product = repo.Find(id.Value);

            if (product == null && product.IsDelete)
            {
                return HttpNotFound();
            }

            // ViewData["OrderLine"] = product.OrderLine.ToList();
            ViewBag.OrderLines = product.OrderLine.ToList();

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                // db.Product.Add(product);
                repo.Add(product);
                // db.SaveChanges();
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Product product = db.Product.Find(id);
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            ////[Bind(Include = "ProductId,ProductName,Price,Active,Stock,IsDelete")] Product product
            //var db = (FabricsEntities)repo.UnitOfWork.Context;
            //db.Entry(product).State = EntityState.Modified;
            //db.SaveChanges();

            Product product = repo.Find(id);

            if (TryUpdateModel<Product>(product, new string[] {
                "ProductId","ProductName","Price","Stock"}))
            {
                repo.UnitOfWork.Commit();
                TempData["EditSuccess"] = "商品編輯成功！";

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Product product = db.Product.Find(id);
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Product product = db.Product.Find(id);
            Product product = repo.Find(id);
            // db.Product.Remove(product);
            product.IsDelete = true;
            //db.SaveChanges();
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (FabricsEntities)repo.UnitOfWork.Context;
                db.Dispose();

            }
            base.Dispose(disposing);
        }        
    }
}
