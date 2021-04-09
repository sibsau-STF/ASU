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

		static FileInfo[] _pluginDirs;

		public void LoadPlugins ()
		{
			Plugins = new List<IMESPlugin>();
			if ( !Directory.Exists("Plugins") )
				return;

			//Load the DLLs from the Plugins directory
			var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
			AppDomain.CurrentDomain.AssemblyResolve += _resolveAssembly;


			_pluginDirs = new DirectoryInfo("Plugins").GetFiles("*.dll");
			foreach ( var file in _pluginDirs )
				Assembly.LoadFile(file.FullName);

			Type interfaceType = typeof(IMESPlugin);
			//Fetch all types that implement the interface IPlugin and are a class
			Type[] types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
				.ToArray();

			//Create a new instance of all found types
			foreach ( Type type in types )
				Plugins.Add((IMESPlugin)Activator.CreateInstance(type));
		}

		private Assembly _resolveAssembly (object sender, ResolveEventArgs args)
		{
			var dllName = args.Name.Split(',')[0];
			var dll = _pluginDirs.FirstOrDefault(fi => fi.Name.Contains(dllName));
			if ( dll == null )
			{
				return null;
			}

			return Assembly.LoadFrom(dll.FullName);
		}
	}
}

