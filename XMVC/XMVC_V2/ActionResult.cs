﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMVC_V2
{
    public abstract class ActionResult
    {
        public abstract void ExecuteResult(ControllerContext controllerContext);
    }
}
