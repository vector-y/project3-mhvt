using Cecs475.BoardGames.Chess.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cecs475.BoardGames.Chess.WpfView
{
    class ChessImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(new Uri("/Resources/blackPawn.png", UriKind.Relative));
            /* try
             {
                 ChessPiece chesspiece = (ChessPiece)value;
                 int player = chesspiece.Player;
                 String playerString = "";
                 if(player == 1)
                 {
                     playerString = "white";
                 }
                 if(player == 2)
                 {
                     playerString = "black";
                 }

                 if(chesspiece.PieceType != ChessPieceType.Empty)
                 {
                     if(chesspiece.PieceType == ChessPieceType.Pawn)
                     {
                         String pieceType = "Pawn";
                         return new BitmapImage(new Uri("/Resources/" + playerString + pieceType + ".png", UriKind.Relative));

                     }
                     else
                     {
                         return null;
                     }
                 }
                 else
                 {
                     return null;
                 }
             }
             catch (Exception e)
             {
                 return null;
             }*/

        }

        private Brush GetFillBrush(int player)
        {
            return player == 1 ? Brushes.Black : Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
