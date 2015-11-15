using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using System.Web.Compilation;
namespace XMVC
{
    //这个类负责项目启动的时候 填充字典
    public static class TypeCacheUtil
    {
        //我们需要一个强大的字典来作为存储,需要 类名，然后完整类名，然后类. 这样子的三层次的结构 那就 字典里面套字典
        //结构类似于
        //   ---HomeController
        //     --Web.HomeController
        //       --typeA
        //     --Test.HomeController
        //       --typeB
        //   ---MealController
        //     --S.MealController
        //       --typeC

        private static Dictionary<string, Dictionary<string, Type>> _cache = new Dictionary<string, Dictionary<string, Type>>();

        public static Dictionary<string, Dictionary<string, Type>> Cache
        {
            get { return _cache; }
            private set { _cache = value; }
        }

        public static void FillInTheCache()
        {
            ICollection allReferencedAssemblies = BuildManager.GetReferencedAssemblies();           
            foreach (Assembly assembly in allReferencedAssemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach(Type type in types)
                {
                    //如果类名不是以Controller结尾，则跳过
                    if(!type.Name.EndsWith("Controller"))
                    {
                        continue;
                    }
                    if (_cache.ContainsKey(type.Name))
                    {
                        Dictionary<string,Type> dic=_cache[type.Name];
                        dic.Add(type.FullName,type);
                    }
                    else
                    {
                        Dictionary<string, Type> dic = new Dictionary<string, Type>();
                        dic.Add(type.FullName,type);
                        _cache.Add(type.Name,dic);
                    }
                }
            }
        }
    }
}
