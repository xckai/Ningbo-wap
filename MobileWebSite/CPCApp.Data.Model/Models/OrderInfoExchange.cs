using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCApp.Data.Model
{
   public class OrderInfoExchange
   {
       [Key]
       [DisplayName("订单信息交流主键")]
       public int Info_ID { get; set; }
       [Required]
       [DisplayName("订单ID")]
       public int Order_ID { get; set; }
       public string OrderInfoContent { get; set; }
       [Required]
       [DisplayName("订单信息企业名称")]
       public string Info_Enterprise_Name { get; set; }
       [Required]
       [DisplayName("订单信息产生时间")]
       public string Info_Produced_Time { get; set; }
       public int Info_PID { get; set; }
      
       //导航属性
       public Order Order { get; set; }
  
    }
}