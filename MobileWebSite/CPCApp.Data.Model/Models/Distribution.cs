using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCApp.Data.Model
{
    public partial class Distribution
    {
       public enum DistributionStatusType{
           DistributionCreated=0,//配送单生成
           Distributing=1,//发货
           Received=2,//签收
       }
        [Key]
        [Required]
        [DisplayName("订单配送主键")]
        public int Distribution_ID { get; set; } 
        [Required]
        [DisplayName("订单ID")]
        public int Order_ID { get; set; }//外键
        [Required]
        [DisplayName("配送单名称")]
        public string Distribution_Name { get; set; }
        [Required]
        [DisplayName("配送单简介")]
        public string Distribution_Content { get; set; }
        [Required]
        [DisplayName("配送单下载地址")]
        public string Distribution_Download_Addr { get; set; }
        [Required]
        [DisplayName("发货源地址")]
        public string Source_Addr { get; set; }
        [Required]
        [DisplayName("配送目的地址")]
        public string Destination_Addr { get; set; }
        [Required]
        [DisplayName("配送数量")]
        public int Distribution_Amount { get; set; }     
        [Required]      
        [DisplayName("配送状态")]
        public DistributionStatusType Distribution_State { get; set; }//配送单生成状态1，配送状态1代表发货未接收，2代表接收方已经接收
        [DisplayName("配送单生成时间")]
        public string Created_Time{get;set;}
        [DisplayName("发货时间")]
        public string Send_Time { get; set; }
        [DisplayName("收货时间")]
        public string Receive_Time { get; set; }

    }
}
