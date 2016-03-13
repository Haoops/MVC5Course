using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{

    [紀錄Action的執行時間]
    public class HomeController : Controller
    {
        [共用的VB資料共享於部分HomeController動作方法Attribute]
        public ActionResult Index()
        {
            return View();
        }

        [共用的VB資料共享於部分HomeController動作方法Attribute]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}