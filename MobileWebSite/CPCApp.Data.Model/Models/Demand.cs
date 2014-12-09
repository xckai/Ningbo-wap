using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Demand
    {
        [Key]
        [DisplayName("需求主键")]
        public int Demand_ID { get; set; }

        [Required]
        [DisplayName("发布商企业主键")]
        public int Enterprise_ID { get; set; }

        [Required]
        [DisplayName("需求名称")]
        public string Demand_Name { get; set; }

        [Required]
        [DisplayName("需求描述")]
        public string Demand_Description { get; set; }
        [Required]
        [DisplayName("发布用户ID")]
        public int PublisherUser_ID { get; set; }
       
        [Required]
        [DisplayName("需求是否公开")]
        public bool Demand_Public { get; set; }

        [Required]
        [DisplayName("发布时间")]
        public DateTime BeginTime { get; set; }

        [Required]
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }

        [DisplayName("需求文件地址")]
        public string filePath { get; set; }

        [DisplayName("需求文件浏览地址")]
        public string flashpaperPath { get; set; }
        [DisplayName("发布方是否删除")]
        public bool DeleteOrNOt { get; set; }
        public Enterprise Enterprise { get; set; }//导航属性

       

    }
}
