using System;

namespace Moonad
{
    public class ResultValueException : ApplicationException
    {
        public ResultValueException() : base() { }
        public ResultValueException(string message) : base(message) { }
    }

    public class ErrorValueException : ApplicationException
    {
        public ErrorValueException() : base() { }
        public ErrorValueException(string message) : base(message) { }
    }
}
