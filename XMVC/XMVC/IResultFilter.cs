using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC
{
    public interface IResultFilter
    {
        void OnResultExecuting();
        void OnResultExecuted();
    }
}
