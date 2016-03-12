using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return PartialView("Index");
        }

        public ActionResult ContentTest()
        {
            // 此為範例；JavaScript 建議寫在 View 上
            // return Content("<script>alert('Redirecting ....');</script>","application/javascript", Encoding.UTF8);
            return JavaScript("alert(HIHI);");
        }

        public ActionResult FileTest()
        {
            // 強迫下載 : 指定第三個參數（DownloadFileName）
            // return File(Server.MapPath("~/Content/ago.png"), "image/png", "ItIsATestFile.png");

            // 顯示於頁面上
            return File(Server.MapPath("~/Content/ago.png"), "image/png");
        }

        public ActionResult JsonTest()
        {
            var db = new FabricsEntities();
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.Product.Take(3);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult RedirectTest()
        {
            // /Products/Edit/1604
            return RedirectToAction("Edit", "Products", new { id = 1604 });
        }

    }
}