using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.Utilities.Exceptions
{
    public class VKStoreException: Exception
    {
        public VKStoreException()
        {
        }

        public VKStoreException(string message)
            : base(message)
        {
        }

        public VKStoreException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
