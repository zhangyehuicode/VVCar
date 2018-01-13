using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Session
{
    public interface ISessionProvider
    {
        ISession GetSession();
    }
}
