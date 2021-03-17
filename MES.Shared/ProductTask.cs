using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
	public class ProductTask
	{
		public readonly int Number;
		public readonly int[] TimeOnBench;
		public int[] StartTime;

		public ProductTask (int number, int[] benchTime)
		{
			Number = number;
			TimeOnBench = benchTime;
		}

		public override string ToString ()
		{
			if ( StartTime != null )
			{
				var timeSpans = StartTime.Zip(TimeOnBench, (s, d) => $"{s}-{d}");
				return Number + String.Format("[{0}]", String.Join(", ", timeSpans));
			}
			return Number + String.Format("[{0}]", String.Join(", ", TimeOnBench));
		}
	}
}
