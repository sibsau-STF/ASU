using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
	class PetrovReorder : IReordable
	{
		public ProductTask[] Reorder (ProductTask[] tasks)
		{
			// получаю таски отсортированные по критерию
			List<List<KeyValuePair<ProductTask, int>>> tasksSumSorted = new List<List<KeyValuePair<ProductTask, int>>>{
				getSortedTasks(tasks, task => task.TimeOnBench.Sum() - task.TimeOnBench.First(), false),
				getSortedTasks(tasks, task => task.TimeOnBench.Sum() - task.TimeOnBench.Last(), true),
				getSortedTasks(tasks, task => task.TimeOnBench.Last() - task.TimeOnBench.First(), false)
  			};
			// получаю двумерные матрицы с накопленным временем для каждого случая
			List<int[][]> durationMatrixies = tasksSumSorted.Select(getDurationMatrix).ToList();

			// вывожу таски и полученные для них матрицы
			for ( int num = 0; num < 3; num++ )
			{
				Console.WriteLine($"Sum number {num + 1}");
				foreach ( var pair in tasksSumSorted[num] )
					Console.WriteLine(pair.Key);

				_printMatrix(durationMatrixies[num]);
				Console.WriteLine();
			}
			// нахожу время каждой стратегии и получаю номер оптимальной стратегии
			var totalTimes = durationMatrixies.Select(getProductDuration).ToList();
			int minTimeIndex = totalTimes.IndexOf(totalTimes.Min());

			// извлекаю оптимальную последовательность тасков
			return tasksSumSorted[minTimeIndex].Select(pair => pair.Key).ToArray();
		}

		private List<KeyValuePair<ProductTask, int>> getSortedTasks (ProductTask[] tasks, Func<ProductTask, int> valueSelector, bool askending)
		{
			var unsortedMatrix = tasks.ToDictionary(task => task, valueSelector).ToList();
			unsortedMatrix.Sort((one, two) => one.Value.CompareTo(two.Value) * ( askending ? 1 : -1 ));
			return unsortedMatrix;
		}

		private int getProductDuration (int[][] matrix)
		{
			return matrix.Last().Last();
		}

		private int[][] getDurationMatrix (List<KeyValuePair<ProductTask, int>> tasks)
		{
			return getDurationMatrix(tasks.Select(pair => (int[])pair.Key.TimeOnBench.Clone()).ToArray());
		}

		private int[][] getDurationMatrix (int[][] matrix)
		{
			var rows = matrix.Length;
			var cols = matrix.First().Length;

			// накопление первой строки
			for ( int col = 0; col < cols; col++ )
				matrix[0][col] = ( col > 0 ? matrix[0][col - 1] : 0 ) + matrix[0][col];

			for ( int row = 1; row < rows; row++ )
			{
				// накопление первого столбца
				matrix[row][0] = matrix[row][0] + matrix[row - 1][0];
				// время = время обработки детали + макс из
				// времени работы прошлого станка для этой детали
				// и времени обработки предыдущей детали на текущем станке
				for ( int col = 1; col < cols; col++ )
					matrix[row][col] = matrix[row][col] + Math.Max(matrix[row - 1][col], matrix[row][col - 1]);
			}
			return matrix;
		}

		private void _printMatrix (int[][] matrix)
		{
			var rows = matrix.Length;
			var cols = matrix.First().Length;
			for ( int row = 0; row < rows; row++ )
			{
				for ( int col = 0; col < cols; col++ )
					Console.Write(matrix[row][col] + "\t");
				Console.WriteLine();
			}
		}
	}
}
