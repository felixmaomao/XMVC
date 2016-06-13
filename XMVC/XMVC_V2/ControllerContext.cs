using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
namespace XMVC_V2
{
    public class ControllerContext
    {
        public HttpContext HttpContext
        {
            get;set;
        }
        public RequestContext RequestContext
        {
            get;set;
        }
        public RouteData RouteData
        {
            get;set;
        }

    }
}
