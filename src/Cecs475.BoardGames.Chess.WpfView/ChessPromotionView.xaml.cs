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
        public ChessPromotionView(ChessViewModel cvm,BoardPosition startPos, BoardPosition endPos)
        {
            InitializeComponent();
            int currentplayer = cvm.CurrentPlayer;
            //how to add to the view?
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            var element = (UIElement)e.Source;
            int col = Grid.GetColumn(element);

            if(col == 1)
            {
                b.Background = Brushes.LightGreen;

            }
            if (col == 2)
            {
                b.Background = Brushes.Orange;
            }
            if (col == 3)
            {
                b.Background = Brushes.LightYellow;
            }
            if (col == 4)
            {
                b.Background = Brushes.DarkBlue;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            b.Background = Brushes.LightBlue;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
