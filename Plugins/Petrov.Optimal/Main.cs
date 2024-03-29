﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES;
using MES.Shared;

namespace Petrov
{
	public class PetrovOptimal : IMESPlugin
	{
		public string Name => "Petrov-Sokolicin Optimal";

		private IMESPlugin[] methods =
		{
			new PetrovFirst(),
			new PetrovSecond(),
			new PetrovThird()
		};

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			// сортирую задачи всеми методами и устанавливаю на временной шкале
			var sortedMatrix = methods.Select(m => m.Apply(Clone(tasks).ToArray())).ToList();

			//foreach ( var sorted in sortedMatrix )
			//	sorted.PrintArray();


			// определяю длительность каждой очереди
			var durations = sortedMatrix
				.Select(ordered =>
				{
					var last = ordered.Last();
					return last.StartTime.Last() + last.TimeOnBench.Last();
				})
				.ToList();

			// возвращаю очередь с минимальной длительностью
			var minimumIndex = durations.IndexOf(durations.Min());
			return sortedMatrix.ElementAt(minimumIndex);
		}

		public override string ToString () => Name;

		private IEnumerable<ProductTask> Clone (IEnumerable<ProductTask> tasks)
		{
			return tasks.Select(t => new ProductTask(t));
		}
	}
}
