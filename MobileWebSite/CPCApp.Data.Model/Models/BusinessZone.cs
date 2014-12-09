using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class BusinessZone
    {
        [Key]
        [DisplayName("企业商圈主键")]
        public int BusinessZone_ID { get; set; }

        [Required]
        [DisplayName("收藏分组主键")]
        public int CollectionClass_ID { get; set; }

        [DisplayName("分组名称")]
        public string CollectionGroup_Name { get; set; }



        [Required]
        [DisplayName("被收藏企业主键")]
        public int Enterprise_ID { get; set; }

        [DisplayName("企业名称")]
        public string Enterprise_Name { get; set; }
        [DisplayName("备注")]
        public string My_Description { get; set; }
        public Enterprise Enterprise { get; set; }//导航属性
        public CollectionClass CollectionClass { get; set; }//导航属性

    }
}
