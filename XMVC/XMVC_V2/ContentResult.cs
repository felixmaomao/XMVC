using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace XMVC_V2
{
    public class ContentResult:ActionResult
    {
        public string Content
        {
            get;
            set;
        }
        public override void ExecuteResult(ControllerContext controllerContext)
        {
            HttpResponse response = controllerContext.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.Content))                
            {
                response.Write(this.Content);
            }
        }
    }
}
