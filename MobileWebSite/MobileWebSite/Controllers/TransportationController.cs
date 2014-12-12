using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileWebSite.BLL.OrderOperation.BLL;
using CPCApp.Data.Model;
using CPCApp.Data.DAL;
using CPCApp.Data.IDAL;
using System.IO;
using System.Threading;
using MobileWebSite.BLL.ToPDF;
namespace MobileWebSite.Controllers
{
    public class TransportationController : Controller
    {
        //
        // GET: /TransportDemo/
        private Transportation transportOper = new Transportation();
        private CPCAppDataContext db = new CPCAppDataContext();

        /*获取订单列表*/
        public JsonResult GetTransportList(int category, string searchValue)  // category 0代表发布方，1代表承接方
        {
            var enterpriseID = 10001;
            try
            {
                enterpriseID = int.Parse(Session["userId"].ToString().Trim());   //获得当前session中的企业id               
            }
            catch
            {
                enterpriseID = 10001;   //设置一个默认的企业id，实际上线时需要做一个异常处理机制
            }

            List<TransportListClass> tempTransportationLists;
            if (searchValue.Equals("notsearch") || searchValue == "")
            {
                tempTransportationLists = transportOper.GetTransportLists(enterpriseID, category); //, transportState
            }
            else
            {
                tempTransportationLists = transportOper.GetTransportBySearch(enterpriseID, category, searchValue);
            }
            return Json(tempTransportationLists, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrderList(int orderID)
        {
            var tempOrderLists = transportOper.GetOrderListsByDistributionId(orderID);
            return Json(tempOrderLists, JsonRequestBehavior.AllowGet);
        }

        /*获取订单详情, 根据订单号*/
        public JsonResult GetTransportDetails(int distributionId)
        {
            //distributionId = 1;  //设置一个默认的企业id，实际上线时需要做一个异常处理机制
            var tempTransportDetailList = new List<TransportListDetailClass>();
            var tempTransportationDetail = transportOper.GetTransportDetailByDistributionId(distributionId);
            tempTransportDetailList.Add(tempTransportationDetail);
            return Json(tempTransportDetailList, JsonRequestBehavior.AllowGet);
        }

        /*获取订单号*/
        public JsonResult GetOrderID()
        {
            //int orderState = 0;
            int ProviderEnterpriseID = 10001;   //获得当前session中的企业id
            try
            {
                if (!Session["enId"].Equals(null))
                {
                    ProviderEnterpriseID = int.Parse(Session["enId"].ToString().Trim());   //获得当前session中的企业id
                }
            }
            catch
            {
                ProviderEnterpriseID = 10001;
            }
            var tempOrderInfor = transportOper.GetOrderIdByProviderEnterpriseid(ProviderEnterpriseID);
            return Json(tempOrderInfor, JsonRequestBehavior.AllowGet);
        }

        public List<Enterprise> GetEnterprise(int sender, int receiver, int catgory)
        {
            List<Enterprise> tempEnterprise = new List<Enterprise>();
            EnterpriseRepository enterpriseRep = new EnterpriseRepository();   //企业model
            //int ProviderEnterpriseID = int.Parse(Session["enId"].ToString().Trim());
            int tempenteprise;
            if (catgory == 0)
            {
                tempenteprise = receiver;
            }
            else if (catgory == 1)
            {
                tempenteprise = sender;
            }
            else
            {
                tempenteprise = int.Parse(Session["enId"].ToString().Trim());
            }
            tempEnterprise = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID == tempenteprise)).ToList();
            return tempEnterprise;
        }

