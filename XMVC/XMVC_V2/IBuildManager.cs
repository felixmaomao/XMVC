using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace XMVC_V2
{
    public interface IBuildManager
    {
        ICollection GetReferencedAssemblies();
    }
}
