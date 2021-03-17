using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
	public static class CSVService
	{
		static char[] _colSplitters = { ';' };
		static char[] _rowSplitters = { '\n', '\r' };

		public static T[][] ReadTable<T> (TextReader reader, Func<string, T> converter, bool header)
		{
			if ( header )
				reader.ReadLine();

			Func<string, IEnumerable<T>> stringToCollection =
									str => str.Split(_colSplitters).Select(converter);

			var lines = reader.ReadToEnd().Split(_rowSplitters, StringSplitOptions.RemoveEmptyEntries);

			return lines.Select(str => stringToCollection(str).ToArray()).ToArray();
		}
	}
}
