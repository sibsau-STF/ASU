using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Shared;

namespace MES
{
	public static class PetrovReorder
	{
		public static ProductTask[] Reorder (ProductTask[] tasks)
		{
			// получаю таски отсортированные по критерию
			List<List<ProductTask>> tasksSumSorted = new List<List<ProductTask>>{
				getSortedTasks(tasks, task => task.TimeOnBench.Sum() - task.TimeOnBench.First(), false),
				getSortedTasks(tasks, task => task.TimeOnBench.Sum() - task.TimeOnBench.Last(), true),
				getSortedTasks(tasks, task => task.TimeOnBench.Last() - task.TimeOnBench.First(), false)
  			};
			// получаю двумерные матрицы с накопленным временем для каждого случая
			List<int[][]> durationMatrixies = tasksSumSorted.Select(Utils.GetDurationMatrix).ToList();

			// вывожу таски и полученные для них матрицы
			for ( int num = 0; num < 3; num++ )
			{
				Console.WriteLine($"Sum number {num + 1}");
				foreach ( var task in tasksSumSorted[num] )
					Console.WriteLine(task);

				Utils.PrintMatrix(durationMatrixies[num]);
				Console.WriteLine();
			}
			// нахожу время каждой стратегии и получаю номер оптимальной стратегии
			var totalTimes = durationMatrixies.Select(Utils.GetProductDuration).ToList();
			int minTimeIndex = totalTimes.IndexOf(totalTimes.Min());

			// извлекаю оптимальную последовательность тасков
			return tasksSumSorted[minTimeIndex].ToArray();
		}

		private static List<ProductTask> getSortedTasks (ProductTask[] tasks, Func<ProductTask, int> valueSelector, bool askending)
		{
			var unsortedMatrix = tasks.ToDictionary(task => task, valueSelector).ToList();
			unsortedMatrix.Sort((one, two) => one.Value.CompareTo(two.Value) * ( askending ? 1 : -1 ));
			return unsortedMatrix.Select(pair=>pair.Key).ToList();
		}
	}
}
