using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class User
    {  
       
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [DisplayName("用户表主键")]
        public int User_ID { get; set; }
        [Required]
        [DisplayName("企业编号")]
        public int Enterprise_ID { get; set; }
        [Required]
        [DisplayName("密码")]
        public string Password { get; set; }
        
        [DisplayName("员工编号")]
        public int User_Code { get; set; }
        
        [DisplayName("用户姓名")]
        public string User_Name { get; set; }
        
        [DisplayName("身份证")]
        public string User_Card { get; set; }
        
        [DisplayName("用户照片")]
        public byte[] User_Photo { get; set; }


        [DisplayName("用户生日")]
        public string User_Birthday { get; set; }
        
        [DisplayName("所属部门")]
        public int InternalOrganization_ID { get; set; }
       
        [DisplayName("用户职位")]
        public string User_Position { get; set; }

        [DisplayName("职位描述")]
        public string User_Position_Des { get; set; }
        
        [DisplayName("用户性别")]
        public bool User_Sex { get; set; }
        
        [Phone]
        [DisplayName("联系电话")]
        public string User_Tel { get; set; }

        [DisplayName("用户地址")]
        public string User_Addr { get; set; }
        
        [EmailAddress]
        [DisplayName("用户邮箱")]
        public string User_Email { get; set; }
        
        [DisplayName("用户状态")]
        public bool User_State { get; set; }
       
        [DisplayName("用户创建时间")]
        public string User_Create_Time { get; set; }

        [DisplayName("用户备注")]
        public string User_Option { get; set; }

        
       
    }
}
