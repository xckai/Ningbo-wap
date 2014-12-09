using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Order_Demand
    {
        [Key]
        [DisplayName("订单需求关系主键")]
        public int Order_Demand_ID { get; set; }
        
        [Required]
        [DisplayName("需求主键")]
        public int Demand_ID { get; set; }

        [Required]
        [DisplayName("订单主键")]
        public int Order_ID { get; set; }
        [Required]
        [DisplayName("来自订单")]
        public bool ViewOrder_JudgeSubOrder { get; set; }//如果为false就是产生子订单时候用的判断该需求是否来自订单，如果为true从需求ViewOrder，为Flase就是SubOrder。
        public Order Order { get; set; }//导航属性
        public Demand Demand { get; set; }//导航属性
    }
}
