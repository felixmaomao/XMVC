using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace XMVC
{
    public abstract class ActionDescriptor : ICustomAttributeProvider
    {
        public abstract string ActionName { get; }
        public abstract string UniqueID { get; }
        public virtual ControllerDescriptor ControllerDescriptor { get; }

        public abstract object Execute(ControllerContext context,IDictionary<string,object> parameters);
        public abstract ParameterDescriptor[] GetParameters();

        //需要一个方法可以获得action上的所有filter
        public virtual FilterInfo GetFilters()
        {
            throw new NotImplementedException();
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
    }
}
