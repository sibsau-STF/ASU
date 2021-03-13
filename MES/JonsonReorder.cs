using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
	public static class JonsonReorder
	{

		public static ProductTask[] FirstRule (ProductTask[] tasks)
		{
			var order = FirstBenchLeastTime(tasks);
			return _sortedTasks(tasks, order).ToArray();
		}

		public static ProductTask[] SecondRule (ProductTask[] tasks)
		{
			var order = LastBenchMostTime(tasks);
			return _sortedTasks(tasks, order).ToArray();
		}

		public static ProductTask[] ThirdRule (ProductTask[] tasks)
		{
			var order = TotalTime(tasks);
			return _sortedTasks(tasks, order).ToArray();
		}

		public static ProductTask[] FourthRule (ProductTask[] tasks)
		{
			var order = FarthestBottleNeck(tasks);
			return _sortedTasks(tasks, order).ToArray();
		}
		public static ProductTask[] FifthRule (ProductTask[] tasks)
		{
			// вычисляю порядок задач по четырем очередям
			var firstBench = FirstBenchLeastTime(tasks);
			var lastBench = LastBenchMostTime(tasks);
			var totalTime = TotalTime(tasks);
			var bottleNeck = FarthestBottleNeck(tasks);

			firstBench.PrintArray(true);
			lastBench.PrintArray(true);
			totalTime.PrintArray(true);
			bottleNeck.PrintArray(true);


			// объединяю очереди и получаю порядок задачи для каждой очереди
			var resultingOrder = tasks.Select(tsk =>
								firstBench.IndexOf(tsk.Number)
								+ lastBench.IndexOf(tsk.Number)
								+ totalTime.IndexOf(tsk.Number)
								+ bottleNeck.IndexOf(tsk.Number)
								+ 4);
			resultingOrder.PrintArray(true);

			// сортирую задачи по очередности
			return _sortedTasks(tasks, resultingOrder).ToArray();
		}

		private static IEnumerable<ProductTask> _sortedTasks (IEnumerable<ProductTask> tasks, IEnumerable<int> order)
		{
			var collection = tasks
				.Zip(order, (tsk, o) => new KeyValuePair<int, ProductTask>(o, tsk))
				.ToList();
			collection.Sort((one, two) => one.Key.CompareTo(two.Key));
			return collection.Select(pair => pair.Value);
		}

		private static List<int> FirstBenchLeastTime (IEnumerable<ProductTask> tasks)
		{
			return SortByPredicate(tasks, tsk => tsk.TimeOnBench.First());
		}

		private static List<int> LastBenchMostTime (IEnumerable<ProductTask> tasks)
		{
			return SortByPredicate(tasks, tsk => tsk.TimeOnBench.Last(), false);
		}


		private static List<int> TotalTime (IEnumerable<ProductTask> tasks)
		{
			return SortByPredicate(tasks, tsk => tsk.TimeOnBench.Sum());
		}

		private static List<int> SortByPredicate (IEnumerable<ProductTask> tasks, Func<ProductTask, int> predicate, bool askending = true)
		{
			var collection =
				tasks.Select(tsk => new KeyValuePair<int, int>(tsk.Number, predicate(tsk))).ToList();
			collection.Sort((one, two) => one.Value.CompareTo(two.Value) * ( askending ? 1 : -1 ));
			return collection.Select(pair => pair.Key).ToList();
		}

		private static List<int> FarthestBottleNeck (IEnumerable<ProductTask> tasks)
		{
			var taskStack = tasks.ToList();
			var cols = tasks.First().TimeOnBench.Length;
			List<int> times = new List<int>();
			List<ProductTask> result = new List<ProductTask>();

			for ( int c = cols - 1; c > 1; c-- )
			{
				// получаю время выполнение каждой детали на n, n-1.. станке
				times = taskStack.Select(tsk => tsk.TimeOnBench[c]).ToList();

				//перемещаю элемент с максимальным временем выполнения в результат
				var maxTime = times.Max();
				var index = times.IndexOf(maxTime);
				result.Add(taskStack[index]);
				taskStack.RemoveAt(index);
			}
			// сортирую задачи по времени выполнения на втором станке
			taskStack.Sort((one, two) => -1 * one.TimeOnBench[1].CompareTo(two.TimeOnBench[1]));
			result.AddRange(taskStack);
			return result.Select(tsk => tsk.Number).ToList();
		}

	}

}
