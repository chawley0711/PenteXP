using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PenteApplication
{
    class ColorConverter : IValueConverter
    {
        //Collin and Jordon
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageBrush ib = new ImageBrush();
            ib.Stretch = Stretch.Fill;
            Fill f = (Fill)value;
            if(f == Fill.White)
            {
                ib.ImageSource = new BitmapImage(new Uri("@../../../../white.png", UriKind.Relative));
            }
            if (f == Fill.Black)
            {
                ib.ImageSource = new BitmapImage(new Uri("@../../../../black.png", UriKind.Relative));
            }
            if (f == Fill.Empty)
            {
                ib.ImageSource = null;
            }
            return ib;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
;