using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Cecs475.BoardGames.Model;
using System.Linq;
using System.Reflection;

namespace Cecs475.BoardGames.Chess.Model {
	/// <summary>
	/// Represents the board state of a game of chess. Tracks which squares of the 8x8 board are occupied
	/// by which player's pieces.
	/// </summary>	
	/// </summary>	
	public class ChessBoard : IGameBoard
	{
		#region Member fields.
		// The history of moves applied to the board.
		//private List<ChessMove> mMoveHistory = new List<ChessMove>();
		public List<ChessMove> mMoveHistory = new List<ChessMove>();

		public const int BoardSize = 8;

		// TODO: create a field for the board position array. You can hand-initialize
		// the starting entries of the array, or set them in the constructor.
		private byte[] board = new byte[32] { 0b_1010_1011, 0b_1100_1101, 0b_1110_1100, 0b_1011_1010,
												  0b_1001_1001, 0b_1001_1001, 0b_1001_1001, 0b_1001_1001,
												  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
												  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
												  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
												  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
												  0b_0001_0001, 0b_0001_0001, 0b_0001_0001, 0b_0001_0001,
												  0b_0010_0011, 0b_0100_0101, 0b_0110_0100, 0b_0011_0010,
		};
		/*private byte[] board = new byte[32] { 0b_0000_0000, 0b_0000_1110, 0b_1101_0000, 0b_0000_0000,
											  0b_0001_0000, 0b_1110_0000, 0b_0000_0001, 0b_0000_0000,
											  0b_0000_0001, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_1001_0000, 0b_0000_0110, 0b_0000_0000, 0b_0000_0000,
											  0b_0000_0001, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
		};*/
		/*private byte[] board = new byte[32] { 0b_0000_000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											 0b_1110_0000, 0b_0110_0000, 0b_0000_0000, 0b_0000_0000,
											0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,};*/

		/*sBoard b = CreateBoardFromPositions(
				Pos("a1"), ChessPieceType.King, 1,
				Pos("b8"), ChessPieceType.King, 2,
				Pos("c7"), ChessPieceType.Knight, 2,
				Pos("h6"), ChessPieceType.Pawn, 2,
				Pos("e6"), ChessPieceType.Pawn, 1,
				Pos("d6"), ChessPieceType.Bishop, 1*/
		/*private byte[] board = new byte[32] {  0b_0000_1110, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_0000_0000, 0b_1011_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_0001_0000, 0b_0000_0100, 0b_0001_0000, 0b_0000_1001,
											  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											   0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
											  0b_0110_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
		};*/

		// TODO: Add a means of tracking miscellaneous board state, like captured pieces and the 50-move rule.
		//add empty
		public List<ChessPiece> capturedPieces = new List<ChessPiece>();

		// TODO: add a field for tracking the current player and the board advantage.		
		private int currentPlayer = 1;
		private int current_advantage = 0;
		private GameAdvantage adv = new GameAdvantage(0,0);

		private List<int> counter = new List<int>() {};
		void printCounter()
		{
			foreach(int i in counter)
			{
				//Console.WriteLine((i));
			}
		}
		//directions for rook, bishop, and queen
		//directions default for queen
		IEnumerable<BoardDirection> CardinalDirections = BoardDirection.CardinalDirections;
		IEnumerable<BoardDirection> rookDirections
			= new BoardDirection[] {
				new BoardDirection(-1, 0),
				new BoardDirection( 1, 0),
				new BoardDirection( 0, 1),
				new BoardDirection( 0, -1)
		};
		IEnumerable<BoardDirection> bishopDirections
			= new BoardDirection[] {
				new BoardDirection(-1, -1),
				new BoardDirection(-1, 1),
				new BoardDirection( 1, -1),
				new BoardDirection( 1, 1)
		};
		IEnumerable<BoardDirection> knightDirections
				= new BoardDirection[] {
				new BoardDirection(2, 1),
				new BoardDirection(2, -1),
				new BoardDirection( 1, 2),
				new BoardDirection( 1, -2),
				new BoardDirection( -1, 2),
				new BoardDirection( -1, -2),
				new BoardDirection(-2, 1),
				new BoardDirection(-2, -1)
		};
		IEnumerable<BoardDirection> PlayerOnePawnDirections
			= new BoardDirection[] {
				new BoardDirection(-1, 0),
				new BoardDirection(-2, 0),
				new BoardDirection(-1, -1),
				new BoardDirection(-1, 1),
		};
		IEnumerable<BoardDirection> PlayerTwoPawnDirections
			= new BoardDirection[] {
				new BoardDirection(1, 0),
				new BoardDirection(2, 0),
				new BoardDirection(1, -1),
				new BoardDirection(1, 1),
		};
		//variables to check if rooks have moved or not
		public bool hasWhiteLeftRookMove = false;
		public bool hasWhiteRightRookMove = false;
		public bool hasBlackLeftRookMove = false;
		public bool hasBlackRightRookMove = false;

		public bool hasWhiteKingMove = false;
		public bool hasBlackKingMove = false;


		#endregion

		#region Properties.
		// TODO: implement these properties.
		// You can choose to use auto properties, computed properties, or normal properties 
		// using a private field to back the property.

		// You can add set bodies if you think that is appropriate, as long as you justify
		// the access level (public, private).

	public bool IsFinished { get { return IsCheckmate || IsDraw || IsStalemate; } }

	public int CurrentPlayer
		{
			get
			{
				return currentPlayer;
			}
			set { currentPlayer = value; }
		}
		//make a copy of a the captured piece stack, check for who has more points
    public GameAdvantage CurrentAdvantage{
			get
			{
				return adv;
			}
		}

		public IReadOnlyList<ChessMove> MoveHistory => mMoveHistory;

		// TODO: implement IsCheck, IsCheckmate, IsStalemate
		public bool IsCheck
		{
			get
			{
				IEnumerable<BoardPosition> kingLocation = GetPositionsOfPiece(ChessPieceType.King, CurrentPlayer);
				BoardPosition pos = kingLocation.First();
				
				int enemy = (CurrentPlayer == 1) ? 2 : 1;
				if (PositionIsAttacked(pos, enemy) && !IsCheckmate)
				{
					return true;
				}
				return false;
			}

		}
		public bool IsCheckmate
		{
			get {
				int enemy = (currentPlayer == 1) ? 2 : 1;
				BoardPosition king_position = GetPositionsOfPiece(ChessPieceType.King, currentPlayer).First();
				return PositionIsAttacked(king_position,enemy) && GetPossibleMoves().Count() == 0;
			}
		}

		public bool IsStalemate
		{
			get { return !IsCheck && !IsCheckmate && GetPossibleMoves().Count() == 0; }
		}

		public bool IsDraw
		{
			get
			{
				if (DrawCounter == 100) { return true; }
				return false;
			}
		}

		/// <summary>
		/// Tracks the current draw counter, which goes up by 1 for each non-capturing, non-pawn move, and resets to 0
		/// for other moves. If the counter reaches 100 (50 full turns), the game is a draw.
		/// </summary>
		public int DrawCounter
		{
			get
			{
				if(counter.Count() > 0)
				{
					return counter.Last();
				}
				return 0;
			}
		}
		#endregion


		#region Public methods.
		public bool IsEnemyCheckmate()
		{
			int enemy = (CurrentPlayer == 1) ? 2 : 1;
			return GetPossibleMoves().Count() == 0;
		}
		public bool isEnemyCheck()
		{
			int enemyPlayer = (CurrentPlayer == 1) ? 2 : 1;
			IEnumerable<BoardPosition> kingLocation = GetPositionsOfPiece(ChessPieceType.King, enemyPlayer);

			if (kingLocation.Count() != 0)
			{
				BoardPosition pos = kingLocation.First();

				//Console.WriteLine("King is at position" + pos.ToString());
				if (PositionIsAttacked(pos, currentPlayer))
				{
					return true;
				}
			}
			return false;
		}

