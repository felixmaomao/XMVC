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
            actionResult.ExecuteResult(controllerContext);
        }
        public ActionResult InvokeActionMethod(ControllerContext controllerContext)
        {
            //use reflection to get action result.
            //这边涉及到model bingding?
            //要给具体方法的参数赋值，这些值都是从request中获得。
            ActionResult result;
            object returnValue;
            GetActionReturnValue(controllerContext, out result, out returnValue);
            result = (returnValue as ActionResult) ?? new ContentResult { Content = returnValue.ToString() };
            return result;
        }

        //下面这个是用vs重构的
        public void GetActionReturnValue(ControllerContext controllerContext, out ActionResult result, out object returnValue)
        {
            result = null;
            string actionName = controllerContext.RequestContext.RouteData.GetRequiredString("Action");
            Controller controllerInstance = controllerContext.Controller;
            MethodInfo methodInfo = controllerInstance.GetType().GetMethod(actionName);
            ParameterInfo[] parameters = methodInfo.GetParameters();
            List<object> parameterValues = new List<object>();
            foreach (ParameterInfo parameterInfo in parameters)
            {
                string parameterName = parameterInfo.Name;
                object parameterValueFromRequest = controllerContext.HttpContext.Request.QueryString[parameterName];
                parameterValues.Add(parameterValueFromRequest);
            }
            returnValue = methodInfo.Invoke(controllerInstance, parameterValues.ToArray());
        }
    }
}
