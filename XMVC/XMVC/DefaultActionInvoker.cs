using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using System.Web.Routing;
namespace XMVC
{
    public class DefaultActionInvoker : IActionInvoker
    {
        #region 照抄的写法
        //public ControllerContext Context
        //{
        //    get;
        //    set;
        //}

        //public DefaultActionInvoker(ControllerContext context)
        //{
        //    this.Context = context;
        //}

        //public bool InvokeAction(string actionname)
        //{
        //    #region 错误的写法
        //    //if(this.Context==null)
        //    //{
        //    //    throw new ArgumentNullException("controllerContext");
        //    //}
        //    //if (string.IsNullOrEmpty(actionname))
        //    //{
        //    //    throw new ArgumentException("actionName");
        //    //}

        //    //bool flag = true;
        //    //string controllername =Context.RequestContext.RouteData.GetRequiredString("Controller");
        //    //if (TypeCacheUtil.Cache.ContainsKey(controllername + "Controller"))
        //    //{
        //    //    Dictionary<string, Type> dic = TypeCacheUtil.Cache[controllername + "Controller"];
        //    //    KeyValuePair<string, Type> pair = dic.ElementAt(0);   //暂时认为是第一个,不考虑重名的
        //    //    Type type = pair.Value;
        //    //    Assembly assembly = type.Assembly;
        //    //    var instance = assembly.CreateInstance(type.ToString());
        //    //    MethodInfo method = type.GetMethod(actionname);
        //    //    if (method == null)
        //    //    {
        //    //        Context.HttpContext.Response.Write("嘿嘿 ，没有找到合适的处理请求！");
        //    //        flag=false;
        //    //    }
        //    //    //这边不可能这么写死的。而且也不会以httpcontext做参数
        //    //    method.Invoke(instance, new object[] { Context.HttpContext });
        //    //}
        //    //else
        //    //{
        //    //    Context.HttpContext.Response.Write("嘿嘿 ，没有找到合适的处理请求！");
        //    //    flag = false;
        //    //}
        //    //return flag;
        //    #endregion
        //    bool flag = true;
        //    ControllerDescriptor controllerDescriptor = GetControllerDescriptor(this.Context);
        //    ActionDescriptor actionDescriptor = FindAction(this.Context,controllerDescriptor,actionname);
        //    //找到了具体的action
        //    if (actionDescriptor!=null)
        //    {
        //        //getfilters
        //        FilterInfo filterInfo = actionDescriptor.GetFilters();
        //        //getparams
        //        IDictionary<string, object> parameters = GetParameterValues(this.Context,actionDescriptor);
        //        //execute to get actionResult
        //        ActionResult actionResult = InvokeActionMethod(this.Context,actionDescriptor,parameters);
        //        //execute result
        //        InvokeActionResult(this.Context,actionResult);
        //    }
        //    return flag;
        //}

        //public ActionResult InvokeActionMethod(ControllerContext context,ActionDescriptor actionDescriptor,IDictionary<string,object> parameters)
        //{
        //    object returnValue = actionDescriptor.Execute(context,parameters);
        //    ActionResult result = CreateActionResult(context,actionDescriptor,returnValue);
        //    return result;
        //}

        //public ActionResult CreateActionResult(ControllerContext context,ActionDescriptor actionDescriptor,object actionReturnValue)
        //{
        //    if (actionReturnValue==null)
        //    {
        //        return new EmptyResult();
        //    }
        //    ActionResult result = (actionReturnValue as ActionResult) ?? new ContentResult { Content=actionReturnValue.ToString()};
        //    return result;
        //}

        ////不同的actionresult的执行也是不同的
        //public void InvokeActionResult(ControllerContext context,ActionResult result)
        //{
        //    result.ExecuteResult(context);
        //}

        //public virtual object GetParameterValue(ControllerContext context,ParameterDescriptor parameterDescriptor)
        //{
        //    return null;
        //}

        //public IDictionary<string, object> GetParameterValues(ControllerContext context,ActionDescriptor actionDescriptor)
        //{
        //    Dictionary<string, object> parameterDict = new Dictionary<string, object>();
        //    ParameterDescriptor[] parameterDescriptors = actionDescriptor.GetParameters();
        //    foreach (ParameterDescriptor parameterDescriptor in parameterDescriptors)
        //    {
        //        parameterDict[parameterDescriptor.ParameterName] = GetParameterValue(context,parameterDescriptor);
        //    }
        //    return parameterDict;
        //}

        //public ControllerDescriptor GetControllerDescriptor(ControllerContext context)
        //{
        //    Type controllerType = context.Controller.GetType();
        //    ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerType);
        //    return controllerDescriptor;
        //}

        //public ActionDescriptor FindAction(ControllerContext context,ControllerDescriptor controllerDescriptor,string actionName)
        //{
        //    ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(context,actionName);
        //    return actionDescriptor;
        //}
        #endregion

        #region 自己的实现


        public DefaultActionInvoker(ControllerContext context)
        {
            this.Context = context;
        }
        public ControllerContext Context
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool InvokeAction(string actionname)
        {
            //get controller descriptor
            ControllerDescriptor controllerDescriptor = GetControllerDescriptor(this.Context);
            //get action descriptor
            ActionDescriptor actionDescriptor = GetActionDescripter(this.Context,controllerDescriptor,actionname);
            //invoke the action method and get actionresult
            ActionResult actionResult = InvokeActionMethod(actionDescriptor);
            //invoke actionresult
            actionResult.ExecuteResult(this.Context);
            return true;   
        }

        public ControllerDescriptor GetControllerDescriptor(ControllerContext context)
        {
            if (context==null)
            {
                throw new ArgumentNullException("controllercontext");
            }
            Type controllerType = context.Controller.GetType();
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerType);
            return controllerDescriptor;
        }

        public ActionDescriptor GetActionDescripter(ControllerContext context,ControllerDescriptor controllerDescriptor,string actionName)
        {
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(context,actionName);
            return actionDescriptor;
        }

        public ActionResult InvokeActionMethod(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == null)
            {
                throw new ArgumentNullException("actionDescriptor");
            }
            IDictionary<string, object> parameters = GetParameters(actionDescriptor);
            object returnObj = actionDescriptor.Execute(this.Context,parameters);
            ActionResult result = (returnObj as ActionResult) ?? new ContentResult { Content=returnObj.ToString()};
            return result;
        }

        public IDictionary<string, object> GetParameters(ActionDescriptor actionDescriptor)
        {
            IDictionary<string, object> parameterDict = new Dictionary<string,object>();
            ParameterDescriptor[] parameters = actionDescriptor.GetParameters();
            foreach (ParameterDescriptor parameterDescriptor in parameters)
            {
                parameterDict[parameterDescriptor.ParameterName] = GetParameter(this.Context,parameterDescriptor);
            }
            return parameterDict;
        }

        public object GetParameter(ControllerContext controllerContext,ParameterDescriptor parameterDescriptor)
        {
            //coding should be like writing
            Type parameterType = parameterDescriptor.GetType();


        }

        #endregion
    }
}
