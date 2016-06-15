using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Collections;
using System.Reflection;
namespace XMVC_V2
{
    internal static class TypeCacheUtil
    {
        //时时刻刻能感受到，是我要怎么写代码，而不是我只能如何写代码。我真正驾驭着代码。
        private static IEnumerable<Type> FilterTypesInAssemblies(IBuildManager buildManager,Predicate<Type> predict)
        {
            //遍历所有的引用的assembly去找到匹配predict的Type
            IEnumerable<Type> typesSoFar = Type.EmptyTypes;
            ICollection assemblies = buildManager.GetReferencedAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] typesInAsm = assembly.GetTypes();
                typesSoFar = typesSoFar.Concat(typesInAsm); //这么写有什么必要吗？？？
            }
            return typesSoFar.Where(type=>predict(type));
        }
    }
}
