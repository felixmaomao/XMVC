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
        private IActionInvoker _actionInvoker;  
        public IActionInvoker ActionInvoker
        {
            get
            {
                if (_actionInvoker==null)
                {
                    _actionInvoker = CreateActionInvoker();  
                }
                return _actionInvoker;
            }
            set
            {
                _actionInvoker = value;
            }
        }

        public IActionInvoker CreateActionInvoker()
        {
            return new DefaultActionInvoker(base.ControllerContext); 
        }

        public Controller()
        {
            //ActionInvoker = new DefaultActionInvoker(base.ControllerContext);  //不要这种在构造函数里调用父类的的某个属性赋值。因为父类的这个属性并没有在构造的时候赋值，而是之后手动赋值的。所以这里就会为空。
        }
     
        //为什么觉得这样写有点“千疮百孔”的感觉   
        public override void ExecuteCore(ControllerContext context)
        {
            string actionname = context.RequestContext.RouteData.GetRequiredString("Action");
            if (!ActionInvoker.InvokeAction(actionname))
            {
                HandleUnKnownAction(actionname);
            }
        }

        public void HandleUnKnownAction(string actionname)
        {
            
        }
    }
}
