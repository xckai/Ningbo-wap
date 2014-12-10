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
    interface IOrderOperation
    {
        //对各个状态进行分类
        string getStatus(int option);
        List<GetDatabaseNum> GetOrderLists(int companyId, int category, int option);
        //取得order的一个list 输入参数 companyid为公司对应的ID，option为取得order的不同类别具体代表如下
        //0取得所有list 
        //1取得已完成的订单
        //2取得未完成的订单
        List<GetOrderDetails> GetOrderDetailByOrderId(int category, int orderId);
        //通过Id获取order的详细信息（包含订单状态，订单的详细信息）
        List<GetDatabaseNum> GetOrderBySearch(int enterpriseid, int option, int category, string keywords);

        //取得订单的最近一项状态
        OrderStatus.orderStatusType GetLatestStatus(int orderid);

        string GetNextStatus(int orderid);

        //获取订单的所有状态
        List<OrderSta> GetOrderStatus(int orderid);

        //获取订单所有状态并加入下一个状态
        List<OrderSta> GetOrderStatus2(int orderid);
        //根据公司id查找相对应的chat
        List<GetChat> GetChartByCompanyId(int companyid);

    }
}
