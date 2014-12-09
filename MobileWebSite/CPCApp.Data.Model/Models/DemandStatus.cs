using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class DemandStatus
    {
        public enum demandStatusType
        {
            demandRelease=0,//需求发布阶段
            demandBid=1,//需求竞标阶段
            demandBidFinished=2//需求竞标结束
        }

        [Key]
        [DisplayName("需求状态主键")]
        public int DemandStatus_ID { get; set; }

        [Required]
        [DisplayName("需求主键")]
        public int Demand_ID { get; set; }


        [Required]
        [DisplayName("状态内容")]
        public demandStatusType Status_Content { get; set; }


        [Required]
        [DisplayName("状态时间")]
        public string Status_Time { get; set; }

        //导航属性
        public Demand Demand { get; set; }
    }
}
