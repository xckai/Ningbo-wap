using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{

    public partial class CollectionClass
    {

        [Key]
        [Required]
        [DisplayName("收藏分组主键")]
        public int CollectionClass_ID { get; set; }

        [Required]
        [DisplayName("企业ID")]
        public int Enterprise_ID { get; set; }


        [DisplayName("上级分组ID")]
        public Nullable<int> PIDCollectionClass_ID { get; set; }

        [Required]
        [DisplayName("分组名称")]
        public string CollectionGroup_Name { get; set; }

        public enum CollectionType
        {
            Enterprise,
            Demand
        }

        [Required]
        [DisplayName("分组类别")]
        public CollectionType CollectionGroup_Class { get; set; }


        //导航属性
        public ICollection<BusinessZone> BusinessZone { get; set; }
        public Enterprise Enterprise { set; get; }
        public CollectionClass PID { set; get; }


    }
}
