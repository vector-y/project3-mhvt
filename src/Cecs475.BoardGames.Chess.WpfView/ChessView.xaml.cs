using Cecs475.BoardGames.WpfView;
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
    /// Interaction logic for ChessView.xaml
    /// </summary>
    public partial class ChessView : UserControl, IWpfGameView
    {
        ChessSquare selectedSquare;
        public ChessView()
        {
            InitializeComponent();
        }
        public ChessViewModel ChessViewModel => FindResource("vm") as ChessViewModel;

        public Control ViewControl => this;

        public IGameViewModel ViewModel => ChessViewModel;

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            var square = b.DataContext as ChessSquare;
            var vm = FindResource("vm") as ChessViewModel;

            foreach(var move in vm.PossibleMoves)
            {
                //if possible move
                if(selectedSquare != null) {
                    if (move.StartPosition == selectedSquare.Position && move.EndPosition == square.Position)
                    {
                        square.IsPossible = true;
                    }
                }
                
                //if its a valid piece to move
                if(move.StartPosition == square.Position)
                {
                    square.IsHighlighted = true;
                }
            }
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Border b = sender as Border;
            var square = b.DataContext as ChessSquare;
            var vm = FindResource("vm") as ChessViewModel;
            foreach (var move in vm.PossibleMoves)
            {
                if (move.StartPosition == square.Position)
                {
                    //if the seleceted square is not the same as before
                    if (selectedSquare != null && selectedSquare.Position != square.Position)
                    {
                        selectedSquare.IsSelected = false;
                    }
                    //initialize selectedSquare
                    selectedSquare = square;
                    square.IsHighlighted = true;
                    square.IsSelected = true;
                }
            }
            //if there is a selected square, 
            if (selectedSquare != null && selectedSquare.Position != square.Position)
            {
                vm.ApplyMove(selectedSquare.Position, square.Position);
                selectedSquare.IsSelected = false;
                selectedSquare = null;
            }

        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            var square = b.DataContext as ChessSquare;
            square.IsHighlighted = false;
            square.IsPossible = false;
        }
    }

}
