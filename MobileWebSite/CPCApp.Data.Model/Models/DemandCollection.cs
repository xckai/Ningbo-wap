using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class DemandCollection
    {
        [Key]
        [DisplayName("需求收藏主键")]
        public int DemandCollection_ID { get; set; }

        
        [Required]
        [DisplayName("需求主键")]
        public int Demand_ID { get; set; }

        public string Demand_Name { get; set; }

        [DisplayName("需求收藏描述")]
        public string Collection_Description { get; set; }

        [Required]
        [DisplayName("收藏分组主键")]
        public int CollectionClass_ID { get; set; }

        public string CollectionGroup_Name { get; set; }

        //导航属性
        public CollectionClass CollectionClass { get; set; }
        public Demand Demand { get; set; }
    }
}
