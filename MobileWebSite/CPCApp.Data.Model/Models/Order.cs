
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Order
    {
        [Key]
        [DisplayName("订单主键")]
        public int Order_ID { get; set; }

        [Required]
        [DisplayName("发布企业主键")]
        public int PublisherEnterprise_ID { get; set; }
        [Required]
        [DisplayName("服务企业主键")]
        public int ProviderEnterprise_ID { get; set; }
        [DisplayName("发布上生成合同用户")]
        public int PublisherUser_ID { get; set; }
        [DisplayName("服务商签订合同用户")]
        public int ProviderUser_ID  { get; set; }
        public string Order_Name { get; set; }
        [DisplayName("上级订单主键")]
        public Nullable<int> PIDOrder_ID { get; set; }
        [DisplayName("订单编号")]
        public string Order_Code { get; set; }
        [DisplayName("合同文件地址")]
        public string Contract_Addr { get; set; }

        [DisplayName("合同文件浏览地址")]
        public string Contract_Browse_Addr { get; set; }
        [DisplayName("合同下载地址")]
        public string Contract_Download_Addr { get; set; }
        [DisplayName("合同修改建议")]
        public string Contact_Suggestion { get; set; }

        [Required]
        [DisplayName("订单内容")]
        public string Order_Content { get; set; }

        [Required]
        [DisplayName("订单生成时间")]
        public string Order_Time { get; set; }
        [DisplayName("是否有子订单")]
        public bool Sub_Orders { get; set; }
        [Required]
        [DisplayName("发布商删除订单")]
        public bool Publisher_Del { get; set; }

        [Required]
        [DisplayName("服务商删除订单")]
        public bool Provider_Del { get; set; }   
        [DisplayName("是否已经阅读")]
        public bool ReadOrNot { get; set; }//为false时表示未读，为true时表示已读。
        //导航属性
        public Enterprise Publisher { get; set; }
        public Enterprise Provider { get; set; }
        public Order PID { get; set; }
     
    }
}
