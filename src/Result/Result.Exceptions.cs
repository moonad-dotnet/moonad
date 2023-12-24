using System;

namespace Moonad
{
    public class ResultValueException : NullReferenceException
    {
        public ResultValueException() : base("No available value. Check for an error.") { }
    }

    public class ResultErrorValueException : NullReferenceException
    {
        public ResultErrorValueException() : base("No available error. Check for a value.") { }
    }
}
