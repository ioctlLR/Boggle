using System;
using System.Collections.Generic;

namespace Boggle.Core
{
	public static class WordListProvider
	{
		static readonly string[] s_wordList;

		static WordListProvider()
		{
			using (var s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Boggle.Core.words.txt"))
			using (var sr = new System.IO.StreamReader(s))
			{
				// create our list, but _try_ to pre-allocate enough space for everything
				var expectedLines = 1 << ((int)Math.Log2(s.Length / 11) + 1);
				var words = new List<string>(expectedLines);

				string line;
				while ((line = sr.ReadLine()?.Trim()) != null)
				{
					if (line.Length >= 3)
					{
						words.Add(line.ToLowerInvariant());
					}
				}
				words.Sort();
				s_wordList = words.ToArray();
			}
		}

		public static IEnumerable<string> GetWordList() => s_wordList;
	}
}
