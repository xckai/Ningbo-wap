using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCApp.Data.Model
{
  public partial class LogRecord
    {

    public enum LogType
      {
          DemandLog = 0,
          OrderLog = 1,
          DistributionLog = 2,
          RefundLog = 3,
      }
      [Key]
      [DisplayName("日志主键1")]
      public int Log_ID { get; set; }
      [Required]
      [DisplayName("企业主键")]
      public int Enterprise_ID { get; set; }

      [Required]
      [DisplayName("用户主键")]
      public int User_ID { get; set; }

      [Required]
      [DisplayName("日志类型")]
      public LogType logtype{get; set; }

      [Required]
      [DisplayName("相关操作主键")]
      public int Operation_ID{ get; set;}
     

      //导航属性
      public Enterprise Enterprise { get; set; }
      public User User { get; set; }
    }
}