		public IEnumerable<ChessMove> GetPossibleMoves()
		{
			List<ChessMove> allPossibleChessMoves = new List<ChessMove>();
			//check pieces that works for the current player only

			//loop through the board and then check each possibleAttackMoves
			ISet<BoardPosition> allAttackPositions = GetAttackedPositions(CurrentPlayer);

			//Console.WriteLine("Getting possible moves for player" + CurrentPlayer);

			foreach (BoardPosition current_position in BoardPosition.GetRectangularPositions(BoardSize, BoardSize))
			{
				ChessPiece piece = GetPieceAtPosition(current_position);
				ChessPieceType piece_type = piece.PieceType;
				int player = piece.Player;
				if (CurrentPlayer == player)
				{

					if (piece_type == ChessPieceType.Rook)
					{
						rookPossibleMove(current_position, ref allPossibleChessMoves);
					}

					if (piece_type == ChessPieceType.Knight)
					{
						knightPossibleMove(current_position, ref allPossibleChessMoves);
					}

					if (piece_type == ChessPieceType.Bishop)
					{
						bishopPossibleMove(current_position, ref allPossibleChessMoves);
					}

					if (piece_type == ChessPieceType.Queen)
					{
						queenPossibleMove(current_position, ref allPossibleChessMoves);
					}
					if (piece_type == ChessPieceType.King)
					{
						kingPossibleMove(current_position, ref allPossibleChessMoves);
						castlingPossibleMoves(current_position, ref allPossibleChessMoves);
					}
					if (piece_type == ChessPieceType.Pawn)
					{
						isPawnPossibleMove(current_position, ref allPossibleChessMoves);
						isEnPassant(current_position, ref allPossibleChessMoves);
					}
				}

			}
			List<ChessMove> filtered_allPossibleChessMoves = new List<ChessMove>();
			for (int i = 0; i < allPossibleChessMoves.Count; i++)
			{
				//Console.WriteLine("checking:" + allPossibleChessMoves[i]);

				ApplyMove(allPossibleChessMoves[i]);
				if (!isEnemyCheck())
				{
					//Console.WriteLine("adding:" + allPossibleChessMoves[i]);
					filtered_allPossibleChessMoves.Add(allPossibleChessMoves[i]);
				}
				UndoLastMove();
			}
			return filtered_allPossibleChessMoves;
			//return allPossibleChessMoves;
	
			/*foreach (ChessMove x in filtered_allPossibleChessMoves)
			{
				Console.WriteLine("Possible positions:" + x.ToString());
			}
			//return allPossibleChessMoves;*/
			//return allPossibleChessMoves;
		}

		public void ApplyMove(ChessMove m)
		{
			// STRONG RECOMMENDATION: any mutation to the board state should be run
			// through the method SetPieceAtPosition.
			//Console.WriteLine(m.StartPosition + "to " + m.EndPosition + ",", m.MoveType);
			BoardPosition start_position = m.StartPosition;
			BoardPosition end_position = m.EndPosition;
			int end_player = GetPlayerAtPosition(end_position);

			//black Rooks
			if(start_position == new BoardPosition(0, 0)) { hasBlackLeftRookMove = true; }
			if (start_position == new BoardPosition(0, 7)) { hasBlackRightRookMove = true; }

			//white Rooks
			if (start_position == new BoardPosition(7, 0)) { hasWhiteLeftRookMove = true; }
			if (start_position == new BoardPosition(7, 7)) { hasWhiteRightRookMove = true; }

			//king
			if (start_position == new BoardPosition(0, 4)) { hasBlackKingMove = true; }
			if (start_position == new BoardPosition(7, 4)) { hasWhiteKingMove = true; }

			ChessMoveType moveType = m.MoveType;
			if (moveType == ChessMoveType.EnPassant)
			{
				ChessPiece start_piece = GetPieceAtPosition(start_position);
				ChessPiece end_piece = GetPieceAtPosition(end_position);
				//player 1
				if (currentPlayer == 1)
				{
					SetPieceAtPosition(end_position, start_piece);//updates old
					SetPieceAtPosition(start_position, ChessPiece.Empty);//updates new start positon to empty spot
					m.Player = currentPlayer;
					mMoveHistory.Add(m);
					capturedPieces.Add(GetPieceAtPosition(new BoardPosition(end_position.Row + 1, end_position.Col)));//capture the piece
					SetPieceAtPosition(new BoardPosition(end_position.Row + 1, end_position.Col), ChessPiece.Empty);
					/*Console.WriteLine("current advantage:" + current_advantage);
					Console.WriteLine("value of the end piece:" + getValueOfPiece(end_piece));*/
					current_advantage += 1;
					// GetPieceAtPosition(end_position.Row - 1, end_position.Col);
				}
				//player 2 
				if (currentPlayer == 2)
				{
					SetPieceAtPosition(end_position, start_piece);//updates old
					SetPieceAtPosition(start_position, ChessPiece.Empty);//updates new start positon to empty spot
					m.Player = currentPlayer;
					mMoveHistory.Add(m);
					capturedPieces.Add(GetPieceAtPosition(new BoardPosition(end_position.Row - 1, end_position.Col)));
					SetPieceAtPosition(new BoardPosition(end_position.Row - 1, end_position.Col), ChessPiece.Empty);
					/*Console.WriteLine("current advantage:" + current_advantage);
					Console.WriteLine("value of the end piece:" + getValueOfPiece(end_piece));*/
					current_advantage -= 1;
					// GetPieceAtPosition(end_position.Row - 1, end_position.Col);
				}
				counter.Add(0);
			}
			if (moveType == ChessMoveType.Normal)
			{
				ChessPiece start_piece = GetPieceAtPosition(start_position);
				ChessPiece end_piece = GetPieceAtPosition(end_position);

				SetPieceAtPosition(end_position, start_piece);
				SetPieceAtPosition(start_position, ChessPiece.Empty);
				m.Player = currentPlayer;
				mMoveHistory.Add(m);
				capturedPieces.Add(end_piece);
				//set advantage
				/*Console.WriteLine("current advantage:" + current_advantage);
				Console.WriteLine("value of the end piece:" + getValueOfPiece(end_piece));*/
				int value = getValueOfPiece(end_piece);
				if (currentPlayer == 1)
				{
					current_advantage += value;
				}
				else if(currentPlayer == 2)
				{
					current_advantage -= value;
				}
				//non capturing
				if ((end_piece.Equals(ChessPiece.Empty)
					&& start_piece.PieceType != ChessPieceType.Pawn))
				{
					if (counter.Count() > 0)
					{
						counter.Add(counter.Last() + 1);
					}
					else
					{
						counter.Add(1);
					}
				}
				//capturing move OR is pawn
				else if (!end_piece.Equals(ChessPiece.Empty)
					|| start_piece.PieceType == ChessPieceType.Pawn)
				{
					counter.Add(0);
				}
			}
			if (moveType == ChessMoveType.PawnPromote)
			{
				ChessPiece start_piece = GetPieceAtPosition(start_position);
				ChessPiece end_piece = GetPieceAtPosition(end_position);

				ChessPiece promotePawnTo = new ChessPiece(m.ChessPieceType, CurrentPlayer);
				SetPieceAtPosition(end_position, promotePawnTo);
				SetPieceAtPosition(start_position, ChessPiece.Empty);
				m.Player = currentPlayer;
				mMoveHistory.Add(m);
				capturedPieces.Add(end_piece);

				int value = getValueOfPiece(promotePawnTo);
				int second_value = getValueOfPiece(end_piece);
				if(currentPlayer == 1)
				{
					current_advantage = current_advantage + value + second_value - 1;
				}
				else if(currentPlayer ==2)
				{
					current_advantage = current_advantage - value - second_value + 1;
				}
				counter.Add(0);
				/*Console.WriteLine("current advantage:" + current_advantage);
				Console.WriteLine("value of the end piece:" + getValueOfPiece(end_piece));*/
			}

			else if(moveType == ChessMoveType.CastleQueenSide || moveType == ChessMoveType.CastleKingSide)
			{
				ChessPiece start_piece = GetPieceAtPosition(start_position);
				ChessPiece end_piece = GetPieceAtPosition(end_position);
				//Console.WriteLine(start_piece.PieceType);
				//Console.WriteLine(end_piece.PieceType);
				//move King
				SetPieceAtPosition(end_position, start_piece);
				SetPieceAtPosition(start_position, end_piece);

				//default?
				BoardPosition rookPosition = new BoardPosition(); 
				BoardPosition endCastlingPosition = new BoardPosition();
				if (CurrentPlayer == 1)
				{
					if(start_position == new BoardPosition(7, 4) && moveType == ChessMoveType.CastleQueenSide)
					{
						rookPosition = new BoardPosition(7, 0);
						endCastlingPosition = new BoardPosition(7, 3);
					}
					if (start_position == new BoardPosition(7, 4) && moveType == ChessMoveType.CastleKingSide)
					{
						rookPosition = new BoardPosition(7, 7);
						endCastlingPosition = new BoardPosition(7, 5);
					}
				}
				else if(CurrentPlayer == 2)
				{
					if (start_position == new BoardPosition(0, 4) && moveType == ChessMoveType.CastleQueenSide)
					{
						rookPosition = new BoardPosition(0, 0);
						endCastlingPosition = new BoardPosition(0, 3);
					}
					if (start_position == new BoardPosition(0, 4) && moveType == ChessMoveType.CastleKingSide)
					{
						rookPosition = new BoardPosition(0, 7);
						endCastlingPosition = new BoardPosition(0, 5);
					}
				}
				SetPieceAtPosition(endCastlingPosition,new ChessPiece(ChessPieceType.Rook,CurrentPlayer));
				SetPieceAtPosition(rookPosition, ChessPiece.Empty);
				//add rook's movement
				ChessMove rookMove = new ChessMove(rookPosition, endCastlingPosition);

				mMoveHistory.Add(rookMove);
				capturedPieces.Add(end_piece);

				//add king's movement
				mMoveHistory.Add(m);
				capturedPieces.Add(end_piece);

				if (counter.Count() > 0)
				{
					counter.Add(counter.Last() + 1);
				}
				counter.Add(1);
			}
			adv = setAdvantage();
			CurrentPlayer = (CurrentPlayer == 1) ? 2 : 1;
			//Console.WriteLine("1. Counter:");
			printCounter();
			//Console.WriteLine("player is now" + CurrentPlayer);

		}

