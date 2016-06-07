using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC
{
    public interface IActionFilter
    {
        void OnActionExcuted();
        void OnActionExcuting();
    }
}
