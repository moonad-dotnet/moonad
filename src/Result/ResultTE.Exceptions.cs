using System;

namespace Moonad
{
    public class ResultValueException : Exception
    {
        public ResultValueException() : base() { }
        public ResultValueException(string message) : base(message) { }
    }

    public class ErrorValueException : Exception
    {
        public ErrorValueException() : base() { }
        public ErrorValueException(string message) : base(message) { }
    }
}
