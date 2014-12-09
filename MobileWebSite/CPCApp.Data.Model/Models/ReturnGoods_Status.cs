using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCApp.Data.Model
{
 public partial   class ReturnGoods_Status
    {
        public enum ReturnStatusType
        {
            ApplyReturn = 0,//申请退货
            AgreeReturn = 1,//同意退货
            GoodsReturn_Distribution = 2,//退货配送
            Rececived = 3,//退货签收
          
        }
        [Key]
        [DisplayName("退货状态主键")]
        public int ReturnGoodsStatus_ID { get; set; }

        [Required]
        [DisplayName("退货订单主键")]
        public int Return_ID { get; set; }

        [Required]
        [DisplayName("订单状态内容")]
        public ReturnStatusType ReturnGoodsStatus_Content { get; set; }

        [Required]
        [DisplayName("订单状态时间")]
        public string ReturnGoodsStatus_Time { get; set; }
        public ReturnGoods ReturnGoods { get; set; }//导航属性

    }
}
