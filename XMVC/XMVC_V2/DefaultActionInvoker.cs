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
            //这边涉及到model bingding?
            ActionResult result = null;
            string actionName = controllerContext.RequestContext.RouteData.GetRequiredString("Action");
            return result;
        }
    }
}
