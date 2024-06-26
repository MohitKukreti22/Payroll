﻿using System.Runtime.Serialization;

namespace PayRoll.Exceptions
{
    internal class ValidationNotFoundException : Exception
    {
        public ValidationNotFoundException()
    {
    }

    public ValidationNotFoundException(string? message) : base(message)
    {
    }

    public ValidationNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ValidationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
}
