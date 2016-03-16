using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web;
namespace XMVC
{
    public class Controller:ControllerBase
    {
        public RequestContext RequestContext
        {
            get;
            set;
        }
        public IActionInvoker ActionInvoker
        {
            get;
            set;
        }
      
        public Controller()
        {
            ActionInvoker = new DefaultActionInvoker(base.ControllerContext);
        }
        
        public override void ExecuteCore(ControllerContext context)
        {
            string actionname = context.RequestContext.RouteData.GetRequiredString("Action");
            ActionInvoker.InvokeAction(actionname,context);
        }
    }
}
