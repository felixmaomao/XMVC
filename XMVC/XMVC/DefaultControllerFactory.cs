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
        public IController CreateControllerInstance(ControllerContext context)
        {
            string controllername = context.RequestContext.RouteData.GetRequiredString("Controller");
            if (TypeCacheUtil.Cache.ContainsKey(controllername + "Controller"))
            {
                Dictionary<string, Type> dic = TypeCacheUtil.Cache[controllername + "Controller"];
                KeyValuePair<string, Type> pair = dic.ElementAt(0);   //暂时认为是第一个,不考虑重名的
                Type type = pair.Value;
                Assembly assembly = type.Assembly;
                var instance = assembly.CreateInstance(type.ToString());               
                IController controllerInstance = instance as IController;
                controllerInstance.ControllerContext = context;   //要给每一个icontroller附上他该有的 controllerContext、 this is important.
                return controllerInstance;                
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