		public void UndoLastMove()
		{
			var itemCount = mMoveHistory.Count;
			//Console.WriteLine(itemCount);
			try
			{
				ChessMove moveToUndo = mMoveHistory[itemCount - 1];
				ChessPiece capturedPieceToUndo = capturedPieces[itemCount - 1]; // capturedPieces.Last()

				BoardPosition startPosition = moveToUndo.StartPosition;
				BoardPosition endPosition = moveToUndo.EndPosition;

				ChessPiece pieceToUndo = GetPieceAtPosition(endPosition);
				ChessPieceType pieceTypeToUndo = pieceToUndo.PieceType;

				if (moveToUndo.MoveType == ChessMoveType.EnPassant)
				//if prev captured piece is pawn
				//&& (prevCapturedMove.EndPosition.Row).Equals(endPosition.Row-1))//position of captured piece has to be -1 of the row
				//&& GetPieceAtPosition(item.EndPosition).PieceType.Equals(ChessPieceType.Pawn))//if prev move history item piece is pawn
				{
					//Console.WriteLine("made it past the gate keeper");
					if (CurrentPlayer == 1)
					{
						//Console.WriteLine("player 1 right now BRO");
						//set attacker back to start position
						SetPieceAtPosition(startPosition, pieceToUndo);
						//set sttackers endPosition to empty
						SetPieceAtPosition(endPosition, ChessPiece.Empty);
						//put the captured piece back 1 row since its player 1
						SetPieceAtPosition(new BoardPosition(endPosition.Row - 1, endPosition.Col), capturedPieceToUndo);
						capturedPieces.RemoveAt(itemCount - 1);
						mMoveHistory.RemoveAt(itemCount - 1);

						current_advantage += 1;
					}
					if (CurrentPlayer == 2)
					{
						//Console.WriteLine("player 2 right now dude");
						//set attacker back to start position
						SetPieceAtPosition(startPosition, pieceToUndo);
						//set sttackers endPosition to empty
						SetPieceAtPosition(endPosition, ChessPiece.Empty);
						//put the captured piece back 1 row since its player 1
						SetPieceAtPosition(new BoardPosition(endPosition.Row + 1, endPosition.Col), capturedPieceToUndo);
						capturedPieces.RemoveAt(itemCount - 1);
						mMoveHistory.RemoveAt(itemCount - 1);

						current_advantage -= 1;
					}
					counter.RemoveAt(counter.Count() - 1);

				}
				if (moveToUndo.MoveType == ChessMoveType.Normal)
				{
					SetPieceAtPosition(startPosition, pieceToUndo);
					SetPieceAtPosition(endPosition, capturedPieceToUndo);
					mMoveHistory.RemoveAt(itemCount - 1);
					capturedPieces.RemoveAt(itemCount - 1);

					int value = getValueOfPiece(capturedPieceToUndo);
					if (currentPlayer == 1)
					{
						current_advantage += value;
					}
					else if (currentPlayer == 2)
					{
						current_advantage -= value;
					}
					counter.RemoveAt(counter.Count() - 1);

				}
				if (moveToUndo.MoveType == ChessMoveType.PawnPromote)
				{
					ChessPiece pawnPromotedTo = GetPieceAtPosition(endPosition);
					//ChessPiece piece_to_undo = new ChessPiece(moveToUndo.ChessPiece,currentPlayer);
					SetPieceAtPosition(startPosition, new ChessPiece(ChessPieceType.Pawn,GetPlayerAtPosition(endPosition)));
					SetPieceAtPosition(endPosition, capturedPieceToUndo);
					mMoveHistory.RemoveAt(itemCount - 1);
					capturedPieces.RemoveAt(itemCount - 1);


					int value = getValueOfPiece(pawnPromotedTo);
					int second_value = getValueOfPiece(capturedPieceToUndo);
					if (currentPlayer == 1)
					{
						current_advantage = current_advantage + value + second_value - 1;
					}
					else if (currentPlayer == 2)
					{
						current_advantage = current_advantage - value - second_value + 1;
					}
					counter.RemoveAt(counter.Count() - 1);

				}
				//black Rooks
				if (startPosition == new BoardPosition(0, 0) &&  pieceToUndo.PieceType == ChessPieceType.Rook) { hasBlackLeftRookMove = false; }
				if (startPosition == new BoardPosition(0, 7) && pieceToUndo.PieceType == ChessPieceType.Rook) { hasBlackRightRookMove = false; }

				//white Rooks
				if (startPosition == new BoardPosition(7, 0) && pieceToUndo.PieceType == ChessPieceType.Rook) { hasWhiteLeftRookMove = false; }
				if (startPosition == new BoardPosition(7, 7) && pieceToUndo.PieceType == ChessPieceType.Rook) { hasWhiteRightRookMove = false; }

				//king
				if (startPosition == new BoardPosition(0, 4) && pieceToUndo.PieceType == ChessPieceType.King) { hasBlackKingMove = false; }
				if (startPosition == new BoardPosition(7, 4) && pieceToUndo.PieceType == ChessPieceType.King) { hasWhiteKingMove = false; }

				if(moveToUndo.MoveType == ChessMoveType.CastleKingSide || moveToUndo.MoveType == ChessMoveType.CastleQueenSide)
				{
					/*Console.WriteLine(startPosition);
					Console.WriteLine(pieceToUndo.PieceType);
					Console.WriteLine(capturedPieceToUndo.PieceType);

					Console.WriteLine("player at that position is " + pieceToUndo.Player);*/

					SetPieceAtPosition(startPosition, pieceToUndo);
					SetPieceAtPosition(endPosition, capturedPieceToUndo);
					//remove
					mMoveHistory.RemoveAt(itemCount - 1);
					capturedPieces.RemoveAt(itemCount - 1);

					var new_itemCount = mMoveHistory.Count;
					//undo one more time for rook
					ChessMove rook_item = mMoveHistory[new_itemCount - 1];
					BoardPosition rook_startPosition = rook_item.StartPosition;
					BoardPosition rook_endPosition = rook_item.EndPosition;
					ChessPiece rook_pieceToUndo = GetPieceAtPosition(rook_endPosition);

					SetPieceAtPosition(rook_startPosition, rook_pieceToUndo);
					mMoveHistory.RemoveAt(new_itemCount - 1);
					capturedPieces.RemoveAt(new_itemCount - 1);
					SetPieceAtPosition(rook_endPosition, ChessPiece.Empty);

					counter.RemoveAt(counter.Count() - 1);

				}

				//now check if the piece where it was at had a piece
				/*if (capturedPieces.Count > 0)
				{
					Dictionary<ChessMove, ChessPiece> dic = capturedPieces.Peek();
					var first_element = dic.First();
					ChessMove possMoveToUndo = first_element.Key;
					//Console.WriteLine(possMoveToUndo.ToString());
					ChessPiece possPieceToUndo = first_element.Value;

					if (possMoveToUndo.Equals(item))
					{

						SetPieceAtPosition(endPosition, possPieceToUndo);
						capturedPieces.Pop();
					}
					else
					{
						SetPieceAtPosition(endPosition, ChessPiece.Empty);
					}
				}
				else
				{
					SetPieceAtPosition(endPosition, ChessPiece.Empty);
				}*/
				//Console.WriteLine("currentpLayer is at" + currentPlayer);
				//Console.WriteLine("1. Counter:");
				printCounter();
				adv = setAdvantage();
				CurrentPlayer = (CurrentPlayer == 1) ? 2 : 1;
				//Console.WriteLine("currentpLayer is now" + currentPlayer);
			}
			catch
			{
				throw new System.InvalidOperationException("No move in history");
			}
		}

