using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Helpers
{
    // Represents a paginated response from the backend, mainly used for getting list of objects
    public class PaginatedResponse<T>
    {
        public int Count { get; set; }
        public object Next { get; set; }
        public object Previous { get; set; }
        public IList<T> Results { get; set; }
        
        public Refit.ApiException ErrorData;

    }

}
