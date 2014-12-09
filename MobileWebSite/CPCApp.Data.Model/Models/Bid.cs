using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public class Bid
    {
        [Key]
        [DisplayName("竞标方案主键")]
        public int Bid_ID { get; set; }

        [Required]
        [DisplayName("需求主键")]
        public int Demand_ID { get; set; }

        [DisplayName("需求名称")]
        public string Demand_Name { get; set; }
        [Required]
        public int ProviderUser_ID { get; set; }//竞标企业用户
        
        [DisplayName("竞标企业主键")]
        public int Enterprise_ID { get; set; }

        [DisplayName("竞标企业名称")]
        public string Enterprise_Name { get; set; }


        [Required]
        [DisplayName("竞标方案内容简介")]
        public string Bid_Content { get; set; }

        public enum BidStateType
        {
            Bidding=0,
            BidSelected=1,
            BidUnselected=2
        }

        [Required]
        [DisplayName("是否中标")]
        public BidStateType BidState { get; set; }

        [Required]
        [DisplayName("竞标时间")]
        public string Bid_Time { get; set; }

        [DisplayName("竞标方案文件地址")]
        public string bidFilePath { get; set; }

        [DisplayName("竞标方案文件地址")]
        public string bidFileBrowsePath { get; set; }


        public Enterprise Enterprise { get; set; }//导航属性
        public Demand Demand { get; set; }//导航属性
    }
}
