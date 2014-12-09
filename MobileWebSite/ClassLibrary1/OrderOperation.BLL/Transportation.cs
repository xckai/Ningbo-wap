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
        public int Distribution_State; //物流状态
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
        public OrderStatus.orderStatusType orderStatuTypes; //订单状态类中的enumerate类型，表示订单的当前状态
        public Distribution.DistributionStatusType transpStatuTypes;
        private DistributionRepository transportRep = new DistributionRepository();
        private EnterpriseRepository enterpriseRep = new EnterpriseRepository();
        private OrderRepository orderRep = new OrderRepository();   //订单
        private OrderStatusRepository orderStatuRep = new OrderStatusRepository();  //订单状态
        public List<TransportListClass> GetTransportLists(int companyId, int category, int option)
        {
            // int distributionCreated = (int)Distribution.DistributionStatusType.DistributionCreated;
            //  int distributing = (int)Distribution.DistributionStatusType.Distributing;
            // int received = (int)Distribution.DistributionStatusType.Received;

            List<Distribution> transportList = new List<Distribution>();
            List<EnterpriseRepository> enterpriseList = new List<EnterpriseRepository>();
            List<Order> orderList = new List<Order>();
            //String str = "";
            //var tempTransportlist = new TransportListClass(); //物流简单信息
            var tempList = new List<TransportListClass>();

            //0代表发布方，1代表承接方
            if (category == 1)
            {
                if (option == 0)
                {
                    orderList = orderRep.LoadEntities((Order => Order.ProviderEnterprise_ID == companyId)).ToList(); //根据企业号找到订单
                    //orderList = orderRep.LoadEntities((Order => Order.PublisherEnterprise_ID == companyId)).ToList(); //根据企业号找到订单
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        var tempTransportlist = new TransportListClass(); //物流简单信息
                        transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID
                            == orderList.ElementAt(i).Order_ID)).ToList();   //根据找到的订单id找出物流订单
                        for (int j = 0; j < transportList.Count; j++)
                        {
                            //transportRep.LoadEntities(((Distribution => Distribution.Distribution_State)
                            //    == orderList.ElementAt(0).Order_ID)).ToList(); 
                            tempTransportlist.distributionId = transportList.ElementAt(j).Distribution_ID;
                            tempTransportlist.TransportNumber = orderList.ElementAt(i).Order_Code; // 物流单号

                            tempTransportlist.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                       == orderList.ElementAt(i).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)

                            tempTransportlist.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                        == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)

                            tempTransportlist.status = "配送单生成";  //物流状态

                            tempList.Add(tempTransportlist);
                        }
                        //return 
                    }
                    // return tempList;
                }
                else if (option == 1)
                {
                    orderList = orderRep.LoadEntities((Order => Order.ProviderEnterprise_ID == companyId)).ToList();  //根据订单号找到订单id
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        var tempTransportlist = new TransportListClass(); //物流简单信息
                        transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID
                            == orderList.ElementAt(i).Order_ID)).ToList();   //根据找到的订单id找出物流订单
                        for (int j = 0; j < transportList.Count; j++)
                        {
                            tempTransportlist.TransportNumber = orderList.ElementAt(i).Order_Code; // 物流单号

                            tempTransportlist.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                       == orderList.ElementAt(i).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)

                            tempTransportlist.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                        == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)

                            tempTransportlist.status = "物品已经发货";  //物流状态
                            tempList.Add(tempTransportlist);
                        }
                    }
                    // return tempList;
                    //return transportRep.LoadEntities((Distribution => Distribution.Order_ID == orderId)).ToList();
                }
                else if (option == 2)
                {
                    orderList = orderRep.LoadEntities((Order => Order.ProviderEnterprise_ID == companyId)).ToList();  //根据订单号找到订单id
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        var tempTransportlist = new TransportListClass(); //物流简单信息
                        transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID
                            == orderList.ElementAt(i).Order_ID)).ToList();   //根据找到的订单id找出物流订单
                        for (int j = 0; j < transportList.Count; j++)
                        {
                            tempTransportlist.TransportNumber = orderList.ElementAt(i).Order_Code; // 物流单号

                            tempTransportlist.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                       == orderList.ElementAt(i).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)

                            tempTransportlist.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                        == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)

                            tempTransportlist.status = "物品已经被签收";  //物流状态
                            tempList.Add(tempTransportlist);
                        }
                    }
                    // return transportRep.LoadEntities((Distribution => Distribution.Order_ID == orderId)).ToList();
                }
                //return tempList;
            }
            else if (category == 0)
            {
                if (option == 0)
                {
                    orderList = orderRep.LoadEntities((Order => Order.PublisherEnterprise_ID == companyId)).ToList(); //根据企业号找到订单
                    //orderList = orderRep.LoadEntities((Order => Order.PublisherEnterprise_ID == companyId)).ToList(); //根据企业号找到订单
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        var tempTransportlist = new TransportListClass(); //物流简单信息
                        transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID
                            == orderList.ElementAt(i).Order_ID)).ToList();   //根据找到的订单id找出物流订单
                        for (int j = 0; j < transportList.Count; j++)
                        {
                            //transportRep.LoadEntities(((Distribution => Distribution.Distribution_State)
                            //    == orderList.ElementAt(0).Order_ID)).ToList(); 
                            tempTransportlist.distributionId = transportList.ElementAt(j).Distribution_ID;
                            tempTransportlist.TransportNumber = orderList.ElementAt(i).Order_Code; // 物流单号

                            tempTransportlist.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                       == orderList.ElementAt(i).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)

                            tempTransportlist.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                        == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)

                            tempTransportlist.status = "配送单生成";  //物流状态

                            tempList.Add(tempTransportlist);
                        }
                        //return 
                    }
                    // return tempList;
                }
                else if (option == 1)
                {
                    orderList = orderRep.LoadEntities((Order => Order.PublisherEnterprise_ID == companyId)).ToList();  //根据订单号找到订单id
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        var tempTransportlist = new TransportListClass(); //物流简单信息
                        transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID
                            == orderList.ElementAt(i).Order_ID)).ToList();   //根据找到的订单id找出物流订单
                        for (int j = 0; j < transportList.Count; j++)
                        {
                            tempTransportlist.TransportNumber = orderList.ElementAt(i).Order_Code; // 物流单号

                            tempTransportlist.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                       == orderList.ElementAt(i).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)

                            tempTransportlist.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                        == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)

                            tempTransportlist.status = "物品已经发货";  //物流状态
                            tempList.Add(tempTransportlist);
                        }
                    }
                    //return tempList;
                    //return transportRep.LoadEntities((Distribution => Distribution.Order_ID == orderId)).ToList();
                }
                else if (option == 2)
                {
                    orderList = orderRep.LoadEntities((Order => Order.PublisherEnterprise_ID == companyId)).ToList();  //根据订单号找到订单id
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        var tempTransportlist = new TransportListClass(); //物流简单信息
                        transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID
                            == orderList.ElementAt(i).Order_ID)).ToList();   //根据找到的订单id找出物流订单
                        for (int j = 0; j < transportList.Count; j++)
                        {
                            tempTransportlist.TransportNumber = orderList.ElementAt(i).Order_Code; // 物流单号

                            tempTransportlist.consignor = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                       == orderList.ElementAt(i).ProviderEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(发货方)

                            tempTransportlist.consignee = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                        == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //根据找到的订单的企业编号查找企业的名称。(收货方)

                            tempTransportlist.status = "物品已经被签收";  //物流状态
                            tempList.Add(tempTransportlist);
                        }
                    }
                    // return transportRep.LoadEntities((Distribution => Distribution.Order_ID == orderId)).ToList();
                }
                //return tempList;
            }
            return tempList;
        }

        public List<TransportListDetailClass> GetTransportDetailByDistributionId(int distributionId)
        {
            List<Distribution> transportList = new List<Distribution>();
            var tempList = new List<TransportListDetailClass>();
            transportList = transportRep.LoadEntities((Distribution => Distribution.Distribution_ID == distributionId)).ToList();
            //transportList = transportRep.LoadEntities((Distribution => Distribution.Order_ID == distributionId)).ToList();
            for (int i = 0; i < transportList.Count; i++)
            {
                var transportDeta = new TransportListDetailClass();
                transportDeta.Created_Time = transportList.ElementAt(i).Created_Time; //
                transportDeta.Destination_Addr = transportList.ElementAt(i).Destination_Addr;
                transportDeta.Distribution_Amount = transportList.ElementAt(i).Distribution_Amount;
                transportDeta.Distribution_Content = transportList.ElementAt(i).Distribution_Content;
                transportDeta.Distribution_Download_Addr = transportList.ElementAt(i).Distribution_Download_Addr;
                transportDeta.Distribution_Name = transportList.ElementAt(i).Distribution_Name;

                transpStatuTypes = transportList.ElementAt(i).Distribution_State;
                if (transpStatuTypes.ToString().Equals("DistributionCreated"))
                {
                    transportDeta.Distribution_State = 0;
                }
                else if (transpStatuTypes.ToString().Equals("Distributing"))
                {
                    transportDeta.Distribution_State = 1;
                }
                else if (transpStatuTypes.ToString().Equals("Received"))
                {
                    transportDeta.Distribution_State = 2;
                }
                //transportDeta.Distribution_State = transpStatuTypes.ToString();

                transportDeta.distributionId = transportList.ElementAt(i).Distribution_ID;
                transportDeta.Receive_Time = transportList.ElementAt(i).Receive_Time;
                transportDeta.Send_Time = transportList.ElementAt(i).Send_Time;
                transportDeta.Source_Addr = transportList.ElementAt(i).Source_Addr;
                tempList.Add(transportDeta);
            }
            //return transportRep.LoadEntities((Distribution => Distribution.Distribution_ID == distributionId)).ToList();
            return tempList;
        }

        //通过供货企业获得当前的订单号
        public List<providerEnterpriseOrderClass> GetOrderIdByProviderEnterpriseid(int providerEnterpriseId, int orderState)
        {
            List<OrderStatus> orderStatu = new List<OrderStatus>();
            List<Distribution> transportList = new List<Distribution>();
            List<EnterpriseRepository> enterpriseList = new List<EnterpriseRepository>();
            List<Order> orderList = new List<Order>();
            //String str = "";
            //var tempTransportlist = new TransportListClass(); //物流简单信息
            var tempList = new List<providerEnterpriseOrderClass>();

            orderList = orderRep.LoadEntities((Order => Order.ProviderEnterprise_ID == providerEnterpriseId)).ToList();

            for (int i = 0; i < orderList.Count; i++)
            {
                //transportList = transportRep.LoadEntities(Distribution=>Distribution.Order_ID == orderList.ElementAt(i).Order_ID).ToList(); //查看数据表中是否已经存在相同的订单号
                orderStatu = orderStatuRep.LoadEntities((OrderStatus => OrderStatus.Order_ID == orderList.ElementAt(i).Order_ID)).ToList(); //获得当前的订单状态
                //if(OrderStatus.orderStatusType.orderDelivery == 0);             
                orderStatuTypes = orderStatu.ElementAt((orderStatu.Count - 1)).OrderStatus_Content; //获得订单当前状态
                string orderSta = orderStatuTypes.ToString(); // || orderSta.Equals("orderReceived")
                if (orderSta.Equals("orderProccessed") || orderSta.Equals("orderDelivery"))
                {
                    var provEnterp = new providerEnterpriseOrderClass();
                    provEnterp.ID = i + 1;
                    provEnterp.orderID = orderList.ElementAt(i).Order_ID;
                    provEnterp.publisherEnterpriseName = enterpriseRep.LoadEntities((EnterpriseRepository => EnterpriseRepository.Enterprise_ID
                                      == orderList.ElementAt(i).PublisherEnterprise_ID)).ToList().ElementAt(0).Enterprise_Name; //发布方企业名称，也就是收货方企业名称
                    provEnterp.publisherEnterpriseNameAndOrderId = "收货公司：" + provEnterp.publisherEnterpriseName + " 订单ID号：" + Convert.ToString(provEnterp.orderID);
                    tempList.Add(provEnterp);
                }
            }
            return tempList;
        }
        public List<Distribution> SearchTransportLists(string keywords)
        {
            return null;
        }
        //获取公司订单数量信息 
        // category 0代表订单发布方 1代表承接方
        //option 0 代表未完成的物流 1代表已完成的物流
        public int GetTransporationNum(int EnterpriseId, int category, int option)
        {
            if (category == 0)
            {
                int compelete = 0;
                int notcompelete = 0;
                try
                {
                    return 5;
                }
                catch (System.Exception ex)
                {
                    return 0;
                }

            }
            else if (category == 1)
            {

                int compelete = 0;
                int notcompelete = 0;
                try
                {
                    return 6;
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
