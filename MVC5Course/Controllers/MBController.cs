using MVC5Course.Models;
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

        ////簡單資料繫結：強型別
        //[HttpPost]
        //public ActionResult Index(string Name, DateTime Birthday)
        //{
        //    return Content(Name + "  " + Birthday);
        //}

        //複雜資料繫結：強型別
        [HttpPost]
        public ActionResult Index(MemberViewModel data)
        {
            return Content(data.Name + "  " + data.Birthday);
        }

    }
}