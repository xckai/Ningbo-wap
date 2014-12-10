using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPCApp.Data.Model;
using CPCApp.Data.DAL;
using CPCApp.Data.IDAL;

namespace MobileWebSite.BLL.OrderOperation.BLL
{
    //物流基本信息
    public class TransportListClass
    {
        public int distributionId; //
        // public int orderID;
        public string TransportNumber; //物流编号
        public string consignor;  //发货方
        public string consignee; //收货方
        public string status; //状态
    }

    public class orderListClass
    {
        public int distributionId; //
        public int orderID;
        public string TransportNumber; //物流编号
        public string consignor;  //发货方
        public string consignee; //收货方
        public string status; //状态
    }

    public class TransportListDetailClass
    {
        public int distributionId;
        public string Distribution_Name; //物流名称
        public string Distribution_Content;  //物流描述
        public string Distribution_Download_Addr; //下载地址
        public string Source_Addr; //发货地址
        public string Destination_Addr; //收货地址
        public int Distribution_Amount; //发货数量
        public string Distribution_State; //物流状态
        public string Created_Time; //创建时间
        public string Send_Time; //发货时间
        public string Receive_Time; //收货时间
    }

    // 供货方的订单信息
    public class providerEnterpriseOrderClass
    {
        public int ID;
        public int orderID;
        public string publisherEnterpriseName;
        public string publisherEnterpriseNameAndOrderId;
    }

    public class Transportation : ITransportation
    {
        private string DistributionCreated = "配送单生成";   //物流的状态，此时表示订单已经生成
        private string DistributionBeenSent = "物品已经发货"; //物流的状态，此时表示订单已经发货
        private string DistributionBeenReceived = "物品已经被签收";  //物流的状态，此时表示订单已经被签收

        private OrderStatus.orderStatusType orderStatuTypes; //订单状态类中的enumerate类型，表示订单的当前状态
        private Distribution.DistributionStatusType transpStatuTypes; //物流状态类中的enumerate类型，表示物流的当前状态

        private DistributionRepository transportRep = new DistributionRepository();  //物流model
        private EnterpriseRepository enterpriseRep = new EnterpriseRepository();   //企业model
        private OrderRepository orderRep = new OrderRepository();   //订单model
        private OrderStatusRepository orderStatuRep = new OrderStatusRepository();  //订单状态model

