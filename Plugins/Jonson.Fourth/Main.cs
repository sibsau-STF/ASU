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
			var sorted = _farthestBottleNeck(tasks).ToArray();
			Utils.SetStartingTimes(sorted);
			return sorted;
		}

		private static IEnumerable<ProductTask> _farthestBottleNeck (IEnumerable<ProductTask> tasks)
		{
			var taskStack = tasks.ToList();
			var cols = tasks.First().TimeOnBench.Length;
			List<ProductTask> result = new List<ProductTask>();

			for ( int c = cols - 1; c > 1; c-- )
			{
				// получаю время выполнение каждой детали на n, n-1.. станке
				var times = taskStack.Select(tsk => tsk.TimeOnBench[c]).ToList();

				//перемещаю элемент с максимальным временем выполнения в результат
				var maxTime = times.Max();
				var index = times.IndexOf(maxTime);
				result.Add(taskStack[index]);
				taskStack.RemoveAt(index);
			}
			// сортирую задачи по времени выполнения на втором станке
			taskStack.Sort((one, two) => -1 * one.TimeOnBench[1].CompareTo(two.TimeOnBench[1]));
			result.AddRange(taskStack);
			return result;
		}

		public override string ToString () => Name;

	}
}
