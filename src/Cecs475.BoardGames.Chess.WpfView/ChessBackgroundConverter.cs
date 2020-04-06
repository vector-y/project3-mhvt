using Cecs475.BoardGames.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Cecs475.BoardGames.Chess.WpfView
{
    class ChessBackgroundConverter : IMultiValueConverter
    {
        private static SolidColorBrush HIGHLIGHT_BRUSH = Brushes.DarkRed;
        private static SolidColorBrush DEFAULT_BRUSH = Brushes.Brown;
        private static SolidColorBrush DEFAULT_TWO_BRUSH = Brushes.Tan;


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            BoardPosition pos = (BoardPosition)values[0];
            bool isHighlighted = (bool)values[1];
            if (isHighlighted)
            {
                return HIGHLIGHT_BRUSH;
            }

            if(pos.Row % 2 == pos.Col % 2)
            {
                return DEFAULT_BRUSH;
            } 
            if(pos.Row % 2 != pos.Col % 2)
            {
                return DEFAULT_TWO_BRUSH;
            }

            return DEFAULT_BRUSH;
      

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
