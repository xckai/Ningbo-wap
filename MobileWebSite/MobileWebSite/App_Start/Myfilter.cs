using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWebSite.App_Start
{
    public class Myfilter: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (!filterContext.RouteData.Values.ContainsValue("Login") && !filterContext.RouteData.Values.ContainsValue("Logincheckuser"))
               {
                   if (filterContext.HttpContext.Session["isLogin"]==null)
                   {
                       filterContext.Result = new RedirectResult("~/CpcMain/Login");

                   }
               }
               
                

            
            }

    }
}