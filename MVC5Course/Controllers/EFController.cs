using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index(bool? IsActive, string Keyword)
        {
            var product = new Product()
            {
                ProductName = "BMWII",
                Price = 120,
                Stock = 6,
                Active = true
            };

            //db.Product.Add(product);

            SaveChanges();

            //宣告變數 pkey 為新新增的資料的 ProductId
            var pkey = product.ProductId;

            // 取得 Product 全部的資料 ToList();
            // var data = db.Product.ToList();

            //﹝Linq﹞ 取得 Product 中資料與 pkey 相同的資料 ToList();
            //var data = db.Product.Where(p => p.ProductId == pkey).ToList();

            //﹝Linq﹞ 取得 Product 中全部的資料以 DESC 排序 ToList();
            //var data = db.Product.OrderByDescending(p => p.ProductId).ToList();

            //﹝Linq﹞ 取得 5筆 Product 中的資料以 DESC 排序 ToList();
            //var data = db.Product.OrderByDescending(p => p.ProductId).Take(5);

            //﹝Linq﹞ 取得 5筆 Product 中的資料以 DESC 排序 ToList();
            //var data = db.Product.Where(p => p.Active.HasValue ? p.Active.Value == IsActive : false).OrderByDescending(p => p.ProductId).Take(5);

            var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();

            //﹝判斷﹞是否有參數 Active
            if (IsActive.HasValue)
            {
                data = data.Where(p => p.Active.HasValue ? p.Active.Value == IsActive.Value : false);
            }

            //﹝判斷﹞是否有參數 Keyword
            if (!string.IsNullOrEmpty(Keyword))
            {
                data = data.Where(p => p.ProductName.Contains(Keyword));
            }

            ////data 中取得的資料 Price + 3
            //foreach (var item in data)
            //{
            //    item.Price = item.Price + 3;
            //}

            //SaveChanges();

            return View(data);
        }

        private void SaveChanges()
        {
            // SaveChanges();可能會發生錯誤，可以用以下 try 片段取得錯誤資訊
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    string entityName = item.Entry.Entity.GetType().Name;

                    foreach (DbValidationError err in item.ValidationErrors)
                    {
                        throw new Exception(entityName + " 類型驗證失敗: " + err.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public ActionResult Detail(int id)
        {
            //尋找 db.Product 中的特定id
            //var data = db.Product.Find(id);

            //﹝Linq﹞尋找 db.Product 中的特定id
            //var data = db.Product.Where(p => p.ProductId == id).FirstOrDefault();

            //﹝Linq﹞尋找 db.Product 中的特定id（與 82 相同，不同寫法）
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);

            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);

            #region 刪除資料與其關聯性的資料

            ////SLN 1.使用 Foreach 的方逐筆刪除 OrderLine 導覽屬性（Product:ProductId 與 OrderLine:ProductId 的關聯）
            //foreach (var ol in db.OrderLine.Where(p => p.ProductId == id).ToList())
            //{
            //    db.OrderLine.Remove(ol);
            //}

            ////SLN 2.使用 Foreach 的方逐筆刪除 OrderLine 導覽屬性（Product:ProductId 與 OrderLine:ProductId 的關聯）
            //foreach (var ol in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(ol);
            //}

            //SLN 3.Enitiy Framework 提供的API:RemoveRange，可刪除與其關聯的所有資料
            db.OrderLine.RemoveRange(product.OrderLine);
            #endregion

            db.Product.Remove(product);
            SaveChanges();

            return RedirectToAction("Index");
        }
    }
}