using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES;
using MES.Shared;

namespace Jonson.Second
{
	public class JonsonSecond : IMESPlugin
	{
		public string Name => "Jonson second Rule";

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			var sorted = Utils.SortedTasks(tasks, tsk => tsk.TimeOnBench.Last(), false).ToArray();
			Utils.SetStartingTimes(sorted);
			return sorted;
		}
	}
}
