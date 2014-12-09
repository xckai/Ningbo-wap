using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPCApp.Data.Model;
using CPCApp.Data.DAL;
using CPCApp.Data.IDAL;
using System.Web;
using System.Web.Mvc;

namespace MobileWebSite.BLL.LogInCheck
{
    public class Checkuserresult//检测用户密码状态匹配后返回checkresult=1 以及对应的Enterprise还有user信息，如果不匹配则返回checkresult=0以及不匹配原因
    {
        public int checkresult;
        public string reason;
        public Enterprise enterprise;
        public User user;

    }
    public class LoginCheck : ILoginCheck
    {
        public Checkuserresult CheckUserresult(int enId, int userId, string password)
        {
            Checkuserresult temp_checkuserresult = new Checkuserresult();
           
            UserRepository userRe = new UserRepository();
            try
            {
                User tempuser = userRe.LoadEntities(User => User.Enterprise_ID == enId && User.User_ID == userId).FirstOrDefault();
                if (tempuser == null)
                {
                    temp_checkuserresult.checkresult = 0;
                    temp_checkuserresult.reason = "公司ID或者用户ID无效，请重新输入";
                    return temp_checkuserresult;
                   
                   
                }
                else
                {
                    if (tempuser.Password==password)
                    { 
                        if (tempuser.User_State==true)
                        {
                            temp_checkuserresult.checkresult = 1;
                            temp_checkuserresult.reason = "登录成功，跳转中";
                            
                            EnterpriseRepository tempEnRe = new EnterpriseRepository();
                            UserRepository tempuserRe = new UserRepository();
                            try
                            {
                                temp_checkuserresult.user = tempuserRe.LoadEntities(User => User.User_ID == userId).FirstOrDefault();
                                temp_checkuserresult.enterprise= tempEnRe.LoadEntities(Enterprise=>Enterprise.Enterprise_ID==enId).FirstOrDefault();
                            }
                            catch (System.Exception ex)
                            {
                                temp_checkuserresult.enterprise.Enterprise_Name = "服务器异常，无法获取公司名称";
                            }
                            return temp_checkuserresult;
                           
                        }
                        else
                        {
                            temp_checkuserresult.checkresult = 0;
                            temp_checkuserresult.reason = "用户未经过验证，请联系企业管理员";
                            return temp_checkuserresult;
                           
                        }
                        

                    }
                    else
                    {
                        temp_checkuserresult.checkresult = 0;
                        temp_checkuserresult.reason = "密码错误，请重新登录";
                        return temp_checkuserresult;
                      
                     
                    }
                
                }
            }
            catch (System.Exception ex)
            {
                temp_checkuserresult.checkresult = 0;
                temp_checkuserresult.reason = "无法连接验证服务器，请联系管理员";
                return temp_checkuserresult;
                
            	
            }
           




          

           


        }

    }
}
