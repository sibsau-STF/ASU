using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES;
using MES.Shared;

namespace Jonson
{
	public class JonsonFourth : IMESPlugin
	{
		public string Name => "Jonson fourth rule";

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			int indexOfBottleNeck (ProductTask task) => task.TimeOnBench.IndexOf(task.TimeOnBench.Max());

			var sorted = Utils.SortedTasks(tasks, indexOfBottleNeck, false).ToArray();
			Utils.SetStartingTimes(sorted);
			return sorted;
		}

		public override string ToString () => Name;

	}
}
