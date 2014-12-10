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
    interface ITransportation
    {
        List<TransportListClass> GetTransportLists(int companyId, int category);//, int option
        //取得物流状态的一个list， 输入参数orderId为订单对应的ID，option为取得order的不同类别具体代表如下
        //0配送单生成list 
        //1发货的订单
        //2签收的订单
        List<orderListClass> GetOrderLists(int orderID);
        List<TransportListDetailClass> GetTransportDetailByDistributionId(int distributionId);
        //通过Id获取物流的详细信息（包含物流状态，物流的详细信息）
        List<providerEnterpriseOrderClass> GetOrderIdByProviderEnterpriseid(int providerEnterpriseId, int orderState);
        //通过Id获取物流的详细信息（包含物流状态，物流的详细信息）
        List<TransportListClass> GetTransportBySearch(int companyId, int category, string keywords);
        //搜索内容获取所有的匹配信息（id，name，以及描述）
        //先搜索匹配的id，然后搜索订单名称 ，最后搜索描述拼接三个搜索结果返回
    }
}
