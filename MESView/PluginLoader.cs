using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MES.Shared;

namespace MES.View
{
	public class PluginLoader
	{
		public static List<IMESPlugin> Plugins { get; set; }

		public void LoadPlugins()
		{
			Plugins = new List<IMESPlugin>();

			//Load the DLLs from the Plugins directory
			if (Directory.Exists("Plugins"))
			{
				string[] files = Directory.GetFiles("Plugins");
				foreach (string file in files)
				{
					if (file.EndsWith(".dll"))
					{
						Assembly.LoadFile(Path.GetFullPath(file));
					}
				}
			}

			Type interfaceType = typeof(IMESPlugin);
			//Fetch all types that implement the interface IPlugin and are a class
			Type[] types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
				.ToArray();
			foreach (Type type in types)
			{
				//Create a new instance of all found types
				Plugins.Add((IMESPlugin)Activator.CreateInstance(type));
			}
		}
	}
}

