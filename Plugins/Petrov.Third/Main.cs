using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES;
using MES.Shared;

namespace Petrov
{
	public class PetrovThird : IMESPlugin
	{
		public string Name => "Petrov-Sokolicin Third";

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			var sorted = Utils.SortedTasks(tasks, _subtraction, false).ToArray();
			Utils.SetStartingTimes(sorted);
			return sorted;
		}
		public override string ToString () => Name;

		private int _subtraction (ProductTask task) => task.TimeOnBench.Last() - task.TimeOnBench.First();
	}
}
