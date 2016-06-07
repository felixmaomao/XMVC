using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace XMVC
{

    public class ContentResult : ActionResult
    {
        public string Content
        {
            get;
            set;
        }
        
        public string ContentType
        {
            get;
            set;
        }
        public Encoding ContentEncoding
        {
            get;
            set;
        }
        
        public override void ExecuteResult(ControllerContext context)
        {
            if (context==null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponse response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            if (ContentEncoding!=null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Content!=null)
            {
                response.Write(Content);
            }

        }
    }
}
