﻿using Cecs475.BoardGames.Model;
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
					Weight = b.BoardWeight,
					Move = null
				};
			}
			
			bool isMaximizing;
			long bestWeight;
			if (b.CurrentPlayer == 2)
			{
				isMaximizing = false;
				bestWeight = beta;
			}
			else
			{
				isMaximizing = true;
				bestWeight = alpha;
			} 

			IGameMove bestMove = null;
			var possible_moves = b.GetPossibleMoves();
			foreach (var move in possible_moves)
			{
				b.ApplyMove(move);
				var w = FindBestMove(b, alpha, beta, depthLeft-1).Weight;
				b.UndoLastMove();
				if (isMaximizing == true && w > bestWeight)
				{
					bestWeight = w;
					bestMove = move;
					//want the maximum
					alpha = Math.Max(alpha, w);
				}
				else if (isMaximizing == false && w < bestWeight)
				{
					bestWeight = w;
					bestMove = move;
					//want the minimumz
					beta = Math.Min(beta, w);
				}
				if(alpha >= beta)
				{
					break;
				}
			}
			return new MinimaxBestMove() {
				Weight = bestWeight,
				Move = bestMove
			};
		}

	}
}
