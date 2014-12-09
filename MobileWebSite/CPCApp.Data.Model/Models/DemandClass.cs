using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class DemandClass
    {
        [Key]
        [DisplayName("需求分类主键")]
        public int DemandClass_ID { get; set; }

        [Required]
        [DisplayName("需求主键")]
        public int Demand_ID { get; set; }
        [Required]
        [DisplayName("类别主键")]
        public int Class_ID { get; set; }

        //导航属性
        public Demand Demand { get; set; }
        public Class Class { get; set; }
    }
}
