using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using XMVC;
namespace XMVCTest.Controllers
{
    public class SayHiController : Controller
    {
        public void Greet(HttpContext context)
        {
            ViewEnginee.RenderView(null,context.Server.MapPath("~/Views/xxxx.Jhtml"),context);
        }
    }
}
