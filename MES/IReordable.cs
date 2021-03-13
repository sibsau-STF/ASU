using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
	interface IReordable
	{
		ProductTask[] Reorder (ProductTask[] tasks);
	}
}
