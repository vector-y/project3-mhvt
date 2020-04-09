using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cecs475.BoardGames.Chess.WpfView
{
    /// <summary>
    /// Interaction logic for ChessPromotionView.xaml
    /// </summary>
    public partial class ChessPromotionView : Window
    {
        private ChessViewModel temp;
        BoardPosition start_position;
        BoardPosition end_position;
        int current_col;
        public ChessPromotionView(ChessViewModel cvm,BoardPosition startPos, BoardPosition endPos)
        {
            InitializeComponent();
            temp = cvm;
            start_position = startPos;
            end_position = endPos;
            int currentplayer = temp.CurrentPlayer;
            String playerString = "";
            if(currentplayer == 1)
            {
                playerString = "white";
            }
            else
            {
                playerString = "black";
            }
            var bishop = new BitmapImage(new Uri("/Cecs475.BoardGames.Chess.WpfView;component/Resources/" + playerString +"Bishop.png", UriKind.Relative));
            var knight = new BitmapImage(new Uri("/Cecs475.BoardGames.Chess.WpfView;component/Resources/" + playerString + "Knight.png", UriKind.Relative));
            var rook = new BitmapImage(new Uri("/Cecs475.BoardGames.Chess.WpfView;component/Resources/" + playerString + "Rook.png", UriKind.Relative));
            var queen = new BitmapImage(new Uri("/Cecs475.BoardGames.Chess.WpfView;component/Resources/" + playerString + "Queen.png", UriKind.Relative));

            //how to add to the view?
            this.Resources.Add("ChessPromotionBishop", bishop);
            this.Resources.Add("ChessPromotionKnight", knight);
            this.Resources.Add("ChessPromotionRook", rook);
            this.Resources.Add("ChessPromotionQueen", queen);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            var element = (UIElement)e.Source;
            int col = Grid.GetColumn(element);

            if(col == 1)
            {
                b.Background = Brushes.LightGreen;
                current_col = 1;
            }
            if (col == 2)
            {
                b.Background = Brushes.Orange;
                current_col = 2;

            }
            if (col == 3)
            {
                b.Background = Brushes.LightYellow;
                current_col = 3;

            }
            if (col == 4)
            {
                current_col = 4;
                b.Background = Brushes.DarkBlue;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            b.Background = Brushes.LightBlue;
            current_col = 0;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (current_col == 1)
            {
                temp.ApplyPromotionMove(start_position, end_position, ChessPieceType.Bishop);
            }
            if (current_col == 2)
            {
                temp.ApplyPromotionMove(start_position, end_position, ChessPieceType.Knight);
            }
            if (current_col == 3)
            {
                temp.ApplyPromotionMove(start_position, end_position, ChessPieceType.Rook);
            }
            if (current_col == 4)
            {
                temp.ApplyPromotionMove(start_position, end_position, ChessPieceType.Queen);
            }
            this.Close();

        }
    }
}
