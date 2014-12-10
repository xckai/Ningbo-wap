using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class DistributionCreation
    {

        
   
        private Distribution distribution;
        private string pdfpath;

        public DistributionCreation(Distribution dis,string path)
        {

            distribution = dis;
            pdfpath = path;
            
        }
        public void Createpdf(){
         Document document = new Document();
         IEnterpriseRepository _IEnterpriseRepository=RepositoryFactory.EnterpriseRepository;
         IOrderRepository _IOrderRepository = RepositoryFactory.OrderRepository;
         Order order = _IOrderRepository.LoadEntities(item => item.Order_ID == distribution.Order_ID).FirstOrDefault();
         string NameOfSender = _IEnterpriseRepository.LoadEntities(item => item.Enterprise_ID == order.ProviderEnterprise_ID).FirstOrDefault().Enterprise_Name;
         string NameOfReceive = _IEnterpriseRepository.LoadEntities(item => item.Enterprise_ID == order.PublisherEnterprise_ID).FirstOrDefault().Enterprise_Name;


            try
            {
                if (File.Exists(pdfpath))
                {
                    File.Delete(pdfpath);
                }

                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(pdfpath, FileMode.CreateNew));
                document.Open();
                ////设定字体,支持中文
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\STSONG.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var qrCode = new QRCode();
                string QRCodeString = "{\"ID\":" +distribution.Distribution_ID.ToString() + ",\"TYPE\":Distribution" + ",\"APP\":CPC}";
                var imageCode = qrCode.GetDimensionalCode(QRCodeString);
                MemoryStream ms = new MemoryStream();
                imageCode.Save(ms, ImageFormat.Jpeg);
                Image image = Image.GetInstance(ms.ToArray());
                image.Alignment = Element.ALIGN_MIDDLE;
               
                Font FontChinese = new Font(baseFont, 12, Font.NORMAL);
                //加入图片
              
                PdfPTable table = new PdfPTable(4);
                
                PdfPCell cell = new PdfPCell(new Paragraph("标题",FontChinese));
                cell.Colspan = 4;
                table.AddCell(cell);
                 cell = new PdfPCell(new Paragraph("发送方企业名称:",FontChinese));
                     
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
                cell = new PdfPCell(new Paragraph("接收方企业名称:", FontChinese));            
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph(NameOfReceive, FontChinese));
                cell.Colspan = 2;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("配送额数量:", FontChinese));
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph(distribution.Distribution_Amount.ToString(),FontChinese));
                cell.Colspan = 2;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("发送方地址:", FontChinese));
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph(distribution.Source_Addr, FontChinese));
                cell.Colspan = 2;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("发送方地址:", FontChinese));
                 
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph(distribution.Destination_Addr,FontChinese));
                cell.Colspan = 3;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("接收方地址:", FontChinese));
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