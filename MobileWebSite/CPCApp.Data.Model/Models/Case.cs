using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Case
    {
        [Key]
        [DisplayName("案例主键")]
        public int Case_ID { get; set; }

        [Required]
        [DisplayName("企业主键")]
        public int Enterprise_ID { get; set; }

        [Required]
        [DisplayName("案例名称")]
        public string Case_Name { get; set; }

 
        [DisplayName("案例图片")]
        public byte[] Case_Picture { get; set; }


        [DisplayName("案例描述")]
        public string Case_Description { get; set; }


        public Enterprise Enterprise { get; set; }//导航属性*/
    }
}
