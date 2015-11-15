using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XMVCTest
{
    public class HelloController
    {
        public void sayhi(HttpContext context)
        {
            context.Response.Write("hahahahahaha");
            context.Response.Write("<img src='1.jpg'></img>");
        }
        
    }
}