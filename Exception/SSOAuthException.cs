using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SSOAuth.Exception
{
    [Serializable]
    public class SSOAuthInvalidParametersException : System.Exception
    {
        public SSOAuthInvalidParametersException() { }
        public SSOAuthInvalidParametersException(string message) : base(message) { }
        public SSOAuthInvalidParametersException(string message, System.Exception inner) : base(message, inner) { }
        protected SSOAuthInvalidParametersException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    
}
