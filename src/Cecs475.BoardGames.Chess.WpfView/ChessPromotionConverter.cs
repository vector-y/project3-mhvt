using Cecs475.BoardGames.Chess.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Cecs475.BoardGames.Chess.WpfView
{
    class ChessPromotionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                ChessPiece chesspiece = (ChessPiece)value;
                int player = chesspiece.Player;
                String playerString = "";
                if (player == 1)
                {
                    playerString = "white";
                }
                if (player == 2)
                {
                    playerString = "black";
                }
                return new BitmapImage(new Uri("/Cecs475.BoardGames.Chess.WpfView;component/Resources/" + playerString + "Bishop.png", UriKind.Relative));

            }
            catch (Exception e) {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
