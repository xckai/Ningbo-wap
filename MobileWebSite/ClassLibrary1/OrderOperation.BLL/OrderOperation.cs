using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPCApp.Data.Model;
using CPCApp.Data.DAL;
using CPCApp.Data.IDAL;

namespace MobileWebSite.BLL.OrderOperation.BLL
{
    public class GetDatabaseNum
    {
        public int orderID;
        public string orderNum;
        public string orderStatus;
        public string orderSupplier;
        public int category;
        public int partner;
    }
    public class GetOrderDetails
    {
        public int orderID;
        public string orderNum;
        public string orderSupplier;
        public int category;
        public string orderName;
        public string orderContent;
        public string orderTime;
        public int orderReceiver;
        public int orderSender;
    }
    public class GetChat
    {
        public string chatTime;
        public string charSender;
        public string chatContent;
        public string chatReceiver;
    }
    public class OrderSta
    {
        public string orderTime;
        public int status;
        public string statusContent;
    }
    public class OrderOperation : IOrderOperation
    {
        private OrderRepository orderRep = new OrderRepository();
        private EnterpriseRepository enterRep = new EnterpriseRepository();
        private OrderStatusRepository statusRep = new OrderStatusRepository();
        private DistributionRepository disRep = new DistributionRepository();
        private ChatRecordRepository chatRep = new ChatRecordRepository();

        public string getStatus(int option)
        {
            switch (option)
            {
                case 0:
                //return "未完成";
                case 1:
                //return "未完成";
                case 2:
                //return "未完成";
                case 3:
                //return "未完成";
                case 4:
                //return "未完成";
                case 5:
                    return "未完成";
                case 6:
                    return "完成";
                default:
                    return null;
            }
        }
        //根据status content id 返回相应的状态内容
        public string getStatusContent(int option)
        {
            switch (option)
            {
                case 0:
                    return "合同未签订";
                case 1:
                    return "合同签订";
                case 2:
                    return "订单正在生产";
                case 3:
                    return "订单生产完成";
                case 4:
                    return "配送完成";
                case 5:
                    return "客户全部签收";
                case 6:
                    return "客户已评论";
                default:
                    return null;
            }
        }

        //根据combox选项获取相应公司的各个状态的订单，option表示combox选取的值
        public List<GetDatabaseNum> GetOrderLists(int companyId, int category, int option)
        {
            //0代表发布方，1代表承接方
            List<Order> orderlist;
            var templist = new List<GetDatabaseNum>();  //满足条件的订单
            var tempAllList = new List<GetDatabaseNum>();  //返回所有订单，设置option=7

            if (category == 0)
            {
                orderlist = orderRep.LoadEntities((Order => Order.PublisherEnterprise_ID == companyId)).ToList();
            }
            else
            {
                orderlist = orderRep.LoadEntities((Order => Order.ProviderEnterprise_ID == companyId)).ToList();
            }

            for (int i = 0; i < orderlist.Count; i++)
            {
                GetDatabaseNum getdata = new GetDatabaseNum();
                getdata.orderID = orderlist.ElementAt(i).Order_ID;
                if (category == 0)
                {
                    getdata.category = 1;
                    getdata.partner = orderlist.ElementAt(i).ProviderEnterprise_ID;
                }
                else if (category == 1)
                {
                    getdata.category = 0;
                    getdata.partner = orderlist.ElementAt(i).PublisherEnterprise_ID;
                }
                var statuslist = statusRep.LoadEntities((OrderStatus => OrderStatus.Order_ID == getdata.orderID)).ToList();
                int statusnum = statuslist.Count - 1;
                getdata.orderNum = orderlist.ElementAt(i).Order_Code;
                //getdata.orderStatus = getStatus((int)statuslist[statusnum].OrderStatus_Content);
                getdata.orderStatus = getStatusContent((int)statuslist[statusnum].OrderStatus_Content);
                var enterlist = enterRep.LoadEntities((Enterprises => Enterprises.Enterprise_ID == getdata.partner)).ToList();
                getdata.orderSupplier = enterlist.ElementAt(0).Enterprise_Name;
                int orderStatContent = (int)statuslist[statusnum].OrderStatus_Content;
                if (orderStatContent == option)
                {
                    templist.Add(getdata);
                }
                tempAllList.Add(getdata);
            }
            if (option == 7)
            {
                return tempAllList;
            }
            else
            {
                return templist;
            }
        }

