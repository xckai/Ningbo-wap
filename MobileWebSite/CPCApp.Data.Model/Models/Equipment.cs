using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Equipment
    {
        [Key]
        [DisplayName("设备主键")]
        public int Equipment_ID { get; set; }

        [Required]
        [DisplayName("企业ID")]
        public int Enterprise_ID { get; set; }

        [Required]
        [DisplayName("设备名称")]
        public string Equipment_Name { get; set; }
        [DisplayName("设备图片")]
        public byte[] Equipment_Picture { get; set; }
        [DisplayName("设备描述")]
        public string Equipment_Description { get; set;}

       [DisplayName("设备空闲时间")]
       public string Equipment_FreeBegin_Time { get; set;}
       public string Equipment_FreeEnd_Time { get; set; }

        public Enterprise Enterprise { get; set; }//导航属性

    }
}
