using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
namespace XMVC
{
    public interface IControllerFactory
    {
        IController CreateControllerInstance(ControllerContext context);
        IController CreateControllerInstance(Type t);
    }
}
