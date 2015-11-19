using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Reflection;
namespace XMVC
{
    public class DefaultControllerFactory:IControllerFactory
    {
        public IController CreateControllerInstance(RequestContext context)
        {
            string controllername = context.RouteData.GetRequiredString("Controller");
            if (TypeCacheUtil.Cache.ContainsKey(controllername + "Controller"))
            {
                Dictionary<string, Type> dic = TypeCacheUtil.Cache[controllername + "Controller"];
                KeyValuePair<string, Type> pair = dic.ElementAt(0);   //暂时认为是第一个,不考虑重名的
                Type type = pair.Value;
                Assembly assembly = type.Assembly;
                var instance = assembly.CreateInstance(type.ToString());
                return instance as IController;
            }
            else
            {
                return null;
            }
        }

        public IController CreateControllerInstance(Type t)
        {
            throw new NotImplementedException();
        }
    }
}
