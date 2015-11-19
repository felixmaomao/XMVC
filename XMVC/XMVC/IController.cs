using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
namespace XMVC
{
     public interface IController
     {
         ControllerContext ControllerContext
         {
             get;
             set;
         }
         void Execute(ControllerContext context);
     }
}
