using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPCApp.Data.IDAL;


namespace CPCApp.Data.DAL
{
    public static class RepositoryFactory//简单工厂
    {
        public static IBidRepository BidRepository
        {

            get { return new BidRepository(); }

        }
        public static IBusinessZoneRepository BusinessZoneRepository
        {

            get { return new BusinessZoneRepository(); }

        }

        public static ICaseRepository CaseRepository
        {

            get { return new CaseRepository(); }

        }
        public static IChatRecordRepository ChatRecordRepository
        {

            get { return new ChatRecordRepository(); }

        }
        public static IEnterpriseRepository EnterpriseRepository
        {

            get { return new EnterpriseRepository(); }

        }
        public static IClassRepository ClassRepository
        {

            get { return new ClassRepository(); }

        }

        public static IEnterpriseClassRepository EnterpriseClassRepository
        {

            get { return new EnterpriseClassRepository(); }

        }

        public static ICollectionClassRepository CollectionClassRepository
        {

            get { return new CollectionClassRepository(); }

        }

        public static IDemandClassRepository DemandClassRepository
        {

            get { return new DemandClassRepository(); }

        }

        public static IDemandCollectionRepository DemandCollectionRepository
        {

            get { return new DemandCollectionRepository(); }

        }

        public static IDemandRepository DemandRepository
        {
            get { return new DemandRepository(); }
        }

        public static IDemandStatusRepository DemandStatusRepository
        {
            get { return new DemandStatusRepository(); }
        }

        public static IDirectional_ReleaseRepository Directional_ReleaseRepository
        {
            get { return new Directional_ReleaseRepository(); }
        }

        public static IEquipmentRepository EquipmentRepository
        {
            get { return new EquipmentRepository(); }
        }

        public static IOrder_DemandRepository Order_DemandRepository
        {
            get { return new Order_DemandRepository(); }
        }

        public static IOrderRepository OrderRepository
        {
            get { return new OrderRepository(); }
        }

        public static IOrderStatusRepository OrderStatusRepository
        {
            get { return new OrderStatusRepository(); }
        }

        public static IReviewRepository ReviewRepository
        {
            get { return new ReviewRepository(); }
        }
        public static IOrderInfoExchangeRepository OrderInfoExchangeRepository
        {
            get { return new OrderInfoExchangeRepository(); }
        }
        public static IReturnGoods_StatusRepository ReturnGoods_StatusRepository 
        {
            get { return new ReturnGoods_StatusRepository(); }
        }
        public static IReturnGoodsRepository ReturnGoodsRepository 
        {
            get { return new ReturnGoodsRepository(); }
        }
        public static IDistributionRepository DistributionRepository 
        {
            get { return new DistributionRepository(); }
        }
        public static ILogRecordRepository LogRecordRepository 
        {
            get { return new LogRecordRepository(); }
        }
        public static IUserRepository UserRepository 
        {
            get { return new UserRepository(); }
        
        }
        public static IFileRecordRepository FileRecordRepository {

            get { return new FileRecordRepository(); }
        }
    }

}
