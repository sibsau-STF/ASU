using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES;
using MES.Shared;

namespace Petrov.First
{
	public class PetrovFirst : IMESPlugin
	{
		public string Name => "Petrov-Sokolicin First";

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			var sorted = Utils.SortedTasks(tasks, _firstSum, false).ToArray();
			Utils.SetStartingTimes(sorted);
			return sorted;
		}

		private int _firstSum (ProductTask task) => task.TimeOnBench.Sum() - task.TimeOnBench.First();
	}
}
