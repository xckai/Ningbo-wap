using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;

namespace MobileWebSite.BLL.ToPDF
{
    public class QRCode
    {
        public Image GetDimensionalCode(string url)
        {
            Image img = null;
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                img = qrCodeEncoder.Encode(url);
            }
            catch
            {
            }
            return img;
        }
    }
}