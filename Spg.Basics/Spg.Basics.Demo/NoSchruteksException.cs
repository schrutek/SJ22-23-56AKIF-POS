using System.Runtime.Serialization;

namespace Spg.Basics.Demo
{
    [Serializable]
    internal class NoSchruteksException : Exception
    {
        public NoSchruteksException()
        {
        }

        public NoSchruteksException(string? message) : base(message)
        {
        }

        public NoSchruteksException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSchruteksException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}