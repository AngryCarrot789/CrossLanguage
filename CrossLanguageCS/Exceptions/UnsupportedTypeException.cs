using System;
using System.Runtime.Serialization;

namespace CrossLanguageCS.Exceptions
{
    [Serializable]
    public class UnsupportedTypeException : Exception
    {
        public UnsupportedTypeException(Type targetType) :
            base($"The type '{targetType.Name}' isn't supported as a parameter")
        {

        }

        protected UnsupportedTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
