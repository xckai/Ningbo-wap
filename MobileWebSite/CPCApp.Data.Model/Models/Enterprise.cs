using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPCApp.Data.Model
{
    public partial class Enterprise
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("企业表主键")]
        public int Enterprise_ID { get; set; }

        [Required]
        [DisplayName("企业名称")]
        public string Enterprise_Name { get; set; }

        [DisplayName("企业标志图标")]
        public byte[] Enterprise_Logo { get; set; }


        [DisplayName("企业规模")]
        public int Enterprise_Scale { get; set; }


        [DisplayName("企业所在省份")]
        public string Enterprise_Province { get; set; }


        [DisplayName("企业所在城市")]
        public string Enterprise_City { get; set; }


        [DisplayName("企业地址")]
        public string Enterprise_Addr { get; set; }


        [DisplayName("邮编")]
        public string Enterprise_ZipCode { get; set; }


        [Phone]
        [DisplayName("企业电话 ")]
        public string Enterprise_Tel { get; set; }

        
        [EmailAddress]
        [DisplayName("企业邮箱")]
        public string Enterprise_Email { get; set; }

        [DisplayName("企业创建时间")]
        public Nullable<System.DateTime> Enterprise_Create_Time { get; set; }

        [DisplayName("企业描述")]
        public string Enterprise_Description { get; set; }

      /*  //导航属性

        public ICollection<ChatRecord> ChatRecords { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Demand> Demands { get; set; }
        public ICollection<CollectionClass> CollectionClasses { get; set; }
        public ICollection<Case> Cases { get; set; }
        public ICollection<Equipment> Equipments { get; set; }
        public ICollection<BusinessZone> BusinessZones { get; set; }
        public ICollection<EnterpriseClass> EnterpriseClasses { get; set; }
        public ICollection<Directional_Release> Directional_Release { get; set; }*/



    }
}
