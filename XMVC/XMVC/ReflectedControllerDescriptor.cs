using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace XMVC
{
    public class ReflectedControllerDescriptor : ControllerDescriptor
    {
        public ReflectedControllerDescriptor(Type controllerType)
        {
            this.ControllerType = controllerType;
        }

        public override string ControllerName
        {
            get
            {
                string typeName = ControllerType.Name.ToString();
                if (typeName.EndsWith("Controller"))
                {
                   return typeName.Substring(0,typeName.Length-"Controller".Length);                   
                }
                return typeName;
            }
        }

        public override Type ControllerType
        {
            get;
            set;
        }

        public override string UniqueID
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
            MethodInfo matched = FindActionMethod(context,actionName);                    
            return new ReflectedActionDescriptor(matched);
        }
        public MethodInfo FindActionMethod(ControllerContext context,string actionName)
        {
            //todo
            return null;
        }


        public override ActionDescriptor[] GetCanonicalActions()
        {
            throw new NotImplementedException();
        }
    }
}
