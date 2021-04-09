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

		public ProductTask (ProductTask task)
		{
			Number = task.Number;
			TimeOnBench = (int[])task.TimeOnBench.Clone();
			StartTime = (int[])task.StartTime?.Clone();
		}

		public override int GetHashCode ()
		{
			return Number.GetHashCode();
		}

		public override bool Equals (object obj)
		{
			return obj is ProductTask task ? task.Number == Number : base.Equals(obj);
		}

		public override string ToString ()
		{
			if ( StartTime == null )
				return Number.ToString() + $"[{string.Join(", ", TimeOnBench)}]";

			var timeSpans = StartTime.Zip(TimeOnBench, (s, d) => $"{s}-{s + d}");
			return Number.ToString() + $"[{string.Join(", ", timeSpans)}]";
		}
	}
}
