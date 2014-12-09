using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCApp.Data.Model
{
  public partial  class ReturnGoods
    {
        [Key]
        [DisplayName("退货主键")]
        public int Return_ID { get; set; }
        [DisplayName("相关订单主键")]
        public int Order_ID { get; set; }
        [Required]
        [DisplayName("退货企业主键")]
        public int ReturnEnterprise_ID { get; set; }
        [Required]
        [DisplayName("接收企业主键")]
        public int ReceiveEnterprise_ID { get; set; }
        [DisplayName("退货单名称")]
        public string ReturnGoods_Name { get; set; }
        [Required]
        [DisplayName("退货企业地址")]
        public string Source_Addr { get; set; }
        [Required]
        [DisplayName("接收企业地址")]
        public string Destination_Addr { get; set; }
        [DisplayName("退货文件地址")]
        public string ReturnGoodsFile_Addr { get; set; }
        [Required]
        [DisplayName("退货数额")]
        public int Return_Amount { get; set; }
        [DisplayName("退货文件浏览地址")]
        public string ReturnGoodsFile_Browse_Addr { get; set; }
        
       [DisplayName("退货单文件下载地址")]
        public string DownloadFormFile_Addr { get; set; }
       [DisplayName("退货文件下载地址")]
       public string DownloadFile_Addr { get; set; }
        [Required]
        [DisplayName("退货单简介")]
        public string ReturnGoods_Content { get; set; }

        [Required]
        [DisplayName("退货订单生成时间")]
        public string Order_Time { get; set; }
       
        [Required]
        [DisplayName("退货商删除退货单")]
        public bool Publisher_Del { get; set; }
        [Required]
        [DisplayName("接收商删除退货单")]
        public bool Provider_Del { get; set; }
        [Required]
        [DisplayName("是否已经阅读")]
        public bool ReadOrNot { get; set; }//为false时表示未读，为true时表示已读。
        //导航属性
        public Enterprise ReturnEnterprise { get; set; }
        public Enterprise ReceiveEnterprise { get; set; }
       
     
    }
}
