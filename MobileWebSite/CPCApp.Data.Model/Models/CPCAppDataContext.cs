using CPCApp.Data.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace CPCApp.Data.Model
{
    public partial class CPCAppDataContext : DbContext
    {
        static CPCAppDataContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CPCAppDataContext>());
        }

        public CPCAppDataContext()
            : base("Name=CPCAppDataContext")
        {
        }

        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<BusinessZone> BusinessZones { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<ChatRecord> ChatRecords { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<CollectionClass> CollectionGroups { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<DemandClass> DemandClasses { get; set; }
        public DbSet<DemandCollection> DemandCollections { get; set; }
        public DbSet<DemandStatus> DemandStatus { get; set; }
        public DbSet<Directional_Release> Directional_Release { get; set; }
        public DbSet<EnterpriseClass> EnterpriseClasses { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Demand> Order_Demand { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderInfoExchange> OrderInfoExchanges {get;set;}
        public DbSet<ReturnGoods> ReturnGoods { get; set; }
        public DbSet<ReturnGoods_Status> ReturnGoods_Status { get; set; }
        public DbSet<Distribution> Distribution { get; set; }
        public DbSet<LogRecord> LogRecord { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<FileRecord> FileRecord { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//取消级联删除
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();//取消级联删除        
        }

    }
}
