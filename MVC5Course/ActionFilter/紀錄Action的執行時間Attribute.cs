using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class 紀錄Action的執行時間Attribute : ActionFilterAttribute
    {
        // OnActionExecuting （Action 開始執行前會先執行的 ActionFilter）
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 紀錄執行開始時間

            base.OnActionExecuting(filterContext);
        }

        // OnActionExecuted （Action 執行完畢後會執行的 ActionFilter）
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // 紀錄執行結束時間

            // 計算執行時間

            base.OnActionExecuted(filterContext);
        }

    }
}