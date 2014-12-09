using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Directional_Release
    {
        [Key]
        [Required]
        [DisplayName("定向发布主键")]
        public int Directional_Release_ID { get; set; }


        [Required]
        [DisplayName("服务商企业ID")]
        public int Enterprise_ID { get; set; }
        [Required]
        [DisplayName("需求ID")]
        public int Demand_ID { get; set; }

        public Enterprise Enterprise { get; set; }//导航属性
        public Demand Demand { get; set; }//导航属性


    }
}
