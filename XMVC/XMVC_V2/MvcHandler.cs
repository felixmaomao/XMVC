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
        public RequestContext RequestContext
        {
            get;set;
        }
        public MvcHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = context,RequestContext=this.RequestContext};
            IControllerFactory controllerFactory;
            IController controller;
            ProcessInit(out controllerFactory,out controller,controllerContext);
            //下面这个写法相当有争议。。参数传递到底应该遵循什么样才好呢。
            (controller as Controller).ControllerContext = controllerContext;
            controller.Execute();
        }
        public void ProcessInit(out IControllerFactory controllerFactory,out IController controller,ControllerContext controllerContext)
        {
            //Instanciate controllerFactory
            string controllerName = controllerContext.RequestContext.RouteData.GetRequiredString("Controller");
            controllerFactory = new DefaultControllerFactory();
            controller = controllerFactory.CreateController(controllerName);
            controllerContext.Controller = controller as Controller;
            //关于这边的写法，我认为controllerContext不应该属于defaultcontrollerfactory，而应该只是作为外界传进去的一个参数，所以不应该把controllerContext作为工厂的一个属性或者变量存在。
        }

    }
}
