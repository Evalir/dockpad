using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Helpers
{
    public class Response<T>
    {
        public T Data;
        public Refit.ApiException ErrorData;
    }
}
