using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
//using XMVC;
using XMVC_V2;
using System.Reflection;
namespace XMVCTest
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerTypeCache.FillInTheCache(new BuildManagerWrapper());
            //TypeCacheUtil.FillInTheCache();   //这边我们手动的让项目在启动的时候填充整个关于controller的_cache.实际不可能这么做。这应该是你框架要自动实现的东西，而不是我的应用程序。

        }
    }
}