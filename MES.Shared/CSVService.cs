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
		static readonly char[] _colSplitters = { ';' };
		static readonly char[] _rowSplitters = { '\n', '\r' };

		public static T[][] ReadTable<T> (TextReader reader, Func<string, T> converter, bool header)
		{
			IEnumerable<T> stringToCollection (string str)
			{
				return str.Split(_colSplitters).Select(converter);
			}

			if ( header )
				reader.ReadLine();

			var lines = reader.ReadToEnd().Split(_rowSplitters, StringSplitOptions.RemoveEmptyEntries);

			return lines.Select(str => stringToCollection(str).ToArray()).ToArray();
		}
	}
}
