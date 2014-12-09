
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Class
    {
        [Key]
        [DisplayName("企业、需求类别主键")]
        public int Class_ID { get; set; }


        
        [DisplayName("上级类别主键")]
        public Nullable <int> PIDClass_ID { get; set; }

        [Required]
        [DisplayName("类别名称")]
        public string Class_Name { get; set; }

        [DisplayName("类别描述")]
        public string Class_Description { get; set; }


        //导航属性
        public Class PID { get; set; }
        public ICollection<EnterpriseClass> EnterpriseClass { get; set; }
        public ICollection<DemandClass> DemandClass { get; set; }

       
    }
}
