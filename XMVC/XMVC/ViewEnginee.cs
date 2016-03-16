using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace XMVC
{
    //这是我们的初代视图引擎，用于读取静态文件，并输出。
    public static class ViewEnginee
    {
        public static string LoadFile(string filename)
        {
            using (FileStream fsRead = new FileStream(filename, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                return myStr;
            }            
        }

        public static void RenderView(object obj,string filename,HttpContext context)
        {
            string htmlstr = LoadFile(filename);
            context.Response.Write(htmlstr);
        }
    }
}