        /// <summary>
        /// 获取订单的详细信息
        /// </summary>
        /// <param name="category"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<GetOrderDetails> GetOrderDetailByOrderId(int category, int orderId)
        {
            var orderlist = orderRep.LoadEntities((Orders => Orders.Order_ID == orderId)).ToList();
            var templist = new List<GetOrderDetails>();
            var enterlist = new List<Enterprise>();
            GetOrderDetails orderDetails = new GetOrderDetails();
            int supplierid = 0;

            orderDetails.orderID = orderlist.ElementAt(0).Order_ID;
            orderDetails.orderName = orderlist.ElementAt(0).Order_Name;
            if (category == 0)
            {
                supplierid = orderlist.ElementAt(0).ProviderEnterprise_ID;
                enterlist = enterRep.LoadEntities((Enterprises => Enterprises.Enterprise_ID == supplierid)).ToList();
                orderDetails.category = 1;
                orderDetails.orderReceiver = orderlist.ElementAt(0).ProviderEnterprise_ID;
                orderDetails.orderSender = orderlist.ElementAt(0).PublisherEnterprise_ID;
            }
            else if (category == 1)
            {
                supplierid = orderlist.ElementAt(0).PublisherEnterprise_ID;
                enterlist = enterRep.LoadEntities((Enterprises => Enterprises.Enterprise_ID == supplierid)).ToList();
                orderDetails.category = 0;
                orderDetails.orderReceiver = orderlist.ElementAt(0).PublisherEnterprise_ID;
                orderDetails.orderSender = orderlist.ElementAt(0).ProviderEnterprise_ID;
            }
            orderDetails.orderSupplier = enterlist.ElementAt(0).Enterprise_Name;
            orderDetails.orderTime = orderlist.ElementAt(0).Order_Time;
            orderDetails.orderNum = orderlist.ElementAt(0).Order_Code;
            orderDetails.orderContent = orderlist.ElementAt(0).Order_Content;
            templist.Add(orderDetails);

            return templist;

            //if (category == 0)
            //{
            //    var templist = new List<GetOrderDetails>();
            //    GetOrderDetails orderDetails = new GetOrderDetails();
            //    orderDetails.category = 0;
            //    orderDetails.orderID = orderlist.ElementAt(0).Order_ID;
            //    orderDetails.orderName = orderlist.ElementAt(0).Order_Name;
            //    orderDetails.orderReceiver = orderlist.ElementAt(0).ProviderEnterprise_ID;
            //    orderDetails.orderSender = orderlist.ElementAt(0).PublisherEnterprise_ID;
            //    orderDetails.orderSupplier = enterlist.ElementAt(0).Enterprise_Name;
            //    orderDetails.orderTime = orderlist.ElementAt(0).Order_Time;
            //    orderDetails.orderNum = orderlist.ElementAt(0).Order_Code;
            //    orderDetails.orderContent = orderlist.ElementAt(0).Order_Content;
            //    templist.Add(orderDetails);
            //    return templist;
            //}
            //else
            //{
            //    var templist = new List<GetOrderDetails>();
            //    GetOrderDetails orderDetails = new GetOrderDetails();
            //    orderDetails.category = 1;
            //    orderDetails.orderID = orderlist.ElementAt(0).Order_ID;
            //    orderDetails.orderName = orderlist.ElementAt(0).Order_Name;
            //    orderDetails.orderSupplier = enterlist.ElementAt(0).Enterprise_Name;
            //    orderDetails.orderReceiver = orderlist.ElementAt(0).PublisherEnterprise_ID;
            //    orderDetails.orderSender = orderlist.ElementAt(0).ProviderEnterprise_ID;
            //    orderDetails.orderTime = orderlist.ElementAt(0).Order_Time;
            //    orderDetails.orderNum = orderlist.ElementAt(0).Order_Code;
            //    orderDetails.orderContent = orderlist.ElementAt(0).Order_Content;
            //    templist.Add(orderDetails);
            //    return templist;
            //}
        }

        //获取订单的最近一项状态
        public OrderStatus.orderStatusType GetLatestStatus(int orderid)
        {
            var statuslist = statusRep.LoadEntities((OrderStatus => OrderStatus.Order_ID == orderid)).ToList();
            int statusnum = statuslist.Count - 1;
            OrderStatus.orderStatusType orderStatContent = statuslist[statusnum].OrderStatus_Content;
            return orderStatContent;
        }

        //获取订单的下一个状态
        public string GetNextStatus(int orderid)
        {
            var statuslist = statusRep.LoadEntities((OrderStatus => OrderStatus.Order_ID == orderid)).ToList();
            int statusnum = statuslist.Count;
            return getStatusContent(statusnum);
        }

