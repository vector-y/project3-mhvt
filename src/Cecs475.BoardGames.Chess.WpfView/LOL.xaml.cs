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
    public partial class Border_MouseEnter : UserControl
    {

        public Border_MouseEnter(BoardPosition startPos, BoardPosition endPos)
        {
            InitializeComponent();
        }
        private Border_MouseEnter()
        {

        }
    }
}
