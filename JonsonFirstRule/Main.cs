using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MES;
using MES.Shared;

namespace JonsonFirstRule
{
	public class JonsonFirst : IMESPlugin
	{
		public string Name => "Jonson first rule";

		public ProductTask[] Apply (ProductTask[] tasks)
		{
			throw new NotImplementedException();
		}
	}
}
