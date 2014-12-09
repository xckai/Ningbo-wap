using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace MobileWebSite.BLL.LogInCheck
{
   public interface ILoginCheck
    {

       Checkuserresult CheckUserresult(int enId, int userId, string password);
    }
}