		/// <summary>
		/// Returns whatever chess piece is occupying the given position.
		/// </summary>
		public ChessPiece GetPieceAtPosition(BoardPosition position)
		{

			int index = (position.Row * 4) + (position.Col / 2);

			//if column is even, then its on the left, otherwise its on the right
			//0 -> left
			//1 -> right
			bool isLeft = (position.Col % 2 == 0) ? true : false;
			var bitAtIndex = board[index];
			//Console.WriteLine("bit at index:" + Convert.ToString(bitAtIndex, toBase: 2));

			//if the position is on the right side, 
			var bitPiece = (isLeft == true) ? (bitAtIndex & 0b_0111_0000) >> 4 : bitAtIndex & 0b_0000_0111;
			//Console.WriteLine("bit at index:" + Convert.ToString(bitAtIndex, toBase: 2));
			var player = (isLeft == true) ? (bitAtIndex >> 7) : (bitAtIndex & 0b_0000_1111) >> 3;
			//Console.WriteLine("player:" + Convert.ToString(player, toBase: 2));


			//Console.WriteLine("bitpiece:" + bitPiece);
			//Console.WriteLine(Convert.ToString(bitPiece, toBase: 2));
			if (bitPiece == 0)
			{
				return new ChessPiece((ChessPieceType)bitPiece, player);
			}
			else
			{
				return new ChessPiece((ChessPieceType)bitPiece, player + 1);

			}

		}


		/// <summary>
		/// Returns whatever player is occupying the given position.
		/// </summary>
		public int GetPlayerAtPosition(BoardPosition pos)
		{
			// As a hint, you should call GetPieceAtPosition.
			return GetPieceAtPosition(pos).Player;

		}

		/// <summary>
		/// Returns true if the given position on the board is empty.
		/// </summary>
		/// <remarks>returns false if the position is not in bounds</remarks>
		public bool PositionIsEmpty(BoardPosition pos)
		{
			return GetPieceAtPosition(pos).PieceType == 0;
		}

		/// <summary>
		/// Returns true if the given position contains a piece that is the enemy of the given player.
		/// </summary>
		/// <remarks>returns false if the position is not in bounds</remarks>
		public bool PositionIsEnemy(BoardPosition pos, int player)
		{
			if (player == 1) return GetPieceAtPosition(pos).Player == 2;
			if (player == 2) return GetPieceAtPosition(pos).Player == 1;
			return false;
		}

		/// <summary>
		/// Returns true if the given position is in the bounds of the board.
		/// </summary>
		public static bool PositionInBounds(BoardPosition pos)
		{
			return (pos.Row <= 7 && pos.Row >= 0) && (pos.Col >= 0 && pos.Col <= 7);

		}

		/// <summary>
		/// Returns all board positions where the given piece can be found.
		/// </summary>
		public IEnumerable<BoardPosition> GetPositionsOfPiece(ChessPieceType piece, int player)
		{
			List<BoardPosition> AllpositionsOfPiece = new List<BoardPosition>();
			foreach (BoardPosition position in BoardPosition.GetRectangularPositions(BoardSize, BoardSize))
			{
				var chess_piece = GetPieceAtPosition(position).PieceType;
				var player_number = GetPlayerAtPosition(position);
				if (chess_piece == piece && player_number == player)
				{
					AllpositionsOfPiece.Add(position);
				}
			}
			return AllpositionsOfPiece;
		}

		/// <summary>
		/// Returns true if the given player's pieces are attacking the given position.
		/// </summary>
		public bool PositionIsAttacked(BoardPosition position, int byPlayer)
		{
			ISet<BoardPosition> all_attack_positions = GetAttackedPositions(byPlayer);
			int player_at_position = GetPlayerAtPosition(position);
			foreach (BoardPosition attacked_positions in all_attack_positions)
			{
				if (position.Equals(attacked_positions))
				{
					return true;
				}
			}
			return false;
			/*IEnumerable<ChessMove> possible_moves = GetPossibleMoves();
			foreach (ChessMove cm in possible_moves)
			{
				if (cm.EndPosition.Row == position.Row && cm.EndPosition.Col == position.Col)
				{
					return true;
				}
			}
			return false;*/
		}

		/// <summary>
		/// Returns a set of all BoardPositions that are attacked by the given player.
		/// </summary>
		public ISet<BoardPosition> GetAttackedPositions(int byPlayer)
		{
			HashSet<BoardPosition> moves = new HashSet<BoardPosition>();
			foreach (BoardPosition position in BoardPosition.GetRectangularPositions(BoardSize, BoardSize))
			{
				ChessPiece piece = GetPieceAtPosition(position);
				ChessPieceType piece_type = piece.PieceType;
				int player = piece.Player;

				HashSet<BoardPosition> temp_moves = new HashSet<BoardPosition>();
				if (player == byPlayer)
				{
					if (piece_type == ChessPieceType.Queen)
					{
						temp_moves = getQueenAttackMoves(position);

					}
					if (piece_type == ChessPieceType.Rook)
					{
						temp_moves = getRookAttackMoves(position);
					}
					if (piece_type == ChessPieceType.Bishop)
					{
						temp_moves = getBishopAttackMoves(position);
					}
					if (piece_type == ChessPieceType.Pawn)
					{
						temp_moves = getPawnAttackMoves(position, player);
					}
					if (piece_type == ChessPieceType.King)
					{
						temp_moves = getKingAttackMoves(position);
					}
					if (piece_type == ChessPieceType.Knight)
					{
						temp_moves = getKnightAttackMoves(position);
					}
					foreach (BoardPosition bp in temp_moves)
					{
						/*Console.WriteLine("attackmoves:");
						Console.WriteLine(bp.ToString());*/
					moves.Add(bp);
					}
				}
			}

			return moves;
		}
		#endregion

		#region Private methods.
		/// <summary>
		/// Mutates the board state so that the given piece is at the given position.
		/// </summary>
		private void SetPieceAtPosition(BoardPosition position, ChessPiece piece)
		{

			int index = (position.Row * 4) + (position.Col / 2);

			bool isLeft = (position.Col % 2 == 0) ? true : false;
			var player = (piece.Player == 1 || piece.Player == 0) ? (byte)0 : (byte)8;
			//var player = (byte) (piece.Player) << 3;
			//Console.WriteLine("Player: " + Convert.ToString(player, toBase: 2));

			var chessPiece = (byte)piece.PieceType;
			//Console.WriteLine(chessPiece);

			var bitAtIndex = board[index];
			var playerANDchessPiece = (byte)(player | chessPiece);
			//Console.WriteLine("playerANDchessPiece: " + Convert.ToString(playerANDchessPiece, toBase: 2));

			//if you want to move a piece to left side of the byte, then you need keep the right sight
			//otherwise keep left side
			var bitsToKeep = (isLeft == true) ? board[index] & 0b_0000_1111 : board[index] & 0b_1111_0000;
			board[index] = (isLeft == true) ? (byte)((playerANDchessPiece << 4) | bitsToKeep) : (byte)(playerANDchessPiece | bitsToKeep);

			//Console.WriteLine("byte at index"+ index + ": "+ Convert.ToString(board[index], toBase: 2));
		}

		#endregion

		#region Explicit IGameBoard implementations.
		IEnumerable<IGameMove> IGameBoard.GetPossibleMoves()
		{
			return GetPossibleMoves();
		}
		void IGameBoard.ApplyMove(IGameMove m)
		{
			ApplyMove(m as ChessMove);
		}
		IReadOnlyList<IGameMove> IGameBoard.MoveHistory => mMoveHistory;
		#endregion

