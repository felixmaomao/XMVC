using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
namespace XMVC
{
    public class ControllerContext
    {
        public RequestContext RequestContext
        {
            get;
            set;
        }
        public HttpContext HttpContext
        {
            get;
            set;
        }
        public ControllerBase Controller
        {
            get;
            set;
        }
    }
}
