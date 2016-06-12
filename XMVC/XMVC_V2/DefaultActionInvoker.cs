using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace XMVC_V2
{
    public class DefaultActionInvoker : IActionInvoker
    {
        public void InvokeAction(ControllerContext controllerContext)
        {
            ActionResult actionResult = InvokeActionMethod(controllerContext);
            actionResult.ExecuteResult();
        }
        public ActionResult InvokeActionMethod(ControllerContext controllerContext)
        {
            //use reflection to get action result.
            ActionResult result = null;
            return result;
        }
    }
}
