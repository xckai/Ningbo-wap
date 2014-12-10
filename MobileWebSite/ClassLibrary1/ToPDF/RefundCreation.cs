using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CPCApp.Data.DAL;
using CPCApp.Data.IDAL;
using CPCApp.Data.Model;

using System.Drawing.Imaging;
using System.Web.UI.HtmlControls;
using iTextSharp.text.html.simpleparser;
using System.Collections;
using System.Text.RegularExpressions;
namespace MobileWebSite.BLL.ToPDF
{
    public class RefundCreation
    {
        private ReturnGoods returngoods;
        private string pdfformpath;

    public RefundCreation(ReturnGoods returngood,string temppath)
        {
            returngoods=returngood;
            pdfformpath = temppath;
        }
        public void Createpdf(){
         Document document = new Document();
         IReturnGoodsRepository _IReturnGoodsRepository = RepositoryFactory.ReturnGoodsRepository;      
         IEnterpriseRepository _IEnterpriseRepository=RepositoryFactory.EnterpriseRepository;        
         string NameOfSender = _IEnterpriseRepository.LoadEntities(item => item.Enterprise_ID == returngoods.ReturnEnterprise_ID).FirstOrDefault().Enterprise_Name;
         string NameOfReceive = _IEnterpriseRepository.LoadEntities(item => item.Enterprise_ID ==returngoods.ReceiveEnterprise_ID).FirstOrDefault().Enterprise_Name;    
            try
            {
                if (File.Exists(returngoods.DownloadFormFile_Addr))
                {
                    File.Delete(returngoods.DownloadFormFile_Addr);
                }

                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(pdfformpath, FileMode.CreateNew));
                document.Open();
                ////设定字体,支持中文
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\STSONG.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var qrCode = new QRCode();
                string QRCodeString = "{\"ID\":" + returngoods.Return_ID.ToString() + ",\"TYPE\":RefundForm" + ",\"APP\":CPC}";

                var imageCode = qrCode.GetDimensionalCode(QRCodeString);
                MemoryStream ms = new MemoryStream();
                imageCode.Save(ms, ImageFormat.Jpeg);
                Image image = Image.GetInstance(ms.ToArray());
                image.Alignment = Element.ALIGN_MIDDLE;
               
                Font FontChinese = new Font(baseFont, 12, Font.NORMAL);
                //加入图片
              
                PdfPTable table = new PdfPTable(4);
                
                PdfPCell cell = new PdfPCell(new Paragraph("标题",FontChinese)); 
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(returngoods.ReturnGoods_Name, FontChinese));
                cell.Colspan = 3;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph("退货企业:",FontChinese));       
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(NameOfSender, FontChinese));
                cell.Colspan=2;
                table.AddCell(cell);

                iTextSharp.text.Image img_1=iTextSharp.text.Image.GetInstance(image);
                img_1.ScaleToFit(50,50);  
                cell = new PdfPCell(img_1);
               
                cell.Rowspan = 4;
                cell.Colspan = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("接收企业:", FontChinese));            
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph(NameOfReceive, FontChinese));
                cell.Colspan = 2;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("退货数量:", FontChinese));
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph(returngoods.Return_Amount.ToString(),FontChinese));
                cell.Colspan = 2;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("退货企业地址:", FontChinese));
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph(returngoods.Source_Addr, FontChinese));
                cell.Colspan = 2;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("接收企业地址:", FontChinese));          
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph(returngoods.Destination_Addr,FontChinese));
                cell.Colspan = 3;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", FontChinese));
                cell.Colspan = 4;
                table.AddCell(cell);    
                document.Add(table);   
                document.Close();

            }
            catch
            {

            }
            finally
            {

            }

        }


    }
}