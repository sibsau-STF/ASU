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
			var task = obj as ProductTask;
			if (task==null)
				return base.Equals(obj);
			return task.Number == Number;
		}

		public override string ToString ()
		{
			if ( StartTime != null )
			{
				var timeSpans = StartTime.Zip(TimeOnBench, (s, d) => $"{s}-{s + d}");
				return Number + String.Format("[{0}]", String.Join(", ", timeSpans));
			}
			return Number + String.Format("[{0}]", String.Join(", ", TimeOnBench));
		}
	}
}
