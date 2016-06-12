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
            ProcessInit(out controllerFactory,out controller);
            controller.Execute();
        }
        public void ProcessInit(out IControllerFactory controllerFactory,out IController controller)
        {
            //Instanciate controllerFactory
            controllerFactory = null;
            controller = null;
        }

    }
}
