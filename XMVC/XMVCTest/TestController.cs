using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XMVC;
using XMVCTest.Models;
namespace XMVCTest
{
    public class TestController
    {

        //我们这里的做法还停留在直接暴露httpcontext进行输出。我们真是的是需要一个html模板，以及相应的引擎来整合我们的模板。
        //那最简单的引擎其实就是单纯的读取文件内容直接输出，而不存在说进行动态整合这样子。
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
            //这边的缺点就是 目前需要手动指定 所对应的文件。但这也不是什么大问题。
            Employee employee = new Employee {Name="Jasonshenw",Location="suzhou",PhoneNum="888888" };
            ViewEnginee_V2.RenderView(employee,context.Server.MapPath("../Views/xxxx.Jhtml"),context);
        }
                
    }
}