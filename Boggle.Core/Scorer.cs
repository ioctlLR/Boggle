using System.Collections.Generic;

namespace Boggle.Core
{
	public class Scorer
	{
		readonly HashSet<string> _knownWords = new HashSet<string>();

		public Scorer(IEnumerable<string> dictionary)
		{
			foreach (var word in dictionary)
			{
				_knownWords.Add(word);
			}
		}

		public (int words, int score) Score(IEnumerable<string> foundWords)
		{
			HashSet<string> validWords = new HashSet<string>();
			foreach (var word in foundWords)
			{
				if (_knownWords.Contains(word))
				{
					if (!validWords.Contains(word))
					{
						validWords.Add(word);
					}
				}
			}
			return (validWords.Count, CalculateTotal(validWords));
		}

		private int CalculateTotal(HashSet<string> validWords)
		{
			var total = 0;
			foreach (var word in validWords)
			{
				switch (word.Length)
				{
					case 3:
					case 4: total += 1; break;
					case 5: total += 2; break;
					case 6: total += 3; break;
					case 7: total += 5; break;
					default: total += 11; break;
				}
			}
			return total;
		}
	}
}