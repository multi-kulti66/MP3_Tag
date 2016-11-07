// ///////////////////////////////////
// File: FileException.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Exception
{
    using System;
    using System.Runtime.Serialization;



    public class FileException : Exception
    {
        #region Constructors

        public FileException() : base()
        { }

        public FileException(string message) : base(message)
        { }

        public FileException(string format, params object[] args) : base(string.Format(format, args))
        { }

        public FileException(string message, Exception innerException) : base(message, innerException)
        { }

        public FileException(string format, Exception innerException, params object[] args) : base(string.Format(format, args), innerException)
        { }

        protected FileException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

        #endregion
    }
}