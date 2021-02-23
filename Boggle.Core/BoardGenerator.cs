using System;
using System.Collections.Generic;
using System.Text;

namespace Boggle.Core
{
	public static class BoardGenerator
	{
		static readonly string[] s_dice =
		{
			"twoota",
			"hneewg",
			"safpkf",
			"erlidx",
			"yedlev",
			"ytdtis",
			"vehwtr",
			"muocti",
			"uiense",
			"nlnhzr",
			"issoet",
			"tretlr",
			"eanage",
			"unhiqm",
			"oscahp",
			"oabbjo",
		};

		public static string GenerateStandardBoard()
		{
			var board = new char[16];
			var dice = new List<string>(s_dice);
			var rng = new Random();
			for (var i = 0; i < board.Length; i++)
			{
				var dieIdx = rng.Next(dice.Count);
				board[i] = dice[dieIdx][rng.Next(6)];
				dice.RemoveAt(dieIdx);
			}
			return new string(board);
		}

		public static string GenerateNonstandardBoard(int boardEdgeLength)
		{
			var size = boardEdgeLength * boardEdgeLength;
			var board = new char[size];
			var rng = new Random();
			for (var i = 0; i < board.Length; i++)
			{
				board[i] = s_dice[rng.Next(s_dice.Length)][rng.Next(6)];
			}
			return board.ToString();
		}
	}
}
