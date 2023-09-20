namespace AuthentikSharp
{
    public class AuthentikException : Exception
    {
        internal AuthentikException(string message) : base(message) { }
    }

    public class AuthentikRestException : AuthentikException
    {
        internal AuthentikRestException(string message, int code) : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// Http error code
        /// </summary>
        public int Code { get; internal set; }
    }

    public class AuthentikArgumentException : AuthentikException
    {
        internal AuthentikArgumentException(string message) : base(message)
        {

        }
    }
}
