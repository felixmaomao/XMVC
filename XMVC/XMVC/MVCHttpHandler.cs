using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Reflection;
using System.Web.Compilation;
using System.Collections;
namespace XMVC
{
    public class MVCHttpHandler:IHttpHandler
    {
        public MVCHttpHandler(RequestContext ctx)
        {
            RequestContext = ctx;
        }
        public RequestContext RequestContext
        {
            get;
            set;
        }
        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }


        //承接处理请求  (这个context 和上面的属性requestcontext什么关系？)
        public void ProcessRequest(HttpContext context)
        {
            #region  最弱爆的可以直接这么写死
            //List<string> strs = new List<string> { "shenwei","zhangxiaomao","vczh"};
            //context.Response.Write("<hr>");            
            //context.Response.Write("<h3>竟然可以直接这么搞</h3>");
            //foreach(string s in strs)
            //{
            //    context.Response.Write("<div>");
            //    context.Response.Write(s);
            //    context.Response.Write("</div>");
            //}
            #endregion
            //是可以直接通过 HttpContext.Response.Write() 向浏览器发送字符字符的(静态网页) 当然也可以通过读取html文件来输出
            //对请求进行分流

            string controllername = RequestContext.RouteData.GetRequiredString("Controller");
            string actionname = RequestContext.RouteData.GetRequiredString("Action");
            #region 其实下面的这段就简单的解释了总体原理 愚蠢的根据 controllername 进行分流
            //switch (controllername)
            //{
            //    case "sayhi":
            //        context.Response.Write("entered the sayhi controller");
            //        if(actionname=="papapa")
            //        {
            //            context.Response.Write("<br>");
            //            context.Response.Write("enterd the papapa action");
            //        }
            //        if(actionname=="read")
            //        {
            //            context.Response.Write("<br>");
            //            context.Response.Write("enter the read action");
            //        }

            //        break;
            //    case "sayhello":
            //        context.Response.Write("hello world");
            //         if(actionname=="sleep")
            //        {
            //            context.Response.Write("<br>");
            //            context.Response.Write("enterd the sleep action");
            //        }
            //        if(actionname=="fit")
            //        {
            //            context.Response.Write("<br>");
            //            context.Response.Write("enter the fit action");
            //        }
            //        break;
            //}
            #endregion
            //于是开发 mvc应用程序其实就是 开发响应不同请求的诸多动态页面
            //框架继续向前发展， 设计思路就是 根据 请求的controllername找到同名的具体的 controller去进行处理，actionname则去对应其中的方法 
            //这样子 一下子就分流开了 需求就变成了 根据 controllername去找对应的对象，进而去执行对象中的相应方法
            //很明显实现技术是反射  可是该如何去搜寻正确的dll，找到我们要的类呢，以及每次都要找会很浪费时间 怎么办呢？
            #region 最初写死的实现
            //Assembly assembly = Assembly.LoadFile(@"H:\XMVC\XMVC\XMVCTest\bin\XMVCTest.dll");
            //Type match = assembly.GetType("XMVCTest."+controllername+"Controller");
            //var instance = assembly.CreateInstance(match.ToString());
            //MethodInfo method = match.GetMethod(actionname);
            //method.Invoke(instance, new object[] {context});   //context是程序向外界输出的嘴巴，不能忘了把嘴巴传下去
            #endregion

            #region 部分死的写法
            //            ICollection allReferencedAssemblies = BuildManager.GetReferencedAssemblies();
            //            bool findmatch = false;
            //            foreach(Assembly item in allReferencedAssemblies)
            //            {
            //                Type match = item.GetType("XMVCTest."+controllername+"Controller");  //这边还是写死的
            //                if (match != null)
            //                {
            //                    var instance = item.CreateInstance(match.ToString());
            //                    MethodInfo method = match.GetMethod(actionname);
            //                    method.Invoke(instance, new object[] { context });
            //                    findmatch = true;                   
            //                }                
            //            }
            //            if(!findmatch)
            //            {
            //                string script = @"<script type='text/javascript'>
            //                               alert('嘿嘿，没找到您要的界面');
            //                              </script>";
            //                context.Response.Write(script);
            //            }
            #endregion

            #region 很不舒服的达到目的,但是这样写太愚蠢了，而且难道每次执行一次请求难道都整个dll全部遍历寻找一遍？            

            //ICollection allReferencedAssemblies = BuildManager.GetReferencedAssemblies();
            //List<Type> matched_types = new List<Type>();
            //foreach (Assembly assembly in allReferencedAssemblies)
            //{
            //    Type[] types = assembly.GetTypes();
            //    foreach (Type type in types)
            //    {
            //        if (type.Name == controllername + "Controller")
            //        {                        
            //            matched_types.Add(type);
            //        }
            //    }
            //}
            //if (matched_types.Count != 0)
            //{
            //    //暂且认为第一个是我们要找的,即不存在同名的controller               
            //    Type type = matched_types[0];
            //    var instance =type.Assembly.CreateInstance(type.ToString());
            //    MethodInfo method = type.GetMethod(actionname);
            //    if (method != null)
            //    {
            //        method.Invoke(instance, new object[] { context });
            //    }
            //    else
            //    {
            //        context.Response.Write("O(∩_∩)O 没有找到合适的页面");
            //    }              
            //}
            //else
            //{
            //    context.Response.Write("O(∩_∩)O 没有找到合适的页面");
            //}
            #endregion

            //显然更好的做法 应该是项目启动的时候 ，就将所有的controller以及相关信息 全部查询好 然后放在一个像字典一样的地方 存储起来（或者说缓存起来），下次要用直接到这边来取就ok,这样子才能够更快速
            //显然这个字典的结构会很复杂，像新华字典一样  而且我们需要一个专门的类用来负责项目启动的时候 将这个字典填满（不过这将导致 第一次启动特别慢？如何优化？并行？）
            //看样子 下面的这种写法有点接近成功了，已成功的将请求分流至各controller和action
            //接下来 就是丰富 IController家族自身的功能，以及使用 IControllerFactory来创建实例了
            #region 算是一大步迈进
            //if (TypeCacheUtil.Cache.ContainsKey(controllername + "Controller"))
            //{
            //    Dictionary<string,Type> dic=TypeCacheUtil.Cache[controllername+"Controller"];
            //    KeyValuePair<string, Type> pair = dic.ElementAt(0);   //暂时认为是第一个,不考虑重名的
            //    Type type = pair.Value;
            //    Assembly assembly = type.Assembly;
            //    var instance = assembly.CreateInstance(type.ToString());
            //    MethodInfo method = type.GetMethod(actionname);
            //    method.Invoke(instance,new object[]{context});
            //}
            //else
            //{
            //    context.Response.Write("嘿嘿 ，没有找到合适的处理请求！");
            //}
            #endregion

            //接下来 应用工厂模式，以及将功能实现转移到controller内部
            #region 迟钝的写法的写法
            //IController controller;
            //IControllerFactory factory;
            //factory = new DefaultControllerFactory();    //这边又写死了 指定了默认的工厂。（隐约感觉到写框架的艺术在哪里，难点在哪里）
            //ControllerContext controllercontext = new ControllerContext { RequestContext = RequestContext, HttpContext = context };
            //controller = factory.CreateControllerInstance(controllercontext);            
            //controller.Execute(controllercontext);
            #endregion

            #region 行云流水的写法
            IControllerFactory factory;
            IController controller;
            ControllerContext controllercontext = new ControllerContext {RequestContext=this.RequestContext,HttpContext=context };
            ProcessRequestInit(out factory,out controller,controllercontext);
            controller.Execute(controllercontext);
            #endregion
        }
        public void ProcessRequestInit(out IControllerFactory fac,out IController controller,ControllerContext context)
        {           
            fac = new DefaultControllerFactory();  //很抱歉 这边还是写死了。这边是期望暴露给外界去配置的。
            controller = fac.CreateControllerInstance(context);
        }


    }
}
