using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES;
using MES.Shared;

namespace Petrov.Second
{
	public class PetrovSecond : IMESPlugin
	{
		public string Name => "Petrov-Sokolicin Second";

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			var sorted = Utils.SortedTasks(tasks, _secondSum, false).ToArray();
			Utils.SetStartingTimes(sorted);
			return sorted;
		}

		private int _secondSum (ProductTask task) => task.TimeOnBench.Sum() - task.TimeOnBench.Last();

	}
}
