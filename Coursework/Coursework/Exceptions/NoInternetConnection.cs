using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Exceptions
{

    [Serializable]
    public class NoInternetConnection: Exception
    {
        public NoInternetConnection() { }
        public NoInternetConnection(string message) : base(message) { }
        public NoInternetConnection(string message, Exception inner) : base(message, inner) { }
        protected NoInternetConnection(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
