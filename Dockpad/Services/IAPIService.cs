using Fusillade;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Services
{
    public interface IAPIService<T>
    {
        T GetApi(Priority priority);
    }
}
