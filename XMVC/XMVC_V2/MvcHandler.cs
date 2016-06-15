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

        //整个思路其实是
        //定位controller类型，获取controller实例，定位并获取具体的MethodInfo，获取methodinfo所需参数，执行method并获取结果，处理结果（输出或者其他方式）
        //那这里面其实会有很多可以定制化的，或者叫默认扩展化，包括日志记录，包括统计，包括某些固定处理，集成autofac等。
        //做技术，不能太有功利心。也不能嫌苦，因为这点苦和社会上真正的苦比起来，简直不需要提。
        public void ProcessRequest(HttpContext context)
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = context,RequestContext=this.RequestContext};
            IControllerFactory controllerFactory;
            IController controller;
            ProcessInit(out controllerFactory,out controller,controllerContext);
            //下面这个写法相当有争议。。参数传递到底应该遵循什么样才好呢。
            (controller as Controller).ControllerContext = controllerContext;
            try
            {
                controller.Execute();
            }
            finally
            {
                controllerFactory.ReleaseController(controller);
            }           
        }
        public void ProcessInit(out IControllerFactory controllerFactory,out IController controller,ControllerContext controllerContext)
        {
            //Instanciate controllerFactory
            string controllerName = controllerContext.RequestContext.RouteData.GetRequiredString("Controller");
            controllerFactory = new DefaultControllerFactory(); //又他么写死了。
            controller = controllerFactory.CreateController(controllerName);
            controllerContext.Controller = controller as Controller;
            //关于这边的写法，我认为controllerContext不应该属于defaultcontrollerfactory，而应该只是作为外界传进去的一个参数，所以不应该把controllerContext作为工厂的一个属性或者变量存在。
        }

    }
}
