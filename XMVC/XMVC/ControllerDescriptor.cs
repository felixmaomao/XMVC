using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace XMVC
{
    //对于某个controller的描述
    public abstract class ControllerDescriptor : ICustomAttributeProvider
    {
        public string ControllerName { get; }
        public abstract Type ControllerType { get; set; }
        public string UniqueID { get; set; }

        public abstract ActionDescriptor FindAction(ControllerContext context, string actionName);
        public abstract ActionDescriptor[] GetCanonicalActions();

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
