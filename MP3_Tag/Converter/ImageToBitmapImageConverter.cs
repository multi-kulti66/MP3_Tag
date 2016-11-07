// ///////////////////////////////////
// File: ImageToBitmapImageConverter.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Converter
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;



    [ValueConversion(typeof(Image), typeof(ImageSource))]
    public class ImageToBitmapImageConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var image = (Image)value;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();

            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Png);
            memoryStream.Seek(0, SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();

            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}