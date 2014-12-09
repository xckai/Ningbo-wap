using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileWebSite.BLL.OrderOperation.BLL;
namespace MobileWebSite.Controllers
{
    public class CpcMainHomeController : Controller
    {
        //
        // GET: /CpcMainHome/
        private int enterpriseId;
        private int userId;
        public CpcMainHomeController()
        {
            try
            {
                enterpriseId = (int)Session["enId"];
                userId = (int)Session["userId"];
            }
            catch (System.Exception ex)
            {
            	
            }

    }
        public ActionResult CpcMainHome()
        {
            var temporderop = new OrderOperation();
            var temptransop = new Transportation();
            ViewBag.getordernum = temporderop.GetOrderNum(enterpriseId, 1, 1);
            ViewBag.sendordernum = temporderop.GetOrderNum(enterpriseId, 0, 1);
            ViewBag.gettransporationnum = temptransop.GetTransporationNum(enterpriseId, 1, 1);
            ViewBag.sendtransporationnum = temptransop.GetTransporationNum(enterpriseId, 0, 1);
            ViewBag.ID = userId;
            ViewBag.Name = Session["enName"];
            ViewBag.UserName = "小明";

            return View();
        }

    }
}
