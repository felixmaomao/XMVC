using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
//using XMVC;
using XMVC_V2;
using System.Text;
namespace XMVCTest.Controllers
{
    public class SayHiController : Controller
    {
        public string Greet()
        {
            return "hello shenwei focus on what you do .Think less about how much it earns!";
            //ViewEnginee.RenderView(null,context.Server.MapPath("~/Views/xxxx.Jhtml"),context);
        }
        public string GreetProductive()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<input type='button' value='Hello'/>");
            return builder.ToString();
        }
    }
}
