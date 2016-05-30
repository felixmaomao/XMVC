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
        public ControllerContext Context
        {
            get;
            set;
        }

        public DefaultActionInvoker(ControllerContext context)
        {
            this.Context = context;
        }
                   
        public void InvokeAction(string actionname)
        {
            string controllername =Context.RequestContext.RouteData.GetRequiredString("Controller");
            if (TypeCacheUtil.Cache.ContainsKey(controllername + "Controller"))
            {
                Dictionary<string, Type> dic = TypeCacheUtil.Cache[controllername + "Controller"];
                KeyValuePair<string, Type> pair = dic.ElementAt(0);   //暂时认为是第一个,不考虑重名的
                Type type = pair.Value;
                Assembly assembly = type.Assembly;
                var instance = assembly.CreateInstance(type.ToString());
                MethodInfo method = type.GetMethod(actionname);
                if (method == null)
                {
                    Context.HttpContext.Response.Write("嘿嘿 ，没有找到合适的处理请求！");
                    return;
                }
                method.Invoke(instance, new object[] { Context.HttpContext });
            }
            else
            {
                Context.HttpContext.Response.Write("嘿嘿 ，没有找到合适的处理请求！");
            }
        }
    }
}
