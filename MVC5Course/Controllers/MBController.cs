using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : Controller
    {
        // GET: MB
        public ActionResult Index()
        {
            return View();
        }

        //資料繫結：強型別
        [HttpPost]
        public ActionResult Index(string Name, DateTime Birthday)
        {
            return Content(Name + "  " + Birthday);
        }

        ////資料繫結：弱型別
        //[HttpPost]
        //public ActionResult Index(FormCollection form)
        //{
        //    return Content(form["Name"] + "  " + form["Birthday"]);
        //}

        ////資料繫結：Request.Form 取得 Request From上的物件名稱內容，極度不建議使用此作法
        //[HttpPost]
        //public ActionResult Index(string a)
        //{
        //    return Content(Request.Form["Name"] + "  " + Request.Form["Birthday"]);
        //}
    }
}