using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC_V2
{
    public class DefaultControllerFactory : IControllerFactory
    {
        public IController CreateController(string controllerName)
        {
            Type controllerType = GetControllerType(controllerName);
            if (controllerType != null)
            {
                return Activator.CreateInstance(controllerType) as IController;
            }
            else
            {
                return null;
            }
            
        }

        public Type GetControllerType(string controllerName)
        {
            Dictionary<string, Dictionary<string, Type>> dict = ControllerTypeCache.Cache;
            if (dict.ContainsKey(controllerName+"Controller"))
            {
                return dict[controllerName+"Controller"].First().Value as Type;
            }
            else
            {
                return null;
            }
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable!=null)
            {
                disposable.Dispose();
            }
        }
    }
}
