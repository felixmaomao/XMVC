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
