using CPCApp.Data.DAL;
using CPCApp.Data.IDAL;
using CPCApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileWebSite.BLL.ToPDF
{
    public class CommonFunction
    {
         public string GetClassList()
        {
            string result = "";
            IClassRepository _IClassRepository =RepositoryFactory.ClassRepository;
            var list = _IClassRepository.LoadEntities(demandclass=>demandclass.Class_ID>0).ToList<Class>();
            foreach (Class node in list)
            {
                if (node.PIDClass_ID == null)
                    result += Recursion(node) + ",";
            }
            return "[" + result.TrimEnd(',') + "]";
        }
        public string Recursion(Class model)
        {
            string res_s = "";
            res_s += "{\"id\":" + model.Class_ID+ ",\"text\":\"" + model.Class_Name + "\"";
            IClassRepository _IClassRepository =RepositoryFactory.ClassRepository;
            
            List<Class> list = _IClassRepository.LoadEntities(dclass=>dclass.PIDClass_ID==model.Class_ID).ToList<Class>();    
            if (list != null)
            {
                res_s += "," + "\"children\":[";
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                        res_s += ",";
                    res_s += Recursion(list[i]);
                }
                res_s += "]";
            }
            res_s += "}";
            return res_s;
        }

        public string GetCollectionClass(int enterpriseid, CollectionClass.CollectionType EnterpriseOrDemand)
        {
            string result = "";
            ICollectionClassRepository _ICollectionClassRepository = RepositoryFactory.CollectionClassRepository;
            var list = _ICollectionClassRepository.LoadEntities(collection => collection.Enterprise_ID == enterpriseid & collection.CollectionGroup_Class == EnterpriseOrDemand).ToList<CollectionClass>();
            foreach (CollectionClass node in list)
            {
                if (node.PIDCollectionClass_ID == null)
                    result += RecursionCollectionClass(node) + ",";
            }
            return "[" + result.TrimEnd(',') + "]";
        
        
        }

        public string RecursionCollectionClass(CollectionClass model)
        {
            string res_s = "";
            res_s += "{\"id\":" + model.CollectionClass_ID + ",\"text\":\"" + model.CollectionGroup_Name + "\"";
            ICollectionClassRepository _ICollectionClassRepository = RepositoryFactory.CollectionClassRepository;

            List<CollectionClass> list = _ICollectionClassRepository.LoadEntities(dclass => dclass.PIDCollectionClass_ID == model.CollectionClass_ID).ToList<CollectionClass>();
            if (list != null)
            {
                res_s += "," + "\"children\":[";
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                        res_s += ",";
                    res_s += RecursionCollectionClass(list[i]);
                }
                res_s += "]";
            }
            res_s += "}";
            return res_s;
        }
        /// <summary>
        /// 获取文件历史编辑记录
        /// </summary>
        /// <param name="id"> project_ID</param>
        /// <param name="type">文件类型</param>
        /// <returns></returns>
        public string GetFileRecord(int id, int type=0) 
        {
           
            FileRecord.FileType Type=0;
            switch(type)
            {
                case(0): Type=FileRecord.FileType.Demand;break;
                case(1):Type=FileRecord.FileType.Bid;break;
                case(2):Type=FileRecord.FileType.Order; break;
                case(3):Type=FileRecord.FileType.Refund;break;
            }
            IFileRecordRepository _IFileRecordRepository = RepositoryFactory.FileRecordRepository;
            List<FileRecord> filerecords = _IFileRecordRepository.LoadEntities(item => item.Project_ID == id&item.File_Type==Type).ToList<FileRecord>();
            string filerecord = "[";
           
           
            foreach (FileRecord record in filerecords)
            {

                filerecord += "{\"id\":" + record.FileRecord_ID + ",\"text\":\"" + record.Edit_Time + "编辑\"},";


            }
            filerecord = filerecord.TrimEnd(',');
            filerecord += "]";
            return filerecord;
        
        
        }


    }
}