using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelWCFServer
{
    class MessageTransferChannel
    {
        public static Action<object, string> MessageCallback;

        public static string message;

        public static int index;
    }
}
