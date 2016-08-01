using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace XMVC_V2
{
    public class JsonResult : ActionResult
    {
        public Encoding ContentEncoding
        {
            get;
            set;
        }
        public string ContentType
        {
            get;
            set;
        }
        public JsonRequestBehavior JsonRequestBehavior
        {
            get;
            set;
        }


        public override void ExecuteResult(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior==JsonRequestBehavior.DenyGet&&
                string.Equals(controllerContext.HttpContext.Request.HttpMethod,"GET"))
            {
                throw new InvalidOperationException("JsonRequest");
            }
            HttpResponse response = controllerContext.HttpContext.Response;
            if (!string.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding!=null)
            {
                response.ContentEncoding = ContentEncoding;
            }             
        }
    }
}
