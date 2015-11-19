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
    public class DefaultActionInvoker:IActionInvoker
    {
        public ControllerContext ControllerContext
        {
            get;
            set;
        }

        public DefaultActionInvoker(ControllerContext context)
        {
            ControllerContext = context;
        }
        //一步一步 挪到actionInvoker里面来处理请求 感觉 有点画蛇添足  因为一开始就能直接通过反射定位， 何必要拐弯子转到这边来处理呢？
        //这里面肯定有玄机
        public void InvokeAction(string actionname,ControllerContext controllercontext)
        {
            string controllername =controllercontext.RequestContext.RouteData.GetRequiredString("Controller");
            if (TypeCacheUtil.Cache.ContainsKey(controllername + "Controller"))
            {
                Dictionary<string, Type> dic = TypeCacheUtil.Cache[controllername + "Controller"];
                KeyValuePair<string, Type> pair = dic.ElementAt(0);   //暂时认为是第一个,不考虑重名的
                Type type = pair.Value;
                Assembly assembly = type.Assembly;
                var instance = assembly.CreateInstance(type.ToString());
                MethodInfo method = type.GetMethod(actionname);
                method.Invoke(instance, new object[] { controllercontext.HttpContext });
            }
            else
            {
                controllercontext.HttpContext.Response.Write("嘿嘿 ，没有找到合适的处理请求！");
            }
        }
    }
}
