using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XMVC;
namespace XMVCTest
{
    public class TestController
    {
        public void sayhi(HttpContext context)
        {
            context.Response.Write("xxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            context.Response.Write("httpcontext 就是我们的嘴巴");
        }
        //这种 直接通过 context.response.write来向浏览器输出原始字符串包括html的方式，并不能大量用来开发。
        //更好的方式还是页面和逻辑分离，如下

        public void sayhello(HttpContext context)
        {
            //通过视图引擎去读取 合适的前台文件 然后输出。
            string HtmlToRender = ViewEnginee.LoadFile(context.Server.MapPath("../HtmlPage1.html"));
            context.Response.Write(HtmlToRender);
        }

        //更近一步的写法
        public void saybye(HttpContext context)
        {
            ViewEnginee.View(null,context.Server.MapPath("../Views/xxxx.Jhtml"),context);
        }
                
    }
}