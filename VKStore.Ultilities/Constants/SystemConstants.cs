using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.Ultilities.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "VKStoreDb";
        public const string CartSession = "CartSession";
        public class AppSetting
        {
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
        }
        public class StatusOrder
        {
            public const string Inprogess = "Chờ duyệt";
        }
    }
}
