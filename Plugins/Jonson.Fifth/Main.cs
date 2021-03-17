using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES;
using MES.Shared;
using Jonson.First;
using Jonson.Second;
using Jonson.Third;
using Jonson.Fourth;

namespace Jonson.Fifth
{
	public class JonsonFifth : IMESPlugin
	{
		public string Name => "Jonson fifth rule (Optimal)";

		private IMESPlugin[] methods =
		{
			new JonsonFirst(),
			new JonsonSecond(),
			new JonsonThird(),
			new JonsonFourth()
		};

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			var sordedMatrix = methods.Select(m => m.Apply(Clone(tasks).ToArray())).ToArray();

			//foreach ( ProductTask[] sorted in sordedMatrix )
			//{
			//	sorted.PrintArray();
			//	Console.WriteLine();
			//}

			// объединяю очереди и получаю порядок задачи для каждой очереди
			var resultingOrder = tasks.Select(tsk =>
								sordedMatrix[0].IndexOf(tsk)
								+ sordedMatrix[1].IndexOf(tsk)
								+ sordedMatrix[2].IndexOf(tsk)
								+ sordedMatrix[3].IndexOf(tsk)
								+ 4);

			//resultingOrder.PrintArray(true);

			// сортирую задачи по очередности
			var sortedTasks = Utils.SortedTasks(tasks, resultingOrder).ToArray();
			Utils.SetStartingTimes(sortedTasks);
			return sortedTasks;
		}

		private IEnumerable<ProductTask> Clone(IEnumerable<ProductTask> tasks)
		{
			return tasks.Select(t => new ProductTask(t));
		}
	}
}
