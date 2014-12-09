using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class ChatRecord
    {
        [Key]
        [DisplayName("聊天记录主键")]
        public int ChatRecord_ID { get; set; }

        [Required]
        [DisplayName("发送方主键")]
        public int SendEnterprise_ID { get; set; }

        [Required]
        [DisplayName("接收方主键")]
        public int ReceiveEnterprise_ID { get; set; }

        [DisplayName("聊天内容")]
        public string Chat_Content { get; set; }


        [Required]
        [DisplayName("聊天时间")]
        public string Chat_Time { get; set; }


        [Required]
        [DisplayName("发送方是否删除聊天")]
        public bool Send_Enterprise_Del { get; set; }

        [Required]
        [DisplayName("接收方是否删除聊天")]
        public bool Receive_Enterprise_Del { get; set; }

        [Required]
        [DisplayName("消息已读标记")]
        public bool Read { get; set; }


        //导航属性
        public Enterprise Send { get; set; }
        public Enterprise Receive { get; set; }

    }
}