        /*向数据库中添加一条新的物流记录
         *
         * *
         */
        public ActionResult addTransportInforToDatabase()
        {
            try
            {
                Distribution transportData = new Distribution();

                transportData.Distribution_Name = Request.Params["distributionName"];
                transportData.Source_Addr = Request.Params["sourceAddress"];
                transportData.Destination_Addr = Request.Params["distributionAddress"];
                transportData.Distribution_Amount = Convert.ToInt16(Request.Params["distributionAmount"]);
                transportData.Distribution_Content = Request.Params["distributionContent"];

                if (Request.Params.GetValues("distributionState").Equals("0"))
                {
                    transportData.Distribution_State = Distribution.DistributionStatusType.DistributionCreated;
                }
                else if (Request.Params.GetValues("distributionState").Equals("1"))
                {
                    transportData.Distribution_State = Distribution.DistributionStatusType.Distributing;
                }
                else if (Request.Params.GetValues("distributionState").Equals("2"))
                {
                    transportData.Distribution_State = Distribution.DistributionStatusType.Received;
                }
                transportData.Created_Time = DateTime.Now.ToString("f");
                // transportData.Created_Time = Request.Params["createdTime"];
                // transportData.Send_Time = Request.Params["sendTime"];
                transportData.Send_Time = DateTime.Now.ToString("f");
                //if (Request.Params["orderID"] != null)
                //{
                transportData.Order_ID = int.Parse(Request.Params.GetValues("orderID")[0]);
                //}
                //else
                //{
                //    transportData.Order_ID = -1;
                //}
                transportData.Receive_Time = "";
                //db.Distribution.Add(transportData);
                string addr = Session["enterpriseID"] + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";//企业ID+当前时间为订单时间

                string pdfpath = Path.Combine(HttpContext.Server.MapPath("../DistributionFile"), addr);
                transportData.Distribution_Download_Addr = "/DistributionFile/" + addr;
                db.Distribution.Add(transportData);
                int count = db.SaveChanges();
                if (!Directory.Exists(Server.MapPath("../DistributionFile")))
                {
                    Directory.CreateDirectory(Server.MapPath("../DistributionFile"));
                }
                string consignee = Request.Params["consignee"];
                int ProviderEnterpriseID = int.Parse(Session["enId"].ToString().Trim());
                DistributionCreation dis2pdf = new DistributionCreation(transportData, pdfpath, consignee, ProviderEnterpriseID);
                Thread thread = new Thread(dis2pdf.Createpdf);
                thread.IsBackground = true;
                thread.Start();
                return Content("1");  //提交成功
            }
            catch
            {
                return Content("0");
            }
        }



        /*
         *修改物流状态
         * *state 订单的转态， 1：发送物流 2：确认收货
         */
        public ActionResult changeTransportState(int state, int distributionId)
        {
            //db.Distribution.
            Distribution transportData = new Distribution();
            transportData = db.Distribution.Find(distributionId);
            if (state == 1)
            {
                transportData.Distribution_State = Distribution.DistributionStatusType.Distributing;
                transportData.Send_Time = DateTime.Now.ToString("f");
            }
            else if (state == 2)
            {
                transportData.Distribution_State = Distribution.DistributionStatusType.Received;
                transportData.Receive_Time = DateTime.Now.ToString("f");
            }
            int count = db.SaveChanges();
            if (count > 0)
            {
                //  this.ConvertFile(file,request.RequestDetectionID,uploadpath);
                return Content("1");  //提交成功
            }
            else
            {
                return Content("0");
            }
        }

        public ActionResult Index(int category, string searchValue)
        {
            ViewBag.Category = category;
            ViewBag.SearchValue = searchValue;
            return View();
        }
        public ActionResult TransportDetails(int distributionID, int category)
        {
            ViewBag.Category = category;
            ViewBag.DistributionID = distributionID;
            return View();
        }

        public ActionResult AddTransportList(int orderId, int Sender, int Receiver, int category)
        { //
            var provideEnterprise = GetEnterprise(Sender, Receiver, category);
            var publishEnterprise = GetEnterprise(Receiver, Sender, category);
            if (orderId != -1)
            {
                var tempOrderLists = transportOper.GetOrderListsByDistributionId(orderId);
                ViewBag.OrderName = tempOrderLists.ElementAt(0).orderName;
            }
            ViewBag.OrderId = orderId;
            ViewBag.Category = category;
            ViewBag.provideEnterpriseName = provideEnterprise.ElementAt(0).Enterprise_Name;
            ViewBag.provideEnterpriseAddress = provideEnterprise.ElementAt(0).Enterprise_Addr;
            ViewBag.publishEnterpriseName = publishEnterprise.ElementAt(0).Enterprise_Name;
            ViewBag.publishEnterpriseAddress = publishEnterprise.ElementAt(0).Enterprise_Addr;
            return View();
        }

        public ActionResult showAddSuccess(int msg)
        {
            ViewBag.Message = msg;
            return View();
        }
        public ActionResult distributionList(int orderId, int category)
        {
            ViewBag.OrderId = orderId;
            ViewBag.Catergory = category;
            return View();
        }
    }

}
