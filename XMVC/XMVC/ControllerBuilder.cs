using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC
{
    //单例模式 static 异常控制 功能重叠
    public class ControllerBuilder
    {
        #region 不是很优美的写法
        //private static ControllerBuilder _instance = new ControllerBuilder();
        //public static ControllerBuilder Current
        //{
        //    get { return _instance;}
        //}

        //private IControllerFactory fac;

        //public IControllerFactory GetControllerFactory()
        //{
        //    this.fac = new DefaultControllerFactory();
        //    return fac;
        //}

        //public void SetControllerFactory(IControllerFactory factory)
        //{
        //    if (factory==null)
        //    {
        //        throw new ArgumentNullException("controllerFactory");
        //    }
        //    this.fac = factory;
        //}
        //public void SetControllerFactory(Type factoryType)
        //{
        //    this.fac = Activator.CreateInstance(factoryType) as IControllerFactory;
        //}
        #endregion
        //controllerBuilder需要一个singleton
        private static ControllerBuilder _instance = new ControllerBuilder();
        private Func<IControllerFactory> _factoryThunk;
        public static ControllerBuilder Current
        {
            get { return _instance; }
        }
        public ControllerBuilder()
        {
            //原本是写的下面这样，这样写其实代表思路不清晰，程序冗杂，功能重复。
            //_factoryThunk = () => { return new DefaultControllerFactory();};
            SetControllerFactory(new DefaultControllerFactory());
        }

        public IControllerFactory GetControllerFactory()
        {
            IControllerFactory controllerFactoryInstance = _factoryThunk();
            return controllerFactoryInstance;
        }

        public void SetControllerFactory(IControllerFactory factory)
        {
            if (factory==null)
            {
                throw new ArgumentNullException("controllerFactory");
            }
            _factoryThunk = () => factory;
        }

        public void SetControllerFactory(Type controllerFactoryType)
        {
            if (controllerFactoryType==null)
            {
                throw new ArgumentNullException("controllerFactoryType");
            }
            if (!typeof(IControllerFactory).IsAssignableFrom(controllerFactoryType))
            {
                throw new ArgumentException("controllerFactoryType");
            }
            _factoryThunk = () => {
                try {
                    return (IControllerFactory)Activator.CreateInstance(controllerFactoryType);
                    }
                catch (Exception e)
                {
                    throw new InvalidOperationException(e.ToString());
                }                              
            };
        }         
    }
}
