using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using System.IO;
using Backend.DAL;

namespace Backend.IMPL {

    public class QRImpl {

        public byte[] Get_QR_Asistance(string url) {
            return genQR(url);
        }


        public byte[] genQR(string information) {

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(information, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            byte[] res;
            using (Bitmap bitMap = qrCode.GetGraphic(20)) {
                using (MemoryStream ms = new MemoryStream()) {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    res = ms.ToArray();
                }
            }
            return res;
        }
    }

}
