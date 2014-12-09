using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class OrderStatus
    {
        public enum orderStatusType
        {
          contractUnsinged=0,//合同未签订
          contractSinged=1,//合同签订
          orderProccessing=2,//订单正在生产
          orderProccessed=3,//订单生产完成，可显示配送按钮
          orderDelivery = 4,//配送完成，可显示配送按钮
          orderReceived=5,//客户全部签收
          orderCommented=6//客户已评论
        }
        [Key]
        [DisplayName("订单状态主键")]
        public int OrderStatus_ID { get; set; }

        [Required]
        [DisplayName("订单主键")]
        public int Order_ID { get; set; }

        [Required]
        [DisplayName("订单状态内容")]
        public orderStatusType OrderStatus_Content { get; set; }
        [Required]
        [DisplayName("订单状态时间")]
        public string OrderStatus_Time { get; set; }
        public Order Order { get; set; }//导航属性
    }
}
