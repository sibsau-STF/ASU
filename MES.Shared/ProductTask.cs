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

		public ProductTask (int number, int[] benchTime)
		{
			Number = number;
			TimeOnBench = benchTime;
		}

		public override string ToString ()
		{
			return Number + String.Format("[{0}]", String.Join(", ", TimeOnBench));
		}
	}
}
