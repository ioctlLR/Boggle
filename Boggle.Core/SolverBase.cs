using System.Collections.Generic;

namespace Boggle.Core
{
	/// <summary>
	/// The base class for all solvers.
	/// </summary>
	abstract public class SolverBase
	{
		/// <summary>
		/// Initialize your solver.  Any pre-solve steps should be accomplished here.
		/// </summary>
		/// <param name="sortedDictionary">All scorable words, in lower case, and sorted.</param>
		/// <param name="boardSize">The board size in total cells (e.g., a 4x4 board would be 16)</param>
		abstract public void Init(IEnumerable<string> sortedDictionary, int boardSize);

		/// <summary>
		/// Search for words in the given board.
		/// </summary>
		/// <param name="board">The board to solve for.  It is a linear list of dice values, starting at the top-left corner and working left to right, top to bottom.
		/// Special case: One die has the combination 'Qu', indicated by 'q' in the board string.  This die requires the 'qu' letter sequence.</param>
		/// <returns></returns>
		abstract public IEnumerable<string> Solve(string board);

		virtual public void Reset() { }
	}
}
