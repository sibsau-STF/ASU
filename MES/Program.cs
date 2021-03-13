using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
	static class Extensions
	{
		public static void PrintArray<T> (this IEnumerable<T> collection, bool inline = false)
		{
			if ( inline )
				Console.WriteLine(String.Format("[{0}]", String.Join(", ", collection)));
			else
				Console.WriteLine(String.Join("\n", collection));
		}
	}

	class Program
	{

		static int parseInt(string str) => str == "" ? 0 : int.Parse(str);

		static void Main (string[] args)
		{
			TextReader reader = new StreamReader("input.csv", Encoding.Default);
			var table = CSVService.ReadTable(reader, parseInt, true);

			ProductTask[] productTasks = new ProductTask[table.Length];

			for ( int i = 0; i < table.Length; i++ )
			{
				var row = table[i];
				var times = row.Skip(1).ToArray();
				var number = row[0];
				productTasks[i] = new ProductTask(number, times);
			}
			Console.WriteLine("Data: ");
			productTasks.PrintArray();
			Console.WriteLine();
			var tasks = PetrovReorder.Reorder(productTasks);
			tasks.PrintArray();

			Console.Read();
		}
	}
}
