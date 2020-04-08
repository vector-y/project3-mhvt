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
        private static SolidColorBrush CHECK_BRUSH = Brushes.Yellow;
        private static SolidColorBrush SELECTED_BRUSH = Brushes.Red;
        private static SolidColorBrush DEFAULT_BRUSH = Brushes.Brown;
        private static SolidColorBrush DEFAULT_TWO_BRUSH = Brushes.Tan;
        private static SolidColorBrush POSSIBLE_MOVE_BRUSH = Brushes.LightGreen;


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            BoardPosition pos = (BoardPosition)values[0];
            bool isHighlighted = (bool)values[1];
            bool isSelected = (bool)values[2];
            bool isPossible = (bool)values[3];
            bool isCheck = (bool)values[4];
            if (isSelected)
            {
                return SELECTED_BRUSH;
            }
            if (isCheck)
            {
                return CHECK_BRUSH;
            }
            if (isPossible)
            {
                return POSSIBLE_MOVE_BRUSH;
            }
           
            if (isHighlighted)
            {
                return POSSIBLE_MOVE_BRUSH;
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
