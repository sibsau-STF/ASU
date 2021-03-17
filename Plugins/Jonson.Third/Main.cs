using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES;
using MES.Shared;

namespace Jonson.Third
{
	public class JonsonThird : IMESPlugin
	{
		public string Name => "Jonson third rule";

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			var sorted = Utils.SortedTasks(tasks, tsk => tsk.TimeOnBench.Sum()).ToArray();
			Utils.SetStartingTimes(sorted);
			return sorted;
		}
	}
}
