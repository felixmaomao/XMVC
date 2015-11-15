using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
namespace XMVC
{
    public class MVCRouteHandler:IRouteHandler
    {

        //通过这个类 成功的和基础的路由框架对接，将请求成功的 迎接进来，请求信息在 requestcontext中
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            //进一步移交 mvcHttpHandler处理
            return new MVCHttpHandler(requestContext);
        }
    }
}
