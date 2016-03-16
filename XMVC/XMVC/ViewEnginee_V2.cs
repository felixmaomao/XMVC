using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Reflection;
namespace XMVC
{
    public static class ViewEnginee_V2
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

        public static void ProcessView(object obj,string filename,HttpContext context)
        {
            string htmlstr = LoadFile(filename);
            //处理 html串 ，拼接上我们的实体属性。 我们打算以#作为标示好了。。毕竟只剩下这个了。。。
            //也就是  #Model.Name 就是 在这个位置填充 我们的Name 属性。
            //将实例的属性以及值装入字典，用来匹配。
            if(obj!=null)
            {
                Dictionary<string, string> obj_property_values = new Dictionary<string, string>();
                Type modelType = obj.GetType();
                PropertyInfo[] propertyNames = modelType.GetProperties();
                foreach (PropertyInfo p in propertyNames)
                {
                    obj_property_values.Add(p.Name, p.GetValue(obj).ToString());
                }
                foreach (var item in obj_property_values)
                {
                    context.Response.Write(item.Key + "  :  " + item.Value);
                }
            }        
            context.Response.Write(htmlstr);
        }

        public static void RenderView(object obj, string filename, HttpContext context)
        {
            ProcessView(obj,filename,context);
        }
    }
}
