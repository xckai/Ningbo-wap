using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileWebSite.BLL.OrderOperation.BLL;
using CPCApp.Data.Model;
using CPCApp.Data.DAL;
using CPCApp.Data.IDAL;

namespace MobileWebSite.Controllers
{
    //public class TransportListClass
    //{
    //    public int ID;
    //    public string TransportNumber; //物流编号
    //    public string consignor;  //发货方
    //    public string consignee; //收货方
    //    public string status; //状态
    //}

    //public class EnterpriseClass
    //{
    //    public int Enterprise_ID;
    //    public string Enterprise_Name; //物流编号
    //    public string enterprise_state;  //发货方
    //    //public string consignee; //收货方
    //    //public string status; //状态
    //}

    public class TransportationController : Controller
    {
        //
        // GET: /TransportDemo/
        private Transportation transportOper = new Transportation();
        private CPCAppDataContext db = new CPCAppDataContext();


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TransportDetails()
        {
            return View();
        }

        public ActionResult AddTransportList()
        {
            return View();
        }

        public ActionResult showAddSuccess()
        {
            return View();
        }
        /*获取订单列表*/
        public JsonResult GetTransportList(int category, int transportState)
        {
            //int distributionCreated = (int)Distribution.DistributionStatusType.DistributionCreated;
            //int distributing = (int)Distribution.DistributionStatusType.Distributing;
            //int received = (int)Distribution.DistributionStatusType.Received;   
            //ViewBag.ID = Session["userId"];
            //var userID = Session["userId"].ToString().Trim();   //获得当前session中的企业id
            var enterpriseID = int.Parse(Session["userId"].ToString().Trim());   //获得当前session中的企业id
            //category = 1;
            //transportState = 0;
            var tempTransportationLists = transportOper.GetTransportLists(enterpriseID, category, transportState);
            //var tempTransportlist = new TransportListClass();
            //var tempList = new List<TransportListClass>();
            //tempList.Add(temporderfromdatabase);
            //int in1 =  tempTransportationLists.ElementAt(0).distributionId;
            return Json(tempTransportationLists, JsonRequestBehavior.AllowGet);
        }

        /*获取订单详情, 根据订单号*/
        public JsonResult GetTransportDetails(int distributionId)
        {
            var tempTransportationDetail = transportOper.GetTransportDetailByDistributionId(distributionId);
            //tempTransportationDetail.Add(tempList) ;
            return Json(tempTransportationDetail, JsonRequestBehavior.AllowGet);
        }

        /*获取订单号*/
        public JsonResult GetOrderID()
        {
            int orderState = 0;
            int ProviderEnterpriseID = 10001;   //获得当前session中的企业id
            try { 
               if (!Session["userId"].Equals(null))
                {
                    ProviderEnterpriseID = int.Parse(Session["userId"].ToString().Trim());   //获得当前session中的企业id
                }
            }
            catch {
                ProviderEnterpriseID = 10001;
            }
            
            var tempOrderInfor = transportOper.GetOrderIdByProviderEnterpriseid(ProviderEnterpriseID, orderState);
            return Json(tempOrderInfor, JsonRequestBehavior.AllowGet);
        }


        /*向数据库中添加一条新的物流记录
         *
         * *
         */
        public ActionResult addTransportInforToDatabase()
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

            transportData.Created_Time = Request.Params["createdTime"];
            transportData.Send_Time = Request.Params["sendTime"];
            transportData.Order_ID = int.Parse(Request.Params.GetValues("orderID")[0]);
            transportData.Receive_Time = "";
            transportData.Distribution_Download_Addr = "1232132";

            db.Distribution.Add(transportData);
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

        /*
         *修改物流状态
         * *
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

    }

}
