using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
namespace XMVC_V2
{
    public class MvcHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = context};
            IControllerFactory controllerFactory;
            IController controller;
            ProcessInit(out controllerFactory,out controller,controllerContext);
            controller.Execute();
        }
        public void ProcessInit(out IControllerFactory controllerFactory,out IController controller,ControllerContext controllerContext)
        {
            //Instanciate controllerFactory
            controllerFactory = new DefaultControllerFactory();
            controller = controllerFactory.CreateController(controllerContext);
            //关于这边的写法，我认为controllerContext不应该属于defaultcontrollerfactory，而应该只是作为外界传进去的一个参数，所以不应该把controllerContext作为工厂的一个属性或者变量存在。
        }

    }
}
