using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Exceptions
{

    [Serializable]
    public class GeoLocationIsNotAvaible : Exception
    {
        public GeoLocationIsNotAvaible() { }
        public GeoLocationIsNotAvaible(string message) : base(message) { }
        public GeoLocationIsNotAvaible(string message, Exception inner) : base(message, inner) { }
        protected GeoLocationIsNotAvaible(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    
}
