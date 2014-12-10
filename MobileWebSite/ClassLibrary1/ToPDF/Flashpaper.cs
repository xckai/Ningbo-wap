using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Word = Microsoft.Office.Interop.Word;
//using Excel = Microsoft.Office.Interop.Excel;
//using Powerpoint = Microsoft.Office.Interop.PowerPoint;
//using Microsoft.Office.Core;
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
using Aspose.Cells;
using Aspose.Words;
using Aspose.Slides;
using Aspose.Pdf;
using Microsoft.VisualBasic;
namespace MobileWebSite.BLL.ToPDF
{
    public enum documentType
    {
        demand,//需求文档
        bid,//竞标方案文档
        order,//订单合同文档
        refund//退货。
    }
    public class FlashPaper
    {
        private string exepath;
        private string sourceFileName;
        private string outputFileName;
        private string fileType;
        int ID;
        documentType docType;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exepath">转换程序映射地址(默认值为HttpContext.Server.MapPath("~/SWFTools/pdf2swf.exe"))</param>
        /// <param name="sourceFileName">文档源地址</param>
        /// <param name="outputFileName">输出地址</param>
        /// <param name="filtType">所上传文档的格式类型</param>
        /// <param name="ID">ID主要为订单ID或者需求ID或者竞标方案ID</param>
        /// <param name="ID">文档类型</param>
        public FlashPaper(string exepath, string sourceFileName, string outputFileName, string fileType, int ID, documentType docType)
        {
            this.exepath = exepath;
            this.sourceFileName = sourceFileName;
            this.outputFileName =@""""+outputFileName+@"""";
            this.fileType = fileType.ToLower();
            this.ID = ID;
            this.docType = docType;
        }
        /// <summary>
        /// flashpaper在线转换
        /// </summary>
        /// <param name="sourceFileName">源文件地址</param>
        /// <param name="outPutFileName">输出文件地址</param>
        public void ProcessExec()
        {

            var pdftemppath = sourceFileName.ToString().Substring(0, sourceFileName.ToString().LastIndexOf(".")) + "temp.pdf";//转换的pdf文件临时存储路径。
            var pdfpath = sourceFileName.ToString().Substring(0, sourceFileName.ToString().LastIndexOf(".")) + ".pdf";//转换的pdf文件最终存储路径。
            if (fileType == ".doc" || fileType == ".docx")
            {
                word2pdf(sourceFileName, pdftemppath);
            }
            if (fileType == ".xls" || fileType == ".xlsx")
            {
                Excel2pdf(sourceFileName, pdftemppath);
            }
            if (fileType == ".ppt" || fileType == ".pptx")
            {
                PPT2pdf(sourceFileName, pdftemppath);
            }
            if (fileType == ".png" || fileType == ".gif" || fileType == ".jpeg" || fileType == ".jpg"||fileType=="bmp")
            {
                Image2pdf(sourceFileName, pdftemppath);
            }
            if (fileType == ".pdf")
            {
                File.Copy(sourceFileName, pdftemppath, true);
                File.Delete(sourceFileName);   
            }
            if (fileType == ".txt")
            {
                txt2pdf(sourceFileName,pdftemppath);

            }
            addLastpage2pdf(pdftemppath, pdfpath, ID, docType);
            sourceFileName = @"""" + pdfpath + @"""";
            var flashPrinter = exepath;
            //var flashPrinter = @"D:\Print2Flash3\p2fServer.exe";
            Process pss = new Process();
            pss.StartInfo.CreateNoWindow = true;
            pss.StartInfo.FileName = flashPrinter;
            pss.StartInfo.Arguments = " -t " + sourceFileName + " -s flashversion=9 -o  " + outputFileName;
            pss.StartInfo.UseShellExecute = false;
            //pss.StartInfo.Arguments = string.Format("{0} {1} /Language:zh-CN /InterfaceOptions:{2} ", sourceFileName,outputFileName, 12286 + 16384);
            try
            {
                pss.Start();
                pss.PriorityClass = ProcessPriorityClass.Normal;
                pss.WaitForExit();
                while (!pss.HasExited)
                {
                    continue;
                }
                System.Threading.Thread.Sleep(4000);
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            finally
            {
                
                pss.Close();
                pss.Dispose();
            }
        }


        private bool txt2pdf(string sourcePath, string targetPath)
        {
            try
            {
                System.IO.TextReader tr = new StreamReader(sourcePath);
                Aspose.Pdf.Generator.Pdf pdf1 = new Aspose.Pdf.Generator.Pdf();
                Aspose.Pdf.Generator.Section sec1 = pdf1.Sections.Add();
                Aspose.Pdf.Generator.Text t2 = new Aspose.Pdf.Generator.Text(tr.ReadToEnd());
                sec1.Paragraphs.Add(t2);
                pdf1.Save(targetPath);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 将Word转换成pdf
        /// </summary>
        /// <param name="sourcePath">输入文件地址</param>
        /// <param name="targetPath">输出文件地址</param>
        private bool word2pdf(string sourcePath, string targetPath)
        {

            try
            {
                Aspose.Words.Document workbook = new Aspose.Words.Document(sourcePath);
                workbook.Save(targetPath, Aspose.Words.SaveFormat.Pdf);
            }
            catch
            {
                return false;
            }
            return true;
           

        }
        /// <summary>
        /// 将Excel转换成pdf
        /// </summary>
        /// <param name="sourcePath">文件源地址</param>
        /// <param name="targetPath">目标文件地址</param>
        private bool Excel2pdf(string sourcePath, string targetPath)
        {
            try
            {

                Workbook workbook = new Workbook(sourcePath);
                workbook.Save(targetPath, Aspose.Cells.SaveFormat.Pdf);
            }
            catch
            {
                return false;
            }
            return true;

        }
        /// <summary>
        /// 将ppt转换成pdf文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        private bool PPT2pdf(string sourcePath, string targetPath)
        {
            try
            {
                Presentation pres = new Presentation(sourcePath);
                pres.Save(targetPath, Aspose.Slides.Export.SaveFormat.Pdf);
            }
            catch
            {
                return false;
            }
            return true;

        }
        /// <summary>
        /// 把图片转换成pdf
        /// </summary>
        /// <param name="sourcePath">图片路径</param>
        /// <param name="targetPath">输出pdf路径</param>
        private bool Image2pdf(string sourcePath, string targetPath)
        {
         
           
            //Pdf pdf = new Pdf();
            //// create a section and add it to pdf document
            //Aspose.Pdf.Section MainSection = pdf.Sections.Add();
            ////Add the radio form field to the paragraphs collection of the section
            //// create an image object
            //Aspose.Pdf.Image sample_image = new Aspose.Pdf.Image();
            //// specify the image file path information
            //sample_image.ImageInfo.File = @"d:/pdftest/untitled.bmp";
            //// specify the image file type
            //sample_image.ImageInfo.ImageFileType = ImageFileType.Bmp;
            //// specify the image width information equal to page width 
            //sample_image.ImageInfo.FixWidth = MainSection.PageInfo.PageWidth - MainSection.PageInfo.Margin.Left - MainSection.PageInfo.Margin.Right;
            //// specify the image Height information equal to page Height
            //sample_image.ImageInfo.FixWidth = MainSection.PageInfo.PageHeight - MainSection.PageInfo.Margin.Top - MainSection.PageInfo.Margin.Bottom;

            //// create bitmap image object to load image information
      
            //// check if the width of the image file is greater than Page width or not
            //if (myimage.Width > MainSection.PageInfo.PageWidth)
            //    // if the Image width is greater than page width, then set the page orientation to Landscape
            //    MainSection.IsLandscape = true;
            //else
            //    // if the Image width is less than page width, then set the page orientation to Portrait
            //    MainSection.IsLandscape = false;

            //// add image to paragraphs collection of section
            //MainSection.Paragraphs.Add(sample_image);
            //// save the resultant PDF
            //pdf.Save(@"d:/pdftest/Image-to-PDF-Conversion.pdf");

            try
            {  
                Aspose.Pdf.Generator.Pdf pdf1 = new Aspose.Pdf.Generator.Pdf();
             
                Aspose.Pdf.Generator.Section sec1 = pdf1.Sections.Add();
                Aspose.Pdf.Generator.Image image1 = new Aspose.Pdf.Generator.Image();              
                image1.ImageInfo.File = sourcePath;
                image1.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Bmp;
                sec1.Paragraphs.Add(image1);   //
                sec1.PageInfo.Margin.Left =sec1.PageInfo.Margin.Left/4;
                sec1.PageInfo.Margin.Right = sec1.PageInfo.Margin.Right/4;
                sec1.PageInfo.Margin.Top = sec1.PageInfo.Margin.Top/100;
                sec1.PageInfo.Margin.Bottom = sec1.PageInfo.Margin.Bottom*2;
                image1.ImageInfo.FixWidth = sec1.PageInfo.PageWidth -sec1.PageInfo.Margin.Left- sec1.PageInfo.Margin.Right;
                image1.ImageInfo.FixHeight = sec1.PageInfo.PageHeight - sec1.PageInfo.Margin.Top - sec1.PageInfo.Margin.Bottom;
                System.Drawing.Bitmap myimage = new System.Drawing.Bitmap(sourcePath);
              
                if (myimage.Width > sec1.PageInfo.PageWidth)
                    sec1.IsLandscape = true;
                else
                    sec1.IsLandscape = false;         
                myimage.Dispose();                
                pdf1.Save(targetPath);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取图片压缩比例
        /// </summary>
        /// <param name="h"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public int getPercent(float h, float w)
        {
            int p = 0;
            float p2 = 0.0f;
            if (h > w)
            {
                p2 = 297 / h * 100;
            }
            else
            {
                p2 = 210 / w * 100;
            }
            p = Convert.ToInt32(Math.Round(p2, 0));
            return p;
        }
   
        /// <summary>
        /// 向临时转换的pdf添加首页；
        /// </summary>
        private void addLastpage2pdf(string pdftemppath, string pdfpath, int id, documentType docType)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(pdftemppath);
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                //指定字体库并创建字体
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\STSONG.TTF", BaseFont.IDENTITY_H,BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15, iTextSharp.text.Font.NORMAL);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(pdfpath, FileMode.Create));
                int n = pdfReader.NumberOfPages;
                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, document.PageSize.Height, 1f);
                document.Open();
                //先复制文档，把简介，二维码放在最后
                PdfContentByte pdfCB = pdfWriter.DirectContent;
                PdfImportedPage newPage;
                for (int i = 1; i <= n; i++)
                {
                    document.NewPage();
                    newPage = pdfWriter.GetImportedPage(pdfReader, i);
                    pdfCB.AddTemplate(newPage, 0, 0);
                }
                document.NewPage();
                //根据上传文档类型 创建尾页以及二维码
                if (docType == documentType.demand)
                {
                    var demand = RepositoryFactory.DemandRepository.LoadEntities(Demand => Demand.Demand_ID == ID).FirstOrDefault();
                    var demandName = new iTextSharp.text.Paragraph("需求名称:" + demand.Demand_Name, font);
                    demandName.Alignment = Element.ALIGN_CENTER;
                    document.Add(demandName);
                    var demandDestitle = new iTextSharp.text.Paragraph("需求简介:", font);
                    demandDestitle.Alignment = Element.ALIGN_LEFT;
                    document.Add(demandDestitle);
                    string des =@"<html><head></head><body>"+System.Web.HttpUtility.HtmlDecode(demand.Demand_Description)+"</body></html>";
                    //Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
                    Regex regex = new Regex(@"<[^>]*>");
                    des = regex.Replace(des, "");

                    des = des.Replace("\t", "");
                    des = des.Replace("&nbsp;", "");

                    var demandDes = new iTextSharp.text.Paragraph("", font);
                    
                    iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                    StringReader stringread = new StringReader(des);
                    StyleSheet st = new StyleSheet();

                    ArrayList content = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(stringread,null);
                    for (int i=0;i<content.Count;i++)
                    {
                        demandDes.Font = font;
                        
                        demandDes.Add((iTextSharp.text.IElement)content[i]);

                    }
                        document.Add(demandDes);
        
                    var qrCode = new QRCode();
                    string QRCodeString="{\"ID\":"+id.ToString()+",\"TYPE\":Demand"+",\"APP\":CPC}";
                    var imageCode = qrCode.GetDimensionalCode(QRCodeString);
                    MemoryStream ms = new MemoryStream();
                    imageCode.Save(ms, ImageFormat.Jpeg);
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    image.Alignment = Element.ALIGN_MIDDLE;
                    document.Add(image);
                }
                if (docType == documentType.bid)
                {
                    var bid = RepositoryFactory.BidRepository.LoadEntities(Bid => Bid.Bid_ID == ID).FirstOrDefault();
                    var demandName = new iTextSharp.text.Paragraph("竞标的需求名称:" + bid.Demand_Name, font);
                    demandName.Alignment = Element.ALIGN_CENTER;
                    document.Add(demandName);

                    var BidDestitle = new iTextSharp.text.Paragraph("竞标方案简介:", font);
                    BidDestitle.Alignment = Element.ALIGN_LEFT;
                    document.Add(BidDestitle);
                    string des = @"<html><head></head><body>" + System.Web.HttpUtility.HtmlDecode(bid.Bid_Content) + "</body></html>";

                    Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
                    des = regex.Replace(des, "");
                    des = des.Replace("\t", "");
                    des = des.Replace("&nbsp;", "");

                    var bidDes = new iTextSharp.text.Paragraph("", font);

                    iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                    StringReader stringread = new StringReader(des);
                    StyleSheet st = new StyleSheet();

                    ArrayList content = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(stringread, null);
                    for (int i = 0; i < content.Count; i++)
                    {
                        bidDes.Font = font;

                        bidDes.Add((iTextSharp.text.IElement)content[i]);

                    }
                    document.Add(bidDes);

                    //var bidContent = new Paragraph("竞标简介:" + bid.Bid_Content, font);
                    //bidContent.Alignment = Element.ALIGN_LEFT;
                    //document.Add(bidContent);
                    var qrCode = new QRCode();
                    string QRCodeString = "{\"ID\":" + id.ToString() + ",\"TYPE\":Bid" + ",\"APP\":CPC}";
                    var imageCode = qrCode.GetDimensionalCode(QRCodeString);
                    MemoryStream ms = new MemoryStream();
                    imageCode.Save(ms, ImageFormat.Jpeg);
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    image.Alignment = Element.ALIGN_MIDDLE;
                    document.Add(image);
                }
                if (docType == documentType.order)
                {
                    var order = RepositoryFactory.OrderRepository.LoadEntities(Order => Order.Order_ID == ID).FirstOrDefault();
                    var orderID = new iTextSharp.text.Paragraph("订单编号:" + order.Order_ID, font);
                    orderID.Alignment = Element.ALIGN_CENTER;
                    document.Add(orderID);
                    var orderDestitle = new iTextSharp.text.Paragraph("订单简介:", font);
                    orderDestitle.Alignment = Element.ALIGN_LEFT;
                    document.Add(orderDestitle);

                    string des = @"<html><head></head><body>" + System.Web.HttpUtility.HtmlDecode(order.Order_Content) + "</body></html>";
               
                    Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
                    des = regex.Replace(des, "");
                    des = des.Replace("\t", "");
                    des = des.Replace("&nbsp;", "");
                    var orderDes = new iTextSharp.text.Paragraph("", font);

                    iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                    StringReader stringread = new StringReader(des);
                    StyleSheet st = new StyleSheet();

                    ArrayList content = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(stringread, null);
                    for (int i = 0; i < content.Count; i++)
                    {
                        orderDes.Font = font;

                        orderDes.Add((iTextSharp.text.IElement)content[i]);

                    }
                    document.Add(orderDes);

                    //var orderDes = new Paragraph("简介:" + order.Order_Content, font);
                    //orderDes.Alignment = Element.ALIGN_LEFT;
                    //document.Add(orderDes);
                    var qrCode = new QRCode();
                    string QRCodeString = "{\"ID\":" + id.ToString() + ",\"TYPE\":Order" + ",\"APP\":CPC}";
                    var imageCode = qrCode.GetDimensionalCode(QRCodeString);
                    MemoryStream ms = new MemoryStream();
                    imageCode.Save(ms, ImageFormat.Jpeg);
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    image.Alignment = Element.ALIGN_MIDDLE;
                    document.Add(image);
                }
                if (docType == documentType.refund) 
                {
                   
                    IEnterpriseRepository _IEnterpriseRepository = RepositoryFactory.EnterpriseRepository;
                   
                    var returngoods = RepositoryFactory.ReturnGoodsRepository.LoadEntities(item => item.Return_ID == ID).FirstOrDefault();
                    string NameOfSender = _IEnterpriseRepository.LoadEntities(item => item.Enterprise_ID == returngoods.ReturnEnterprise_ID).FirstOrDefault().Enterprise_Name;
                    string NameOfReceive = _IEnterpriseRepository.LoadEntities(item => item.Enterprise_ID == returngoods.ReceiveEnterprise_ID).FirstOrDefault().Enterprise_Name;    
                  
                    var ReturnGoodsName = new iTextSharp.text.Paragraph("退货单名称：" + returngoods.ReturnGoods_Name, font);
                    ReturnGoodsName.Alignment = Element.ALIGN_CENTER;
                    document.Add(ReturnGoodsName);
                    document.Add(new iTextSharp.text.Paragraph("_",font));
                    float[] widths = { 0.4f, 0.6f };
                    PdfPTable table = new PdfPTable(widths);
                    PdfPCell cell = new PdfPCell(new iTextSharp.text.Paragraph("退货企业:", font));
                    table.AddCell(cell);

                    cell = new PdfPCell(new iTextSharp.text.Paragraph(NameOfSender, font));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    cell = new PdfPCell(new iTextSharp.text.Paragraph("接收企业:", font));
                    table.AddCell(cell);
                    cell = new PdfPCell(new iTextSharp.text.Paragraph(NameOfReceive, font));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    cell = new PdfPCell(new iTextSharp.text.Paragraph("退货数量:", font));
                    table.AddCell(cell);
                    cell = new PdfPCell(new iTextSharp.text.Paragraph(returngoods.Return_Amount.ToString(), font));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    cell = new PdfPCell(new iTextSharp.text.Paragraph("退货企业地址:", font));
                    table.AddCell(cell);
                    cell = new PdfPCell(new iTextSharp.text.Paragraph(returngoods.Source_Addr, font));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    cell = new PdfPCell(new iTextSharp.text.Paragraph("接收企业地址:", font));
                    table.AddCell(cell);
                    cell = new PdfPCell(new iTextSharp.text.Paragraph(returngoods.Destination_Addr, font));
                    cell.Colspan = 2;
                    table.AddCell(cell);
         
                    widths[0] = 150f;
                    widths[1] = 450f;
                   

                    iTextSharp.text.Rectangle r = new iTextSharp.text.Rectangle(iTextSharp.text.PageSize.A4.Right, iTextSharp.text.PageSize.A4.Top+72);
                    table.SetWidthPercentage(widths,r);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    document.Add(table);  
                    var ReturnGoodstitle = new iTextSharp.text.Paragraph("退货单简介：", font);

                    ReturnGoodstitle.Alignment = Element.ALIGN_LEFT;
                    document.Add(ReturnGoodstitle);
                
                    string des = @"<html><head></head><body>" + System.Web.HttpUtility.HtmlDecode(returngoods.ReturnGoods_Content) + "</body></html>";

                    Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
                    des = regex.Replace(des, "");
                    des = des.Replace("\t", "");
                    des = des.Replace("&nbsp;", "");
                    var ReturnGoodsDes = new iTextSharp.text.Paragraph("", font);

                    iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                    StringReader stringread = new StringReader(des);
                    StyleSheet st = new StyleSheet();

                    ArrayList content = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(stringread, null);
                    for (int i = 0; i < content.Count; i++)
                    {
                        ReturnGoodsDes.Font = font;

                        ReturnGoodsDes.Add((iTextSharp.text.IElement)content[i]);

                    }
                    document.Add(ReturnGoodsDes);
                    var qrCode = new QRCode();
                    string QRCodeString = "{\"ID\":" + id.ToString() + ",\"TYPE\":Refund" + ",\"APP\":CPC}";
                    var imageCode = qrCode.GetDimensionalCode(QRCodeString);
                    MemoryStream ms = new MemoryStream();
                    imageCode.Save(ms, ImageFormat.Jpeg);
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    image.Alignment = Element.ALIGN_MIDDLE;
                    document.Add(image);
                }

                document.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //finally
            //{
                if (File.Exists(pdftemppath))
                {
                    File.Delete(pdftemppath); 
                }

            //}


        }


    }
}
