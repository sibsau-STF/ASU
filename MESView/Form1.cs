using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using MES.Shared;

namespace MES.View
{
	public partial class Form1 : Form
	{
		List<IMESPlugin> _plugins;
		ProductTask[] _inputData;
		ProductTask[] _resultData;

		public Form1 ()
		{
			InitializeComponent();
		}

		private void button1_Click (object sender, EventArgs e)
		{
			if ( openFileDialog1.ShowDialog() == DialogResult.OK )
			{
				string path = openFileDialog1.FileName;
				_inputData = _readTasks(path);
				Utils.SetStartingTimes(_inputData);
				_plotData(inputPlot, _inputData);
				tabControl1.SelectedIndex = 0;
			}
		}

		private void _plotData (PlotView plot, IEnumerable<ProductTask> tasks)
		{
			var lastTask = tasks.Last();
			var totalTime = lastTask.StartTime.Last() + lastTask.TimeOnBench.Last();
			var model = new PlotModel
			{
				Title = "Total time = " + totalTime,
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.LeftTop,
			};

			foreach ( var task in tasks )
			{
				var taskSeries = new RectangleBarSeries { Title = "Task" + task.Number };
				for ( int i = 0; i < task.TimeOnBench.Length; i++ )
				{
					taskSeries.Items.Add(new RectangleBarItem
					{
						X0 = task.StartTime[i],
						X1 = task.StartTime[i] + task.TimeOnBench[i],
						Y0 = 0 + i,
						Y1 = 1 + i,
						Title = task.TimeOnBench[i].ToString()
					});

				}
				model.Series.Add(taskSeries);
			}
			plot.Model = model;
		}

		static int _parseInt (string str) => str == "" ? 0 : int.Parse(str);

		private ProductTask[] _readTasks (string path)
		{
			TextReader reader = new StreamReader(path, Encoding.Default);
			var table = CSVService.ReadTable(reader, _parseInt, true);

			ProductTask[] productTasks = new ProductTask[table.Length];

			for ( int i = 0; i < table.Length; i++ )
			{
				var row = table[i];
				var times = row.Skip(1).ToArray();
				var number = row[0];
				productTasks[i] = new ProductTask(number, times);
			}
			return productTasks;
		}

		private void Form1_Load (object sender, EventArgs e)
		{
			var loader = new PluginLoader();
			loader.LoadPlugins();

			_plugins = PluginLoader.Plugins;
			_plugins.Sort((l, r) => l.Name.CompareTo(r.Name));

			if (_plugins.Count < 1)
			{
				MessageBox.Show("Плагины не обнаружены");
				this.Close();
				return;
			};

			methodBox.Items.AddRange(_plugins.ToArray());
			methodBox.SelectedIndex = 0;
		}

		private void evaluateBtn_Click (object sender, EventArgs e)
		{
			var plugin = methodBox.SelectedItem as IMESPlugin;
			if ( plugin == null || _inputData == null )
				return;

			_resultData = plugin.Apply(_inputData);
			_plotData(outputPlot, _resultData);
			tabControl1.SelectedIndex = 1;
		}
	}
}
