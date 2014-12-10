using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPCApp.Data.Model;
using CPCApp.Data.IDAL;
using CPCApp.Data.DAL;
using MobileWebSite.BLL.OrderOperation.BLL;


namespace MobileWebSite.Controllers
{


    public class OrderDemoController : Controller
    {
        private CPCAppDataContext db = new CPCAppDataContext();
        private OrderOperation orderoper = new OrderOperation();

        //0代表发布方，1代表承接方
        // GET: /OrderDemo/
       // public  int category=0;
        public ActionResult Index(int category)
        {
            ViewBag.category = category;
            return View();
        }
        //根据orderid来获取订单详情
        public ActionResult OrderDetails(int orderId, int category)
        {
            var tempList = orderoper.GetOrderDetailByOrderId(category, orderId);
             ViewBag.OrderNum = tempList[0].orderNum;
             ViewBag.OrderName = tempList[0].orderName;
             ViewBag.OrderSupplier = tempList[0].orderSupplier;
             ViewBag.OrderContent = tempList[0].orderContent;
             ViewBag.OrderTime = tempList[0].orderTime;

             ViewBag.Category = category;
             ViewBag.OrderId = orderId;


             var tempListStatus = orderoper.GetOrderStatus(orderId);
             ViewBag.templist = tempListStatus;
             var NextStatus = orderoper.GetNextStatus(orderId);
             ViewBag.NextSta = NextStatus;

            return View();
        }

     //通过combox查找相应的订单
        public JsonResult GetOrderList(int option,int category,string content)
        {
            //ViewBag.Category = category;
            //ViewBag.Select = option;
            int enterpriseID = int.Parse(Session["userId"].ToString().Trim());   //获得当前session中的企业id
            List<GetDatabaseNum> tempList;
            if (content == null)
            {
               tempList = orderoper.GetOrderLists(enterpriseID, category, option);
            }
            else
            {
             tempList = orderoper.GetOrderBySearch(enterpriseID, option,category, content); 
            }
            return Json(tempList, JsonRequestBehavior.AllowGet);
        }

        //通过搜索查找订单
        public JsonResult GetOrderBySearch(int option, int category, string content)
        {
            int enterpriseID = int.Parse(Session["userId"].ToString().Trim());
          

            List<GetDatabaseNum> tempList = orderoper.GetOrderBySearch(enterpriseID, category, option,content); 
            return Json(tempList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult OrderDetailed(int option)
        {
            var tempList = orderoper.GetOrderDetailByOrderId(0, option);
            return Json(tempList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult OrderChat()
        {
            var tempList = orderoper.GetChartByCompanyId(10001);
            return Json(tempList, JsonRequestBehavior.AllowGet);
        }
   
        //OrderChat2加入了total统计数据个数，rows具体的条目
        public JsonResult OrderChat2()
        {
            var tempList = orderoper.GetChartByCompanyId(10001);
            var jsonResult = new { total = tempList.Count(), rows = tempList };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        //获取所有的Status
        public JsonResult OrderStatusChange(int option)
        {
            var tempListStatus = orderoper.GetOrderStatus2(option);
           //var nextStatus= orderoper.GetNextStatus(option);
            var jsonResult = new { total = tempListStatus.Count(), rows = tempListStatus };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }



        //改变订单的状态
        public JsonResult changeStatus(int option)
        {
           
            OrderStatus request = new OrderStatus();
            request.Order_ID = option;
            request.OrderStatus_Time = DateTime.Now.ToString("f");
            request.OrderStatus_Content =orderoper.GetLatestStatus(option)+1;
            db.OrderStatus.Add(request);

            var tempList = new List<OrderStatus>();
            tempList.Add(request);
            int count = db.SaveChanges();
            if (count > 0)
            {
                //  this.ConvertFile(file,request.RequestDetectionID,uploadpath);
                return Json(tempList, JsonRequestBehavior.AllowGet); ;
            }

            return null;


        }
        public JsonResult AddChat(string comment)
        {
            ChatRecord request = new ChatRecord();
            request.Chat_Content = comment;
            request.ChatRecord_ID = 1;
           // request.Chat_Time = content[1];
            request.Chat_Time = DateTime.Now.ToString("f");
            request.ReceiveEnterprise_ID = 10002;
            request.Read = true;
            request.Receive_Enterprise_Del = false;
            request.Receive = null;
            request.Send = null;
            request.Send_Enterprise_Del = false;
            request.SendEnterprise_ID = 10001;
            
            var tempList=new List<ChatRecord>();
            tempList.Add(request);
            db.ChatRecords.Add(request);
            Console.WriteLine("dsaglsgjewhghewiohg");
             int count=db.SaveChanges();
                    if (count > 0)
                    {
                      //  this.ConvertFile(file,request.RequestDetectionID,uploadpath);
                        return   Json(tempList, JsonRequestBehavior.AllowGet);;
                    }
          
            return null;
        }

        public class InfoExchange
        {
            public string OrderInfoContent;
            public int Category;
            public string Info_Produced_Time;
        }
        public JsonResult GetInfoList(int option)//获得商家关于订单交流信息
        {
            List<OrderInfoExchange> myoie = RepositoryFactory.OrderInfoExchangeRepository.LoadEntities(OrderInfoExchange => OrderInfoExchange.Order_ID == option).OrderBy(OrderInfoExchange => OrderInfoExchange.Info_Produced_Time).ToList();
            List<InfoExchange> listie = new List<InfoExchange>();
            int Enterpriseid = Convert.ToInt32(Session["enId"]);
            foreach (var node in myoie)
            {
                InfoExchange ie = new InfoExchange();
                if (Enterpriseid == RepositoryFactory.EnterpriseRepository.LoadEntities(Enterprise => Enterprise.Enterprise_Name == node.Info_Enterprise_Name).FirstOrDefault().Enterprise_ID)
                    ie.Category = 1;
                else
                    ie.Category = 0;
                ie.Info_Produced_Time = node.Info_Produced_Time;
                ie.OrderInfoContent = node.OrderInfoContent;
                listie.Add(ie);
            }
            return Json(listie, JsonRequestBehavior.AllowGet);
        }
        public bool MessageAdd(string message, int option)
        {
            IOrderInfoExchangeRepository _IOrderInfoExchangeRepository = RepositoryFactory.OrderInfoExchangeRepository;
            OrderInfoExchange oie = new OrderInfoExchange();
            oie.Info_Produced_Time = DateTime.Now.ToString();
            int Enterpriseid = Convert.ToInt32(Session["enId"]);
            oie.Info_Enterprise_Name = RepositoryFactory.EnterpriseRepository.LoadEntities(Enterprise => Enterprise.Enterprise_ID == Enterpriseid).FirstOrDefault().Enterprise_Name;
            oie.Info_PID = 0;
            oie.OrderInfoContent = message;
            oie.Order_ID = option;
            return _IOrderInfoExchangeRepository.AddEntity(oie);
        }

    }
}
