using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
namespace XMVC_V2
{
    public class BuildManagerWrapper : IBuildManager
    {
        public ICollection GetReferencedAssemblies()
        {
            return BuildManager.GetReferencedAssemblies();
        }
    }
}
