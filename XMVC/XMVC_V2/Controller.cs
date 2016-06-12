using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC_V2
{
    public class Controller : IController
    {
        private IActionInvoker _actionInvoker;
        public IActionInvoker ActionInvoker
        {
            get {
                _actionInvoker = CreateActionInvoker();
                return _actionInvoker;
            }
        }
        public IActionInvoker CreateActionInvoker()
        {
            return new DefaultActionInvoker();
        }

        public ControllerContext ControllerContext
        {
            get;set;
        }
        public void Execute()
        {
            ActionInvoker.InvokeAction(this.ControllerContext);
        }
    }
}
