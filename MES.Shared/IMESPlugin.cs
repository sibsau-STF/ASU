using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared
{
    public interface IMESPlugin
    {
        string Name { get; }
        ProductTask[] Apply (ProductTask[] tasks);

    }
}
