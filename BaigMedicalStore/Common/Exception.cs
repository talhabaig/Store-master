using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BaigMedicalStore.Common
{
    public class InvalidModelException : System.Exception
    {
        public InvalidModelException() : base() { }
        public InvalidModelException(string message) : base(message) { }
        public InvalidModelException(string message, System.Exception innerException) : base(message, innerException) { }
        protected InvalidModelException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    public class InvalidFormatException : System.Exception
    {
        public InvalidFormatException() : base() { }
        public InvalidFormatException(string message) : base(message) { }
        public InvalidFormatException(string message, System.Exception innerException) : base(message, innerException) { }
        protected InvalidFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class NotFoundException : System.Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, System.Exception innerException) : base(message, innerException) { }
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}