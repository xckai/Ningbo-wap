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
            enterpriseId  = int.Parse(Session["enId"].ToString().Trim());   //获得当前session中的企业id      
            userId = int.Parse(Session["userId"].ToString().Trim());   //获得当前session中的企业id      
            var temporderop = new OrderOperation();
            var temptransop = new Transportation();
            ViewBag.getordernum = temporderop.GetOrderNum(enterpriseId, 1, 0);
            ViewBag.sendordernum = temporderop.GetOrderNum(enterpriseId, 0, 0);
            int getTransNum = temptransop.GetTransporationNum(enterpriseId, 1, 0);
            ViewBag.gettransporationnum = getTransNum;
            int sendTransNum = temptransop.GetTransporationNum(enterpriseId, 0, 0);
            ViewBag.sendtransporationnum = sendTransNum;

            ViewBag.ID = userId;
            ViewBag.Name = Session["enName"];
            ViewBag.UserName = "小明";

            return View();
        }

    }
}