		// You may or may not need to add code to this constructor.
		public ChessBoard()
		{

		}

		public ChessBoard(IEnumerable<Tuple<BoardPosition, ChessPiece>> startingPositions)
			: this()
		{
			var king1 = startingPositions.Where(t => t.Item2.Player == 1 && t.Item2.PieceType == ChessPieceType.King);
			var king2 = startingPositions.Where(t => t.Item2.Player == 2 && t.Item2.PieceType == ChessPieceType.King);
			if (king1.Count() != 1 || king2.Count() != 1)
			{
				throw new ArgumentException("A chess board must have a single king for each player");
			}

			foreach (var position in BoardPosition.GetRectangularPositions(8, 8))
			{
				SetPieceAtPosition(position, ChessPiece.Empty);
			}

			int[] values = { 0, 0 };
			foreach (var pos in startingPositions)
			{
				SetPieceAtPosition(pos.Item1, pos.Item2);
				// TODO: you must calculate the overall advantage for this board, in terms of the pieces
				// that the board has started with. "pos.Item2" will give you the chess piece being placed
				// on this particular position.
				int value = getValueOfPiece(pos.Item2);
				if (pos.Item2.Player == 1)
				{
					current_advantage += value;
				}
				else if (pos.Item2.Player == 2)
				{
					current_advantage -= value;
				}
			}
			adv = setAdvantage();
		}

		#region AttackMoves
		public HashSet<BoardPosition> getPawnAttackMoves(BoardPosition position, int player)
		{
			HashSet<BoardPosition> pawn_Moves = new HashSet<BoardPosition>();
			int row = position.Row;
			int col = position.Col;
			if (player == 1)
			{
				//Console.WriteLine(PositionInBounds(new BoardPosition((row - 1), col)));
				if (PositionInBounds(new BoardPosition(row - 1, col - 1)))
				{
					pawn_Moves.Add(new BoardPosition((row - 1), col - 1));
				}
				if (PositionInBounds(new BoardPosition(row - 1, col + 1)))
				{
					pawn_Moves.Add(new BoardPosition((row - 1), col + 1));
				}
				/*if (PositionInBounds(new BoardPosition(row - 1, col)))
				{
					pawn_Moves.Add(new BoardPosition((row - 1), col));
				}
				if (position.Row == 6)
				{
					pawn_Moves.Add(new BoardPosition((row - 2), col));
				}*/
			}
			else if (player == 2)
			{
				if (PositionInBounds(new BoardPosition(row + 1, col - 1)))
				{
					pawn_Moves.Add(new BoardPosition(row + 1, col - 1));
				}
				if (PositionInBounds(new BoardPosition(row + 1, col + 1)))
				{
					pawn_Moves.Add(new BoardPosition(row + 1, col + 1));
				}
				/*if (PositionInBounds(new BoardPosition(row + 1, col)))
				{
					pawn_Moves.Add(new BoardPosition((row + 1), col));
				}
				if (position.Row == 1)
				{
					pawn_Moves.Add(new BoardPosition((row + 2), col));
				}*/
			}
			return pawn_Moves;
		}
		public HashSet<BoardPosition> getKnightAttackMoves(BoardPosition position)
		{
			HashSet<BoardPosition> moves = new HashSet<BoardPosition>();
			foreach (BoardDirection dir in knightDirections)
			{
				BoardPosition newPos = position.Translate(dir);
				//if either player or enemy piece, then we add to the attacked positions and break
				if (PositionInBounds(newPos))
				{
					int current_player = GetPlayerAtPosition(position);
					int new_position_player = GetPlayerAtPosition(newPos);
					//if either player or enemy piece, then we add to the attacked positions and break
					if (current_player == new_position_player || PositionIsEnemy(newPos, current_player))
					{
						moves.Add(newPos);
					}
					moves.Add(newPos);
					newPos = newPos.Translate(dir);
				}
			}
			return moves;
		}

		HashSet<BoardPosition> getQueenAttackMoves(BoardPosition position)
		{
			HashSet<BoardPosition> moves = new HashSet<BoardPosition>();

			ChessPieceType piece_type = GetPieceAtPosition(position).PieceType;
			//default directions to be cardinal directions
			IEnumerable<BoardDirection> directions = CardinalDirections;
			if (piece_type == ChessPieceType.Queen) directions = CardinalDirections;

			foreach (BoardDirection dir in directions)
			{
				BoardPosition newPos = position.Translate(dir);
				while (PositionInBounds(newPos))
				{
					int current_player = GetPlayerAtPosition(position);
					int new_position_player = GetPlayerAtPosition(newPos);
					//if either player or enemy piece, then we add to the attacked positions and break
					if (current_player == new_position_player || PositionIsEnemy(newPos, current_player))
					{
						moves.Add(newPos);
						break;
					}
					else
					{
						moves.Add(newPos);
						newPos = newPos.Translate(dir);
					}
				}
			}
			return moves;
		}
		HashSet<BoardPosition> getRookAttackMoves(BoardPosition position)
		{
			HashSet<BoardPosition> moves = new HashSet<BoardPosition>();

			ChessPieceType piece_type = GetPieceAtPosition(position).PieceType;
			//default directions to be cardinal directions
			IEnumerable<BoardDirection> directions = CardinalDirections;
			if (piece_type == ChessPieceType.Rook) directions = rookDirections;

			foreach (BoardDirection dir in directions)
			{
				BoardPosition newPos = position.Translate(dir);
				while (PositionInBounds(newPos))
				{
					int current_player = GetPlayerAtPosition(position);
					int new_position_player = GetPlayerAtPosition(newPos);
					//if either player or enemy piece, then we add to the attacked positions and break
					if (current_player == new_position_player || PositionIsEnemy(newPos, current_player))
					{
						moves.Add(newPos);
						break;
					}
					else
					{
						moves.Add(newPos);
						newPos = newPos.Translate(dir);
					}
				}
			}
			/*Console.WriteLine("Attack moves for Rook");
			*//*foreach (BoardPosition bp in moves)
			{
				if(bp.Row == 1 && bp.Col == 7)
				{
					Console.WriteLine("attackmove:" + bp.ToString());
				}
			}*/
			return moves;
		}
		HashSet<BoardPosition> getBishopAttackMoves(BoardPosition position)
		{
			HashSet<BoardPosition> moves = new HashSet<BoardPosition>();

			ChessPieceType piece_type = GetPieceAtPosition(position).PieceType;
			//default directions to be cardinal directions
			IEnumerable<BoardDirection> directions = CardinalDirections;
			if (piece_type == ChessPieceType.Bishop) directions = bishopDirections;

			foreach (BoardDirection dir in directions)
			{
				BoardPosition newPos = position.Translate(dir);
				while (PositionInBounds(newPos))
				{
					int current_player = GetPlayerAtPosition(position);
					int new_position_player = GetPlayerAtPosition(newPos);
					//if either player or enemy piece, then we add to the attacked positions and break
					if (current_player == new_position_player || PositionIsEnemy(newPos, current_player))
					{
						moves.Add(newPos);
						break;
					}
					else
					{
						moves.Add(newPos);
						newPos = newPos.Translate(dir);
					}
				}
			}
			return moves;
		}

