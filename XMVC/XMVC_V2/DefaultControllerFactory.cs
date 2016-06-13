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
            return Activator.CreateInstance(controllerType) as IController;
        }   
         
        public Type GetControllerType(string controllerName)
        {

        }
    }
}
