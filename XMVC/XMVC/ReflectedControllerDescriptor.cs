using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC
{
    public class ReflectedControllerDescriptor : ControllerDescriptor
    {
        public ReflectedControllerDescriptor(Type controllerType)
        {

        }

        public override Type ControllerType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override ActionDescriptor FindAction(ControllerContext context, string actionName)
        {
            throw new NotImplementedException();
        }

        public override ActionDescriptor[] GetCanonicalActions()
        {
            throw new NotImplementedException();
        }
    }
}
