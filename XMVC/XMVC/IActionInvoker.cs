using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web;
namespace XMVC
{
    public interface IActionInvoker
    {
        //every iactioninvoker should own it's controllercontext.
        ControllerContext Context { get; set; } 

        bool InvokeAction(string actionname);

    }
}