		HashSet<BoardPosition> getKingAttackMoves(BoardPosition position)
		{
			HashSet<BoardPosition> moves = new HashSet<BoardPosition>();
			IEnumerable<BoardDirection> directions = BoardDirection.CardinalDirections;

			ChessPieceType pieceType = GetPieceAtPosition(position).PieceType;
			if (pieceType == ChessPieceType.King) directions = BoardDirection.CardinalDirections;

			foreach (BoardDirection dir in directions)
			{
				BoardPosition newPos = position.Translate(dir);

				if (PositionInBounds(newPos))
				{
					moves.Add(newPos);
				}
				newPos = newPos.Translate(dir);
			}
/*
			if (position == new BoardPosition(0, 0))
			{
				if (hasBlackLeftRookMove == false && hasBlackKingMove == false)
				{
					//rook can castle
					moves.Add(new BoardPosition(0, 5));
					//King can castle
					moves.Add(new BoardPosition(0, 6));
				}
			}
			if (position == new BoardPosition(0, 7))
			{
				if (hasBlackRightRookMove == false && hasBlackKingMove == false)
				{
					//rook can castle
					moves.Add(new BoardPosition(0, 3));
					//King can castle
					moves.Add(new BoardPosition(0, 2));
				}
			}
			if (position == new BoardPosition(7, 0))
			{
				if (hasWhiteLeftRookMove == false && hasBlackKingMove == false)
				{
					//rook can castle
					moves.Add(new BoardPosition(7, 5));
					//King can castle
					moves.Add(new BoardPosition(7, 6));
				}
			}
			if (position == new BoardPosition(7, 7))
			{
				if (hasWhiteRightRookMove == false && hasBlackKingMove == false)
				{
					//rook can castle
					moves.Add(new BoardPosition(7, 3));
					//King can castle
					moves.Add(new BoardPosition(7, 2));
				}
			}*/
			return moves;
		}
		#endregion
		#region possibleMoves
		void queenPossibleMove(BoardPosition current_position, ref List<ChessMove> allPossibleChessMoves)
		{
			foreach (var p in getQueenAttackMoves(current_position))
			{
				if (PositionIsEmpty(p) || PositionIsEnemy(p, CurrentPlayer))
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, p));
				}
			}

				/*int current_player = GetPlayerAtPosition(current_position);

				bool isEmpty = PositionIsEmpty(possible_position);
				bool isEnemy = PositionIsEnemy(possible_position, current_player);
				foreach (BoardDirection dir in CardinalDirections)
				{
					BoardPosition newPos = current_position.Translate(dir);
					while (PositionInBounds(newPos))
					{
						if (newPos.Equals(possible_position) && (isEmpty || isEnemy))
						{
							/*Console.Write("adding: ");
							Console.Write(current_position.ToString());
							Console.Write("to ");
							Console.WriteLine(possible_position.ToString());
							allPossibleChessMoves.Add(new ChessMove(current_position, possible_position));
							break;
						}
						else if (GetPlayerAtPosition(newPos) == CurrentPlayer || PositionIsEnemy(newPos, CurrentPlayer))
						{
							break;
						}
						else
						{
							newPos = newPos.Translate(dir);
						}
					}
				}*/
		}
		void rookPossibleMove(BoardPosition current_position, ref List<ChessMove> allPossibleChessMoves)
		{
			foreach (var p in getRookAttackMoves(current_position))
			{
				if (PositionIsEmpty(p) || PositionIsEnemy(p, CurrentPlayer))
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, p));
				}
			}
			/*int current_player = GetPlayerAtPosition(current_position);

			bool isEmpty = PositionIsEmpty(possible_position);
			bool isEnemy = PositionIsEnemy(possible_position, current_player);
			
			
		
			foreach (BoardDirection dir in rookDirections)
			{
				BoardPosition newPos = current_position.Translate(dir);
				while (PositionInBounds(newPos))
				{
					if (newPos.Equals(possible_position) && (isEmpty || isEnemy))
					{
						allPossibleChessMoves.Add(new ChessMove(current_position, possible_position));
						break;
					}
					else if (GetPlayerAtPosition(newPos) == CurrentPlayer || PositionIsEnemy(newPos, CurrentPlayer))
					{
						break;
					}
					else
					{
						newPos = newPos.Translate(dir);
					}
				}
			}*/
		}
		void castlingPossibleMoves(BoardPosition current_position, ref List<ChessMove> allPossibleChessMoves)
		{
			//cannot castle through check	
			//need to check if in between works.
			int enemy_player = (currentPlayer == 1)? 2:1;
			if (!PositionIsAttacked(current_position, enemy_player)){
				if (current_position == new BoardPosition(0, 4))
				{
					if (hasBlackLeftRookMove == false && hasBlackKingMove == false)
					{
						if (GetPieceAtPosition(new BoardPosition(0, 1)).PieceType == ChessPieceType.Empty &&
							GetPieceAtPosition(new BoardPosition(0, 2)).PieceType == ChessPieceType.Empty &&
							GetPieceAtPosition(new BoardPosition(0, 3)).PieceType == ChessPieceType.Empty &&
							!PositionIsAttacked(new BoardPosition(0, 2), enemy_player) &&
							!PositionIsAttacked(new BoardPosition(0, 3), enemy_player)) //and position is not attacked only for 0,2, 0,3 
						{
							//Console.WriteLine("leftRook can castle");
							allPossibleChessMoves.Add(new ChessMove(current_position, new BoardPosition(0, 2), ChessMoveType.CastleQueenSide));
						}
					}
				}
				if (current_position == new BoardPosition(0, 4))
				{
					if (hasBlackRightRookMove == false && hasBlackKingMove == false)
					{
						if (GetPieceAtPosition(new BoardPosition(0, 5)).PieceType == ChessPieceType.Empty &&
							GetPieceAtPosition(new BoardPosition(0, 6)).PieceType == ChessPieceType.Empty &&
							!PositionIsAttacked(new BoardPosition(0, 5), enemy_player) &&
							!PositionIsAttacked(new BoardPosition(0, 6), enemy_player)
							)
						{
							//Console.WriteLine("leftRook can castle");
							allPossibleChessMoves.Add(new ChessMove(current_position, new BoardPosition(0, 6), ChessMoveType.CastleKingSide));
						}
					}
				}
				if (current_position == new BoardPosition(7, 4))
				{
					if (hasWhiteLeftRookMove == false && hasWhiteKingMove == false)
					{
						if (GetPieceAtPosition(new BoardPosition(7, 1)).PieceType == ChessPieceType.Empty &&
							GetPieceAtPosition(new BoardPosition(7, 2)).PieceType == ChessPieceType.Empty &&
							GetPieceAtPosition(new BoardPosition(7, 3)).PieceType == ChessPieceType.Empty &&
							!PositionIsAttacked(new BoardPosition(7, 2), enemy_player) &&
							!PositionIsAttacked(new BoardPosition(7, 3), enemy_player))
						{
							//rook can castle
							/*Console.WriteLine("left white rook can castle");
							Console.Write("adding: ");
							Console.Write(current_position.ToString());
							Console.Write("to ");
							Console.WriteLine(possible_position.ToString());*/
							allPossibleChessMoves.Add(new ChessMove(current_position, new BoardPosition(7, 2), ChessMoveType.CastleQueenSide));
						}
					}
				}
				if (current_position == new BoardPosition(7, 4))
				{
					if (hasWhiteRightRookMove == false && hasWhiteKingMove == false)
					{
						if (GetPieceAtPosition(new BoardPosition(7, 5)).PieceType == ChessPieceType.Empty &&
							GetPieceAtPosition(new BoardPosition(7, 6)).PieceType == ChessPieceType.Empty &&
							!PositionIsAttacked(new BoardPosition(7, 5), enemy_player) &&
							!PositionIsAttacked(new BoardPosition(7, 6), enemy_player))
						{
							//rook can castle
							/*Console.WriteLine("right white rook can castle");
							Console.Write("adding: ");
							Console.Write(current_position.ToString());
							Console.Write("to ");
							Console.WriteLine(possible_position.ToString());*/
							allPossibleChessMoves.Add(new ChessMove(current_position, new BoardPosition(7, 6), ChessMoveType.CastleKingSide));
						}
					}
				}
			}
		}
		void bishopPossibleMove(BoardPosition current_position, ref List<ChessMove> allPossibleChessMoves)
		{
			foreach (var p in getBishopAttackMoves(current_position))
			{
				if (PositionIsEmpty(p) || PositionIsEnemy(p, CurrentPlayer))
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, p));
				}
			}
			/*int current_player = GetPlayerAtPosition(current_position);

			bool isEmpty = PositionIsEmpty(possible_position);
			bool isEnemy = PositionIsEnemy(possible_position, current_player);
			foreach (BoardDirection dir in bishopDirections)
			{
				BoardPosition newPos = current_position.Translate(dir);
				while (PositionInBounds(newPos))
				{
					if (newPos.Equals(possible_position) && (isEmpty || isEnemy))
					{
						allPossibleChessMoves.Add(new ChessMove(current_position, possible_position));
						break;
					}
					else if (GetPlayerAtPosition(newPos) == CurrentPlayer || PositionIsEnemy(newPos, CurrentPlayer))
					{
						break;
					}
					else
					{
						newPos = newPos.Translate(dir);
					}
				}
			}*/
		}
		void knightPossibleMove(BoardPosition current_position, ref List<ChessMove> allPossibleChessMoves)
		{
			foreach (var p in getKnightAttackMoves(current_position))
			{
				if (PositionIsEmpty(p) || PositionIsEnemy(p, CurrentPlayer))
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, p));
				}
			}
			/*int current_player = GetPlayerAtPosition(current_position);


			bool isEmpty = PositionIsEmpty(possible_position);
			bool isEnemy = PositionIsEnemy(possible_position, current_player);
			foreach (BoardDirection dir in knightDirections)
			{
				BoardPosition newPos = current_position.Translate(dir);

				if (PositionInBounds(newPos))
				{
					ChessPiece piece_at_possible_position = GetPieceAtPosition(newPos);
					ChessPieceType type_at_possible_position = piece_at_possible_position.PieceType;
					if (newPos.Equals(possible_position) && (isEmpty || isEnemy))
					{
						allPossibleChessMoves.Add(new ChessMove(current_position, possible_position));
					}
				}
			}*/
		}
		void kingPossibleMove(BoardPosition current_position, ref List<ChessMove> allPossibleChessMoves)
		{
			foreach (var p in getKingAttackMoves(current_position))
			{
				if (PositionIsEmpty(p) || PositionIsEnemy(p, CurrentPlayer))
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, p));
				}
			}
			/*int current_player = GetPlayerAtPosition(current_position);

			bool isEmpty = PositionIsEmpty(possible_position);
			bool isEnemy = PositionIsEnemy(possible_position, current_player);
			foreach (BoardDirection dir in CardinalDirections)
			{
				BoardPosition newPos = current_position.Translate(dir);
				if (PositionInBounds(newPos))
				{
					ChessPiece piece_at_possible_position = GetPieceAtPosition(newPos);
					ChessPieceType type_at_possible_position = piece_at_possible_position.PieceType;
					if (newPos.Equals(possible_position) && (isEmpty || isEnemy))
					{
						allPossibleChessMoves.Add(new ChessMove(current_position, possible_position));
					}
				}

			}*/
		}
		void isEnPassant(BoardPosition current_position, ref List<ChessMove> allPossibleChessMoves)
		{
			//BoardDirection direction = new BoardDirection();
			if (mMoveHistory.Count > 0)
			{
				ChessMove lastPieceMoved = mMoveHistory[mMoveHistory.Count - 1];
				foreach (var p in getPawnAttackMoves(current_position, currentPlayer))
				{   //P1 Right En Passant
					if (currentPlayer == 1
						//&& p == new BoardPosition(-1,1)
						&& GetPieceAtPosition(lastPieceMoved.EndPosition).PieceType.Equals(ChessPieceType.Pawn)
						&& lastPieceMoved.StartPosition.Row == 1
						&& lastPieceMoved.EndPosition.Row - lastPieceMoved.StartPosition.Row == 2)
					{
						//Console.WriteLine("P1 Right En Passant");
						if (PositionIsEnemy(new BoardPosition(p.Row + 1, p.Col), currentPlayer)
							&& current_position.Col + 1 == lastPieceMoved.EndPosition.Col
							&& current_position.Row == lastPieceMoved.EndPosition.Row 
							&& lastPieceMoved.StartPosition.Col == p.Col)
						{
							//Console.WriteLine("P1 Right En Passant Added");
							allPossibleChessMoves.Add(new ChessMove(current_position, p, ChessMoveType.EnPassant));
							break;
						}
					}
					//P1 Left En Passant
					if (currentPlayer == 1
						//&& p == new BoardPosition(-1, -1)
						&& GetPieceAtPosition(lastPieceMoved.EndPosition).PieceType.Equals(ChessPieceType.Pawn)
						&& lastPieceMoved.StartPosition.Row == 1
						&& lastPieceMoved.EndPosition.Row - lastPieceMoved.StartPosition.Row == 2)
					{
						//Console.WriteLine("P1 Left En Passant");
						if (PositionIsEnemy(new BoardPosition(p.Row + 1, p.Col), currentPlayer)
							&& current_position.Col - 1 == lastPieceMoved.EndPosition.Col
							&& current_position.Row == lastPieceMoved.EndPosition.Row
							&& lastPieceMoved.StartPosition.Col == p.Col)
						{
							//Console.WriteLine("P1 Left En Passant Added");
							allPossibleChessMoves.Add(new ChessMove(current_position, p, ChessMoveType.EnPassant));
							break;
						}
					}
					//P2 Right En Passant
					if (currentPlayer == 2
						//&& p == new BoardPosition(1, 1)
						&& GetPieceAtPosition(lastPieceMoved.EndPosition).PieceType.Equals(ChessPieceType.Pawn)
						&& lastPieceMoved.StartPosition.Row == 6
						&& lastPieceMoved.StartPosition.Row - lastPieceMoved.EndPosition.Row == 2)
					{
						//Console.WriteLine("P2 Right En Passant");
						if (PositionIsEnemy(new BoardPosition(p.Row - 1, p.Col), currentPlayer)
							&& current_position.Col + 1 == lastPieceMoved.EndPosition.Col
							&& current_position.Row == lastPieceMoved.EndPosition.Row
							&& lastPieceMoved.StartPosition.Col == p.Col)
						{
							//Console.WriteLine("P2 Right En Passant Added");
							allPossibleChessMoves.Add(new ChessMove(current_position, p, ChessMoveType.EnPassant));
							break;
						}
					}
					//P2 Left En Passant
					if (currentPlayer == 2
						//&& p == new BoardPosition(1, -1)
						&& GetPieceAtPosition(lastPieceMoved.EndPosition).PieceType.Equals(ChessPieceType.Pawn)
						&& lastPieceMoved.StartPosition.Row == 6
						&& lastPieceMoved.StartPosition.Row - lastPieceMoved.EndPosition.Row == 2
						&& current_position.Row == lastPieceMoved.EndPosition.Row)
					{
						//Console.WriteLine("P2 Left En Passant");
						if (PositionIsEnemy(new BoardPosition(p.Row-1, p.Col), currentPlayer)
							&& current_position.Col - 1 == lastPieceMoved.EndPosition.Col
							&& current_position.Row == lastPieceMoved.EndPosition.Row
							&& lastPieceMoved.StartPosition.Col == p.Col)
						{
							//Console.WriteLine("P1 Left En Passant Added");
							allPossibleChessMoves.Add(new ChessMove(current_position, p, ChessMoveType.EnPassant));
							break;
						}
					}
				}
			}
		}
		void isPawnPossibleMove(BoardPosition current_position, ref List<ChessMove> allPossibleChessMoves)
		{
			foreach (var p in getPawnAttackMoves(current_position, currentPlayer))
			{
				if (PositionInBounds(p) && PositionIsEnemy(p, CurrentPlayer))
				{
					if (p.Row == 0 || p.Row == 7)
					{
						allPossibleChessMoves.Add(new ChessMove(current_position, p, ChessPieceType.Bishop));
						allPossibleChessMoves.Add(new ChessMove(current_position, p, ChessPieceType.Rook));
						allPossibleChessMoves.Add(new ChessMove(current_position, p, ChessPieceType.Queen));
						allPossibleChessMoves.Add(new ChessMove(current_position, p, ChessPieceType.Knight));
					}
					else
					{
						allPossibleChessMoves.Add(new ChessMove(current_position, p));
					}
				}
			}
			//up One pawn promotion
			bool flag = false;
			if (currentPlayer == 1)
			{
				BoardDirection upOne = new BoardDirection(-1, 0);
				if (PositionInBounds(current_position.Translate(upOne)) &&
					GetPieceAtPosition(current_position.Translate(upOne)).PieceType == ChessPieceType.Empty 
					&& current_position.Translate(upOne).Row == 0)
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne), ChessPieceType.Bishop));
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne), ChessPieceType.Rook));
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne), ChessPieceType.Queen));
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne), ChessPieceType.Knight));
					flag = true;
				}
			}
			if (currentPlayer == 2)
			{
				BoardDirection upOne = new BoardDirection(1, 0);
				if (PositionInBounds(current_position.Translate(upOne)) &&
					GetPieceAtPosition(current_position.Translate(upOne)).PieceType == ChessPieceType.Empty
					&& current_position.Translate(upOne).Row == 7)
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne), ChessPieceType.Bishop));
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne), ChessPieceType.Rook));
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne), ChessPieceType.Queen));
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne), ChessPieceType.Knight));
					flag = true;
				}

			}
			//up 2 or up 1 
			if (currentPlayer == 1 && flag == false)
			{
				BoardDirection upOne = new BoardDirection(-1, 0);
				BoardDirection upTwo = new BoardDirection(-2, 0);
				//check 2 spaces up
				if (PositionInBounds(current_position.Translate(upTwo)) &&
					GetPieceAtPosition(current_position.Translate(upTwo)).PieceType == ChessPieceType.Empty &&
					GetPieceAtPosition(current_position.Translate(upOne)).PieceType == ChessPieceType.Empty &&
					current_position.Row == 6)
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upTwo)));
				}
				//check 1 space up
				if (PositionInBounds(current_position.Translate(upOne)) && 
					GetPieceAtPosition(current_position.Translate(upOne)).PieceType == ChessPieceType.Empty)
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne)));

				}
			}
			if (currentPlayer == 2 && flag == false)
			{
				BoardDirection upOne = new BoardDirection(1, 0);
				BoardDirection upTwo = new BoardDirection(2, 0);
				if (PositionInBounds(current_position.Translate(upTwo)) &&
					GetPieceAtPosition(current_position.Translate(upTwo)).PieceType == ChessPieceType.Empty &&
					GetPieceAtPosition(current_position.Translate(upOne)).PieceType == ChessPieceType.Empty && 
					current_position.Row == 1)
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upTwo)));
				}
				if (PositionInBounds(current_position.Translate(upOne)) && 
					GetPieceAtPosition(current_position.Translate(upOne)).PieceType == ChessPieceType.Empty)
				{
					allPossibleChessMoves.Add(new ChessMove(current_position, current_position.Translate(upOne)));

				}
			}

			/*ChessPiece piece = GetPieceAtPosition(current_position);
			ChessPieceType piece_type = piece.PieceType;
			int player = piece.Player;

			ChessPiece piece_at_possible_position = GetPieceAtPosition(possible_position);
			ChessPieceType type_at_possible_position = piece_at_possible_position.PieceType;
			int player_at_possible_position = piece_at_possible_position.Player;

			int current_row = current_position.Row;
			int current_col = current_position.Col;

			IEnumerable<BoardDirection> directions = new BoardDirection[] { };
			if (player == 1) directions = PlayerOnePawnDirections;	
			if (player == 2) directions = PlayerTwoPawnDirections;
			foreach (BoardDirection dir in directions)
			{
				if (current_position.Translate(dir) == possible_position)
				{
					if (mMoveHistory.Count > 0)
					{
						*//*Console.WriteLine("current:" + current_position.ToString());
						Console.WriteLine("possible:" + possible_position.ToString());*//*
						ChessMove lastPieceMove = mMoveHistory[mMoveHistory.Count - 1];
						*//*if ((dir == new BoardDirection(1, 1) && GetPieceAtPosition(lastPieceMove.EndPosition).PieceType.Equals(ChessPieceType.Pawn)
							&& lastPieceMove.StartPosition.Row == 6
							&& lastPieceMove.EndPosition.Row - lastPieceMove.StartPosition.Row == 2))
						{*//*
						//player 2's right en peasant
						if ((CurrentPlayer == 2 && dir == new BoardDirection(1, 1)) && GetPieceAtPosition(lastPieceMove.EndPosition).PieceType.Equals(ChessPieceType.Pawn)
							&& lastPieceMove.StartPosition.Row == 6 && lastPieceMove.StartPosition.Row - lastPieceMove.EndPosition.Row == 2)
						{
							//check if attack move is in bound or not	
							if (PositionInBounds(new BoardPosition(current_position.Row + 1, current_position.Col + 1)))
							{
								//need 
								check if pawn to the right is the last move
								*//*if (GetPieceAtPosition(lastPieceMove.EndPosition).Equals(GetPieceAtPosition(new BoardPosition(current_position.Row, current_position.Col + 1))) && PositionIsEnemy(new BoardPosition(current_position.Row,current_col+1),currentPlayer))
								{*/
			/*Console.WriteLine(PositionIsEnemy(new BoardPosition(current_row, current_col + 1), currentPlayer));*//*
			if (PositionIsEnemy(new BoardPosition(current_row, current_col + 1), CurrentPlayer))
			{
				allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.EnPassant));
			}
		}
	}
}

*//*if ((dir == new BoardDirection(1, -1) &&  mMoveHistory[mMoveHistory.Count()-1].Equals(ChessPieceType.Pawn) )){
	allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.EnPassant));
}
//
if ((dir == new BoardDirection(-1, 0) &&  mMoveHistory[mMoveHistory.Count()-1].Equals(ChessPieceType.Pawn) )){
	allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.EnPassant));
}
if ((dir == new BoardDirection(-1, -1) &&  mMoveHistory[mMoveHistory.Count()-1].Equals(ChessPieceType.Pawn) )){
	allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.EnPassant));
}*//*

//if player 1, diagonal
//player 2 diagonal
if ((dir == new BoardDirection(1, -1) || dir == new BoardDirection(1, 1)) && PositionIsEnemy(possible_position,CurrentPlayer))
{
	if (current_position.Row + 1 == 7)
	{
		//Console.WriteLine("Player 2 can promotion!");
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Bishop));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Rook));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Queen));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Knight));
	}
	else
	{
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.Normal));
	}
}
//player 2 diagonal
else if ((dir == new BoardDirection(-1, -1) || dir == new BoardDirection(-1, 1)) && PositionIsEnemy(possible_position,CurrentPlayer))
{
	if (current_position.Row - 1 == 0)
	{
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Bishop));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Rook));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Queen));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Knight));
	}
	else
	{

		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.Normal));
	}
}
//if player 2's pawn is moving up
else if (dir == new BoardDirection(-2, 0) && GetPieceAtPosition(new BoardPosition(current_row - 1, current_col)).PieceType == ChessPieceType.Empty 
	&& GetPieceAtPosition(new BoardPosition(current_row - 2, current_col)).PieceType == ChessPieceType.Empty && current_row == 6)
{
	allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.Normal));
}
//if player 1's pawn is moving up
else if (dir == new BoardDirection(-1, 0) && GetPieceAtPosition(new BoardPosition(current_row - 1, current_col)).PieceType == ChessPieceType.Empty)
{
	if (current_position.Row - 1 == 0)
	{
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Bishop));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Rook));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Queen));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Knight));
	}
	else
	{
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.Normal));
	}
}
//player 2 move down
else if (dir == new BoardDirection(2, 0) && GetPieceAtPosition(new BoardPosition(current_row + 1, current_col)).PieceType == ChessPieceType.Empty &&
	GetPieceAtPosition(new BoardPosition(current_row + 2, current_col)).PieceType == ChessPieceType.Empty && current_row == 1)
{
	allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.Normal));
}
//if player 2's pawn is moving up
else if (dir == new BoardDirection(1, 0) && GetPieceAtPosition(new BoardPosition(current_row + 1, current_col)).PieceType == ChessPieceType.Empty)
{
	if (current_position.Row + 1 == 7)
	{
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Bishop));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Rook));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Queen));
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessPieceType.Knight));
	}
	else
	{
		allPossibleChessMoves.Add(new ChessMove(current_position, possible_position, ChessMoveType.Normal));
	}
}

}
*/
		}
        #endregion
        #region setAdvantage
		GameAdvantage setAdvantage()
		{
			if(current_advantage > 0)
			{
				return new GameAdvantage(1, current_advantage);
			}
			else if (current_advantage < 0)
			{
				return new GameAdvantage(2, Math.Abs(current_advantage));
			}
			else
			{
				return new GameAdvantage(0, 0);
			}

		}
		#endregion
		#region getValueOfPiece
		int getValueOfPiece(ChessPiece cp)
		{
			ChessPieceType piece_type = cp.PieceType;
			switch (piece_type) {
				case ChessPieceType.Empty:
					return 0;
				case ChessPieceType.Pawn:
					return 1;
				case ChessPieceType.Rook:
					return 5;
				case ChessPieceType.Knight:
					return 3;
				case ChessPieceType.Bishop:
					return 3;
				case ChessPieceType.Queen:
					return 9;
				}
			return 0;
		}
	}
		#endregion



}
