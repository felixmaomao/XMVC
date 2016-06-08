using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace XMVC
{
    public class ReflectedActionDescriptor : ActionDescriptor
    {
        public override string ActionName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string UniqueID
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override object Execute(ControllerContext context, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override ParameterDescriptor[] GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}
