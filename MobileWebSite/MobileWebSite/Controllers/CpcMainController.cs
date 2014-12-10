using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileWebSite.BLL.LogInCheck;
using CPCApp.Data.DAL;
using System.Drawing;

//Session["enId"]=enId;验证登录信息，登录成功写入对应的session值里面
//Session["userId"]=userId;
namespace MobileWebSite.Controllers
{
    public class CpcMainController : Controller
    {
        //
        // GET: /CpcMain/
        private int enterpriseId;
        private int userId;
        public CpcMainController()
        {
            try
            {
                enterpriseId = (int)Session["enId"];
                userId = (int)Session["userId"];
            }
            catch (System.Exception ex)
            {

            }
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            ViewBag.ID = Session["userId"];
            ViewBag.Name = Session["enName"];
            return View();
        }
        public ActionResult OrderDemo()
        {
            return View();
        }
        public Image get_Enlogo()
        {
            try
            {
                int id = (int)Session["enId"];
                EnterpriseRepository _enrep = new EnterpriseRepository();
                var _en = _enrep.LoadEntities(Enterprise => Enterprise.Enterprise_ID == id).FirstOrDefault();

                Response.ContentType = "image/jpeg";
                if (_en.Enterprise_Logo != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(_en.Enterprise_Logo); //photo: 图片的二进制数据..
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                    image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return image;
                }
                else
                {
                    Image originalImage = System.Drawing.Image.FromFile("F:\\360\\GitHub\\MobileWebSite-12-03\\MobileWebSite\\Kendo\\123.png");
                    originalImage.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return originalImage;
                }
            }
            catch (System.Exception ex)
            {
                Image originalImage = System.Drawing.Image.FromFile("F:\\360\\GitHub\\MobileWebSite-12-03\\MobileWebSite\\Kendo\\123.png", true);
                originalImage.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return originalImage;
            }
        }
        public JsonResult Logincheckuser()
        {
            JsonResult tempjson = new JsonResult();
            try
            {
                int enId = Convert.ToInt32(Request.Form["enId"]);
                int userId = Convert.ToInt32(Request.Form["userId"]);
                string password = Request.Form["password"];
                ILoginCheck tempCheck = new LoginCheck();
                var tempchecktresult = tempCheck.CheckUserresult(enId, userId, password);
                if (tempchecktresult.checkresult == 1)//登录返回成功写入session
                {
                    Session["enId"] = enId;
                    Session["userId"] = userId;
                    Session["isLogin"] = "1";
                    Session["enName"] = tempchecktresult.enterprise.Enterprise_Name;
                }
                var tempdata = new
                {
                    reason = tempchecktresult.reason,
                    checkresult = tempchecktresult.checkresult
                };
                tempjson.Data = tempdata;
                return tempjson;
            }
            catch (System.Exception ex)
            {
                var tempdata = new
                {
                    checkresult = 0,
                    reason = "输入格式有误，请重新输入"
                };

                tempjson.Data = tempdata;
                return tempjson;
            }
        }
        public ActionResult mytest()
        {
            return View();
        }
    }
}
