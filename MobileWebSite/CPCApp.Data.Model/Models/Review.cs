using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Review
    {

        [Key]
        [DisplayName("评价主键")]
        public int Review_ID { get; set; }
        [DisplayName("订单主键")]
        public int Order_ID{get;set;}
        [Required]
        [DisplayName("被评价企业主键")]
        public int ReceiveEnterprise_ID { get; set; }//被评价企业

        [Required]
        [DisplayName("评价企业主键")]
        public int PublishEnterprise_ID { get; set; }//评价企业

        [Required]
        [DisplayName("评价内容")]

        public string Review_Content { get; set; }

        public enum reviewclass
        {
            publisher, provider
        }

        [Required]
        [DisplayName("评价类型")]
        public reviewclass Review_Class { get; set; } //被评价对象

        [Required]
        [DisplayName("评价时间")]
        public string Review_Time { get; set; }

        //导航属性
        public Enterprise Receive { get; set; }
        public Enterprise Publish { get; set; }
        public Order Order { get; set; }
    }
}
