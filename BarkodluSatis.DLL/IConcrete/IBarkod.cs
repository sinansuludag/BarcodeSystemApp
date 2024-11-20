using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatis.DLL.IConcrete
{
    public interface IBarkod<T>:IRepository<T> where T : class
    {

    }
}
