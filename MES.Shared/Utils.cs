using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared
{
	public static class Utils
	{

		public static int[][] GetDurationMatrix (List<KeyValuePair<ProductTask, int>> tasks)
		{
			return GetDurationMatrix(tasks.Select(pair => (int[])pair.Key.TimeOnBench.Clone()).ToArray());
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
	}
}
