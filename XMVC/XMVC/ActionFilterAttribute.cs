using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC
{
    
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,Inherited =true)]
    public abstract class ActionFilterAttribute : Attribute, IActionFilter, IResultFilter
    {
        public virtual void OnActionExcuted() { }

        public virtual void OnActionExcuting() { }

        public virtual void OnResultExecuted() { }

        public virtual void OnResultExecuting() { }       
    }
}
