using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class EnterpriseClass
    {
        [Key]
        [DisplayName("企业分类主键")]
        public int EnterpriseClass_ID { get; set; }

        [Required]
        [DisplayName("分类主键")]
        public int Class_ID { get; set; }

        [Required]
        [DisplayName("企业主键")]
        public int Enterprise_ID { get; set; }


        //导航属性
        public Enterprise Enterprise { get; set; }
        public Class Class { get; set; }
    }
}
