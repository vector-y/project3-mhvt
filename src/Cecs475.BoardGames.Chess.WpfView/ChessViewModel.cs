using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.WpfView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cecs475.BoardGames.Chess.WpfView
{
    public class ChessSquare : INotifyPropertyChanged
    {
        private ChessPiece mChessPiece;

        public ChessPiece chessPiece
        {
            get
            {
                return mChessPiece;
            }
            set
            {
                if (!value.Equals(mChessPiece))
                {
                    mChessPiece = value;
                    OnPropertyChanged(nameof(chessPiece));
                }
            }
        }
        public BoardPosition Position
        {
            get; set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private bool mIsHighlighted;
        /// <summary>
        /// Whether the square should be highlighted because of a user action.
        /// </summary>
        public bool IsHighlighted
        {
            get { return mIsHighlighted; }
            set
            {
                if (value != mIsHighlighted)
                {
                    mIsHighlighted = value;
                    OnPropertyChanged(nameof(IsHighlighted));
                }
            }
        }

        private bool mIsSelected;
        public bool IsSelected
        {
            get { return mIsSelected; }
            set
            {
                if (value != mIsSelected)
                {
                    mIsSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }


        private bool mIsPossible;
        public bool IsPossible
        {
            get { return mIsPossible; }
            set
            {
                if (value != mIsPossible)
                {
                    mIsPossible = value;
                    OnPropertyChanged(nameof(IsPossible));
                }
            }
        }

        private bool mIsCheck;
        public bool IsCheck
        {
            get { return mIsCheck; }
            set
            {
                if(value != mIsCheck)
                {
                    mIsCheck = value;
                    OnPropertyChanged(nameof(IsCheck));
                }
            }
        }
    }
    public class ChessViewModel : INotifyPropertyChanged, IGameViewModel
    {
        private ChessBoard mChessBoard;
        private ObservableCollection<ChessSquare> mSquares;

        public event EventHandler GameFinished;
        public event PropertyChangedEventHandler PropertyChanged;

        public ChessViewModel()
        {
            mChessBoard = new ChessBoard();
            mSquares = new ObservableCollection<ChessSquare>(
                BoardPosition.GetRectangularPositions(8, 8)
                .Select(pos => new ChessSquare()
                {
                    Position = pos,
                    chessPiece = mChessBoard.GetPieceAtPosition(pos)
                })
            );


            //might  not be select from m.startposition
            PossibleMoves = new HashSet<ChessMove>(
                from ChessMove m in mChessBoard.GetPossibleMoves()
                select m
            ) ;

        }
        public ObservableCollection<ChessSquare> ChessSquares
        {
            get { return mSquares; }
        }
        public int CurrentPlayer
        {
            get { return mChessBoard.CurrentPlayer; }
        }
        public HashSet<ChessMove> PossibleMoves { get; private set; }

        public GameAdvantage BoardAdvantage => mChessBoard.CurrentAdvantage;


        public bool CanUndo => mChessBoard.MoveHistory.Any();

        public void UndoMove()
        {
            if (CanUndo)
            {
                mChessBoard.UndoLastMove();
                RebindState();
            }
           
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void ApplyMove(BoardPosition start_position, BoardPosition end_position)
        {
            var possMoves = mChessBoard.GetPossibleMoves() as IEnumerable<ChessMove>;
            // Validate the move as possible.
            foreach (var move in possMoves)
            {
                
                if (move.EndPosition.Equals(end_position) && move.StartPosition.Equals(start_position))
                {
                    if (move.MoveType == ChessMoveType.PawnPromote)
                    {
                        //open new window
                        //in new window return chesspieceType
                        //apply in the new window and break
                        var pawnPromotionSelect = new ChessPromotionView(this,start_position, end_position);
                        pawnPromotionSelect.Show();
                        break;
                    }
                    else
                    {
                        mChessBoard.ApplyMove(move);
                        
                        break;
                    }
                    
                }
            }

            RebindState();
            if (mChessBoard.IsFinished)
            {
                GameFinished?.Invoke(this, new EventArgs());
            }
        }
        internal void ApplyPromotionMove(BoardPosition start_position, BoardPosition end_position,ChessPieceType pieceType)
        {
            mChessBoard.ApplyMove(new ChessMove(start_position,end_position,pieceType));
            RebindState();
            if (mChessBoard.IsFinished)
            {

                GameFinished?.Invoke(this, new EventArgs());
            }
        }

        public void RebindState()
        {
            //might  not be select from m.startposition
            PossibleMoves = new HashSet<ChessMove>(
               from ChessMove m in mChessBoard.GetPossibleMoves()
               select m
           );
            // Update the collection of squares by examining the new board state.
            var newSquares = BoardPosition.GetRectangularPositions(8, 8);
            int i = 0;
            foreach (var pos in newSquares)
            {
                mSquares[i].chessPiece = mChessBoard.GetPieceAtPosition(pos);

                //if piece at position is not king, set ischeck to false
                if(mSquares[i].chessPiece.PieceType != ChessPieceType.King)
                {
                    mSquares[i].IsCheck = false;
                }
                //if peice is in check and it's their turn, check if he is in check
                if (mSquares[i].chessPiece.PieceType == ChessPieceType.King && CurrentPlayer == mSquares[i].chessPiece.Player)
                {
                    mSquares[i].IsCheck = mChessBoard.IsCheck || mChessBoard.IsCheckmate;
                }
                //if not then check if the enemy is in check
                if (mSquares[i].chessPiece.PieceType == ChessPieceType.King && CurrentPlayer != mSquares[i].chessPiece.Player)
                {
                    mSquares[i].IsCheck = mChessBoard.isEnemyCheck();
                }

                i++;
            }

            OnPropertyChanged(nameof(BoardAdvantage));
            OnPropertyChanged(nameof(CurrentPlayer));
            OnPropertyChanged(nameof(CanUndo));
        }
    }
}
