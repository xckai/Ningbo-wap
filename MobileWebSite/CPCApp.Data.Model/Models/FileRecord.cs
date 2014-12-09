using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCApp.Data.Model
{
    public partial class FileRecord
    {
        public enum FileType
        {
           Demand=0, //需求
           Bid=1,//竞标
           Order=2,//订单
           Refund=3// 退货
        }
        [Key]
        [DisplayName("文件更改记录ID")]
        public int FileRecord_ID { get; set; }
        [Required]
        [DisplayName("对应企业主键")]
        public int Enterprise_ID { get; set; }
        [Required]
        [DisplayName("文件类型")]
        public FileType File_Type { get; set; }
        [DisplayName("记录对应文件主键")]
        public int Project_ID { get; set; }
        [Required]
        [DisplayName("swf文件存储路径")]
        public string Swf_Path { get; set; }
        [Required]
        [DisplayName("编辑时间")]
        public string Edit_Time { get; set; }
    }
}
