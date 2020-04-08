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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cecs475.BoardGames.Chess.WpfView
{
    /// <summary>
    /// Interaction logic for ChessPromotionView.xaml
    /// </summary>
    public partial class ChessPromotionView : UserControl
    {
        public ChessPromotionView(BoardPosition startPos, BoardPosition endPos)
        {
            InitializeComponent();
        }
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            var element = (UIElement)e.Source;
            int col = Grid.GetColumn(element);

        }
        private void Border_MouseUp(object sender, MouseEventArgs e)
        {

        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
