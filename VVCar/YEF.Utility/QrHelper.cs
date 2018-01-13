using System.IO;
using Gma.QrCodeNet.Encoding;
using System.Drawing;
using System.Drawing.Imaging;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace YEF.Utility
{
    public class QrHelper
    {
        public static Image Create(string url)
        {
            var ms = QrStream(url);
            return new Bitmap(Image.FromStream(ms), new Size(300, 300));
        }

        private static MemoryStream QrStream(string url)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(url);
            var renderer = new GraphicsRenderer(new FixedModuleSize(9, QuietZoneModules.Two));
            var ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Jpeg, ms);
            ms.Position = 0;
            return ms;
        }

        public static byte[] ImageBuffer(string url)
        {
            var ms = QrStream(url);
            var buffer = new byte[ms.Length];
            ms.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
