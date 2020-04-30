using Cecs475.BoardGames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.BoardGames.ComputerOpponent {
	internal struct MinimaxBestMove {
		public long Weight { get; set; }
		public IGameMove Move { get; set; }
	}

	public class MinimaxAi : IGameAi {
		private int mMaxDepth;
		public MinimaxAi(int maxDepth) {
			mMaxDepth = maxDepth;
		}

		public IGameMove FindBestMove(IGameBoard b) {
			return FindBestMove(b,
				true ? long.MinValue : long.MaxValue,
				true ? long.MaxValue : long.MinValue,
				mMaxDepth).Move;
		}

		private static MinimaxBestMove FindBestMove(IGameBoard b, long alpha, long beta, int depthLeft) {
			if (depthLeft == 0 || b.IsFinished)
			{
				return new MinimaxBestMove()
				{
					Move = null
				};
			}
			//how do u know depending on alpha and beta??
			//bestWeight = -inf if maximizingPlayer, otherwise +inf
			bool maximizing;
			long bestWeight;
			if (b.CurrentPlayer == 1)
			{
				maximizing = true;
				bestWeight = long.MinValue;
			}
			else
			{
				maximizing = false;
				bestWeight = long.MaxValue;
			}

			IGameMove bestMove = null;
			var possible_moves = b.GetPossibleMoves();
			foreach (var move in possible_moves)
			{
				b.ApplyMove(move);
				var w = FindBestMove(b, alpha, beta, depthLeft).Weight;
				b.UndoLastMove();
				if (maximizing == true && w > bestWeight)
				{
					bestWeight = w;
					bestMove = move;
				}
				else if (maximizing == false && w < bestWeight)
				{
					bestWeight = w;
					bestMove = move;
				}
			}
			return new MinimaxBestMove() {
				Weight = bestWeight,
				Move = bestMove
			};
		}

	}
}
