using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
namespace XMVC
{
    public abstract class ControllerBase:IController
    {

        public void Execute(ControllerContext context)
        {
            // 做一些验证啊 什么的预先
            ExecuteCore(context);
        }

        public abstract void ExecuteCore(ControllerContext context);


        public ControllerContext ControllerContext
        {
            get;
            set;
        }
    }
}
