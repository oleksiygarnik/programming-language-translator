using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CompilerDevelopment
{
    class ContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DrawingGroup draw = (value as DrawingBrush).Drawing as DrawingGroup;
            PathGeometry path = new PathGeometry();

            foreach (GeometryDrawing item in draw.Children)
            {
                path.AddGeometry(item.Geometry);
            }

            return path;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
