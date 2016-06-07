using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC
{
    public class FilterInfo
    {
        IList<IActionFilter> ActionFilters { get; set; }
        IList<IResultFilter> ResultFilters { get; set; }
    }
}
