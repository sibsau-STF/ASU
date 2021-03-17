using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared
{

	public static class Extensions
	{
		public static void PrintArray<T> (this IEnumerable<T> collection, bool inline = false)
		{
			if ( inline )
				Console.WriteLine(String.Format("[{0}]", String.Join(", ", collection)));
			else
				Console.WriteLine(String.Join("\n", collection));
		}


		public static int IndexOf<T> (this T[] collection, T value)
		{
			return Array.IndexOf(collection, value);
		}
	}

	public static class Utils
	{
		public static void SetStartingTimes (ProductTask[] tasks)
		{
			var matrix = GetDurationMatrix(tasks);
			int taskCount = tasks.Length;
			int benchCount = tasks.First()?.TimeOnBench.Length ?? 0;

			for ( int t = 0; t < taskCount; t++ )
			{
				tasks[t].StartTime = new int[benchCount];
				for ( int time = 0; time < benchCount; time++ )
					tasks[t].StartTime[time] = matrix[t][time] - tasks[t].TimeOnBench[time];
			}
		}

		public static int[][] GetDurationMatrix (IEnumerable<ProductTask> tasks)
		{
			return GetDurationMatrix(tasks.Select(t => (int[])t.TimeOnBench.Clone()).ToArray());
		}

		public static int[][] GetDurationMatrix (int[][] matrix)
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

		public static int GetProductDuration (int[][] matrix)
		{
			return matrix.Last().Last();
		}


		public static void PrintMatrix (int[][] matrix)
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

		public static IEnumerable<ProductTask> SortedTasks (IEnumerable<ProductTask> tasks, IEnumerable<int> order)
		{
			var collection = tasks
				.Zip(order, (tsk, o) => new KeyValuePair<int, ProductTask>(o, tsk))
				.ToList();
			collection.Sort((one, two) => one.Key.CompareTo(two.Key));
			return collection.Select(pair => pair.Value);
		}

		public static IEnumerable<ProductTask> SortedTasks (IEnumerable<ProductTask> tasks, Func<ProductTask, int> predicate, bool askending = true)
		{
			var collection =
				tasks.Select(tsk => new KeyValuePair<int, int>(tsk.Number, predicate(tsk))).ToList();
			collection.Sort((one, two) => one.Value.CompareTo(two.Value) * ( askending ? 1 : -1 ));

			var order = collection.Select(pair => pair.Key);
			return SortedTasks(tasks, order);
		}
	}
}
