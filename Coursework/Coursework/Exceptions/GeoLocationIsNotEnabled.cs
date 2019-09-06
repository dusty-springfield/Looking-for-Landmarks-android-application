using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Exceptions
{

    [Serializable]
    public class GeoLocationIsNotEnabled : Exception
    {
        public GeoLocationIsNotEnabled() { }
        public GeoLocationIsNotEnabled(string message) : base(message) { }
        public GeoLocationIsNotEnabled(string message, Exception inner) : base(message, inner) { }
        protected GeoLocationIsNotEnabled(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
