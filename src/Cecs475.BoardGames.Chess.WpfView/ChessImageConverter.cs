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
            try
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

                if (chesspiece.PieceType != ChessPieceType.Empty)
                {
                    String pieceType = "";
                    if (chesspiece.PieceType == ChessPieceType.Pawn)
                    {
                        pieceType = "Pawn";
                    }
                    if (chesspiece.PieceType == ChessPieceType.Rook)
                    {
                        pieceType = "Rook";
                    }
                    if (chesspiece.PieceType == ChessPieceType.Bishop)
                    {
                        pieceType = "Bishop";
                    }
                    if (chesspiece.PieceType == ChessPieceType.Knight)
                    {
                        pieceType = "Knight";
                    }
                    if (chesspiece.PieceType == ChessPieceType.Queen)
                    {
                        pieceType = "Queen";
                    }
                    if (chesspiece.PieceType == ChessPieceType.King)
                    {
                        pieceType = "King";
                    }
                    return new BitmapImage(new Uri("/Cecs475.BoardGames.Chess.WpfView;component/Resources/" + playerString + pieceType + ".png", UriKind.Relative));
                }
                return null;

            }
            catch (Exception e)
             {
                 return null;
             }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
