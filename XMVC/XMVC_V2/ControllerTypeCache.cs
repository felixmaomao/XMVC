using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using System.Web.Compilation;
namespace XMVC_V2
{   
    public static class ControllerTypeCache
    {
        private static Dictionary<string, Dictionary<string, Type>> _cache = new Dictionary<string, Dictionary<string, Type>>();
        public static Dictionary<string,Dictionary<string,Type>> Cache
        {
            get { return _cache; }
            set { _cache = value; }
        }
        //这边势必应该对结果做个文件缓存，否则第一次会很慢？ 该是什么样的优化方式呢？？mvc4中仍然是将其缓存成文件保存在硬盘上
        //这样，第一次直接读取文件貌似比整体查找要快一点
        public static void FillInTheCache()
        {
            ICollection allReferencedAssemblies = BuildManager.GetReferencedAssemblies();
            foreach (Assembly assembly in allReferencedAssemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    //这边就需要编写规定了,统一以controller结尾
                    if (type.Name.EndsWith("Controller"))
                    {
                        if (_cache.ContainsKey(type.Name))
                        {
                            Dictionary<string, Type> dict = _cache[type.Name];
                            dict.Add(type.FullName,type);
                        }
                        else
                        {
                            Dictionary<string, Type> dict = new Dictionary<string, Type>();
                            dict.Add(type.FullName,type);
                            _cache.Add(type.Name,dict);
                        }
                    }
                }
            }
        }
    }
}
