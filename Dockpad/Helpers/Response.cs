using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Helpers
{
    /// <summary>
    /// The Response Class
    /// Used to handle API responses clearly, receiving the data and handling
    /// errors dpeending on the status code.
    /// </summary>
    /// <typeparam name="T">Expected API Model</typeparam>
    public class Response<T>
    {
        public T Data;
        public Refit.ApiException ErrorData;
    }
}