        private List<Distribution> transportList = new List<Distribution>();  //物流单列表
        private List<Order> orderList = new List<Order>();   //订单列表
        private List<OrderStatus> orderStatuList = new List<OrderStatus>(); //订单状态列表
        private List<EnterpriseRepository> enterpriseList = new List<EnterpriseRepository>(); //企业列表
        public List<orderListClass> GetOrderLists(int orderID)
        {
            var tempOrderList = new List<orderListClass>();
            transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID == orderID)).ToList();
            for (int i = 0; i < transportList.Count; i++)
            {
                var tempTempOrderlists = new orderListClass();
                tempTempOrderlists.orderID = transportList.ElementAt(i).Order_ID;
                tempTempOrderlists.distributionId = transportList.ElementAt(i).Distribution_ID;
                orderList = orderRep.LoadEntities((Order => Order.Order_ID == orderID)).ToList();
                tempTempOrderlists.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                      == orderList.ElementAt(0).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)
                tempTempOrderlists.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                         == orderList.ElementAt(0).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)
                transpStatuTypes = transportList.ElementAt(i).Distribution_State;
                if (transpStatuTypes.ToString().Equals("DistributionCreated"))
                {
                    tempTempOrderlists.status = DistributionCreated;
                }
                else if (transpStatuTypes.ToString().Equals("Distributing"))
                {
                    tempTempOrderlists.status = DistributionBeenSent;
                }
                else if (transpStatuTypes.ToString().Equals("Received"))
                {
                    tempTempOrderlists.status = DistributionBeenReceived;
                }
                tempTempOrderlists.TransportNumber = orderList.ElementAt(0).Order_Code;
                tempOrderList.Add(tempTempOrderlists);
            }
            return tempOrderList;
        }
        public List<TransportListClass> GetTransportLists(int companyId, int category)  //, int option
        {
            var tempTransportList = new List<TransportListClass>();
            //0代表发布方，1代表承接方
            if (category == 1)
            {
                orderList = orderRep.LoadEntities((Order => Order.ProviderEnterprise_ID == companyId)).ToList(); //根据企业号找到订单
                for (int i = 0; i < orderList.Count; i++)
                {
                    var tempTempTransportlist = new TransportListClass(); //物流简单信息
                    transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID
                        == orderList.ElementAt(i).Order_ID)).ToList();   //根据找到的订单id找出物流订单
                    for (int j = 0; j < transportList.Count; j++)
                    {
                        tempTempTransportlist.distributionId = transportList.ElementAt(j).Distribution_ID;
                        tempTempTransportlist.TransportNumber = orderList.ElementAt(i).Order_Code; // 物流单号
                        tempTempTransportlist.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                   == orderList.ElementAt(i).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)
                        tempTempTransportlist.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                    == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)
                        transpStatuTypes = transportList.ElementAt(j).Distribution_State;
                        if (transpStatuTypes.ToString().Equals("DistributionCreated"))
                        {
                            tempTempTransportlist.status = DistributionCreated;
                        }
                        else if (transpStatuTypes.ToString().Equals("Distributing"))
                        {
                            tempTempTransportlist.status = DistributionBeenSent;
                        }
                        else if (transpStatuTypes.ToString().Equals("Received"))
                        {
                            tempTempTransportlist.status = DistributionBeenReceived;
                        }
                        tempTransportList.Add(tempTempTransportlist);
                    }
                }
                return tempTransportList;
            }
            else if (category == 0)
            {
                orderList = orderRep.LoadEntities((Order => Order.PublisherEnterprise_ID == companyId)).ToList(); //根据企业号找到订单
                for (int i = 0; i < orderList.Count; i++)
                {
                    var tempTempTransportlist = new TransportListClass(); //物流简单信息
                    transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID
                        == orderList.ElementAt(i).Order_ID)).ToList();   //根据找到的订单id找出物流订单
                    for (int j = 0; j < transportList.Count; j++)
                    {
                        tempTempTransportlist.distributionId = transportList.ElementAt(j).Distribution_ID;
                        tempTempTransportlist.TransportNumber = orderList.ElementAt(i).Order_Code; // 物流单号
                        tempTempTransportlist.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                   == orderList.ElementAt(i).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)
                        tempTempTransportlist.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                    == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)
                        transpStatuTypes = transportList.ElementAt(j).Distribution_State;
                        if (transpStatuTypes.ToString().Equals("DistributionCreated"))
                        {
                            tempTempTransportlist.status = DistributionCreated;
                        }
                        else if (transpStatuTypes.ToString().Equals("Distributing"))
                        {
                            tempTempTransportlist.status = DistributionBeenSent;
                        }
                        else if (transpStatuTypes.ToString().Equals("Received"))
                        {
                            tempTempTransportlist.status = DistributionBeenReceived;
                        }
                        tempTransportList.Add(tempTempTransportlist);
                    }
                }
            }
            return tempTransportList;
        }

        //通过物流单号获得当前的所有的订单
        public List<TransportListDetailClass> GetTransportDetailByDistributionId(int distributionId)
        {
            var tempTransportDetailList = new List<TransportListDetailClass>();
            transportList = transportRep.LoadEntities((Distribution => Distribution.Distribution_ID
                == distributionId)).ToList();
            for (int i = 0; i < transportList.Count; i++)
            {
                var tempTempTransportDetail = new TransportListDetailClass();
                tempTempTransportDetail.Created_Time = transportList.ElementAt(i).Created_Time; //
                tempTempTransportDetail.Destination_Addr = transportList.ElementAt(i).Destination_Addr;
                tempTempTransportDetail.Distribution_Amount = transportList.ElementAt(i).Distribution_Amount;
                tempTempTransportDetail.Distribution_Content = transportList.ElementAt(i).Distribution_Content;
                tempTempTransportDetail.Distribution_Download_Addr = transportList.ElementAt(i).Distribution_Download_Addr;
                tempTempTransportDetail.Distribution_Name = transportList.ElementAt(i).Distribution_Name;

                transpStatuTypes = transportList.ElementAt(i).Distribution_State;
                // int lastStatus = (int)transportList.LastOrDefault().Distribution_State;
                if (transpStatuTypes.ToString().Equals("DistributionCreated"))
                {
                    tempTempTransportDetail.Distribution_State = DistributionCreated;
                }
                else if (transpStatuTypes.ToString().Equals("Distributing"))
                {
                    tempTempTransportDetail.Distribution_State = DistributionBeenSent;
                }
                else if (transpStatuTypes.ToString().Equals("Received"))
                {
                    tempTempTransportDetail.Distribution_State = DistributionBeenReceived;
                }
                tempTempTransportDetail.distributionId = transportList.ElementAt(i).Distribution_ID;
                tempTempTransportDetail.Receive_Time = transportList.ElementAt(i).Receive_Time;
                tempTempTransportDetail.Send_Time = transportList.ElementAt(i).Send_Time;
                tempTempTransportDetail.Source_Addr = transportList.ElementAt(i).Source_Addr;
                tempTransportDetailList.Add(tempTempTransportDetail);
            }
            return tempTransportDetailList;
        }

        //通过供货企业获得当前的订单号
        public List<providerEnterpriseOrderClass> GetOrderIdByProviderEnterpriseid(int providerEnterpriseId, int orderState)
        {
            var tempProviderEnterpriseList = new List<providerEnterpriseOrderClass>();
            orderList = orderRep.LoadEntities((Order => Order.ProviderEnterprise_ID == providerEnterpriseId)).ToList();
            for (int i = 0; i < orderList.Count; i++)
            {
                orderStatuList = orderStatuRep.LoadEntities((OrderStatus => OrderStatus.Order_ID
                    == orderList.ElementAt(i).Order_ID)).ToList(); //获得当前的订单状态      
                orderStatuTypes = orderStatuList.ElementAt((orderStatuList.Count - 1)).OrderStatus_Content; //获得订单当前状态
                string orderSta = orderStatuTypes.ToString(); // || orderSta.Equals("orderReceived")
                if (orderSta.Equals("orderProccessed") || orderSta.Equals("orderDelivery"))
                {
                    var tempProvEnterp = new providerEnterpriseOrderClass();
                    tempProvEnterp.ID = i + 1;
                    tempProvEnterp.orderID = orderList.ElementAt(i).Order_ID;
                    tempProvEnterp.publisherEnterpriseName = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                      == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //发布方企业名称，也就是收货方企业名称
                    tempProvEnterp.publisherEnterpriseNameAndOrderId =
                        "收货公司：" + tempProvEnterp.publisherEnterpriseName +
                        " 订单ID号：" + Convert.ToString(tempProvEnterp.orderID);
                    tempProviderEnterpriseList.Add(tempProvEnterp);
                }
            }
            return tempProviderEnterpriseList;
        }

        public List<TransportListClass> GetTransportBySearch(int companyId, int category, string keywords)
        {
            //GetTransportLists(int companyId, int category)
            var templist = GetTransportLists(companyId, category);
            var temp_GetList = new List<TransportListClass>();
            foreach (var temp in templist)
            {
                if (category == 1)
                {
                    if ((temp.TransportNumber).ToString().Contains(keywords)
                        || (temp.consignee).ToString().Contains(keywords)
                        || (temp.status).ToString().Contains(keywords))
                    {
                        var temp_new = new TransportListClass();
                        temp_new = temp;
                        temp_GetList.Add(temp_new);
                    }
                }
                else if (category == 0)
                {
                    if ((temp.TransportNumber).ToString().Contains(keywords)
                        || (temp.consignor).ToString().Contains(keywords)
                        || (temp.status).ToString().Contains(keywords))
                    {
                        var temp_new = new TransportListClass();
                        temp_new = temp;
                        temp_GetList.Add(temp_new);
                    }
                }
            }
            return temp_GetList;
        }

        //获取公司订单数量信息 
        //category 0代表订单发布方 1代表承接方
        //option 0 代表未完成的物流 1代表已完成的物流
        public int GetTransporationNum(int EnterpriseId, int category, int option)
        {
            
            if (category == 0)
            {
                int received = 0;
                int notreceived = 0;
                try
                {
                    var tempOrderList = orderRep.LoadEntities(Order => Order.ProviderEnterprise_ID
                        == EnterpriseId).ToList();
                    foreach (var tempTempOrder in tempOrderList)
                    {
                        var tempTranspotList = transportRep.LoadEntities((
                            Distribution => Distribution.Order_ID == tempTempOrder.Order_ID)).ToList();
                        foreach (var tempTempTranspot in tempTranspotList)
                        {
                            int lastStatus = (int)tempTranspotList.LastOrDefault().Distribution_State;
                            if (lastStatus == 2)
                            {
                                received++;
                            }
                            else
                            {
                                notreceived++;
                            }
                        }
                    }
                    if (option == 0)
                    {
                        return notreceived;
                    }
                    else
                    {
                        return received;
                    }
                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }
            else if (category == 1)
            {
                int received = 0;
                int notreceived = 0;
                try
                {
                    var tempOrderList = orderRep.LoadEntities(Order => Order.PublisherEnterprise_ID
                        == EnterpriseId).ToList();
                    foreach (var tempTempOrder in tempOrderList)
                    {
                        var tempTranspotList = transportRep.LoadEntities((
                            Distribution => Distribution.Order_ID == tempTempOrder.Order_ID)).ToList();
                        foreach (var tempTempTranspot in tempTranspotList)
                        {
                            int lastStatus = (int)tempTranspotList.LastOrDefault().Distribution_State;
                            if (lastStatus == 2)
                            {
                                received++;
                            }
                            else
                            {
                                notreceived++;
                            }
                        }
                    }
                    if (option == 0)
                    {
                        return notreceived;
                    }
                    else
                    {
                        return received;
                    }
                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
