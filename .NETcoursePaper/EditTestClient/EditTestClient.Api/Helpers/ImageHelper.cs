using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EditTestClient.Api.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ImageToByte(string path)
		{
			// todo(v): мб return File.ReadAllBytes(path); ?
            if (string.IsNullOrEmpty(path)) return null;
            byte[] bmpBytes;
            using (var ms = new MemoryStream())
            {

                using (var bmp = new Bitmap(path))
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bmpBytes = ms.GetBuffer();
                }

            }
            return bmpBytes;
        }

		// todo(v): мб объединить с ByteToBitMap? или сделать третий метод, который будет вызывать оба
		public static BitmapSource BitmapToBitmapSource(Bitmap source)
        {
            if (source == null) return null;
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                source.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        public static Bitmap ByteToBitMap(byte[] image)
        {
            if (image == null) return null;
            Bitmap bmp;
            using (var ms = new MemoryStream(image))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }
    }
}

        
    