        //获取订单的所有状态变化
        public List<OrderSta> GetOrderStatus(int orderid)
        {
            var orderlist = orderRep.LoadEntities((Orders => Orders.Order_ID == orderid)).ToList();
            var statuslist = statusRep.LoadEntities((OrderStatus => OrderStatus.Order_ID == orderid)).ToList();
            var templist = new List<OrderSta>();
            for (int i = statuslist.Count - 1; i >= 0; i--)
            {
                OrderSta orderS = new OrderSta();
                orderS.orderTime = statuslist.ElementAt(i).OrderStatus_Time;
                orderS.status = (int)statuslist.ElementAt(i).OrderStatus_Content;
                orderS.statusContent = getStatusContent(orderS.status);
                templist.Add(orderS);
            }
            ////返回下一个状态
            //OrderSta orderStat = new OrderSta();
            //orderStat.orderTime = "0";
            //orderStat.status = 0;
            //orderStat.statusContent = GetNextStatus(orderid);
            //templist.Add(orderStat);
            return templist;
        }
        public List<OrderSta> GetOrderStatus2(int orderid)
        {
            var orderlist = orderRep.LoadEntities((Orders => Orders.Order_ID == orderid)).ToList();
            var statuslist = statusRep.LoadEntities((OrderStatus => OrderStatus.Order_ID == orderid)).ToList();
            var templist = new List<OrderSta>();
            for (int i = 0; i < statuslist.Count; i++)
            {
                OrderSta orderS = new OrderSta();
                orderS.orderTime = statuslist.ElementAt(i).OrderStatus_Time;
                orderS.status = (int)statuslist.ElementAt(i).OrderStatus_Content;
                orderS.statusContent = getStatusContent(orderS.status);
                templist.Add(orderS);
            }
            //返回下一个状态
            OrderSta orderStat = new OrderSta();
            orderStat.orderTime = "0";
            orderStat.status = 0;
            orderStat.statusContent = GetNextStatus(orderid);
            templist.Add(orderStat);

            return templist;
        }

        //获取某个公司的对话
        public List<GetChat> GetChartByCompanyId(int companyid)
        {
            var chatlist = chatRep.LoadEntities((ChatRecords => ChatRecords.SendEnterprise_ID == companyid)).ToList();
            var enterlist = enterRep.LoadEntities((Enterprises => Enterprises.Enterprise_ID == companyid)).ToList();

            var templist = new List<GetChat>();
            for (int i = 0; i < chatlist.Count; i++)
            {
                GetChat chat = new GetChat();
                chat.charSender = enterlist.ElementAt(0).Enterprise_Name;
                chat.chatContent = chatlist.ElementAt(i).Chat_Content;
                chat.chatTime = chatlist.ElementAt(i).Chat_Time;
                int receiver = chatlist.ElementAt(i).ReceiveEnterprise_ID;
                var receivername = enterRep.LoadEntities((Enterprises => Enterprises.Enterprise_ID == receiver)).ToList();
                chat.chatReceiver = receivername.ElementAt(0).Enterprise_Name;
                templist.Add(chat);
            }
            return templist;
        }
        public List<GetDatabaseNum> GetOrderBySearch(int enterpriseid, int option, int category, string keywords)
        {
            var templist = GetOrderLists(enterpriseid, category, option);
            var temp_GetList = new List<GetDatabaseNum>();
            foreach (var temp in templist)
            {
                if ((temp.orderNum).ToString().Contains(keywords) || (temp.orderSupplier).ToString().Contains(keywords))
                {
                    var temp_new = new GetDatabaseNum();
                    temp_new = temp;
                    temp_GetList.Add(temp_new);
                }
            }
            return temp_GetList;
        }

        //获取公司订单数量信息 
        // category 0代表订单发布方 1代表承接方
        //option 0 代表未完成的订单 1代表已完成的订单
        public int GetOrderNum(int EnterpriseId, int category, int option)
        {
            var tempOrderList = new List<Order>();
            int compelete = 0;
            int notcompelete = 0;
            try
            {
                if (category == 0)
                {
                    tempOrderList = orderRep.LoadEntities(Order => Order.PublisherEnterprise_ID == EnterpriseId).ToList();
                }
                else if (category == 1)
                {
                    tempOrderList = orderRep.LoadEntities(Order => Order.ProviderEnterprise_ID == EnterpriseId).ToList();
                }
                foreach (var tempOrder in tempOrderList)
                {
                    var statuslist = statusRep.LoadEntities(OrderStatus => OrderStatus.Order_ID == tempOrder.Order_ID).ToList();

                    int lastStatus = (int)statuslist.LastOrDefault().OrderStatus_Content;
                    if (lastStatus == 6)
                    {
                        compelete++;

                    }
                    else
                    {
                        notcompelete++;
                    }
                }
                if (option == 0)
                {
                    return notcompelete;
                }
                else
                {
                    return compelete;
                }
            }
            catch (System.Exception ex)
            {
                return 0;
            }         
        }
    }
}

