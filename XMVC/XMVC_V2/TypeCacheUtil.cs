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
        //时时刻刻能感受到，是我要怎么写代码，而不是我只能如何写代码,而是我真正驾驭着代码。
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

        //读取全部是从缓存文件中读，若为空先写入缓存，再读取。（基本上所有的涉及缓存的操作都是这么个套路）
        public static IEnumerable<Type> GetFilteredTypesFromAssemblies(string cacheName,IBuildManager buildManager,Predicate<Type> predict)
        {
            TypeCacheSerializer serializer = new TypeCacheSerializer();
            IEnumerable<Type> matchingTypes = ReadTypesFromCache(cacheName,predict,buildManager,serializer);
            if (matchingTypes!=null)
            {
                return matchingTypes;
            }
            matchingTypes = FilterTypesInAssemblies(buildManager,predict);
            SaveTypesToCache(cacheName,matchingTypes,buildManager,serializer);
            return matchingTypes;
        }

        //为了提升性能，这里提供基于文件的缓存策略，而不需要每次重新遍历
        private static IEnumerable<Type> ReadTypesFromCache(string cacheName,Predicate<Type> predict,IBuildManager buildManager,TypeCacheSerializer serializer)
        {
            return null; //涉及到文件又不会了。。。
        }

        public static void SaveTypesToCache(string cacheName,IEnumerable<Type> matchingTypes,IBuildManager builderManager,TypeCacheSerializer serializer)
        {

        }

    }
}
