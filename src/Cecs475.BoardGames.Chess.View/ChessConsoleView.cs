using System;
using System.Text;
using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.View;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Cecs475.BoardGames.Chess.View {
	//TeSTING hihihhi
	//testing 2 hihihi
	/// <summary>
	/// A chess game view for string-based console input and output.
	/// </summary>
	public class ChessConsoleView : IConsoleView {
		private static char[] LABELS = { '.', 'P', 'R', 'N', 'B', 'Q', 'K' };
		
		// Public methods.
		public string BoardToString(ChessBoard board) {
			StringBuilder str = new StringBuilder();

			for (int i = 0; i < ChessBoard.BoardSize; i++) {
				str.Append(8 - i);
				str.Append(" ");
				for (int j = 0; j < ChessBoard.BoardSize; j++) {
					var space = board.GetPieceAtPosition(new BoardPosition(i, j));
					if (space.PieceType == ChessPieceType.Empty)
						str.Append(". ");
					else if (space.Player == 1)
						str.Append($"{LABELS[(int)space.PieceType]} ");
					else
						str.Append($"{char.ToLower(LABELS[(int)space.PieceType])} ");
				}
				str.AppendLine();
			}
			str.AppendLine("  a b c d e f g h");
			return str.ToString();
		}

		/// <summary>
		/// Converts the given ChessMove to a string representation in the form
		/// "(start, end)", where start and end are board positions in algebraic
		/// notation (e.g., "a5").
		/// 
		/// If this move is a pawn promotion move, the selected promotion piece 
		/// must also be in parentheses after the end position, as in 
		/// "(a7, a8, Queen)".
		/// </summary>
		public string MoveToString(ChessMove move) {
			var row_map = new Dictionary<char,char>();

			row_map.Add('0', '8');
			row_map.Add('1', '7');
			row_map.Add('2', '6');
			row_map.Add('3', '5');
			row_map.Add('4', '4');
			row_map.Add('5', '3');
			row_map.Add('6', '2');
			row_map.Add('7', '1');


			var col_map = new Dictionary<char, char>();

			col_map.Add('0', 'a');
			col_map.Add('1', 'b');
			col_map.Add('2', 'c');
			col_map.Add('3', 'd');
			col_map.Add('4', 'e');
			col_map.Add('5', 'f');
			col_map.Add('6', 'g');
			col_map.Add('7', 'h');


			String start_position = move.StartPosition.ToString();
			/*Console.Write("start_position: ");
			Console.WriteLine(start_position);
			Console.WriteLine(move.MoveType.ToString());*/
			start_position = start_position.Replace(" ", "");
			start_position = start_position.Replace("(", "");
			start_position = start_position.Replace(")", "");
			start_position = start_position.Replace(",", "");

			char[] start_array = start_position.ToCharArray(0, start_position.Length);
			start_array[0] = row_map[start_array[0]];
			start_array[1] = col_map[start_array[1]];

			String start_moveString = start_array[1].ToString() + start_array[0].ToString();

				
			String end_position = move.EndPosition.ToString();
			end_position = end_position.Replace(" ", "");
			end_position = end_position.Replace("(", "");
			end_position = end_position.Replace(")", "");
			end_position = end_position.Replace(",", "");
			char[] end_array = end_position.ToCharArray(0, end_position.Length);
			end_array[0] = row_map[end_array[0]];
			end_array[1] = col_map[end_array[1]];

			String end_moveString = end_array[1].ToString() + end_array[0].ToString();
			ChessMoveType movetype = move.MoveType;
			if(movetype == ChessMoveType.PawnPromote)
			{
				return ("(" + start_moveString + "," + end_moveString + "," + move.ChessPieceType + ")");
			}
			else if (movetype == ChessMoveType.Normal)
			{
				return ("(" + start_moveString + "," + end_moveString + ")");
			}
			else
			{
				return ("(" + start_moveString + "," + end_moveString + "," + movetype + ")");

			}
		}

		public string PlayerToString(int player) {
			return player == 1 ? "White" : "Black";
		}

		/// <summary>
		/// Converts a string representation of a move into a ChessMove object.
		/// Must work with any string representation created by MoveToString.
		/// </summary>
		public ChessMove ParseMove(string moveText) {
			var col_map = new Dictionary<char, int>();
			col_map.Add('a', 0);
			col_map.Add('b', 1);
			col_map.Add('c', 2);
			col_map.Add('d', 3);
			col_map.Add('e', 4);
			col_map.Add('f', 5);
			col_map.Add('g', 6);
			col_map.Add('h', 7);

			var row_map = new Dictionary<char, int>();

			row_map.Add('8', 0);
			row_map.Add('7', 1);
			row_map.Add('6', 2);
			row_map.Add('5', 3);
			row_map.Add('4', 4);
			row_map.Add('3', 5);
			row_map.Add('2', 6);
			row_map.Add('1', 7);

			//Console.WriteLine(moveText);
			moveText = Regex.Replace(moveText, @" ", ""); 
			moveText = moveText.Replace("(", "");
			moveText = moveText.Replace(")", "");
			moveText = moveText.Replace(" ", "");

			//Console.WriteLine(moveText);
			String[] move = moveText.Split(",");

			String start_move = "";
			String end_move = "";

			if(move.Length > 0)
			{
				start_move = move[0];
				end_move = move[1];
			}
			//Console.WriteLine("parsed start move:" + row_map[start_move[1]] + "," + col_map[start_move[0]]);

			int parsedStart_row = row_map[start_move[1]];
			int parsedStart_col = col_map[start_move[0]];

			int parsedEnd_row = row_map[end_move[1]];
			int parsedEnd_col = col_map[end_move[0]];

			String moveType = "";
			ChessMove test = null;
			if (move.Length > 2)
			{
				moveType = move[move.Length-1];
				Console.WriteLine(moveType);
				if (moveType == "Queen" || moveType == "queen")
				{
					test = new ChessMove(new BoardPosition(parsedStart_row, parsedStart_col), new BoardPosition(parsedEnd_row, parsedEnd_col), ChessPieceType.Queen,ChessMoveType.PawnPromote);
				}
				else if (moveType == "Bishop" || moveType == "bishop")
				{
					test = new ChessMove(new BoardPosition(parsedStart_row, parsedStart_col), new BoardPosition(parsedEnd_row, parsedEnd_col), ChessPieceType.Bishop, ChessMoveType.PawnPromote);
				}
				else if (moveType == "Rook" || moveType == "rook")
				{
					test = new ChessMove(new BoardPosition(parsedStart_row, parsedStart_col), new BoardPosition(parsedEnd_row, parsedEnd_col), ChessPieceType.Rook, ChessMoveType.PawnPromote);
				}
				else if (moveType == "Knight" || moveType == "knight")
				{
					test = new ChessMove(new BoardPosition(parsedStart_row, parsedStart_col), new BoardPosition(parsedEnd_row, parsedEnd_col), ChessPieceType.Knight, ChessMoveType.PawnPromote);
				}
				else
				{
					ChessMoveType typeParsed = (ChessMoveType)(Enum.Parse(typeof(ChessMoveType), move[move.Length - 1]));
					test = new ChessMove(new BoardPosition(parsedStart_row, parsedStart_col), new BoardPosition(parsedEnd_row, parsedEnd_col), typeParsed);
				}
			}
			else
			{
				test = new ChessMove(new BoardPosition(parsedStart_row, parsedStart_col), new BoardPosition(parsedEnd_row, parsedEnd_col));
			}
			return test;
		}

		public static BoardPosition ParsePosition(string pos) {
			return new BoardPosition(8 - (pos[1] - '0'), pos[0] - 'a');
		}

		public static string PositionToString(BoardPosition pos) {
			return $"{(char)(pos.Col + 'a')}{8 - pos.Row}";
		}

		#region Explicit interface implementations
		// Explicit method implementations. Do not modify these.
		string IConsoleView.BoardToString(IGameBoard board) {
			return BoardToString(board as ChessBoard);
		}

		string IConsoleView.MoveToString(IGameMove move) {
			return MoveToString(move as ChessMove);
		}

		IGameMove IConsoleView.ParseMove(string moveText) {
			return ParseMove(moveText);
		}
		#endregion
	}
}
