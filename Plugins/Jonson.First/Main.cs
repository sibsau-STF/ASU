using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MES;
using MES.Shared;

namespace Jonson.First
{
	public class JonsonFirst : IMESPlugin
	{
		public string Name => "Jonson first rule";

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			var sorted = Utils.SortedTasks(tasks, tsk => tsk.TimeOnBench.First(), true).ToArray();
			Utils.SetStartingTimes(sorted);
			return sorted;
		}

	}
}
